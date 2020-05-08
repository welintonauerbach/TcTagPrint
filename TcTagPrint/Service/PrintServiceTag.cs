using bpac;
using System;
using System.Windows;
using TcTagPrint.Controller;
using TcTagPrint.Model;

namespace TcTagPrint.Service
{
    /// <summary>
    /// Classe de Serviço de gerenciamento de carregamento dos dados e impressão
    /// </summary>
    public class PrintServiceTag
    {
        /// <summary>
        ///     Imprime as Etiquetas
        /// </summary>
        public void Print()
        {
            try
            {
                var doc = new DocumentClass();
                if (doc.Open(TagInstance.GetFileService().TemplatePath()))
                {
                    foreach (var productTag in TagInstance.GetProductServiceTag().GetTags())
                    {
                        doc.GetObject(TagTemplateFieldNames.TagPosicao).Text = productTag.Position ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagItem).Text = productTag.Item ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagDescription).Text = productTag.Description ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagOf).Text = productTag.Of ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagOrcamento).Text = productTag.OrderNumber ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagNumDesenho).Text = productTag.DrawingCodeName ?? string.Empty;
                        doc.GetObject(TagTemplateFieldNames.TagData).Text = DateTime.Now.ToString("MM/dd/yyyy") ?? string.Empty;

                        doc.StartPrint(string.Empty, PrintOptionConstants.bpoDefault);
                        doc.PrintOut(productTag.Quantity, PrintOptionConstants.bpoDefault);
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
                throw new Exception("Erro ao imprimir.",e);
            }
        }
    }

}
