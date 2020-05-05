using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcTagPrint.Model;

namespace TcTagPrint.Service
{
    public class ProductServiceTag
    {        
            private List<ProductTag> TagItemList { get; set; }

            public ProductServiceTag()
            {
                TagItemList = new List<ProductTag>();
            }

            public void AddEtiqueta(ProductTag etiqueta)
            {
                if (etiqueta == null) return;

                TagItemList.Add(etiqueta);
            }

            public List<ProductTag> GetEtiquetas()
            {
                return TagItemList;
            }

        
    }
}
