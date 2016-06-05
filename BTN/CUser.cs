using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTN
{
    public class CUser
    {
        public CUser(string userName, string userSession)
        {
            this.userName = userName;
            this.loginSession = userSession;
        }
        public string userName;
        public string loginSession;
    }
}
