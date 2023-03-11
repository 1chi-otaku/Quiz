using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quiz
{
    static class XMLQuestionHandler
    {
        public static bool WriteToXML(Question question)
        {
            FileStream stream = new FileStream("Questions.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Question));
            serializer.Serialize(stream, question);
            stream.Close();
            return true;
        }
    }
}
