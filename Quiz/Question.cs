using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public enum AnswerOption {A,B,C,D}
  
    public interface IQuestion
    {
        void PrintQuestion();
    }

    [Serializable]
    [KnownType(typeof(Question))]
    public class Question:IQuestion
    {
        public string question { get; set; }
        public string answer_a { get; set; }
        public string answer_b { get; set; }
        public string answer_c { get; set;}
        public string answer_d { get; set; }
        public AnswerOption answer { get; set; }

        public Question() { }

        public Question(string question, string answer_a, string answer_b, string answer_c, string answer_d, AnswerOption answer)
        {
            this.question = question;
            this.answer_a = answer_a;
            this.answer_b = answer_b;
            this.answer_c = answer_c;
            this.answer_d = answer_d;
            this.answer = answer;
        }
        public void PrintQuestion()
        {
            Console.WriteLine(question);
            Console.WriteLine("A - " + answer_a + "\nB - " + answer_b);
            Console.WriteLine("C - " + answer_c + "\nD - " + answer_d);
        }
        public bool IsTrueAnswer(AnswerOption answer)
        {
            if(this.answer == answer)return true;
            return false;
        }
    }

}
