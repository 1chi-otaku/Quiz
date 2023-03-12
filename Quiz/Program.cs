using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    class Program
    {
        static void Main(string[] args)
        {

            Quiz a = new Quiz("Sega", "segalol");
            a.SelectQuiz("Persona");
            a.QuizStart();
            a.AddStats("Persona");
            a.PrintStats("Persona");

            //string str = Console.ReadLine();
            //AnswerOption week1;
            //Enum.TryParse<AnswerOption>(str, out week1);

            //if (question.IsTrueAnswer(week1))
            //{
            //    Console.WriteLine("Hola!!! -1 chas rabotbl");
            //}

            //LoginHandler handler = new LoginHandler("1chi","123");
            //if (handler.Exists("otaku"))
            //{
            //    Console.WriteLine("yeah");
            //}

        }
    }
}
