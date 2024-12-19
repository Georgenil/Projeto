using Projeto.Domain.Models;

namespace Projeto.Service.DTO
{
    public class UserDTO
    {
        public User user { get; set; }
        public string Token { get; set; }
        public string TokenAtualizacao { get; set; }
        public long TokenValidade { get; set; }
    }
}
