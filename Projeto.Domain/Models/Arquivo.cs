using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Models
{
    public class Arquivo : Base
    {
        public string Name { get; set; }
        public string Extensao { get; set; }
        public byte[] Bytes { get; set; }
    }
}
