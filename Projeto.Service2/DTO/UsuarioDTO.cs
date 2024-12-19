using Projeto.Domain.Models;

namespace Projeto.Service.DTO
{
    public class UsuarioDTO
    {
        public Usuario usuario { get; set; }
        public string Token { get; set; }
        public string TokenAtualizacao { get; set; }
        public long TokenValidade { get; set; }
    }
}
