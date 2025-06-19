namespace FinTracker.AlternativeTelas
{
    partial class MudarSenha
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
            this.btnSalvar = new System.Windows.Forms.Button();
            this.novaSenha = new System.Windows.Forms.TextBox();
            this.senhaAtual = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.novaSenha2 = new System.Windows.Forms.TextBox();
            this.checkSenha2 = new System.Windows.Forms.CheckBox();
            this.checkSenha1 = new System.Windows.Forms.CheckBox();
            this.checkSenhaAtual = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(39)))), ((int)(((byte)(29)))));
            this.btnSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSalvar.FlatAppearance.BorderSize = 2;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSalvar.Location = new System.Drawing.Point(543, 531);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(240, 45);
            this.btnSalvar.TabIndex = 68;
            this.btnSalvar.Text = "Definir nova senha";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // novaSenha
            // 
            this.novaSenha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.novaSenha.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novaSenha.Location = new System.Drawing.Point(22, 190);
            this.novaSenha.Name = "novaSenha";
            this.novaSenha.PasswordChar = '•';
            this.novaSenha.Size = new System.Drawing.Size(761, 31);
            this.novaSenha.TabIndex = 66;
            // 
            // senhaAtual
            // 
            this.senhaAtual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.senhaAtual.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.senhaAtual.Location = new System.Drawing.Point(22, 110);
            this.senhaAtual.Name = "senhaAtual";
            this.senhaAtual.PasswordChar = '•';
            this.senhaAtual.Size = new System.Drawing.Size(761, 31);
            this.senhaAtual.TabIndex = 67;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(39)))), ((int)(((byte)(29)))));
            this.label3.Location = new System.Drawing.Point(19, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 18);
            this.label3.TabIndex = 63;
            this.label3.Text = "Nova senha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(39)))), ((int)(((byte)(29)))));
            this.label2.Location = new System.Drawing.Point(19, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 18);
            this.label2.TabIndex = 64;
            this.label2.Text = "Senha atual";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(39)))), ((int)(((byte)(29)))));
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 25);
            this.label1.TabIndex = 65;
            this.label1.Text = "Mudar senha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(39)))), ((int)(((byte)(29)))));
            this.label4.Location = new System.Drawing.Point(19, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 18);
            this.label4.TabIndex = 63;
            this.label4.Text = "Repita a nova senha";
            // 
            // novaSenha2
            // 
            this.novaSenha2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.novaSenha2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novaSenha2.Location = new System.Drawing.Point(22, 270);
            this.novaSenha2.Name = "novaSenha2";
            this.novaSenha2.PasswordChar = '•';
            this.novaSenha2.Size = new System.Drawing.Size(761, 31);
            this.novaSenha2.TabIndex = 66;
            // 
            // checkSenha2
            // 
            this.checkSenha2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkSenha2.AutoSize = true;
            this.checkSenha2.Location = new System.Drawing.Point(758, 278);
            this.checkSenha2.Margin = new System.Windows.Forms.Padding(2);
            this.checkSenha2.Name = "checkSenha2";
            this.checkSenha2.Size = new System.Drawing.Size(15, 14);
            this.checkSenha2.TabIndex = 69;
            this.checkSenha2.UseVisualStyleBackColor = true;
            this.checkSenha2.CheckedChanged += new System.EventHandler(this.checkSenha2_CheckedChanged);
            // 
            // checkSenha1
            // 
            this.checkSenha1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkSenha1.AutoSize = true;
            this.checkSenha1.Location = new System.Drawing.Point(758, 198);
            this.checkSenha1.Margin = new System.Windows.Forms.Padding(2);
            this.checkSenha1.Name = "checkSenha1";
            this.checkSenha1.Size = new System.Drawing.Size(15, 14);
            this.checkSenha1.TabIndex = 69;
            this.checkSenha1.UseVisualStyleBackColor = true;
            this.checkSenha1.CheckedChanged += new System.EventHandler(this.checkSenha1_CheckedChanged);
            // 
            // checkSenhaAtual
            // 
            this.checkSenhaAtual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkSenhaAtual.AutoSize = true;
            this.checkSenhaAtual.Location = new System.Drawing.Point(758, 118);
            this.checkSenhaAtual.Margin = new System.Windows.Forms.Padding(2);
            this.checkSenhaAtual.Name = "checkSenhaAtual";
            this.checkSenhaAtual.Size = new System.Drawing.Size(15, 14);
            this.checkSenhaAtual.TabIndex = 69;
            this.checkSenhaAtual.UseVisualStyleBackColor = true;
            this.checkSenhaAtual.CheckedChanged += new System.EventHandler(this.checkSenhaAtual_CheckedChanged);
            // 
            // MudarSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.checkSenhaAtual);
            this.Controls.Add(this.checkSenha1);
            this.Controls.Add(this.checkSenha2);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.novaSenha2);
            this.Controls.Add(this.novaSenha);
            this.Controls.Add(this.senhaAtual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MudarSenha";
            this.Text = "MudarSenha";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox novaSenha;
        private System.Windows.Forms.TextBox senhaAtual;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox novaSenha2;
        private System.Windows.Forms.CheckBox checkSenha2;
        private System.Windows.Forms.CheckBox checkSenha1;
        private System.Windows.Forms.CheckBox checkSenhaAtual;
    }
}