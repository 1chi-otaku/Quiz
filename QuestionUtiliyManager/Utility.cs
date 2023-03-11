using Quiz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace QuestionUtiliyManager
{
    [Serializable]
    class Utility
    {
        public void ShowQuizzes()
        {
            Console.WriteLine("Available quizzes:");
            DirectoryInfo dir = new DirectoryInfo(".");
            FileInfo[] files = dir.GetFiles(@"QuizType_*.xml");
            foreach (FileInfo f in files)
            {
                string s = f.Name;
                s = s.Replace("QuizType_", "");
                s = s.Replace(".xml", "");
                Console.WriteLine(s);
            }
        }
        public void ShowQuestions(string quiz_name)
        {
            FileStream stream = new FileStream("QuizType_" + quiz_name + ".xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            List<Question> l = (List<Question>)serializer.Deserialize(stream);
            foreach (Question j in l)
            {
                j.PrintQuestion();
            }
            stream.Close();
        }
        public void Create()
        {
            List<Question> list = new List<Question>();
            Console.WriteLine("Enter name of quiz:");
            string name = Console.ReadLine();
            uint i = 1;

            string question,answer_a,answer_b,answer_c,answer_d;
            AnswerOption option = AnswerOption.A;
            while(Console.ReadLine() != "exit")
            {
                Console.Clear();
                Console.WriteLine("Question " + i);
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
                Enum.TryParse<AnswerOption>(str, out option);
                list.Add(new Question(question, answer_a, answer_b, answer_c, answer_d, option));
                Console.Clear();
                Console.WriteLine("Question " + i + " was added\n'exit' to exit\nenter to continue");
                i++;
            }
            FileStream stream = new FileStream("QuizType_" + name + ".xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            serializer.Serialize(stream, list);
            stream.Close();
        }
        public void Edit()
        {
            ShowQuizzes();
            Console.WriteLine("Enter a name of a quiz:");
            string quiz = Console.ReadLine();
            Console.WriteLine();
            ShowQuestions(quiz);
        }
        public void DeleteQuestion(string quiz_name, int position)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("QuizType_" + quiz_name + ".xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode firstNode = xRoot.ChildNodes[position-1];
            xRoot.RemoveChild(firstNode);
            xDoc.Save("QuizType_" + quiz_name + ".xml");
        }
        public void AddQuestion(string quiz_name, string question, string answer_a, string answer_b, string answer_c, string answer_d, AnswerOption answerOption)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("QuizType_" + quiz_name + ".xml");
            XmlNode root = doc.SelectSingleNode("ArrayOfQuestion");

            XmlElement child = doc.CreateElement("Question");
            root.AppendChild(child);

            XmlElement Question = doc.CreateElement("question");
            XmlElement Answer_a = doc.CreateElement("answer_a");
            XmlElement Answer_b = doc.CreateElement("answer_b");
            XmlElement Answer_c = doc.CreateElement("answer_c");
            XmlElement Answer_d = doc.CreateElement("answer_d");
            XmlElement Answer = doc.CreateElement("answer");
            Question.InnerText = question;
            Answer_a.InnerText = answer_a;
            Answer_b.InnerText = answer_b;
            Answer_c.InnerText = answer_c;
            Answer_d.InnerText = answer_d;
            Answer.InnerText = answerOption.ToString();
            child.AppendChild(Question);
            child.AppendChild(Answer_a);
            child.AppendChild(Answer_b);
            child.AppendChild(Answer_c);
            child.AppendChild(Answer_d);
            child.AppendChild(Answer);
            doc.Save("QuizType_" + quiz_name + ".xml");

        }
    }
}
