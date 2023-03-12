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

            Quiz a = new Quiz("1chi", "123");
            //a.SelectQuiz("Persona");
            //a.QuizStart();
            //a.AddStats("Persona");
            a.PrintStats("Persona");

        }
    }
}
