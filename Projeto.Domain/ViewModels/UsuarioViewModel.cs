namespace Projeto.Domain.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; }

        public string Token { get; set; }
        public string TokenAtualizacao { get; set; }
    }

    public class UsuarioCadastroViewModel
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class UsuarioLoginViewModel
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
