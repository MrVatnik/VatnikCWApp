using LinqToDB;
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

namespace VatnikCWApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connectionString = @"Data Source=WATNIK-LAPTOP\MSSQLSERVER01;Initial Catalog=VatnikElements;Integrated Security=True";
        public static DataContext db;


        List<string> comboList = new List<string>{ 
            "Elements",
            "Resistors",
            "Capacitors",
            "Diodes",
            "Field Effect Transistors",
            "Bipolar Transistors" 
        };


        public MainWindow()
        {
            InitializeComponent();

            db = new DataContext("SqlServer.2019", connectionString);

            /*List<Element> ElementsList = new List<Element>
            {
                new Element {Id=1,Name="aaa",Type=ElTypes.Resistor,Price=(float)1.1},
                new Element {Id=1,Name="bbb",Type=ElTypes.Capacitor,Price=(float)2.2},
                new Element {Id=1,Name="ccc",Type=ElTypes.Diode,Price=(float)3.3}
            };*/
            /*ITable<Element> ElementsList = db.GetTable<Element>();
            dataGridMain.ItemsSource = ElementsList;*/

            TypesComboBox.ItemsSource = comboList;
            TypesComboBox.SelectedIndex = 0;
        }

        private void TypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = TypesComboBox.SelectedIndex;
            switch (i){
                case 0:
                    dataGridMain.ItemsSource = db.GetTable<Element>();
                    break;
                case 1:
                    dataGridMain.ItemsSource = db.GetTable<Resistor>();
                    break;
                case 2:
                    dataGridMain.ItemsSource = db.GetTable<Capacitor>();
                    break;
                case 3:
                    dataGridMain.ItemsSource = db.GetTable<Diode>();
                    break;
                case 4:
                    dataGridMain.ItemsSource = db.GetTable<FieldEffectTransistor>();
                    break;
                case 5:
                    dataGridMain.ItemsSource = db.GetTable<BipolarTransistor>();
                    break;
            }
        }

        private void dataGridMain_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "ResId" || e.Column.Header.ToString() == "CapId" ||
                e.Column.Header.ToString() == "DioId" || e.Column.Header.ToString() == "FETId" || e.Column.Header.ToString() == "BTId")
            {
                //e.Cancel = true;   // For not to include 
                 e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }

        private void NewElButton_Click(object sender, RoutedEventArgs e)
        {
            AddElementWindow win = new AddElementWindow();
            win.Show();
        }
    }
}
