using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load Configuration (Reading settings from appsettings.json)
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing."); // Secret key for JWT authentication
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer is missing."); // JWT Issuer
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience is missing."); // JWT Audience
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Database connection string is missing."); // Database connection

// ✅ Configure Database (EF Core - Entity Framework Core is used for database management)
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString)); // Using SQL Server database

// ✅ Configure Authentication & JWT (JWT is used to secure API endpoints)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Ensure the JWT is signed with a valid key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), // Secret key for JWT
            ValidateIssuer = true, // Ensure the issuer is valid
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Expected issuer
            ValidateAudience = true, // Ensure the audience is valid
            ValidAudience = builder.Configuration["Jwt:Audience"], // Expected audience
            ValidateLifetime = true, // Ensure the token hasn't expired
            ClockSkew = TimeSpan.Zero // No tolerance for expired tokens
        };
    });

// ✅ Add Authorization Middleware (Allows role-based access control)
builder.Services.AddAuthorization();

// ✅ Configure Swagger with JWT Authentication (Swagger UI helps to test API endpoints)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Management System API", Version = "v1" });

    // 🔹 Enable JWT Authentication in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token as: Bearer {your-token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

// ✅ Add Controllers (Registers all API controllers in the project)
builder.Services.AddControllers();

var app = builder.Build();

// ✅ Enable Swagger UI (For API Testing)
if (app.Environment.IsDevelopment()) // Swagger should only be enabled in development mode
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Ensures API uses HTTPS for secure communication
app.UseAuthentication();  // ✅ Enable Authentication Middleware (Ensures users must log in with JWT)
app.UseAuthorization();   // ✅ Enable Authorization Middleware (Ensures users have the right permissions)
app.MapControllers(); // ✅ Maps controller endpoints (Makes API accessible via routes)

app.Run(); // ✅ Starts the API application
