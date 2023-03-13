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
        QuizResult result;
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
                string answer = Console.ReadLine().ToUpper();
                AnswerOption option;
                Enum.TryParse<AnswerOption>(answer, out option);
                if (questions[i].IsTrueAnswer(option))
                    points++;
            }
            result = new QuizResult();
            result.name = loginHandler.login;
            result.answer = points;
            result.questions = questions.Count;
            result.date = DateTime.Now.ToString();

            questions.Clear();
        }
        public void PrintResult(string quiz_name)
        {
            if (result.questions == 0) throw new Exception("Result doesn't exist exception.");
            
            if(result.answer == result.questions)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("100%! This is an absolute W!\n");
            }
            else if(result.answer > result.questions / 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A truly remarkable result!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is L. Try again.\n");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("User - " + result.name);
            Console.WriteLine("Quiz - " + quiz_name);
            Console.WriteLine("Result - " + result.answer + "/" + result.questions);
            Console.WriteLine("Date - " + result.date);

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
        public void ChangePass(string new_password, string current_password)
        {
            if (loginHandler == null) throw new Exception("Trying to change password of account that doesn't exist");
            loginHandler.ChangePassword(current_password,new_password);
            Console.WriteLine("Success!");
        }

    }
}
