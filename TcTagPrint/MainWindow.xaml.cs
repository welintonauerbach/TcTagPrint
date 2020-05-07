﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcTagPrint.Controller;
using TcTagPrint.Model;
using TcTagPrint.Service;

namespace TcTagPrint
{
    /*
        Resumo da lógica para implementação e uso

        O usuário clica em um botão para importar o arquivo XML com os dados das TAGs
            Na importação será realizado uma validação para garantir a integridade dos dados
        
        Os dados importados serão apresentados em um DataGridView
            No DataGridView o usuário poderá selecionar quais TAGs serão impressas
        
        Para imprimir o usuário clicará em um botão IMPRIMIR TAGS
            Durante a impressão será mostrado uma barra de status o andamento da operação
            Ao finalizar será mostado Processo Finalizado
    */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Armazena o caminho do arquivo Xml
        /// </summary>
        private string _filePath = @"C:\TEMP\Cópia de TAGS - 450-0071C-20.xml";

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento do Botão Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLoadXml_Click(object sender, RoutedEventArgs e)
        {
            LoadDataTags();
            SetTagsListToDatagrid();
        }

        /// <summary>
        /// Carrega os dados das Tags do arquivo XML
        /// </summary>
        private void LoadDataTags()
        {
            TagInstance.GetProductServiceTag().GetTags().Clear();
            TagInstance.GetFileService().LoadTags(_filePath);
        }
        
        /// <summary>
        /// Seta a lista de Tags para o DataGrid
        /// </summary>
        private void SetTagsListToDatagrid()
        {
            DataTags.ItemsSource = TagInstance.GetProductServiceTag().GetTags();
        }
    }
}
