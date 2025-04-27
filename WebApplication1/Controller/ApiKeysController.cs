using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System;
using WebApplication1.Auth;

namespace WebApplication1.Controller
{
    [ApiController]
    [Route("api/apikeys")]
    public class ApiKeysController : ControllerBase
    {   
        private readonly IConfiguration _configuration;

        public ApiKeysController( IConfiguration configuration)
        {            
            _configuration = configuration;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")] // 只有管理员可以颁发新 Key
        public async Task<IActionResult> CreateApiKey([FromBody] CreateApiKeyRequest request)
        {
            var apiKey = new ApiKey
            {
                Key = GenerateApiKey(),
                Owner = request.Owner,
                Created = DateTime.UtcNow,
                Expires = request.ExpiresInDays.HasValue
                    ? DateTime.UtcNow.AddDays(request.ExpiresInDays.Value)
                    : null,
                IsActive = true,
                Roles = request.Roles.Select(r => new ApiKeyRole { Name = r }).ToList()
            };
                      

            return Ok(new { Key = apiKey.Key });
        }

        private string GenerateApiKey()
        {
            // 使用安全的随机数生成器创建 API Key
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)
                .Replace("/", "")
                .Replace("+", "")
                .Replace("=", "");
        }
    }

    public class CreateApiKeyRequest
    {
        public string Owner { get; set; }
        public int? ExpiresInDays { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
