using GameAPI.DTOs;
using GameAPI.Models;

namespace GameAPI.Mapper
{
    public class UserMapper
    {
        public static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserID, 
                Email = user.Email,
                CharacterName = user.CharacterName,
                Class = user.Class,
                Level = user.Level,
                IsActive = user.IsActive
            };
        }
    }
}

