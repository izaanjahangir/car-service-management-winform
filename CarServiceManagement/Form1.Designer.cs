namespace CarServiceManagement
{
    partial class Login
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
            this.email_tb = new System.Windows.Forms.TextBox();
            this.login_lb = new System.Windows.Forms.Label();
            this.password_lb = new System.Windows.Forms.Label();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.title_lb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loading_lb = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // email_tb
            // 
            this.email_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.email_tb.Location = new System.Drawing.Point(12, 166);
            this.email_tb.Name = "email_tb";
            this.email_tb.Size = new System.Drawing.Size(297, 20);
            this.email_tb.TabIndex = 0;
            // 
            // login_lb
            // 
            this.login_lb.AutoSize = true;
            this.login_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_lb.Location = new System.Drawing.Point(8, 143);
            this.login_lb.Name = "login_lb";
            this.login_lb.Size = new System.Drawing.Size(48, 20);
            this.login_lb.TabIndex = 1;
            this.login_lb.Text = "Email";
            // 
            // password_lb
            // 
            this.password_lb.AutoSize = true;
            this.password_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_lb.Location = new System.Drawing.Point(8, 223);
            this.password_lb.Name = "password_lb";
            this.password_lb.Size = new System.Drawing.Size(78, 20);
            this.password_lb.TabIndex = 3;
            this.password_lb.Text = "Password";
            // 
            // password_tb
            // 
            this.password_tb.Location = new System.Drawing.Point(12, 246);
            this.password_tb.Name = "password_tb";
            this.password_tb.PasswordChar = '*';
            this.password_tb.Size = new System.Drawing.Size(297, 20);
            this.password_tb.TabIndex = 2;
            // 
            // login_btn
            // 
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.Location = new System.Drawing.Point(12, 317);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(297, 30);
            this.login_btn.TabIndex = 4;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // title_lb
            // 
            this.title_lb.AutoSize = true;
            this.title_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lb.Location = new System.Drawing.Point(68, 43);
            this.title_lb.Name = "title_lb";
            this.title_lb.Size = new System.Drawing.Size(182, 37);
            this.title_lb.TabIndex = 5;
            this.title_lb.Text = "Car Service";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 37);
            this.label1.TabIndex = 6;
            this.label1.Text = "Management";
            // 
            // loading_lb
            // 
            this.loading_lb.AutoSize = true;
            this.loading_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loading_lb.Location = new System.Drawing.Point(38, 390);
            this.loading_lb.Name = "loading_lb";
            this.loading_lb.Size = new System.Drawing.Size(251, 25);
            this.loading_lb.TabIndex = 7;
            this.loading_lb.Text = "Logging in... Please Wait";
            this.loading_lb.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Hint: Use admin@example.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Hint: Use 12345678";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loading_lb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.title_lb);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.password_lb);
            this.Controls.Add(this.password_tb);
            this.Controls.Add(this.login_lb);
            this.Controls.Add(this.email_tb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox email_tb;
        private System.Windows.Forms.Label login_lb;
        private System.Windows.Forms.Label password_lb;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.Label title_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label loading_lb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

