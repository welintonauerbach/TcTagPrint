using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using Microsoft.Win32;
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
        /// Endereço do arquivo XML
        /// </summary>
        public string XmlFilePath { get; set; }

        /// <summary>
        /// Cria a lista de Tags
        /// </summary>
        /// <param name="xmlPath"></param>
        public void LoadTags(string xmlPath)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlPath))
                {
                    return;
                }
                var resultFile = LoadXmlFile(xmlPath);
                if (resultFile != null)
                {
                    LoadList(resultFile);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar as Tags", e);
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
                FileInfo info = new FileInfo(xmlPath);
                if (IsFileLocked(info))
                {
                    MessageBox.Show("O arquivo selecionado está em uso", "Arquivo em Uso");
                    return null;
                }

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
                        tag.Position = node.ChildNodes[0].InnerText;
                        tag.Item = node.ChildNodes[1].InnerText;
                        tag.Description = node.ChildNodes[2].InnerText;
                        tag.Of = node.ChildNodes[3].InnerText;
                        tag.OrderNumber = node.ChildNodes[4].InnerText;
                        tag.DrawingCodeName = node.ChildNodes[5].InnerText;

                        // Valida a quantidade
                        var quant = node.ChildNodes[6].InnerText;
                        if (!string.IsNullOrEmpty(quant))
                        {
                            tag.Quantity = Convert.ToInt16(quant.Replace(".00", ""));
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
                throw new Exception("Erro ao Importar o XML para os objetos", e);
            }
        }

        /// <summary>
        /// Local do arquivo de Template de Etiquetas -> .LBX
        /// </summary>
        /// <returns></returns>
        public string TemplatePath()
        {
            var templatePath = $"{Path.GetDirectoryName(Assembly.GetAssembly(typeof(App)).Location)}\\TagTemplate\\TagTemplate.lbx";
            return templatePath;
        }

        /// <summary>
        /// Retorna o caminho do arquivo XML Selecionado
        /// </summary>
        /// <returns></returns>
        public string GetXmlFilePath()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivo XML (*.xml)|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
            {
                XmlFilePath = openFileDialog.FileName;
            }
            return XmlFilePath;
        }

        /// <summary>
        /// Verifica se o arquivo está disponível
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //o arquivo está indiposnível pelas seguintes causas:
                //está sendo escrito
                //utilizado por uma outra thread
                //não existe ou sendo criado
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //arquivo está disponível
            return false;
        }
    }
}