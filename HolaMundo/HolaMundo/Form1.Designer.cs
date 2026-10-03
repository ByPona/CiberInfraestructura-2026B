namespace HolaMundo
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();

            SuspendLayout();

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(387, 45);
            label1.Name = "label1";
            label1.Size = new Size(270, 32);
            label1.TabIndex = 0;
            label1.Text = "Validador de contraseña";

            // 
            // button1
            // 
            button1.Location = new Point(352, 243);
            button1.Name = "button1";
            button1.Size = new Size(350, 46);
            button1.TabIndex = 1;
            button1.Text = "Validar Contraseña";
            button1.UseVisualStyleBackColor = true;

            // 
            // textBox1
            // 
            textBox1.Location = new Point(421, 103);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(362, 39);
            textBox1.TabIndex = 2;

            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(165, 106);
            label2.Name = "label2";
            label2.Size = new Size(240, 32);
            label2.TabIndex = 3;
            label2.Text = "Escribe tu contraseña";

            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 157);
            label3.Name = "label3";
            label3.Size = new Size(339, 32);
            label3.TabIndex = 5;
            label3.Text = "Vuelva a escribir tu contraseña";

            // 
            // textBox2
            // 
            textBox2.Location = new Point(421, 157);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(362, 39);
            textBox2.TabIndex = 4;

            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(722, 216);
            label4.Name = "label4";
            label4.Size = new Size(334, 160);
            label4.TabIndex = 6;
            label4.Text = "Debe contener:\r\n" +
                         "Al menos una letra mayúscula\r\n" +
                         "Al menos una letra minúscula\r\n" +
                         "Al menos un símbolo\r\n" +
                         "Al menos un número";

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 701);

            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label1);

            Name = "Form1";
            Text = "Validador de contraseña";

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
    }
}