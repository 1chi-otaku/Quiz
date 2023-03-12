using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    class LoginHandler
    {
        public string login { get;}
        private string password;
        public LoginHandler() {
            Console.WriteLine("Enter new login:");
            login = Console.ReadLine();
            if (Exists(login))
                throw new Exception("This login already exists");
            Console.Write("Enter new password:");
            password = Console.ReadLine();
            string toFile = login + "|" + password;
            StreamWriter sw = new StreamWriter(login + ".log", true);
            sw.WriteLine(toFile);
            sw.Close();
        }
        public LoginHandler(string login, string password)
        {
            if (!Exists(login))
                throw new Exception("There is no user with such login");

            StreamReader sw = new StreamReader(login + ".log", true);
            string pass = sw.ReadToEnd();
            string trypass = login+ "|" + password +"\r\n";
            if(pass == trypass)
            {
                this.login = login;
                this.password = password;
            }
            else
                Console.WriteLine("Bruh");
            sw.Close();
        }
        public bool Exists(string login)
        {
            if (File.Exists(login + ".log"))
            {
                return true;
            }
            return false;
        }
    }
}
