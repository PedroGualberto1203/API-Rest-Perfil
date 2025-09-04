namespace ApiPerfil{
    public static class Configuration
    {
        //TOKEN - JWT(Json Web Token)
        public static string JwtKey = "ZmVkYWY3ZDg4NjNDhlMTk3YjkyODdkNDkyYjcwOGU=";
        public static string ApiKeyName = "api_key"; // Nome da chave que será passada, o parâmetro que será utilizado na URL para autenticação, se esse parâmetro estiver la, ja vamos saber que esta autenticado/tem a permissão
        public static string ApiKey = "perfil_api_IlTevUM/z0ey3NwCV/unWg==";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration //Class para envio de email. As props tem seus valores dentro de appsettings.json
    {
        public string Host { get; set; } // Ex: gmail.com
        public int Port { get; set; } // Geralemente 25
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    }

}