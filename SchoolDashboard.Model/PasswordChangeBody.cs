using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDashboard.Model
{
    public class PasswordChangeBody
    {
        public string UserPassword { get; set; }
        public string UserPasswordValidaton { get; set; }
    }
}
