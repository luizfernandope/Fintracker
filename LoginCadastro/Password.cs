using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace FinTracker.LoginCadastro
{
    public partial class Password : Form
    {
        Principal telaMae;
        public Password()
        {
            InitializeComponent();
        }
        public Password(Principal principal)
        {
            InitializeComponent();
            telaMae = principal;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            telaMae.mudarTelaDoPanelPrincipal(new Login(telaMae));
        }

        public string gerarNovaSenha()
        {
            // Gera uma nova senha aleatória de 8 caracteres
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder novaSenha = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(caracteres.Length);
                novaSenha.Append(caracteres[index]);
            }
            return novaSenha.ToString();
        }

        public void EnviarEmailRecuperacao(string destinatario)
        {
            string novaSenha = gerarNovaSenha();
            string remetente = "seuemail@gmail.com";
            string senhaApp = "senha-do-app-aqui";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(remetente);
            mail.To.Add(destinatario);
            mail.Subject = "Recuperação de Senha";
            mail.Body = $"Sua nova senha é: {novaSenha}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(remetente, senhaApp);
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Email enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
            }
        }
    }
}
