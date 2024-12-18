namespace Projeto.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
