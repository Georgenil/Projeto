namespace Projeto.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Ativo { get; set; }

        public string Token { get; set; }
        public string TokenAtualizacao { get; set; }
    }

    public class UserLoginViewModel
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
