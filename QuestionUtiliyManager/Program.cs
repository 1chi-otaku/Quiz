using QuestionUtiliyManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Xml.Serialization;

namespace Quiz
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("-----Quiz Manager-----");
            Console.WriteLine("1 - Show list of quizzes");
            Console.WriteLine("2 - Create Quiz");
            Console.WriteLine("3 - Edit List");
            Console.WriteLine("0 - Exit");
            Console.Write(":-");
        }
        static void PrintEditMenu(string quiz_name)
        {
            Console.WriteLine("---" + quiz_name + " Editor---");
            Console.WriteLine("1 - Print Questions");
            Console.WriteLine("2 - Add a question");
            Console.WriteLine("3 - Delete a question");
            Console.WriteLine("0 - Exit");
            Console.Write(":-");
        }
        static void Main(string[] args)
        {
            short input = -1;
            while (input != 0) {
                Console.Clear();
                PrintMenu();
                input = Convert.ToInt16(Console.ReadLine());
                Console.Clear();

                switch (input)
                {
                    case 0: return;
                    case 1:
                        Utility.ShowQuizzes();
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of a new quiz:\n:- ");
                        Utility.Create(Console.ReadLine());
                        break;
                    case 3:
                        Utility.ShowQuizzes();
                        Console.WriteLine("Print name of the quiz to edit:");
                        string quiz_name = Console.ReadLine();
                        int input2 = -1;
                        while (input2 != 0)
                        {
                            Console.Clear();
                            PrintEditMenu(quiz_name);
                            input2 = Convert.ToInt32(Console.ReadLine());    
                            Console.Clear();
                            switch (input2)
                            {
                                case 0: break;
                                case 1:
                                    Utility.ShowQuestions(quiz_name);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    string question, answer_a, answer_b, answer_c, answer_d;
                                    Console.WriteLine("Enter Question:");
                                    question = Console.ReadLine();
                                    Console.WriteLine("Enter answer A:");
                                    answer_a = Console.ReadLine();
                                    Console.WriteLine("Enter answer B:");
                                    answer_b = Console.ReadLine();
                                    Console.WriteLine("Enter answer C:");
                                    answer_c = Console.ReadLine();
                                    Console.WriteLine("Enter answer D:");
                                    answer_d = Console.ReadLine();
                                    Console.WriteLine("Enter correct answer:");
                                    string str = Console.ReadLine();
                                    AnswerOption option = AnswerOption.A;
                                    Enum.TryParse<AnswerOption>(str, out option);
                                    Utility.AddQuestion(quiz_name,question,answer_a,answer_b,answer_c,answer_d,option);
                                    Console.Clear();
                                    Console.WriteLine("Success!");
                                    break;
                                case 3:
                                    Console.Write("Enter a number of question to delete (0 to return):- ");
                                    int number = Convert.ToInt32(Console.ReadLine());
                                    if (number <= 0) break;
                                    Utility.DeleteQuestion(quiz_name,number);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                }
            }
        }
    }
}
