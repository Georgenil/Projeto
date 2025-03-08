namespace Projeto.Domain.ViewModels
{
    public class ArquivoViewModel : BaseViewModel
    {
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public byte[] Bytes { get; set; }
    }
}
