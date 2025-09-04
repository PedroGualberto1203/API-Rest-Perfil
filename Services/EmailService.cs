using System.Net;
using System.Net.Mail;

namespace ApiPerfil.Services
{
    public class EmailService
    {
        public bool Send( //Método para enviar um email
            string toName, //Para quem, seu nome
            string toEmail,//Para quem, seu email
            string subject, //Assunto do email
            string body, //Corpo do email
            string fromName = "Equipe Perfil Acessórios",
            string fromEmail = "pedro.gualberto1203@gmail.com" //Email validado no sendgrid, se n nao vai funcionar
            )
        {
            var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port); //Cria o cliente SMTP que pede o host e a porta do servidor de email, utilizando a class criada no Configuration para envio de email. Tanto Host quanto Port estão setados no appsettings.json

            smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password); //Credenciais do servidor de email, utilizando a class criada no Configuration para envio de email. UserName e Password estão setados no appsettings.json
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; //Define o método de entrega do email, nesse caso é pela rede
            smtpClient.EnableSsl = true; //Habilita o SSL, que é uma camada de segurança para o envio de email

            var mail = new MailMessage(); //Cria a mensagem de email

            mail.From = new MailAddress(fromEmail, fromName); //Define o remetente do email, utilizando o email e o nome do remetente
            mail.To.Add(new MailAddress(toEmail, toName)); //Define o destinatário do email, utilizando o email e o nome do destinatário
            mail.Subject = subject; //Define o assunto do email
            mail.Body = body; //Define o corpo do email
            mail.IsBodyHtml = true; //Define que o corpo do email é em HTML

            try
            {
                smtpClient.Send(mail); //Tenta enviar o email
                return true; //Se enviar com sucesso, retorna true
            }
            catch (Exception ex)
            {
                return false; //Se der erro, retorna false
            }
        }
    }       
}   
