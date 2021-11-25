using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Utility
{
    public class AuthenticatedUserInfo
    {
        public EUserRole Role { get; set; }
        public Guid userId { get; set; }
    }
}
