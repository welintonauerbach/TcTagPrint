using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml;
using TcTagPrint.Controller;
using TcTagPrint.Model;

namespace TcTagPrint.Service
{
    /// <summary>
    /// Classe responsável pelo carregamento do XML e carregamento dos dados para a lista
    /// </summary>
    public class FileService
    {

        /// <summary>
        /// Cria a lista de Tags
        /// </summary>
        /// <param name="xmlPath"></param>
        public void LoadTags(string xmlPath)
        {
            try
            {
                LoadList(LoadXmlFile(xmlPath));
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar as Tags",e);
            }
        }
        
        /// <summary>
        /// Carrega o arquivo XML
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        private XmlDocument LoadXmlFile(string xmlPath)
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
                        TagInstance.GetProductServiceTag().AddTag(tag);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Importar o XML para os objetos",e);
            }
        }
    }
}