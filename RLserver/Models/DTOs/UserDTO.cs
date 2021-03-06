﻿using System.ComponentModel;

namespace RLserver.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [DefaultValue(new int[]{})]
        public int[] TeamIds { get; set; }
    }
}