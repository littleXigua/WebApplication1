using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET472CONSOLE
{
    public static class StringExtensions
    {
        public static string GetUsernameWithoutDomain(this string userAccount)
        {
            if (string.IsNullOrEmpty(userAccount))
                return userAccount;

            int backslashIndex = userAccount.IndexOf('\\');
            return backslashIndex >= 0
                ? userAccount.Substring(backslashIndex + 1)
                : userAccount;
        }
    }

}
