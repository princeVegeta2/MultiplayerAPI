    using GameAPI.Models;
    using GameAPI.DTOs;
    using GameAPI.Mapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using GameAPI.Services;

[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordService _passwordService; // Password hasher

    public UserController(IUserRepository userRepository, PasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    [HttpGet]
        public IEnumerable<UserDto> GetAll()
        {
            var users = _userRepository.GetAllUsers();
            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(UserMapper.MapToDto(user));
            }

            return userDtos;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return UserMapper.MapToDto(user);
        }

        [HttpPost]
        public ActionResult<UserDto> Create([FromBody] CreateUserDto newUserDto)
        {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string hashedPassword = _passwordService.HashPassword(newUserDto.Password);

        User newUser = new User
            {
                Email = newUserDto.Email,
                CharacterName = newUserDto.CharacterName,
                Class = newUserDto.Class,
                Level = newUserDto.Level,
                IsActive = newUserDto.IsActive,
                PasswordHash = hashedPassword
            };
            _userRepository.CreateUser(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.UserID }, UserMapper.MapToDto(newUser));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto updatedUserDto)
        {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            if (id != updatedUserDto.UserId)
            {
                return BadRequest();
            }

            user.Email = updatedUserDto.Email;
            user.CharacterName = updatedUserDto.CharacterName;
            user.Class = updatedUserDto.Class;
            user.Level = updatedUserDto.Level;
            user.IsActive = updatedUserDto.IsActive;

            _userRepository.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userToDelete = _userRepository.GetUser(id);
            if (userToDelete == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(id);
            return NoContent();
        }



    }

