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
        public List<Answer> Answers { get; set; }
        public bool IsAnswered { get; set; }
        public string DisplayMember => !IsAnswered ? $"{Number} -- NA" : $"{Number} -- {Math.Max(Answers.Count(e => e.IsCorrectAnswer && e.IsSelected) - Answers.Count(e => !e.IsCorrectAnswer && e.IsSelected), 0)}/{Answers.Count(e => e.IsCorrectAnswer)}";

        public bool IsAnsweredCorrect()
        {
            var totalCorrect = Answers.Count(e => e.IsCorrectAnswer);
            var currentPointsSelected =
                Math.Max(
                    Answers.Count(e => e.IsCorrectAnswer && e.IsSelected) -
                    Answers.Count(e => !e.IsCorrectAnswer && e.IsSelected), 0);
            return totalCorrect == currentPointsSelected;
        }

        public static List<Question> ParseFile(string filePath)
        {
            List<Question> questions = new List<Question>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                Question currentQuestion = null;
                foreach (string line in lines)
                {
                    if (line.Trim() == String.Empty)
                        continue;

                    if (line.StartsWith("----------"))
                    {
                        if (currentQuestion != null)
                        {
                            questions.Add(currentQuestion);
                            currentQuestion = null;
                        }

                        currentQuestion = new Question();
                        currentQuestion.Answers = new List<Answer>();
                        var l = line.Replace("----------", "");
                        var split = l.Split("##");
                        currentQuestion.Number = split[0].Trim();
                        currentQuestion.Text = split[1].Trim();
                    }
                    else
                    {
                        var answer = Answer.Parse(line.Trim());
                        currentQuestion.Answers.Add(answer);
                    }
                }

                if (currentQuestion != null)
                {
                    questions.Add(currentQuestion);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return questions;
        }

    }
}
