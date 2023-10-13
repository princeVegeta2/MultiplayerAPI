﻿using System.ComponentModel.DataAnnotations;

namespace GameAPI.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Character name is required.")]
        [MaxLength(50, ErrorMessage = "Character name can't be longer than 50 characters")]
        public string CharacterName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Class { get; set; }
        public int Level { get; set; }
        public bool IsActive { get; set; }
    }
}

