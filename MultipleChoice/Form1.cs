using System.Diagnostics;
using System.Windows.Forms;

namespace MultipleChoice
{
    public partial class Form1 : Form
    {

        private List<Question> questions;
        private int currentQuestionIndex = 1;
        private static Random _rnd = new Random();
        private static QuestionManager questionManager = new QuestionManager();

        public Form1()
        {
            InitializeComponent();
        }


        private void LoadQuestions(string filePath)
        {
            try
            {
                questionManager.LoadQuestions(filePath);
                questions = questionManager.Questions;
                //Questions = Question.ParseFile(filePath);

                listBox1.DisplayMember = "DisplayMember";
                foreach (var question in questions)
                {
                    listBox1.Items.Add(question);
                }

                // randomize the Questions
                if (checkBoxRandomize.Checked)
                {
                    questions = questions.OrderBy(q => _rnd.Next()).ToList();
                }

                if (checkBoxOrderByHistory.Checked)
                {
                    // Order based on success percentage
                    questions = questions.OrderBy(q => questionManager.QuestionHistories.FirstOrDefault(e => e.QuestionNumber == q.Number)?.GetAnsweredCorrectPercentage() ?? 0).ToList();
                }

                Debug.WriteLine("Loaded " + questions.Count + " Questions");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void UpdateListBox()
        {
            listBox1.DisplayMember = null;
            listBox1.DisplayMember = "DisplayMember";
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count + 1)
            {
                Question currentQuestion = questions[currentQuestionIndex - 1];
                listBox1.SelectedItem = currentQuestion;

                // Clear previous answer controls
                flowLayoutPanel1.Controls.Clear();

                // Display the question
                var lblQuestion = new Label();
                //lblQuestion.Text = $"{currentQuestion.Number} -- {currentQuestion.Text}";
                lblQuestion.Text = $"{currentQuestion.Text}";
                lblQuestion.Font = new Font("Tahoma", 10, FontStyle.Bold);
                flowLayoutPanel1.Controls.Add(lblQuestion);
                flowLayoutPanel1.SetFlowBreak(lblQuestion, true);
                lblQuestion.Margin = new Padding(20, 20, 0, 20);
                lblQuestion.AutoSize = true;

                var answers = currentQuestion.Answers;

                if (!currentQuestion.IsAnswered)
                    answers = answers.OrderBy(a => _rnd.Next()).ToList();

                foreach (Answer answer in answers)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        //Text = $"{answer.Text}",
                        Tag = answer,
                        AutoSize = true,
                        TextAlign = ContentAlignment.TopLeft,
                        Margin = new Padding(10, 0, 0, 0),
                        Checked = answer.IsSelected,
                    };

                    Label label = new Label
                    {
                        Text = answer.Text,
                        AutoSize = true,
                        TextAlign = ContentAlignment.TopLeft,
                        Margin = new Padding(0, 0, 0, 5),
                    };

                    label.Font = new Font("Tahoma", 9, FontStyle.Regular);

                    label.Click += (s, e) =>
                    {
                        checkBox.Checked = !checkBox.Checked;
                    };

                    if (currentQuestion.IsAnswered)
                    {
                        // Handle the Paint event to set the text color based on validity
                        label.Paint += (sender, e) =>
                        {
                            Label lbl = (Label)sender;
                            Answer ans = answer;

                            // Set text color based on validity (green if valid, else red)
                            lbl.ForeColor = ans.IsCorrectAnswer ? Color.Green : Color.Red;
                        };
                    }

                    // Add anonymous checkbox checked
                    checkBox.CheckedChanged += (s, e) =>
                    {
                        answer.IsSelected = checkBox.Checked;
                    };

                    flowLayoutPanel1.Controls.Add(checkBox);



                    flowLayoutPanel1.Controls.Add(label);

                    // SetFlowBreak to force the control to start on a new line
                    flowLayoutPanel1.SetFlowBreak(label, true);
                }
                labelIndex.Text = $"Index: {currentQuestionIndex.ToString()}/{questions.Count.ToString()}";
            }
        }


        private void ReloadQuestions()
        {
            // Specify the path to your .txt file
            string filePath = "Questions.txt";
            // Load Questions from the file
            LoadQuestions(filePath);
            UpdateLabels();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadQuestions();
            DisplayCurrentQuestion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex > 1)
            {
                currentQuestionIndex--;
                DisplayCurrentQuestion();
            }
            UpdateListBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentQuestionIndex = 1;
            listBox1.Items.Clear();
            ReloadQuestions();
            DisplayCurrentQuestion();
            UpdateListBox();
        }

        private void UpdateLabels()
        {
            labelAnswered.Text = $"Answered: {questions.Count(e => e.IsAnswered)}/{questions.Count}";
            labelPoints.Text = $"Points: {questions.Sum(e => Math.Max(e.Answers.Count(e => e.IsCorrectAnswer && e.IsSelected) - e.Answers.Count(e => !e.IsCorrectAnswer && e.IsSelected), 0))}/{questions.Sum(e => e.Answers.Count(a => a.IsCorrectAnswer))}";
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {

            MouseEventArgs me = (MouseEventArgs)e;
            Question currentQuestion = questions[currentQuestionIndex - 1];
            if (me.Button == MouseButtons.Left)
            {
                questionManager.MarkQuestionAsAnswered(currentQuestion);
                if (currentQuestionIndex < questions.Count)
                {
                    currentQuestion.IsAnswered = true;
                    currentQuestionIndex++;
                    DisplayCurrentQuestion();
                }

                if (currentQuestionIndex == questions.Count)
                {
                    currentQuestion.IsAnswered = true;
                    DisplayCurrentQuestion();
                }
            }

            if (me.Button == MouseButtons.Right)
            {
                if (currentQuestionIndex < questions.Count)
                {
                    currentQuestionIndex++;
                    DisplayCurrentQuestion();
                }
            }

            UpdateListBox();
            UpdateLabels();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var item = (Question)listBox1.SelectedItem;
            var index = questions.IndexOf(item);
            currentQuestionIndex = index + 1;
            DisplayCurrentQuestion();
        }

        private void checkBoxOrderByHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOrderByHistory.Checked)
            {
                checkBoxRandomize.Checked = !checkBoxOrderByHistory.Checked;
            }
        }

        private void checkBoxRandomize_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRandomize.Checked)
            {
                checkBoxOrderByHistory.Checked = !checkBoxRandomize.Checked;
            }
        }
    }
}