using HabitWise.Services;
using HabitWise.PageModels;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HabitWise.Tests
{
    public class SignInViewModelTests
    {
        private readonly Mock<FirebaseAuthService> _firebaseAuthServiceMock;
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly SignInPageModel _viewModel;

        public SignInViewModelTests()
        {
            // Mock Services
            _firebaseAuthServiceMock = new Mock<FirebaseAuthService>();
            _navigationServiceMock = new Mock<INavigationService>();

            // Instantiate ViewModel with the mocked service
            _viewModel = new SignInPageModel(_firebaseAuthServiceMock.Object, _navigationServiceMock.Object);
        }

        [Fact]
        public async Task SignInAsync_WithValidCredentials_SetsSuccessMessage()
        {
            // Arrange
            var mockUserCredential = new Mock<Firebase.Auth.UserCredential>();
            _firebaseAuthServiceMock
                .Setup(service => service.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _viewModel.SignInModel.Email = "test@example.com";
            _viewModel.SignInModel.Password = "password123";

            // Act
            await _viewModel.SignInCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal("Login successful!", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task SignInAsync_WithInvalidCredentials_SetsErrorMessage()
        {
            // Arrange
            _firebaseAuthServiceMock
                .Setup(service => service.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Invalid credentials"));

            _viewModel.SignInModel.Email = "wrong@example.com";
            _viewModel.SignInModel.Password = "wrongpassword";

            // Act
            await _viewModel.SignInCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal("Invalid credentials", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task SignInAsync_WhenServiceThrowsException_SetsErrorMessage()
        {
            // Arrange
            _firebaseAuthServiceMock
                .Setup(service => service.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Service unavailable"));

            _viewModel.SignInModel.Email = "test@example.com";
            _viewModel.SignInModel.Password = "password123";

            // Act
            await _viewModel.SignInCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal("Service unavailable", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task SignInAsync_WhenAlreadyBusy_DoesNotCallService()
        {
            // Arrange
            _viewModel.IsBusy = true;

            // Act
            await _viewModel.SignInCommand.ExecuteAsync(null);

            // Assert
            _firebaseAuthServiceMock.Verify(
                service => service.SignInAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public async Task SignInAsync_WithEmptyEmailOrPassword_SetsErrorMessage()
        {
            // Arrange
            _viewModel.SignInModel.Email = "";
            _viewModel.SignInModel.Password = "";

            // Act
            await _viewModel.SignInCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal("Sign-in failed. UserCredential returned null.", _viewModel.ErrorMessage);
        }
    }
}
