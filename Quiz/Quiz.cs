using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quiz
{
    class Quiz
    {
        List<Question> questions= new List<Question>();
        LoginHandler loginHandler;

        public Quiz() { 
            loginHandler = new LoginHandler();
        }
        public Quiz(string login, string password)
        {
            loginHandler = new LoginHandler(login, password);
        }
        public void QuizStart(string quiz)
        {
            if (!Exists(quiz)) throw new Exception("Such quiz doesn't exist");
            FileStream stream = new FileStream("QuizType_" + quiz + ".xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            questions = (List<Question>)serializer.Deserialize(stream);
            stream.Close();

            int points = 0;
            for(int i = 0; i < questions.Count; i++)
            {
                Console.Clear();
                questions[i].PrintQuestion();
                string answer = Console.ReadLine();
                AnswerOption option;
                Enum.TryParse<AnswerOption>(answer, out option);
                if (questions[i].IsTrueAnswer(option))
                    points++;
            }
            Console.WriteLine("Your score - " + points + "/" + questions.Count);
            questions.Clear();

        }
        public bool Exists(string quiz_name)
        {
            if (File.Exists("QuizType_" + quiz_name + ".xml"))
                return true;
            return false;
        }

    }
}
