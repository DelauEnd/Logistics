using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Utility
{
    public class AuthenticatedUserInfo
    {
        public UserRole Role { get; set; }
        public Guid userId { get; set; }
    }
}
