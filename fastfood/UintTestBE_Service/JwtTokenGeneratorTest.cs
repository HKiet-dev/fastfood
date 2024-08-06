using BackEnd.Models;
using BackEnd.Repository.Services;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UintTestBE_Service
{
    public class JwtTokenGeneratorTest
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly Mock<IOptions<JwtOptions>> _jwtOptionsMock;
        public JwtTokenGeneratorTest()
        {
            // Mock JwtOptions
            _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();
            _jwtOptionsMock.SetupGet(opt => opt.Value).Returns(new JwtOptions
            {
                Secret = "supersecretkeyfortesting",
                Issuer = "testIssuer",
                Audience = "testAudience"
            });

            // Instantiate JwtTokenGenerator with mocked JwtOptions
            _jwtTokenGenerator = new JwtTokenGenerator(_jwtOptionsMock.Object);
        }
        [Fact]
        public void GenerateToken_ShouldReturnToken_WhenCalledWithValidUser()
        {
            // Arrange
            var user = new User
            {
                Id = "user123",
                Email = "test@example.com",
                Name = "John Doe"
            };

            var roles = new List<string> { "Admin", "User" };

            // Act
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            // Assert
            Assert.NotNull(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);

            Assert.Equal(user.Email, jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value);
            Assert.Equal(user.Id, jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value);
            Assert.Equal(user.Name, jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value);
            Assert.Contains(jsonToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            Assert.Contains(jsonToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == "User");
        }

        [Fact]
        public void GenerateToken_ShouldThrowException_WhenSecretIsNull()
        {
            // Arrange
            _jwtOptionsMock.Setup(opt => opt.Value.Secret).Returns((string)null);

            var user = new User
            {
                Id = "user123",
                Email = "test@example.com",
                Name = "John Doe"
            };

            var roles = new List<string> { "Admin" };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _jwtTokenGenerator.GenerateToken(user, roles));
        }

        [Fact]
        public void GenerateToken_ShouldHaveCorrectExpiration()
        {
            // Arrange
            var user = new User
            {
                Id = "user123",
                Email = "test@example.com",
                Name = "John Doe"
            };

            var roles = new List<string> { "Admin" };

            // Act
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);

            var expirationClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
            Assert.NotNull(expirationClaim);

            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationClaim.Value)).UtcDateTime;
            Assert.True(expirationDate > DateTime.UtcNow, "Token expiration is not set correctly.");
        }
    }
}
