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
    #region Base Question class
    public class Question : ICloneable, IComparable<Question>
    {
        #region Getter and setter
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public Answer[] Answers { get; set; }
        public Answer RightAnswer { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Question(string header, string body, int mark)
        {
            this.Header = header;
            this.Body = body;
            this.Mark = mark;
            Answers = new Answer[0];
        }
        #endregion

        #region Clone()
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
        public object Clone()
        {
            return QuestionIsClone();
        }
        #endregion

        #region CompareTo
        public int CompareTo(Question other)
        {
            return this.Mark.CompareTo(other.Mark);
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"{Header}: {Body} || The mark is: {Mark}";
        }
        #endregion

    }
    #endregion

    #region True or false
    public class TrueOrFalse : Question
    {
        public TrueOrFalse(string header, string body, int mark) : base(header, body, mark)
        {
            Answers = new Answer[]
            {
                new Answer(true, "True"),
                new Answer(false, "False")
            };
            RightAnswer = Answers[0];
        }
    }
    #endregion

    #region MCQ
    public class MCQQuestion : Question
    {
        public MCQQuestion(string header, string body, int mark, Answer[] answers, Answer rightAnswer) : base(header, body, mark)
        {
            Answers = answers;
            RightAnswer = rightAnswer;
        }
    }
    #endregion

    #region Exam class
    public class Exam : ICloneable, IComparable<Exam>
    {
        #region Getter and setter
        public DateTime TimeOfExam { get; set; }
        public int NumberOfQuestions { get; set; }
        public Subject Subject { get; set; }
        public List<Question> Questions { get; set; }

        public Exam(DateTime timeOfExam, int numberOfQuestions, Subject subject)
        {
            TimeOfExam = timeOfExam;
            NumberOfQuestions = numberOfQuestions;
            Subject = subject;
            Questions = new List<Question>();
        }
        #endregion

        #region Clone()
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region CompareTo
        public int CompareTo(Exam other)
        {
            return this.TimeOfExam.CompareTo(other.TimeOfExam);
        }
        #endregion

        #region idk
        public virtual void ShowExamFunctionality()
        {
            Console.WriteLine("Exam functionality not implemented.");
        }
        #endregion
    }
    #endregion

    #region Final exam
    public class FinalExam : Exam
    {
        public FinalExam(DateTime TimeOfExam, int NumberOfQuestions, Subject Subject) : base(TimeOfExam, NumberOfQuestions, Subject) { }

        public override void ShowExamFunctionality()
        {
            Console.WriteLine("Final Exam Results:");
            foreach (var question in Questions)
            {
                Console.WriteLine($"{question.Header}: {question.Body}");
                foreach (var answer in question.Answers)
                {
                    Console.WriteLine($"- {answer.AnswerText} (Correct: {answer.IsCorrect})");
                }
                Console.WriteLine($"Grade: {question.Mark}");
            }
        }
    }
    #endregion


    #region Practical exam
    public class PracticalExam : Exam
    {
        public PracticalExam(DateTime timeOfExam, int numberOfQuestions, Subject subject) : base(timeOfExam, numberOfQuestions, subject) { }

        public override void ShowExamFunctionality()
        {
            Console.WriteLine("Practical Exam Results:");
            foreach (var question in Questions)
            {
                Console.WriteLine($"{question.Header}: Right Answer - {question.RightAnswer.AnswerText}");
            }
        }
    }
    #endregion

    //subject class (copy pasted from others)

    #region Subject class
    public class Subject : ICloneable, IComparable<Subject>
    {
        #region Getter and setter
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; set; }

        public Subject(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
        }
        #endregion

        #region Create exam
        public void CreateExam(Exam exam)
        {
            Exam = exam;
        }
        #endregion

        #region Clone()
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region CompareTo
        public int CompareTo(Subject other)
        {
            return this.SubjectId.CompareTo(other.SubjectId);
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"{SubjectName} (ID: {SubjectId})";
        }
        #endregion
    } 
    #endregion
    //answer class

    #region Answer class
    public class Answer
    {
        #region Setter and getter
        public Answer() { }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; }
        #endregion

        #region Bool
        public Answer(bool isCorrect, string answerText)
        {
            IsCorrect = isCorrect;
            AnswerText = answerText;
        }
        #endregion
    } 
    #endregion

    public static void Main()

        //lets test this
    {
        Subject subject = new Subject(1, "Mathematics");

        // Create a final exam
        FinalExam finalExam = new FinalExam(DateTime.Now, 5, subject);
        finalExam.Questions.Add(new TrueFalseQuestion("Q1", "Is 2+2=4?", 2));
        finalExam.Questions.Add(new MCQQuestion("Q2", "What is 5+5?", 3,
            new Answer[] {
                new Answer(true, "10"),
                new Answer(false, "5"),
                new Answer(false, "15")
            },
            new Answer(true, "10")));

        // Associate exam with subject
        subject.CreateExam(finalExam);

        // Show exam functionality
        finalExam.ShowExamFunctionality();
    }
}