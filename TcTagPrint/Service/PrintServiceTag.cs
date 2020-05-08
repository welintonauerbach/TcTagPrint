using bpac;
using System;
using System.Threading.Tasks;
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
        public string PrintMessage { get; set; }

        /// <summary>
        ///     Imprime as Etiquetas
        /// </summary>
        public async Task Print()
        {
            await Task.Run(SendToPrint);
        }
        private void SendToPrint()
        {
            try
            {
                //var printer = new PrinterClass();
                //if (!printer.IsPrinterOnline("Engenharia_Etiquetas"))
                //{
                //    MessageBox.Show("A impressora não está Online, verifique se ela está ligada!","Impressora OFF-LINE");
                //    return;
                //}

                var docClass = new DocumentClass();

                if (docClass.Open(TagInstance.GetFileService().TemplatePath()))
                {
                    foreach (var productTag in TagInstance.GetProductServiceTag().GetTags())
                    {
                        if (productTag.Print != true) continue;

                        docClass.GetObject(TagTemplateFieldNames.TagPosicao).Text = productTag.Position ?? string.Empty;
                        docClass.GetObject(TagTemplateFieldNames.TagItem).Text = productTag.Item ?? string.Empty;
                        docClass.GetObject(TagTemplateFieldNames.TagDescription).Text = productTag.Description ?? string.Empty;
                        docClass.GetObject(TagTemplateFieldNames.TagOf).Text = productTag.Of ?? string.Empty;
                        docClass.GetObject(TagTemplateFieldNames.TagOrcamento).Text = productTag.OrderNumber ?? string.Empty;
                        docClass.GetObject(TagTemplateFieldNames.TagNumDesenho).Text = productTag.DrawingCodeName ?? string.Empty;
                        docClass.GetObject(TagTemplateFieldNames.TagData).Text = DateTime.Now.ToString("MM/dd/yyyy") ?? string.Empty;

                        docClass.StartPrint(string.Empty, PrintOptionConstants.bpoDefault);
                        docClass.PrintOut(productTag.Quantity, PrintOptionConstants.bpoDefault);
                        productTag.Printed = docClass.EndPrint();
                    }
                    docClass.Close();
                }
                else
                {
                    MessageBox.Show($"Erro ao Abrir.\n{docClass.ErrorCode}");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao imprimir.", e);
            }
        }
    }

    

}
