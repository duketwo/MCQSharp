namespace MultipleChoice
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            labelIndex = new Label();
            listBox1 = new ListBox();
            labelAnswered = new Label();
            labelPoints = new Label();
            checkBoxRandomize = new CheckBox();
            checkBoxOrderByHistory = new CheckBox();
            button4 = new Button();
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(325, 646);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Previous";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(488, 646);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Next";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            button2.MouseDown += button2_MouseDown;
            // 
            // button3
            // 
            button3.Location = new Point(569, 646);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 2;
            button3.Text = "Restart";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1074, 685);
            flowLayoutPanel1.TabIndex = 3;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // labelIndex
            // 
            labelIndex.AutoSize = true;
            labelIndex.Location = new Point(1095, 9);
            labelIndex.Name = "labelIndex";
            labelIndex.Size = new Size(59, 15);
            labelIndex.TabIndex = 4;
            labelIndex.Text = "Index: 0/0";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(1095, 27);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(96, 619);
            listBox1.TabIndex = 6;
            listBox1.MouseClick += listBox1_MouseClick;
            // 
            // labelAnswered
            // 
            labelAnswered.AutoSize = true;
            labelAnswered.Location = new Point(1095, 650);
            labelAnswered.Name = "labelAnswered";
            labelAnswered.Size = new Size(82, 15);
            labelAnswered.TabIndex = 7;
            labelAnswered.Text = "Answered: 0/0";
            // 
            // labelPoints
            // 
            labelPoints.AutoSize = true;
            labelPoints.Location = new Point(1095, 665);
            labelPoints.Name = "labelPoints";
            labelPoints.Size = new Size(63, 15);
            labelPoints.TabIndex = 8;
            labelPoints.Text = "Points: 0/0";
            // 
            // checkBoxRandomize
            // 
            checkBoxRandomize.AutoSize = true;
            checkBoxRandomize.Location = new Point(674, 640);
            checkBoxRandomize.Name = "checkBoxRandomize";
            checkBoxRandomize.Size = new Size(118, 19);
            checkBoxRandomize.TabIndex = 9;
            checkBoxRandomize.Text = "Randomize Order";
            checkBoxRandomize.UseVisualStyleBackColor = true;
            checkBoxRandomize.CheckedChanged += checkBoxRandomize_CheckedChanged;
            // 
            // checkBoxOrderByHistory
            // 
            checkBoxOrderByHistory.AutoSize = true;
            checkBoxOrderByHistory.Location = new Point(674, 660);
            checkBoxOrderByHistory.Name = "checkBoxOrderByHistory";
            checkBoxOrderByHistory.Size = new Size(150, 19);
            checkBoxOrderByHistory.TabIndex = 10;
            checkBoxOrderByHistory.Text = "Order Based On History";
            checkBoxOrderByHistory.UseVisualStyleBackColor = true;
            checkBoxOrderByHistory.CheckedChanged += checkBoxOrderByHistory_CheckedChanged;
            // 
            // button4
            // 
            button4.Location = new Point(142, 645);
            button4.Name = "button4";
            button4.Size = new Size(88, 23);
            button4.TabIndex = 11;
            button4.Text = "View History";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(407, 646);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 12;
            button5.Text = "Submit";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1203, 685);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(checkBoxOrderByHistory);
            Controls.Add(checkBoxRandomize);
            Controls.Add(labelPoints);
            Controls.Add(labelAnswered);
            Controls.Add(listBox1);
            Controls.Add(labelIndex);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(flowLayoutPanel1);
            Name = "Form1";
            Text = "MultipleChoice";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label labelIndex;
        private ListBox listBox1;
        private Label labelAnswered;
        private Label labelPoints;
        private CheckBox checkBoxRandomize;
        private CheckBox checkBoxOrderByHistory;
        private Button button4;
        private Button button5;
    }
}