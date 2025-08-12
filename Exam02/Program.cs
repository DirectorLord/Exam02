using System.Reflection.PortableExecutable;

namespace Exam02;
internal class Program
{
    //Design a Class to represent the Question Object, Question is
    //consisting of:
    //a.Header of the question
    //b.Body of the question
    //c.Mark

    //making a class for the exam or the question ig
    public class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public float Mark { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public Answer[] Answers { get; set; }
        public Answer RightAnswer { get; set; }

        //getter and setter
        public Question(string header, string body, int mark)
        {
            this.Header = header;
            this.Body = body;
            this.Mark = mark;
            Answers = new Answer[0];
        }

        // Fix for CS0103: Implement the missing QuestionIsClone method
        public Question QuestionIsClone()
        {
            return new Question(this.Header, this.Body, (int)this.Mark)
            {
                AnswerId = this.AnswerId,
                AnswerText = this.AnswerText,
                Answers = (Answer[])this.Answers.Clone(),
                RightAnswer = this.RightAnswer
            };
        }
        //clone method ig??
        public object Clone()
        {
            return QuestionIsClone();
        }
        //compare to 
        public int CompareTo(Question other)
        {
            return this.Mark.CompareTo(other.Mark);
        }
        //override
        public override string ToString()
        {
            return $"{Header}: {Body} || The mark is: {Mark}";
        }
    }
    //types of questions
    //true or false
    public class TrueOrFalse : Question
    {
        public TrueOrFalse(string header, string body, int mark) : base(header, body,mark) {
            Answers = new Answer[]
            {
                new Answer(true, "True")
                new Answer(false, "false")
            };
        }
        RightAnswer = Answers[0];
    }
    //MCQ
    public class MCQQuestion : Question
    {
        public MCQQuestion(string header, string body, int mark, Answer[] answers, Answer rightAnswer) : base(header, body, mark)
        {
            Answers = answers;
            RightAnswer = rightAnswer;
        }
    }

    //general Exam
    public class Exam: ICloneable, IComparable<Exam>
    {
        public DateTime TimeOfExam { get; set; }
        public int NumberOfQuestions { get; set; }
        public Subject Subject { get; set; }
        public List<Question> Questions { get; set; }

        //copy paste it
        //getter and setter
        public Question(DateTime TimeOfExam, int NumberOfQuestions, Subject Subject)
        {
            this.TimeOfExam = timeOfExam;
            this.NumberOfQuestions = numberOfQuestions;
            this.Subject = subject;
            this.Questions = new List<Questions>();
        }

        //cloning methods
        public object Clone()
        {
            return MemberwiseClone();
        }
        //compare to 
        public int CompareTo(Exam other)
        {
            return this.TimeOfExam.CompareTo(other.TimeOfExam);
        }
        //override
        public override string ToString()
        {
            return $"Exam at: {TimeOfExam} || for subject: {Subject}";
        }

    }

    public static void Main()
    {
        Console.ReadLine();
    }
}