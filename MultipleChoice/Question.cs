using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoice
{
    public class Question
    {
        public string Number { get; set; }
        public string Text { get; set; }
        public Answer[] Answers { get; set; }
        public bool IsAnswered { get; set; }
        public string DisplayMember => !IsAnswered ? $"{Number} -- NA" :  $"{Number} -- {Answers.Count(e => e.IsCorrectAnswer && e.IsSelected)}/{Answers.Count(e => e.IsCorrectAnswer)}";

        public static Question Parse(string line)
        {
            string[] parts = line.Split("##", StringSplitOptions.None);
            return new Question
            {
                Number = parts[0],
                Text = parts[1],
                Answers = parts.Skip(2).Select(Answer.Parse).ToArray()
            };
        }
    }
}
