using System.Diagnostics;
using System.Windows.Forms;

namespace MultipleChoice
{
    public partial class Form1 : Form
    {

        private List<Question> questions;
        private int currentQuestionIndex = 1;
        private static Random _rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }


        private void LoadQuestions(string filePath)
        {
            try
            {
                questions = File.ReadAllLines(filePath)
                    .Select(line => Question.Parse(line))
                    .ToList();

                // randomize the questions
                questions = questions.OrderBy(q => _rnd.Next()).ToList();

                Debug.WriteLine("Loaded " + questions.Count + " questions");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count + 1)
            {
                Question currentQuestion = questions[currentQuestionIndex-1];
                //lblQuestionNumber.Text = $"Question {currentQuestion.Number}";
                //lblQuestion.Text = currentQuestion.Text;

                // Clear previous answer controls
                flowLayoutPanel1.Controls.Clear();

                foreach (Answer answer in currentQuestion.Answers.OrderBy(q => _rnd.Next()).ToList())
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = answer.Text,
                        Tag = answer,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Margin = new Padding(20, 0, 0, 10)
                    };

                    flowLayoutPanel1.Controls.Add(checkBox);

                    // SetFlowBreak to force the control to start on a new line
                    flowLayoutPanel1.SetFlowBreak(checkBox, true);
                }
                labelIndex.Text = $"Index: {currentQuestionIndex.ToString()}/{questions.Count.ToString()}";
            }
        }

        private void ReloadQuestions()
        {
            // Specify the path to your .txt file
            string filePath = "Questions.txt";
            // Load questions from the file
            LoadQuestions(filePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadQuestions();
            DisplayCurrentQuestion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questions.Count)
            {
                currentQuestionIndex++;
                DisplayCurrentQuestion();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex > 1)
            {
                currentQuestionIndex--;
                DisplayCurrentQuestion();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentQuestionIndex = 1;
            ReloadQuestions();
            DisplayCurrentQuestion();
        }
    }
}