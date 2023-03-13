using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Quiz
{
    [Serializable]
    public struct QuizResult
    {
        public string name;
        public int answer;
        public int questions;
        public string date;
    }
    interface IStatisticsHandler
    {
        void PrintStatistics(string quiz_name);
        void CreateStatistics(string quiz_name,QuizResult result);
        void AddStatistics(string quiz_name, QuizResult result);
        bool Exists(string quiz_name);
    }
    class FileQuizStatistics : IStatisticsHandler
    {
        public void PrintStatistics(string quiz_name)
        {
            FileStream stream = new FileStream("QuizStatType_" + quiz_name + ".xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<QuizResult>));
            List<QuizResult> results = (List<QuizResult>)serializer.Deserialize(stream);
            stream.Close();

            var sorted = results.OrderByDescending(u => u.answer).ThenBy(u => u.date);
            List<QuizResult> list = sorted.ToList();

            Console.WriteLine("\t\t\t\t\t        \"" + quiz_name + "\"" + " quiz leaderboard");
            for (int id = 0; id < list.Count(); id++)
            {
                if (id == 0) Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if(id == 1) Console.ForegroundColor = ConsoleColor.Gray;
                else if(id == 2) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--:" + (id+1) + ":--");
                Console.WriteLine("User - " + list[id].name + "\nResult - " + list[id].answer + "/" + list[id].questions + "\nDate - " + list[id].date);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

            }
        }
        public void CreateStatistics(string quiz_names, QuizResult result)
        {
            List<QuizResult> list = new List<QuizResult> { result };
            FileStream stream = new FileStream("QuizStatType_" + quiz_names + ".xml", FileMode.OpenOrCreate);
            XmlSerializer serializer = new XmlSerializer(typeof(List<QuizResult>));
            serializer.Serialize(stream, list);
            stream.Close();
        }
        public bool Exists(string quiz_name)
        {
            if (File.Exists("QuizStatType_" + quiz_name + ".xml"))
                return true;
            return false;
        }
        public void AddStatistics(string quiz_name, QuizResult result)
        {
            if (!Exists(quiz_name)) throw new Exception("Quiz with this name doesn't exist");
            XmlDocument doc = new XmlDocument();
            doc.Load("QuizStatType_" + quiz_name + ".xml");

            XmlNode root = doc.SelectSingleNode("ArrayOfQuizResult");
            XmlElement child = doc.CreateElement("QuizResult");
            root.AppendChild(child);

            XmlElement Login = doc.CreateElement("name");
            XmlElement Answer = doc.CreateElement("answer");
            XmlElement Question = doc.CreateElement("questions"); 
            XmlElement Date = doc.CreateElement("date");
            Login.InnerText = result.name;
            Answer.InnerText = result.answer.ToString();
            Question.InnerText = result.questions.ToString();
            Date.InnerText = result.date;

            child.AppendChild(Login);
            child.AppendChild(Answer);
            child.AppendChild(Question);
            child.AppendChild(Date);
            doc.Save("QuizStatType_" + quiz_name + ".xml");
        }
    }
}
