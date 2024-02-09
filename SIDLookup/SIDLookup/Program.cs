using Pfizer.MAI.Common.Security;

using System;
using System.Collections.Generic;
using System.Text;

namespace SIDLookup
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 2 || (args.Length == 1 && args[0] == "/?"))
            {
                ShowUsage();
            }
            else
            {

                string result = string.Empty;

                if (args.Length == 0)
                    result = GetSIDForCurrentUser();
                else
                {
                    string username = args[0];
                    string password = string.Empty;
                    if (args.Length == 2)
                        password = args[1];

                    result = GetSIDForNamedUser(username);
                }

                Console.WriteLine("\r\nSID : {0}\r\n", result);
                Console.ReadLine();
            }

            //            Console.ReadLine();
        }

        static string GetSIDForCurrentUser()
        {
            string result = AuthenticationService.GetSIDStringForCurrentWindowsUser();
            return result;
        }

        static string GetSIDForNamedUser(string userName)
        {
            string result = string.Empty;

            string domain = string.Empty;
            string user = string.Empty;

            if (string.IsNullOrEmpty(userName) == false && userName.IndexOf("\\") > 0)
            {
                string[] parts = userName.Split(new char[] { '\\' });
                domain = parts[0];
                user = parts[1];

                result = AuthenticationService.GetSIDStringForNamedUser(user, domain);
            }



            if (result == string.Empty)
                result = string.Format("Unable to resolve a SID for the user {0}", userName);

            return result;
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage:\r\n");
            Console.WriteLine("SIDLookup");
            Console.WriteLine("    Gets the SID for the current user\r\n");
            Console.WriteLine("SIDLookup <domain>\\<user>");
            Console.WriteLine("    Gets the SID for the named user using the current user's credentials to query the domain.");
        }
    }
}
