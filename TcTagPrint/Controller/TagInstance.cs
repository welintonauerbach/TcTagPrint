using TcTagPrint.Service;

namespace TcTagPrint.Controller
{
    /// <summary>
    /// Instância de TAG para classes de serviços
    /// Criação do Singleton
    /// </summary>
    public static class TagInstance
    {
        private static ProductServiceTag _productServiceTag;
        private static PrintServiceTag _printServiceTag;
        private static FileService _fileService;

        /// <summary>
        /// Retorna a instância da classe ProductServiceTag
        /// </summary>
        /// <returns></returns>
        public static ProductServiceTag GetProductServiceTag()
        {
            if (_productServiceTag != null) return _productServiceTag;
            {
                _productServiceTag = new ProductServiceTag();
                return _productServiceTag;
            }
        }

        /// <summary>
        /// Retorna a instância da classe PrintServiceTag
        /// </summary>
        /// <returns></returns>
        public static PrintServiceTag GetPrintServiceTag()
        {
            if (_printServiceTag != null) return _printServiceTag;
            {
                _printServiceTag = new PrintServiceTag();
                return _printServiceTag;
            }
        }

        /// <summary>
        /// Retorna a instância da classe PrintServiceTag
        /// </summary>
        /// <returns></returns>
        public static FileService GetFileService()
        {
            if(_fileService != null) return _fileService;
            {
                _fileService = new FileService();
                return _fileService;
            }
        }
    }
}
