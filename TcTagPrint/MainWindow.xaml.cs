using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var ps = new ProductServiceTag();

            var print = new PrintServiceTag();

            //print.ReadExcelFile(@"C:\TEMP\TAGS-STRICT.xlsx");

            print.CreateTags(@"C:\TEMP\TAGS-test.xml");

        }
    }
}
