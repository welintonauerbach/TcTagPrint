using bpac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
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
        /// <summary>
        /// Lista de Tags
        /// </summary>
        private readonly ProductServiceTag productServiceTag;

        /// <summary>
        /// Construtor
        /// </summary>
        public PrintServiceTag()
        {
            productServiceTag = new ProductServiceTag();
        }

        /// <summary>
        /// Cria a lista de Tags
        /// </summary>
        /// <param name="xmlPath"></param>
        public void CreateTags(string xmlPath)
        {
            LoadList(LoadXml(xmlPath));
            if (productServiceTag.GetTags().Count > 0)
            {
                Print(productServiceTag);
            }
        }

        /// <summary>
        /// Carrega o arquivo XML
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        private XmlDocument LoadXml(string xmlPath)
        {
            try
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(xmlPath);

                return xmlDoc;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Carregar o XML", e);
            }
        }

        /// <summary>
        /// Lê os dados do XmlDocument.
        /// </summary>
        /// <param name="xmlDoc"></param>
        private void LoadList(XmlDocument xmlDoc)
        {
            try
            {
                // Obtem o elemento ROW do XML
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Row");

                for (var i = 1; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];

                    var tag = new ProductTag();

                    try
                    {
                        // Carrega o objeto com os dados do XML
                        tag.Posicao = node.ChildNodes[0].InnerText;
                        tag.Item = node.ChildNodes[1].InnerText;
                        tag.Descricao = node.ChildNodes[2].InnerText;
                        tag.Of = node.ChildNodes[3].InnerText;
                        tag.Orcamento = node.ChildNodes[4].InnerText;
                        tag.NomeDesenho = node.ChildNodes[5].InnerText;

                        // Valida a quantidade
                        var quant = node.ChildNodes[6].InnerText;
                        if (!string.IsNullOrEmpty(quant))
                        {
                            tag.Quantidade = Convert.ToInt16(quant.Replace(".00", ""));
                        }
                    }
                    catch
                    {
                        tag = null;
                    }

                    if (tag != null)
                    {
                        productServiceTag.AddTag(tag);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao Importar o XML\nErro:\n{e.Message}");
            }
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
