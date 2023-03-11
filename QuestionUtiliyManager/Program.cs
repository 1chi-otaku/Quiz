using QuestionUtiliyManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Xml.Serialization;

namespace Quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            Utility utility = new Utility();
            utility.ShowQuizzes();
            utility.AddQuestion("Test", "How are you", "Good", "Great", "Awesome", "Outstanding", AnswerOption.A);
            utility.ShowQuestions("Test");

        }
    }
}
