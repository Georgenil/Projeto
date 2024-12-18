namespace Projeto.Infra.Data.Constants
{
    public class ModelMaxLengthConstants
    {
        public const int Sigla = 4;
        public const int Nome = 250;
        public const int Email = 250;
        public const int CpfCnpj = 18;
        public const int WebSite = 350;
        public const int EscopoNome = 10;
        public const int SiglaPais = 2;
        public const int NomePais = 50;

        public class Cliente
        {
            public const int NomeFantasia = 350;
            public const int CodigoArea = 5;
            public const int Telefone = 20;
            public const int CodigoVat = 30;
            public const int InscricaoEstadual = 15;
            public const int InscricaoMunicipal = 15;
            public const int CNAE = 15;
        }

        public class Usuario
        {
            public const int Login = 100;
            public const int Senha = 100;
        }
    }
}

