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
        QuizResult result = new QuizResult();
        IStatisticsHandler statistics = new FileQuizStatistics();

        public Quiz() { 
            loginHandler = new LoginHandler();
        }
        public Quiz(string login, string password)
        {
            loginHandler = new LoginHandler(login, password);
        }
        public void SelectQuiz(string quiz_name)
        {
            if (!Exists(quiz_name)) throw new Exception("Such quiz doesn't exist");
            FileStream stream = new FileStream("QuizType_" + quiz_name + ".xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            questions = (List<Question>)serializer.Deserialize(stream);
            stream.Close();
        }
        public void QuizStart()
        {
            if (questions.Count == 0) throw new Exception("You have to select a quiz you like first.");
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
            result.name = loginHandler.login;
            result.answer = points;
            result.questions = questions.Count;
            result.date = DateTime.Now.ToString();

            questions.Clear();
        }
        public void PrintStats(string quiz_name)
        {
            if (!statistics.Exists(quiz_name))
                Console.WriteLine("No one has taken this quiz yet.");
            else
                statistics.PrintStatistics(quiz_name);
        }
        public void AddStats(string quiz_name)
        {
            if (!statistics.Exists(quiz_name)) statistics.CreateStatistics(quiz_name, result);
            else statistics.AddStatistics(quiz_name, result);
        }
        public bool Exists(string quiz_name)
        {
            if (File.Exists("QuizType_" + quiz_name + ".xml"))
                return true;
            return false;
        }

    }
}
