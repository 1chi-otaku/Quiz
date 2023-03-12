using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    class Program
    {
        static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t    ####         ##       ##   ##   ############");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t   ###  ###      ##       ##   ##             # ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\t\t  ##      ##     ##       ##   ##            ## ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t  ##      ##     ##       ##   ##           ##  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t  ##      ##     ##       ##   ##   ############");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\t\t  ##      ##     ##       ##   ##   ############");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t  ##      ##     ##       ##   ##        ##     ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t  ##     ##      ##       ##   ##       ##      ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\t\t  ##    ##       ###     ###   ##      ##       ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t   ###########     #######     ##    ########## ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\t\t\t\t\t      (The name is temporary)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n");
            Console.WriteLine("\t\t\t\t\t      ENTER ANY KEY TO CONTINUE");
            Console.WriteLine("\t\t\t\t\t           ENTER 0 TO EXIT");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t\t\t\t\t\t\t:-");
        }
        static void QuizMenu(string login)
        {
            Console.Write("Hello, " + login + "!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\t1 - Start a quiz");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\t\t\t\t2 - Show my results");
            Console.WriteLine("\t\t\t\t\t\t3 - Change Password");
            Console.WriteLine("\t\t\t\t\t\t0 - Exit");
            Console.Write("\t\t\t\t\t\t\t:-");
        }
        static void ShowQuizzes()
        {
            Console.WriteLine("Available quizzes:");
            DirectoryInfo dir = new DirectoryInfo(".");
            FileInfo[] files = dir.GetFiles(@"QuizType_*.xml");
            foreach (FileInfo f in files)
            {
                string s = f.Name;
                s = s.Replace("QuizType_", "");
                s = s.Replace(".xml", "");
                Console.WriteLine(s);
            }
        }
        static void Main(string[] args)
        {
            int input = -1;
            string login = string.Empty, password = string.Empty;
            Quiz quiz = new Quiz("1chi","123");
            try
            {
                if(String.IsNullOrEmpty(login)) {

                    while (input != 0)
                    {
                        Console.Clear();
                        MainMenu();
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input == 0) return;
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t    1 - Login");
                        Console.WriteLine("\t\t\t\t\t\t    2 - Register");
                        Console.Write("\t\t\t\t\t\t        :-");
                        input = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        if (input == 1)
                        {
                            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\tEnter your login: ");
                            login = Console.ReadLine();
                            Console.Write("\t\t\t\t\t\t    Enter password:");
                            password = Console.ReadLine();
                            quiz = new Quiz(login, password);
                            input = 0;
                            continue;
                        }
                        else if (input == 2)
                        {
                            quiz = new Quiz();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tSuccess! Please login now.");
                            Console.ReadKey();
                            input = 0;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Error.");
                            Console.ReadKey();
                            continue;
                        }
                    }

                }
             
                input = -1;
                while (input != 0)
                {
                    Console.Clear();
                    QuizMenu(login);
                    input = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (input)
                    {
                        case 1:
                            ShowQuizzes(); 
                            Console.WriteLine("\nEnter name of quiz to start");
                            string selected_quiz = Console.ReadLine();
                            quiz.SelectQuiz(selected_quiz);
                            quiz.QuizStart();

                            break;
                        default:
                            break;
                    }
                }
               

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
