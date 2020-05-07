using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcTagPrint.Model
{
    /// <summary>
    /// Clase da TAG
    /// </summary>
    public class ProductTag
    {
        public bool Print { get; set; }
        public string Posicao { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Of { get; set; }
        public string Orcamento { get; set; }
        public string Codigo { get; set; }
        public string NomeDesenho { get; set; }
        public int Quantidade { get; set; }
    }
}
