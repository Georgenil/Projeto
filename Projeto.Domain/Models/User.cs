namespace Projeto.Domain.Models
{
    public class User:Base
    {
        public Guid GUID { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
