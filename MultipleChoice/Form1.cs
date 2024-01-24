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


                listBox1.DisplayMember = "DisplayMember";
                foreach (var question in questions)
                {
                    listBox1.Items.Add(question);
                }

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


                // Clear previous answer controls
                flowLayoutPanel1.Controls.Clear();

                // Display the question
                var lblQuestion = new Label();
                //lblQuestion.Text = $"{currentQuestion.Number} -- {currentQuestion.Text}";
                lblQuestion.Text = $"{currentQuestion.Text}";
                flowLayoutPanel1.Controls.Add(lblQuestion);
                flowLayoutPanel1.SetFlowBreak(lblQuestion, true);
                lblQuestion.Margin = new Padding(20, 20, 0, 20);
                lblQuestion.AutoSize = true;

                var answers = currentQuestion.Answers;
                
                if(!currentQuestion.IsAnswered)
                    answers = answers.OrderBy(a => _rnd.Next()).ToArray();

                foreach (Answer answer in answers)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = $"{answer.Text}",
                        Tag = answer,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Margin = new Padding(20, 0, 0, 10),
                        Checked = answer.IsSelected,
                    };

                    if (currentQuestion.IsAnswered)
                    {

                        // Handle the Paint event to set the text color based on validity
                        checkBox.Paint += (sender, e) =>
                        {
                            CheckBox cb = (CheckBox)sender;
                            Answer ans = (Answer)cb.Tag;

                            // Set text color based on validity (green if valid, else red)
                            cb.ForeColor = ans.IsCorrectAnswer ? Color.Green : Color.Red;
                        };
                    }

                    // Add anonymous checkbox checked
                    checkBox.CheckedChanged += (s, e) =>
                    {
                        answer.IsSelected = checkBox.Checked;
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
            labelPoints.Text = $"Points: {questions.Sum(e => e.Answers.Count(a => a.IsCorrectAnswer && a.IsSelected))}/{questions.Sum(e => e.Answers.Count(a => a.IsCorrectAnswer))}";
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            
            MouseEventArgs me = (MouseEventArgs)e;
            Question currentQuestion = questions[currentQuestionIndex - 1];
            if (me.Button == MouseButtons.Left)
            {
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
    }
}