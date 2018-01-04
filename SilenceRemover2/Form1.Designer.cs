namespace SilenceRemover2
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
            this.fileButton = new System.Windows.Forms.Button();
            this.textBlockBoye = new System.Windows.Forms.Label();
            this.processButton = new System.Windows.Forms.Button();
            this.fileSaveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.decibelPicker = new System.Windows.Forms.NumericUpDown();
            this.Preview = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.decibelPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(13, 13);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(130, 35);
            this.fileButton.TabIndex = 0;
            this.fileButton.Text = "Choose input";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.inputButton_Click);
            // 
            // textBlockBoye
            // 
            this.textBlockBoye.AutoSize = true;
            this.textBlockBoye.Location = new System.Drawing.Point(97, 116);
            this.textBlockBoye.Name = "textBlockBoye";
            this.textBlockBoye.Size = new System.Drawing.Size(49, 17);
            this.textBlockBoye.TabIndex = 1;
            this.textBlockBoye.Text = "Ready";
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(499, 12);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(111, 79);
            this.processButton.TabIndex = 2;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // fileSaveButton
            // 
            this.fileSaveButton.Location = new System.Drawing.Point(13, 54);
            this.fileSaveButton.Name = "fileSaveButton";
            this.fileSaveButton.Size = new System.Drawing.Size(130, 37);
            this.fileSaveButton.TabIndex = 3;
            this.fileSaveButton.Text = "Choose output";
            this.fileSaveButton.UseVisualStyleBackColor = true;
            this.fileSaveButton.Click += new System.EventHandler(this.fileSaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Minimum decibels";
            // 
            // decibelPicker
            // 
            this.decibelPicker.Location = new System.Drawing.Point(353, 316);
            this.decibelPicker.Name = "decibelPicker";
            this.decibelPicker.Size = new System.Drawing.Size(120, 22);
            this.decibelPicker.TabIndex = 6;
            this.decibelPicker.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.decibelPicker.ValueChanged += new System.EventHandler(this.decibelValue_Changed);
            // 
            // Preview
            // 
            this.Preview.Location = new System.Drawing.Point(499, 116);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(111, 79);
            this.Preview.TabIndex = 7;
            this.Preview.Text = "Preview";
            this.Preview.UseVisualStyleBackColor = true;
            this.Preview.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(100, 316);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Sample length (seconds)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 409);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.decibelPicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileSaveButton);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.textBlockBoye);
            this.Controls.Add(this.fileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.decibelPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Label textBlockBoye;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.Button fileSaveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown decibelPicker;
        private System.Windows.Forms.Button Preview;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
    }
}

