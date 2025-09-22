
using E_LearningPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using Dapper;
using System.Text.Json;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : Controller
    {
        private readonly IConfiguration _config;
        private readonly Fireworksembeddinggenerator _embeddingGenerator;
        private readonly IMongoClient mongoClient;
        private readonly RagService ragService;

        private const string _collectionName = "Embeddings";
        private const string _databaseName = "RagDataSet";

        public ChatbotController(IConfiguration config, RagService ragService, Fireworksembeddinggenerator embeddingGenerator, IMongoClient mongoClient)
        {
            _config = config;
            _embeddingGenerator = embeddingGenerator;
            this.mongoClient = mongoClient;
            this.ragService = ragService;
        }

        [HttpGet("ask")]
        
        public async Task<IActionResult> AskAsync([FromQuery] string prompt)
        {
            var result = await ragService.AskAiWithRagAsync(prompt);
            return Ok(new { answer = result });
        }

        [HttpGet("process")]
        public async Task<IActionResult> ProcessProductsAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("PlatformDBContext"));
            string sql = "SELECT [InstructorName],[SubjectName]     ,[SubjectDescription]     ,SubjectPrice FROM Chats";

            var quizes = connection.Query<Chat>(sql).ToList();
            //Console.WriteLine($"quizes count: {quizes.Count}");

            var productTexts = quizes.Select(p =>
                JsonSerializer.Serialize(new { InstructorName = p.InstructorName,  SubjectName = p.SubjectName, SubjectPrice = p.SubjectPrice, SubjectDescription = p.SubjectDescription })
            ).ToList();

            var dataChunks = productTexts.SelectMany(text => ChunkText(text, 500)).ToList();
            Console.WriteLine($" Data chunks count: {dataChunks.Count}");

            var embeddingResponses = await _embeddingGenerator.GenerateEmbeddingsAsync(dataChunks);
            var allEmbeddingData = embeddingResponses.SelectMany(r => r.Data).ToList();
            Console.WriteLine($" Total embeddings received: {allEmbeddingData.Count}");

            if (allEmbeddingData.Count != dataChunks.Count)
                Console.WriteLine(" Warning: Embedding count doesn't match chunk count!");

            var database = mongoClient.GetDatabase(_databaseName);
            var collection = database.GetCollection<BsonDocument>(_collectionName);


            var documents = allEmbeddingData.Select((embeddingData, index) =>
                new BsonDocument
                {
                    { "embedding", new BsonArray(embeddingData.Embedding) },
                    { "text", dataChunks[index] }
                }
            ).ToList();

            Console.WriteLine(" Documents to insert:");
            foreach (var doc in documents)
                Console.WriteLine(doc.ToJson());

            await collection.InsertManyAsync(documents);
            return Ok(new { insertedCount = documents.Count });
        }

        private IEnumerable<string> ChunkText(string text, int size)
        {
            for (int i = 0; i < text.Length; i += size)
                yield return text.Substring(i, Math.Min(size, text.Length - i));
        }
    }
}


