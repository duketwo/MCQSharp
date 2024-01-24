using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoice
{
    public class QuestionManager
    {
        public List<Question> Questions;
        public List<QuestionHistory> QuestionHistories;

        public QuestionManager()
        {
            Questions = new List<Question>();
            QuestionHistories = new List<QuestionHistory>();
        }

        public void LoadQuestions(string filePath)
        {
            try
            {

                Questions = Question.ParseFile(filePath);

                if (File.Exists("questionHistory.dat"))
                {
                    LoadQuestionHistories("questionHistory.dat");
                }

                foreach (var question in Questions)
                {
                    // Create only if it does not exist yet
                    if (QuestionHistories.All(e => e.QuestionNumber != question.Number))
                    {
                        QuestionHistories.Add(new QuestionHistory
                        {
                            QuestionNumber = question.Number,
                            TimesAnswered = 0,
                            TimesAnsweredCorrect = 0
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadQuestionHistories(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
                    QuestionHistories = (List<QuestionHistory>)binaryFormatter.Deserialize(fileStream);
#pragma warning restore SYSLIB0011
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading question histories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveQuestionHistories(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Typ oder Element ist veraltet
                    binaryFormatter.Serialize(fileStream, QuestionHistories);
#pragma warning restore SYSLIB0011 // Typ oder Element ist veraltet
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving question histories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MarkQuestionAsAnswered(Question question)
        {
            QuestionHistory questionHistory = QuestionHistories.FirstOrDefault(h => h.QuestionNumber == question.Number);

            if (questionHistory == null)
            {
                questionHistory = new QuestionHistory
                {
                    QuestionNumber = question.Number,
                    TimesAnswered = 0,
                    TimesAnsweredCorrect = 0,
                };

                QuestionHistories.Add(questionHistory);
            }

            if (questionHistory != null)
            {
                questionHistory.TimesAnswered++;

                if (question.IsAnsweredCorrect())
                {
                    questionHistory.TimesAnsweredCorrect++;
                }
                Debug.WriteLine($"Question {question.Number} has been answered {questionHistory.TimesAnswered} times. Correct Percentage [{questionHistory.GetAnsweredCorrectPercentage()}]%");
            }
            SaveQuestionHistories("questionHistory.dat");
        }
    }
}
