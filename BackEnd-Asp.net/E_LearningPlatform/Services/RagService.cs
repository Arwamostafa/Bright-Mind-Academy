using MongoDB.Bson;
using MongoDB.Driver;
using E_LearningPlatform.Services;
using System.Linq;

namespace E_LearningPlatform.Services
{
    public class RagService
    {
        private readonly IMongoClient mongoClient;
        private readonly Fireworksembeddinggenerator firework;
        private readonly FireWorkAiChat fireworkchatai;
        private readonly IMongoCollection<BsonDocument> collection;
        //private readonly FireworksChatService
        public RagService(IMongoClient _mongoClient,    FireWorkAiChat fireworkchatai, Fireworksembeddinggenerator _firework)
        {
            mongoClient = _mongoClient;
            firework = _firework;
            this.fireworkchatai = fireworkchatai;
        }

        public async Task<string> AskAiWithRagAsync(string prompt)
        {
            // Step 1: Generate embedding for the prompt
            var embeddingResponse = await firework.GenerateEmbeddingsAsync(new List<string> { prompt });
            if (embeddingResponse == null || embeddingResponse.Count == 0 ||
    embeddingResponse[0].Data == null || embeddingResponse[0].Data.Count == 0)
            {
                throw new Exception("❌ Embedding response is empty. Please check your API Key, model name, or endpoint.");
            }

            var userEmbedding = embeddingResponse[0].Data[0].Embedding.ToArray();
            // Step 2: Retrieve stored documents
            var collection = mongoClient
                .GetDatabase("RagDataSet")
                .GetCollection<BsonDocument>("Embeddings");

            var ragData = await collection.Find(new BsonDocument()).ToListAsync();

            // Step 3: Score and select top documents
            var scoredChunks = ragData
                .Select(doc =>
                {
                    var text = doc["text"].AsString;
                    var embedding = doc["embedding"].AsBsonArray.Select(x => (float)x.AsDouble).ToArray();
                    var score = CalculateCosineSimilarity(userEmbedding, embedding);
                    return new { Text = text, Score = score };
                })
                .OrderByDescending(x => x.Score)
                .Take(5)
                .Select(x => x.Text)
                .ToArray();
            var context=string.Join("\n", scoredChunks);
            // Step 4: Return the top scored chunks
            var prompttext = $"Use the following product data to answer:\n{context}\n\nQuestion: {prompt}";
            var response = await fireworkchatai.AskAiAsync(prompttext);

            return response;
        }

        // Step 4: Cosine Similarity
        private float CalculateCosineSimilarity(float[] a, float[] b)
        {
            float dot = 0;
            float magA = 0;
            float magB = 0;

            for (int i = 0; i < a.Length; i++)
            {
                dot += a[i] * b[i];
                magA += a[i] * a[i];
                magB += b[i] * b[i];
            }

            return dot / ((float)(Math.Sqrt(magA) * Math.Sqrt(magB)) + 1e-10f);
        }
    }
}
///////////////////
/////using MongoDB.Bson;
//using MongoDB.Driver;
//using static System.Formats.Asn1.AsnWriter;

//namespace Ai5.Services
//{
//    public class RagService
//    {
//        private readonly IMongoClient mongoClient;
//        private readonly Fireworksembeddinggenerator firework;
//        public RagService(IMongoClient _mongoClien, Fireworksembeddinggenerator _firework)
//        {

//            mongoClient = _mongoClien;
//            firework = _firework;

//        }

//        public async void AskAiWithRag(string prompt)
//        {
//            var emdeddingresponse=await firework.GenerateEmbeddingsAsync([prompt]);
//            var userEmbedding = emdeddingresponse[0].Data[0].Embedding;
//            var collection = mongoClient.GetDatabase("RagDataSet").GetCollection<BsonDocument>("Embeddings");
//            var ragdata=collection.FindAsync(new BsonDocument()).Result.ToList();
//            var scoredChunks=ragdata.Select(doc =>
//            {
//                var Text = doc["text"].AsString;
//                var Embedding = doc["embedding"].AsBsonArray
//                 .Select(x => x.AsDouble).ToArray()})
//                .select(x => new
//                { x.Text,score = CalculateCosineSimilarity(promptEmbedding.Data[0].Embedding.toarray(), x.Embedding);

//        }).OrderByDescending(x => x.Score).Take(5).select(x=>x.text).ToArray();
//    })
//                }


//        }

//        private object CalculateCosineSimilarity(float[] floats, object embedding)
//        {
//            throw new NotImplementedException();
//}
//    }
//}