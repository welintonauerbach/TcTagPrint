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
    public class PrintServiceTag
    {
        private readonly ProductServiceTag productServiceTag;

        public PrintServiceTag()
        {
            productServiceTag = new ProductServiceTag();
        }

        public void LoadData(string dataPath)
        {
            ImportData(dataPath);
        }

        public void ReadExcelFile(string fileName)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                //WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                //WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                //SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                //string text;
                //foreach (Row r in sheetData.Elements<Row>())
                //{
                //    foreach (Cell c in r.Elements<Cell>())
                //    {
                //        text = c.CellValue.Text;
                //        Debug.Write(text + " ");
                //    }
                //}

                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

                OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
                string text;
                while (reader.Read())
                {
                    if (reader.ElementType == typeof(Cell))
                    {
                        var cell = reader.LoadCurrentElement() as Cell;

                        text = cell.CellValue?.InnerText ?? "- null -";// GetText();
                        Debug.Write(text + " ");
                    }
                }
            }
        }

        private void ImportData(string dataPath)
        {
            var contents = File.ReadAllText(dataPath).Split('\n');

            ProcessData(1, contents);
        }

        public void ImportFromCmd(List<string> content)
        {
            ProcessData(0, content.ToArray());
        }

        /// <summary>
        /// Realiza o processamento do arquivo para impressão
        /// </summary>
        /// <param name="headerRows"></param>
        /// <param name="contents"></param>
        private void ProcessData(int headerRows, string[] contents)
        {
            try
            {
                var csv = from line in contents select line.Split(';').ToArray();

                foreach (string[] row in csv.Skip(headerRows).TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
                {
                    string textCompleto2 = string.Empty;
                    int iii = 0;
                    foreach (var c in contents)
                    {
                        textCompleto2 = $"{textCompleto2}\n{++iii} <-> {c}";
                    }
                    
                    if (string.IsNullOrEmpty(row[1])) continue;

                    var et = new ProductTag
                    {
                        Codigo = row[0].Replace("\"", "").Trim(),
                        Of = row[1].Replace("\"", "").Trim(),
                        Descricao = row[2].Replace("\"", "").Trim(),
                        Orcamento = row[3].Replace("\"", "").Trim(),
                        NomeDesenho = row[4].Replace("\"", "").Trim(),
                        Item = row[5].Replace("\"", "").Trim(),
                        Posicao = row[6].Replace("\"", "").Trim(),                       
                    };

                    productServiceTag.AddEtiqueta(et);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Algum erro ocorreu.\n\n{e}");
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
                var templatePath = "";

                var doc = new DocumentClass();
                if (doc.Open($"{templatePath}"))
                {
                    foreach (var productTag in productService.GetEtiquetas())
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
