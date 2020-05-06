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

            public void AddTag(ProductTag tag)
            {
                if (tag == null) return;

                TagItemList.Add(tag);
            }

            public List<ProductTag> GetTags()
            {
                return TagItemList;
            }

        
    }
}
