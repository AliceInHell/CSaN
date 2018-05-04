namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox_Input = new System.Windows.Forms.TextBox();
            this.label_CurDir = new System.Windows.Forms.Label();
            this.textBox_CurDir = new System.Windows.Forms.TextBox();
            this.richTextBox_Output = new System.Windows.Forms.RichTextBox();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.label_Status = new System.Windows.Forms.Label();
            this.Button_SetDir = new System.Windows.Forms.Button();
            this.button_Download = new System.Windows.Forms.Button();
            this.button_Upload = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_mkDir = new System.Windows.Forms.Button();
            this.button_rmDir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox_Input
            // 
            this.textBox_Input.Location = new System.Drawing.Point(578, 307);
            this.textBox_Input.Name = "textBox_Input";
            this.textBox_Input.Size = new System.Drawing.Size(376, 20);
            this.textBox_Input.TabIndex = 0;
            // 
            // label_CurDir
            // 
            this.label_CurDir.AutoSize = true;
            this.label_CurDir.Location = new System.Drawing.Point(12, 34);
            this.label_CurDir.Name = "label_CurDir";
            this.label_CurDir.Size = new System.Drawing.Size(36, 13);
            this.label_CurDir.TabIndex = 5;
            this.label_CurDir.Text = "CurDir";
            // 
            // textBox_CurDir
            // 
            this.textBox_CurDir.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_CurDir.Location = new System.Drawing.Point(66, 34);
            this.textBox_CurDir.Name = "textBox_CurDir";
            this.textBox_CurDir.ReadOnly = true;
            this.textBox_CurDir.Size = new System.Drawing.Size(392, 13);
            this.textBox_CurDir.TabIndex = 6;
            // 
            // richTextBox_Output
            // 
            this.richTextBox_Output.Location = new System.Drawing.Point(15, 70);
            this.richTextBox_Output.Name = "richTextBox_Output";
            this.richTextBox_Output.ReadOnly = true;
            this.richTextBox_Output.Size = new System.Drawing.Size(443, 346);
            this.richTextBox_Output.TabIndex = 11;
            this.richTextBox_Output.Text = "";
            // 
            // textBox_Status
            // 
            this.textBox_Status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Status.Location = new System.Drawing.Point(578, 252);
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.ReadOnly = true;
            this.textBox_Status.Size = new System.Drawing.Size(376, 13);
            this.textBox_Status.TabIndex = 12;
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(515, 252);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(37, 13);
            this.label_Status.TabIndex = 13;
            this.label_Status.Text = "Status";
            // 
            // Button_SetDir
            // 
            this.Button_SetDir.Location = new System.Drawing.Point(879, 366);
            this.Button_SetDir.Name = "Button_SetDir";
            this.Button_SetDir.Size = new System.Drawing.Size(75, 23);
            this.Button_SetDir.TabIndex = 14;
            this.Button_SetDir.Text = "SetDir";
            this.Button_SetDir.UseVisualStyleBackColor = true;
            this.Button_SetDir.Click += new System.EventHandler(this.Button_SetDir_Click);
            // 
            // button_Download
            // 
            this.button_Download.Location = new System.Drawing.Point(783, 366);
            this.button_Download.Name = "button_Download";
            this.button_Download.Size = new System.Drawing.Size(75, 23);
            this.button_Download.TabIndex = 15;
            this.button_Download.Text = "Download";
            this.button_Download.UseVisualStyleBackColor = true;
            this.button_Download.Click += new System.EventHandler(this.button_Download_Click);
            // 
            // button_Upload
            // 
            this.button_Upload.Location = new System.Drawing.Point(681, 366);
            this.button_Upload.Name = "button_Upload";
            this.button_Upload.Size = new System.Drawing.Size(75, 23);
            this.button_Upload.TabIndex = 16;
            this.button_Upload.Text = "Upload";
            this.button_Upload.UseVisualStyleBackColor = true;
            this.button_Upload.Click += new System.EventHandler(this.button_Upload_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(578, 366);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Delete.TabIndex = 17;
            this.button_Delete.Text = "Delete";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_mkDir
            // 
            this.button_mkDir.Location = new System.Drawing.Point(477, 366);
            this.button_mkDir.Name = "button_mkDir";
            this.button_mkDir.Size = new System.Drawing.Size(75, 23);
            this.button_mkDir.TabIndex = 18;
            this.button_mkDir.Text = "mkDir";
            this.button_mkDir.UseVisualStyleBackColor = true;
            this.button_mkDir.Click += new System.EventHandler(this.button_mkDir_Click);
            // 
            // button_rmDir
            // 
            this.button_rmDir.Location = new System.Drawing.Point(477, 304);
            this.button_rmDir.Name = "button_rmDir";
            this.button_rmDir.Size = new System.Drawing.Size(75, 23);
            this.button_rmDir.TabIndex = 19;
            this.button_rmDir.Text = "rmDir";
            this.button_rmDir.UseVisualStyleBackColor = true;
            this.button_rmDir.Click += new System.EventHandler(this.button_rmDir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 428);
            this.Controls.Add(this.button_rmDir);
            this.Controls.Add(this.button_mkDir);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.button_Upload);
            this.Controls.Add(this.button_Download);
            this.Controls.Add(this.Button_SetDir);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.textBox_Status);
            this.Controls.Add(this.richTextBox_Output);
            this.Controls.Add(this.textBox_CurDir);
            this.Controls.Add(this.label_CurDir);
            this.Controls.Add(this.textBox_Input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox_Input;
        private System.Windows.Forms.Label label_CurDir;
        private System.Windows.Forms.TextBox textBox_CurDir;
        public System.Windows.Forms.RichTextBox richTextBox_Output;
        private System.Windows.Forms.TextBox textBox_Status;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Button Button_SetDir;
        private System.Windows.Forms.Button button_Download;
        private System.Windows.Forms.Button button_Upload;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_mkDir;
        private System.Windows.Forms.Button button_rmDir;
    }
}

