using System.Windows.Forms;

namespace PrzeniesAplikacje
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
            components = new System.ComponentModel.Container();
            listBox1 = new ListBox();
            label1 = new Label();
            listBox2 = new ListBox();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label3 = new Label();
            label4 = new Label();
            button4 = new Button();
            timer1 = new Timer(components);
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new System.Drawing.Point(12, 27);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(225, 139);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(98, 15);
            label1.TabIndex = 1;
            label1.Text = "Wybierz aplikację";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new System.Drawing.Point(243, 27);
            listBox2.Name = "listBox2";
            listBox2.Size = new System.Drawing.Size(225, 139);
            listBox2.TabIndex = 2;
            listBox2.SelectedIndexChanged += listBox2_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(243, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(81, 15);
            label2.TabIndex = 3;
            label2.Text = "Wybierz ekran";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(474, 139);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(128, 40);
            button1.TabIndex = 4;
            button1.Text = "Przenieś";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(474, 185);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(128, 40);
            button2.TabIndex = 5;
            button2.Text = "Przenieś i powiększ";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(12, 202);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(225, 23);
            button3.TabIndex = 6;
            button3.Text = "Odśwież listę";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 169);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(70, 15);
            label3.TabIndex = 7;
            label3.Text = "Procesów: 0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(243, 169);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(78, 15);
            label4.TabIndex = 8;
            label4.Text = "Monitorów: 0";
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(243, 202);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(225, 23);
            button4.TabIndex = 9;
            button4.Text = "Odśwież listę";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // timer1
            // 
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(474, 61);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(99, 15);
            label5.TabIndex = 10;
            label5.Text = "Wybrany proces:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(474, 76);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(27, 15);
            label6.TabIndex = 11;
            label6.Text = "____";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.Location = new System.Drawing.Point(474, 100);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(122, 15);
            label7.TabIndex = 12;
            label7.Text = "mainWindowHandle:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(474, 115);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(27, 15);
            label8.TabIndex = 13;
            label8.Text = "____";
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(474, 27);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(62, 23);
            button5.TabIndex = 14;
            button5.Text = "PL";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(542, 27);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(60, 23);
            button6.TabIndex = 15;
            button6.Text = "EN";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // Form1
            // 
            ClientSize = new System.Drawing.Size(614, 232);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button4);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(listBox2);
            Controls.Add(label1);
            Controls.Add(listBox1);
            MaximumSize = new System.Drawing.Size(630, 271);
            MinimumSize = new System.Drawing.Size(630, 271);
            Name = "Form1";
            Text = "Przenieś aplikację";
            TopMost = true;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Label label1;
        private ListBox listBox2;
        private Label label2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label3;
        private Label label4;
        private Button button4;
        private Timer timer1;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Button button5;
        private Button button6;
    }
}