using HabitWise.Services;
using HabitWise.PageModels;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HabitWise.Tests
{
    public class SignUpViewModelTests
    {
        private readonly Mock<FirebaseAuthService> _firebaseAuthServiceMock;
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly SignUpPageModel _viewModel;

        public SignUpViewModelTests()
        {
            _firebaseAuthServiceMock = new Mock<FirebaseAuthService>();
            _navigationServiceMock = new Mock<INavigationService>();

            _viewModel = new SignUpPageModel(_firebaseAuthServiceMock.Object, _navigationServiceMock.Object);
        }

        [Fact]
        public async Task SignUpAsync_WithValidInput_SetsSuccessMessage()
        {
            // Arrange
            _firebaseAuthServiceMock
                .Setup(service => service.SignUpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _viewModel.SignUpModel.Email = "test@example.com";
            _viewModel.SignUpModel.Password = "password123";
            _viewModel.SignUpModel.Username = "testuser";

            // Act
            await _viewModel.SignUpCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal("Sign-up successful!", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task SignUpAsync_WithInvalidEmail_SetsErrorMessage()
        {
            // Arrange
            _firebaseAuthServiceMock
                .Setup(service => service.SignUpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Invalid email"));

            _viewModel.SignUpModel = new Models.SignUpModel();
            _viewModel.SignUpModel.Email = "invalid-email";
            _viewModel.SignUpModel.Password = "password123";
            _viewModel.SignUpModel.Username = "testuser";

            // Act
            await _viewModel.SignUpCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal("Invalid email", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task SignUpAsync_WhenServiceThrowsException_SetsErrorMessage()
        {
            // Arrange
            _firebaseAuthServiceMock
                .Setup(service => service.SignUpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Service unavailable"));

            _viewModel.SignUpModel = new Models.SignUpModel();
            _viewModel.SignUpModel.Email = "test@example.com";
            _viewModel.SignUpModel.Password = "password123";
            _viewModel.SignUpModel.Username = "testuser";

            // Act
            await _viewModel.SignUpCommand.ExecuteAsync(null);

            // Assert
            Assert.Equal("Service unavailable", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task SignUpAsync_WhenAlreadyBusy_DoesNotCallService()
        {
            // Arrange
            _viewModel.IsBusy = true;

            // Act
            await _viewModel.SignUpCommand.ExecuteAsync(null);

            // Assert
            _firebaseAuthServiceMock.Verify(
                service => service.SignUpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }
    }
}
