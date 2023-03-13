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

            var result2 = results.OrderByDescending(u => u.answer).ThenByDescending(u => u.date);
            int i = 1;
            foreach (var item in result2)
            {
                Console.WriteLine(":" + i + ":");
                Console.WriteLine("User - " + item.name + "\nResult - " + item.answer + "/" + item.questions + "\nDate - " + item.date);
                Console.WriteLine();
                i++;
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
