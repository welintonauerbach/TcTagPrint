using System.Collections.ObjectModel;
using TcTagPrint.Model;

namespace TcTagPrint.Service
{
    /// <summary>
    /// Classe de serviço para ProductTag
    /// Responsável por adicionar itens e retornar a lista.
    /// </summary>
    public class ProductServiceTag
    {
        /// <summary>
        /// Lista de ProductTag
        /// </summary>
        private ObservableCollection<ProductTag> TagItemList { get; set; }

        /// <summary>
        /// Método Construtor
        /// </summary>
        public ProductServiceTag()
        {
            TagItemList = new ObservableCollection<ProductTag>();
        }

        /// <summary>
        /// Adiciona um ProductTag na lista
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(ProductTag tag)
        {
            if (tag == null) return;

            TagItemList.Add(tag);
        }

        /// <summary>
        /// Retona a lista
        /// </summary>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<ProductTag> GetTags()
        {
            return TagItemList;
        }


    }
}
