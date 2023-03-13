using System;
using System.CodeDom;
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Available quizzes:\n");
            Console.ForegroundColor = ConsoleColor.White;
            DirectoryInfo dir = new DirectoryInfo(".");
            FileInfo[] files = dir.GetFiles("QuizType_*.xml");
            int i = 1;
            foreach (FileInfo f in files)
            {
                string s = f.Name;
                s = s.Replace("QuizType_", "").Replace(".xml", "");
                Console.WriteLine(i + "|:| " + s);
                i++;
            }
            Console.WriteLine();
        }
        static void MainQuizMenu(string quiz_name)
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t\t" + quiz_name);
            Console.WriteLine("\n\t\t\t\t\t\t    1 - Play!");
            Console.WriteLine("\t\t\t\t\t\t    2 - Leaderboard!");
            Console.WriteLine("\t\t\t\t\t\t    0 - Return!");
            Console.Write("\t\t\t\t\t\t        :-");

        }
        static void Main(string[] args)
        {
            try
            {
                int input = -1;
                string login = string.Empty, password = string.Empty;
                Quiz quiz = null;
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
                    Console.Write("\t\t\t\t\t\tEnter password:");
                    password = Console.ReadLine();
                    quiz = new Quiz(login, password);
                    input = 0;
                }
                else if (input == 2)
                {
                    quiz = new Quiz();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tSuccess! Please login now.");
                    Console.ReadKey();
                    input = 0;
                }
                else
                {
                    Console.WriteLine("Error.");
                    Console.ReadKey();
                    return;
                }
                Console.Clear();
                MainMenu();
                input = Convert.ToInt32(Console.ReadLine());
                if (input == 0) return;
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
                            int input2 = -1;
                            while (input2 != 0)
                            {
                                Console.Clear();
                                MainQuizMenu(selected_quiz);
                                input2 = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                switch (input2)
                                {
                                    case 1:
                                        quiz.QuizStart();
                                        Console.Clear();
                                        quiz.PrintResult(selected_quiz);
                                        Console.ReadKey();
                                        Console.WriteLine("Want to save your result?");
                                        Console.WriteLine("1 - Yes");
                                        Console.WriteLine("2 - No");
                                        Console.WriteLine("0 - Return");
                                        int input3 = Convert.ToInt32(Console.ReadLine());
                                        if (input3 == 1)
                                        {
                                            quiz.AddStats(selected_quiz);
                                            quiz.AddStats(selected_quiz, login);
                                        }
                                        break;
                                    case 2:
                                        quiz.PrintStats(selected_quiz);
                                        Console.ReadKey();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case 2:
                            quiz.PrintLocalStats(login);
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.Write("Enter your previous password:");
                            string prev_pass = Console.ReadLine();
                            Console.Write("Enter a new password:");
                            string new_pass = Console.ReadLine();
                            quiz.ChangePass(new_pass, prev_pass);
                            Console.ReadKey();
                            break;
                        default:
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
