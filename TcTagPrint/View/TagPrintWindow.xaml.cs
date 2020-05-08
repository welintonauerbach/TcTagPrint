using System.Reflection;
using System.Windows;
using TcTagPrint.Controller;
using TcTagPrint.Model;

namespace TcTagPrint.View
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
        
        public MainWindow()
        {
            InitializeComponent();
            this.Title = $"TecniCAD TAG PRINT - Ver.: {typeof(MainWindow).Assembly.GetName().Version}";
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
            StatusBarLabel.Content = $"Arquivo XML -> {TagInstance.GetFileService().XmlFilePath}";
        }

        /// <summary>
        /// Evento do Botão Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonPrintTags_Click(object sender, RoutedEventArgs e)
        {
            await TagInstance.GetPrintServiceTag().Print().ConfigureAwait(false);
        }

        /// <summary>
        /// Carrega os dados das Tags do arquivo XML
        /// </summary>
        private void LoadDataTags()
        {
            TagInstance.GetProductServiceTag().GetTags().Clear();
            TagInstance.GetFileService().LoadTags(TagInstance.GetFileService().GetXmlFilePath());
        }
        
        /// <summary>
        /// Seta a lista de Tags para o DataGrid
        /// </summary>
        private void SetTagsListToDatagrid()
        {
            DataTags.ItemsSource = TagInstance.GetProductServiceTag().GetTags();
        }


        /// <summary>
        /// Evento para selecionar um ou mais itens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var itens = DataTags.SelectedItems;
            foreach (ProductTag p in itens)
            {
                p.Print = true;
            }
        }

        /// <summary>
        /// Evento para Deselecionar um ou mais itens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var itens = DataTags.SelectedItems;

            
            foreach (ProductTag p in itens)
            {
                p.Print = false;
            }
        }

        /// <summary>
        /// Limpa todas a linhas selecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClearSelectedTags_Click(object sender, RoutedEventArgs e)
        {
            foreach (var productTag in TagInstance.GetProductServiceTag().GetTags())
            {
                productTag.Print = false;
            }
        }
    }
}
