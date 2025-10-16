
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain.Models;
using E_LearningPlatform.Hubs;
using E_LearningPlatform.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Repository;
using Repository.Contract;
using Repository.Implementation;
using Repository.Repositories.Implementations;
using RepositoryImplementation;
using Service.Services.Contract;
using Service.Services.Implementation;

namespace E_LearningPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //  Fireworks config
            var fireworksApiKey = builder.Configuration["Fireworks:APIKey"];
            var embeddingEndpoint = builder.Configuration["Fireworks:Embedding:Endpoint"];
            var embeddingModelName = builder.Configuration["Fireworks:Embedding:ModelName"];
            var chatEndpoint = builder.Configuration["Fireworks:ChatEndPoint"];
            var chatModelName = builder.Configuration["Fireworks:ChatModelName"];

            if (string.IsNullOrEmpty(fireworksApiKey) || string.IsNullOrEmpty(embeddingEndpoint))
            {
                throw new InvalidOperationException("Fireworks API key or endpoint is missing.");
            }

            builder.Services.AddSingleton(new Fireworksembeddinggenerator(fireworksApiKey, embeddingEndpoint, embeddingModelName));
            builder.Services.AddSingleton(new FireWorkAiChat(fireworksApiKey, chatEndpoint, chatModelName));
            builder.Services.AddSingleton<RagService>();

            //  MongoDB config
            var mongoConnection = builder.Configuration["Mongo:RagDbConnection"];
            if (string.IsNullOrEmpty(mongoConnection))
            {
                throw new InvalidOperationException("MongoDB connection string is missing.");
            }

            // register MongoClient twice
            builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnection));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformDBContext"));
            });

            builder.Services.AddScoped<IClassRepository, ClassRepository>();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<ITrackRepository, TrackRepository>();
            builder.Services.AddScoped<ITrackService, TrackService>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<ILessonRepository, LessonRepository>();
            builder.Services.AddScoped<ILessonService, LessonService>();
            builder.Services.AddScoped<IPaymentService,PaymentService>();

            builder.Services.AddScoped<IQuizService, QuizService>();



            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 100_000_000;
            });
            
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //[authrize]
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>//verified key
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["JWT:AudienceIP"],
                    ValidIssuer = builder.Configuration["JWT:IssuerIP"],
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SCRKey"])),
                    //RoleClaimType = "role"
                };
            });

            //builder.Services.AddAuthorization();



            builder.Services.AddSignalR();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FreePlan", policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); ;
                });
            });

            

            /*-----------------------------Swagger PArt-----------------------------*/
            #region Swagger Region
            ////builder.Services.AddSwaggerGen();

            //builder.Services.AddSwaggerGen(swagger =>
            //{
            //    //This is to generate the Default UI of Swagger Documentation    
            //    swagger.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "ASP.NET 5 Web API",
            //        Description = " ITI Projrcy"
            //    });
            //    // To Enable authorization using Swagger (JWT)    
            //    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //    {
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer",
            //        BearerFormat = "JWT",
            //        In = ParameterLocation.Header,
            //        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            //    });
            //    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //        new OpenApiSecurityScheme
            //        {
            //        Reference = new OpenApiReference
            //        {
            //        Type = ReferenceType.SecurityScheme,
            //        Id = "Bearer"
            //        }
            //        },
            //        new string[] {}
            //        }
            //        });
            //});
            #endregion

            var app = builder.Build();
            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("FreePlan");
            app.UseAuthentication(); //for 403 forbidden
            app.UseAuthorization();




            app.MapHub<ChatHub>("/chat");

            app.MapControllers();

            app.Run();
        }
    }
}
