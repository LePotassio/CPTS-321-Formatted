namespace HW11_ThreadingApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SortButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.ThreadTimeTextBox = new System.Windows.Forms.TextBox();
            this.DownloadTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.URLTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SortButton
            // 
            this.SortButton.Location = new System.Drawing.Point(51, 7);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(311, 23);
            this.SortButton.TabIndex = 0;
            this.SortButton.Text = "Go (sorting)";
            this.SortButton.UseVisualStyleBackColor = true;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(432, 86);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(344, 23);
            this.DownloadButton.TabIndex = 1;
            this.DownloadButton.Text = "Go (download string from URL)";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ThreadTimeTextBox
            // 
            this.ThreadTimeTextBox.Location = new System.Drawing.Point(51, 52);
            this.ThreadTimeTextBox.Multiline = true;
            this.ThreadTimeTextBox.Name = "ThreadTimeTextBox";
            this.ThreadTimeTextBox.ReadOnly = true;
            this.ThreadTimeTextBox.Size = new System.Drawing.Size(311, 366);
            this.ThreadTimeTextBox.TabIndex = 2;
            // 
            // DownloadTextBox
            // 
            this.DownloadTextBox.Location = new System.Drawing.Point(6, 20);
            this.DownloadTextBox.Multiline = true;
            this.DownloadTextBox.Name = "DownloadTextBox";
            this.DownloadTextBox.ReadOnly = true;
            this.DownloadTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DownloadTextBox.Size = new System.Drawing.Size(332, 277);
            this.DownloadTextBox.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.URLTextBox);
            this.groupBox1.Location = new System.Drawing.Point(432, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 64);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "URL";
            // 
            // URLTextBox
            // 
            this.URLTextBox.Location = new System.Drawing.Point(6, 28);
            this.URLTextBox.Name = "URLTextBox";
            this.URLTextBox.Size = new System.Drawing.Size(332, 20);
            this.URLTextBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DownloadTextBox);
            this.groupBox2.Location = new System.Drawing.Point(432, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 303);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Download Result (as string)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ThreadTimeTextBox);
            this.Controls.Add(this.SortButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.TextBox ThreadTimeTextBox;
        private System.Windows.Forms.TextBox DownloadTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox URLTextBox;
    }
}

