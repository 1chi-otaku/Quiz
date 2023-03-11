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
            Console.WriteLine();
            Console.Write("Enter name to edit: ");
            string edit = Console.ReadLine();
            FileStream stream = new FileStream("QuizType_" + edit + ".xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            List<Question> l = (List<Question>)serializer.Deserialize(stream);
            foreach (Question j in l)
            {
                j.PrintQuestion();
            }
            stream.Close();
        }

    }
}
