﻿using CIT_Web.Models.Dto.User;

namespace CIT_Web.Models.Dto.Login
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
