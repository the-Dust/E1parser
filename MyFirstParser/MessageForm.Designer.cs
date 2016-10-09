namespace E1Parser
{
    partial class MessageForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMessageForm = new System.Windows.Forms.Label();
            this.buttonRewrite = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMessageForm
            // 
            this.labelMessageForm.AutoSize = true;
            this.labelMessageForm.Location = new System.Drawing.Point(18, 22);
            this.labelMessageForm.Name = "labelMessageForm";
            this.labelMessageForm.Size = new System.Drawing.Size(329, 65);
            this.labelMessageForm.TabIndex = 0;
            this.labelMessageForm.Text = "База данных с таким именем уже существует.\r\n\r\nПерезаписать - перезаписать данную " +
                "базу.\r\nДобавить - добавить новые данные в существующую базу.\r\nИзменить - изменит" +
                "ь директорию сохранения и/или имя базы.";
            // 
            // buttonRewrite
            // 
            this.buttonRewrite.Location = new System.Drawing.Point(21, 110);
            this.buttonRewrite.Name = "buttonRewrite";
            this.buttonRewrite.Size = new System.Drawing.Size(96, 23);
            this.buttonRewrite.TabIndex = 1;
            this.buttonRewrite.Text = "Перезаписать";
            this.buttonRewrite.UseVisualStyleBackColor = true;
            this.buttonRewrite.Click += new System.EventHandler(this.buttonRewrite_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(134, 110);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(96, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonChange
            // 
            this.buttonChange.Location = new System.Drawing.Point(247, 110);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(96, 23);
            this.buttonChange.TabIndex = 3;
            this.buttonChange.Text = "Изменить";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 157);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRewrite);
            this.Controls.Add(this.labelMessageForm);
            this.Name = "MessageForm";
            this.Text = "MessageForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessageForm;
        private System.Windows.Forms.Button buttonRewrite;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonChange;
    }
}