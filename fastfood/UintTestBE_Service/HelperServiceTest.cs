using BackEnd.Repository.Services;
namespace xUintTestBackEnd
{
    public class HelperServiceTest
    {
        private readonly HelperService _helperService;

        public HelperServiceTest()
        {
            _helperService = new HelperService();
        }
        [Theory]
        [InlineData(8)] // Test với độ dài mật khẩu 8 ký tự
        [InlineData(12)] // Test với độ dài mật khẩu 12 ký tự
        [InlineData(16)] // Test với độ dài mật khẩu 16 ký tự
        public void GenerateRandomPassword_ShouldGeneratePasswordWithCorrectLength(int length)
        {
            // Act
            string password = _helperService.GenerateRandomPassword(length);

            // Assert
            Assert.Equal(length, password.Length);
        }
        [Fact]
        public void GenerateRandomPassword_ShouldContainAtLeastOneUppercaseLetter()
        {
            // Act
            string password = _helperService.GenerateRandomPassword(8);

            // Assert
            Assert.Contains(password, c => char.IsUpper(c));
        }

        [Fact]
        public void GenerateRandomPassword_ShouldContainAtLeastOneLowercaseLetter()
        {
            // Act
            string password = _helperService.GenerateRandomPassword(8);

            // Assert
            Assert.Contains(password, c => char.IsLower(c));
        }

        [Fact]
        public void GenerateRandomPassword_ShouldContainAtLeastOneDigit()
        {
            // Act
            string password = _helperService.GenerateRandomPassword(8);

            // Assert
            Assert.Contains(password, c => char.IsDigit(c));
        }

        [Fact]
        public void GenerateRandomPassword_ShouldContainAtLeastOneSpecialCharacter()
        {
            // Act
            string password = _helperService.GenerateRandomPassword(8);

            // Assert
            Assert.Contains(password, c => "!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c));
        }

        [Fact]
        public void GenerateRandomPassword_ShouldContainOnlyAllowedCharacters()
        {
            // Act
            string password = _helperService.GenerateRandomPassword(8);

            // Assert
            Assert.All(password, c =>
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c));
        }

        [Fact]
        public void GenerateRandomPassword_ShouldGenerateDifferentPasswords()
        {
            // Act
            string password1 = _helperService.GenerateRandomPassword(8);
            string password2 = _helperService.GenerateRandomPassword(8);

            // Assert
            Assert.NotEqual(password1, password2);
        }

        [Fact]
        public void GenerateRandomPassword_ShouldThrowExceptionForLengthLessThan4()
        {
            // Arrange
            int length = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _helperService.GenerateRandomPassword(length));
        }

    }
}
