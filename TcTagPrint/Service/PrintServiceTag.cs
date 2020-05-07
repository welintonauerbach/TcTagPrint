using bpac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using TcTagPrint.Model;

namespace TcTagPrint.Service
{
    /// <summary>
    /// Classe de Serviço de gerenciamento de carregamento dos dados e impressão
    /// </summary>
    public class PrintServiceTag
    {
        private readonly FileService _fileService;

        /// <summary>
        /// Construtor
        /// </summary>
        public PrintServiceTag()
        {
            _fileService = new FileService();
        }

        public FileService FileService
        {
            get { return _fileService; }
        }
        
        /// <summary>
        ///     Imprime as Etiquetas
        /// </summary>
        /// <param name="etiquetas"></param>
        public static void Print(ProductServiceTag productService)
        {
            try
            {
                var templatePath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(PrintServiceTag)).Location)}\\TagTemplate\\TagTemplate.lbx";

                var doc = new DocumentClass();
                if (doc.Open($"{templatePath}"))
                {
                    foreach (var productTag in productService.GetTags())
                    {
                        doc.GetObject(TagTemplateFieldNames.TagPosicao).Text = productTag.Posicao ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagItem).Text = productTag.Item ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagDescription).Text = productTag.Descricao ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagOf).Text = productTag.Of ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagOrcamento).Text = productTag.Orcamento ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagNumDesenho).Text = productTag.NomeDesenho ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagData).Text = DateTime.Now.ToString("MM/dd/yyyy") ?? string.Empty;

                        doc.StartPrint(string.Empty, PrintOptionConstants.bpoDefault);
                        doc.PrintOut(productTag.Quantidade, PrintOptionConstants.bpoDefault);
                        doc.EndPrint();
                    }
                    doc.Close();
                }
                else
                {
                    MessageBox.Show($"Erro ao Abrir.\n{doc.ErrorCode}");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao imprimir.\n {e}");
            }
        }
    }

}
