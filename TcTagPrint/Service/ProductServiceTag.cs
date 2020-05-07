using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcTagPrint.Model;

namespace TcTagPrint.Service
{
    public class ProductServiceTag
    {        
            private ObservableCollection<ProductTag> TagItemList { get; set; }
            //public ObservableCollection<ProductTag> ProductTags { get; set; }

            public ProductServiceTag()
            {
                TagItemList = new ObservableCollection<ProductTag>();
            }

            public void AddTag(ProductTag tag)
            {
                if (tag == null) return;

                TagItemList.Add(tag);
                //ProductTags.Add(tag);
            }

            public ObservableCollection<ProductTag> GetTags()
            {
                return TagItemList;
            }

        
    }
}
