namespace Projeto.Domain.Models
{
    public class Usuario:Base
    {
        public Guid GUID { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
