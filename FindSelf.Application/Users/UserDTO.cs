using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.Users
{
    public class UserDTO
    {
        public string Nickname { get; set; }
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
    }
}
