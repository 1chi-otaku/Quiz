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

            Question question = new Question("What year Soviet union was collapsed?", "1992", "1990", "1980", "1991", AnswerOption.D);
            Question question2 = new Question("Do you like blueberries?", "YES", "NO", "NO", "NO", AnswerOption.A);
            question.PrintQuestion();
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
