namespace Projeto.Infra.Utils.Configurations
{
    public class AppConfig
    {
        public string ChaveSecreta { get; set; }
        public int MinutosValidadeTokenAcesso { get; set; }
        public int MinutosJanelaReativacaoToken { get; set; }
        public int MinutosValidadeTokenRecuperarSenha { get; set; }
    }
}
