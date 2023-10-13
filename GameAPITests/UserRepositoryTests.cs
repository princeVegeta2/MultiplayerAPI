using GameAPI.DTOs;
using GameAPI.Models;
using GameAPI.Repositories;
using GameAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace GameAPITests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly GameContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<GameContext>()
                .UseInMemoryDatabase(databaseName: "AllomantsDB")
                .Options;

            _context = new GameContext(options);
            _userRepository = new UserRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private User CreateTestUser(int userId, string email, string characterName, string className, string passwordHash)
        {
            return new User
            {
                UserID = userId,
                Email = email,
                CharacterName = characterName,
                Class = className,
                PasswordHash = passwordHash
            };
        }

        public User CreateUserFromDto(CreateUserDto createUserDto, PasswordService passwordService)
        {
            string hashedPassword = passwordService.HashPassword(createUserDto.Password);

            User newUser = new User
            {
                Email = createUserDto.Email,
                CharacterName = createUserDto.CharacterName,
                Class = createUserDto.Class,
                Level = createUserDto.Level,
                IsActive = createUserDto.IsActive,
                PasswordHash = hashedPassword
            };

            return newUser;
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var user1 = CreateTestUser(1, "user1@test.com", "Char1", "Warrior", "password1");
            Assert.NotNull(user1.CharacterName); // Debug Assert
            Assert.NotNull(user1.Class); // Debug Assert
            Assert.NotNull(user1.PasswordHash); // Debug Assert
            _context.Users.Add(user1);

            var user2 = CreateTestUser(2, "user2@test.com", "Char2", "Mage", "password2");
            Assert.NotNull(user2.CharacterName); // Debug Assert
            Assert.NotNull(user2.Class); // Debug Assert
            Assert.NotNull(user2.PasswordHash); // Debug Assert
            _context.Users.Add(user2);

            _context.SaveChanges();

            // Act
            var result = _userRepository.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Email == "user1@test.com");
            Assert.Contains(result, u => u.Email == "user2@test.com");
        }


        [Fact]
        public void GetUser_ReturnsExpectedUser()
        {
            // Arrange
            var userId = 1;
            _context.Users.Add(CreateTestUser(userId, "user1@test.com", "Char1", "Warrior", "password1"));
            _context.SaveChanges();

            // Act
            var result = _userRepository.GetUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserID);
            Assert.Equal("user1@test.com", result.Email);
        }

        [Fact]
        public void CreateUser_AddsUser()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                Email = "newuser@test.com",
                CharacterName = "CharNew",
                Password = "password",
                Class = "Rogue",
                Level = 1,
                IsActive = true
            };

            // This could be a mock if _passwordService involves complex logic or external dependencies
            var passwordService = new PasswordService();

            User newUser = CreateUserFromDto(createUserDto, passwordService);

            // Act
            _userRepository.CreateUser(newUser);

            // Assert
            Assert.Single(_context.Users);
            Assert.Equal("newuser@test.com", _context.Users.Single().Email);
        }


        [Fact]
        public void UpdateUser_UpdatesUserDetails()
        {
            // Arrange
            var existingUser = CreateTestUser(1, "user@test.com", "Char3", "Archer", "oldHashedPassword");
            _context.Users.Add(existingUser);
            _context.SaveChanges();

            // Act
            existingUser.Email = "updatedemail@test.com";
            existingUser.PasswordHash = "newHashedPassword";
            _userRepository.UpdateUser(existingUser);

            // Assert
            var updatedUser = _context.Users.Single();
            Assert.Equal("updatedemail@test.com", updatedUser.Email);
            Assert.Equal("newHashedPassword", updatedUser.PasswordHash);
        }

        [Fact]
        public void DeleteUser_RemovesUser()
        {
            // Arrange
            var userId = 1;
            var userToDelete = CreateTestUser(userId, "user@test.com", "Char4", "Knight", "passwordToRemove");
            _context.Users.Add(userToDelete);
            _context.SaveChanges();

            // Act
            _userRepository.DeleteUser(userId);

            // Assert
            Assert.Empty(_context.Users);
        }
    }
}


