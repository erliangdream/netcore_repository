using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace Trials.Kevin.Contract.IOC
{
    public class UserModel
    {
        readonly static Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "1001", "wu1001" },
            { "1002", "wu1002" },
            { "1003", "wu1003" },
            { "1004", "wu1004" },
            { "1005", "wu1005" },
            { "1006", "wu1006" },
            { "1007", "wu1007" },
            { "1008", "wu1008" },
            { "1009", "wu1009" },
            { "1010", "wu1010" },
        };

        public string UserNo { get; set; }

        public string UserName { get; set; }

        public UserModel()
        {
            int userLength = users.Count;
            Random random = new Random();
            int userIndex = random.Next(0, userLength - 1);
            UserNo = users.ElementAt(userIndex).Key;
            UserName = users.ElementAt(userIndex).Value;
        }
    }
}
