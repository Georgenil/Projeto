﻿namespace Projeto.Domain.Models
{
    public class Arquivo : Base
    {
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public byte[] Bytes { get; set; }
    }
}
