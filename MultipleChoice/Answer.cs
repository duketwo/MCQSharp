using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoice
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public static Answer Parse(string answerText)
        {
            bool isCorrectAnswer = answerText.StartsWith("[") && answerText.EndsWith("]");
            return new Answer
            {
                Text = isCorrectAnswer ? answerText.Trim('[', ']') : answerText,
                IsCorrectAnswer = isCorrectAnswer
            };
        }
    }
}
