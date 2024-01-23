using System.Diagnostics;

namespace MultipleChoice
{
    public partial class Form1 : Form
    {

        private List<Question> questions;

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

                Debug.WriteLine("Loaded " + questions.Count + " questions");    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Specify the path to your .txt file
            string filePath = "Questions.txt";

            // Load questions from the file
            LoadQuestions(filePath);
        }
    }
}