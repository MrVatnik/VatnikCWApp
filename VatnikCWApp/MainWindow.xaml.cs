using Humanizer;
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
        public static DataContext db;


        List<string> comboList = new List<string>{ 
            "Elements",
            "Resistors",
            "Capacitors",
            "Diodes",
            "Field Effect Transistors",
            "Bipolar Transistors" 
        };



        List<Element> ElementsTable;
        List<Resistor> ResistorsTable;
        List<Capacitor> CapacitorsTable;
        List<Diode> DiodesTable;
        List<FieldEffectTransistor> FETTable;
        List<BipolarTransistor> BTTable;


        public MainWindow()
        {
            InitializeComponent();
            
            db = new DataContext();
            

            TypesComboBox.ItemsSource = comboList;
            TypesComboBox.SelectedIndex = 0;
        }

        private void TypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = TypesComboBox.SelectedIndex;
          
            switch (i){
                case 0:
                    ElementsTable = db.GetTable<Element>().ToList<Element>();
                    dataGridMain.ItemsSource = ElementsTable;
                    break;
                case 1:
                    ResistorsTable = db.GetTable<Resistor>().ToList<Resistor>();
                    dataGridMain.ItemsSource = ResistorsTable;
                    break;
                case 2:
                    CapacitorsTable = db.GetTable<Capacitor>().ToList<Capacitor>();
                    dataGridMain.ItemsSource = CapacitorsTable;
                    break;
                case 3:
                    DiodesTable = db.GetTable<Diode>().ToList<Diode>();
                    dataGridMain.ItemsSource = DiodesTable;
                    break;
                case 4:
                    FETTable = db.GetTable<FieldEffectTransistor>().ToList<FieldEffectTransistor>();
                    dataGridMain.ItemsSource = FETTable;
                    break;
                case 5:
                    BTTable = db.GetTable<BipolarTransistor>().ToList<BipolarTransistor>();
                    dataGridMain.ItemsSource = BTTable;
                    break;
            }
        }

        private void dataGridMain_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //e.Cancel = true;   // For not to include 
            e.Column.IsReadOnly = true; // Makes the column as read only
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "ResId" || e.Column.Header.ToString() == "CapId" ||
                e.Column.Header.ToString() == "DioId" || e.Column.Header.ToString() == "FETId" || e.Column.Header.ToString() == "BTId")
            {
                e.Column.Header = "Id";
            }
            //e.Column.Header = e.Column.Header.ToString().Humanize();
            if(e.Column.Header.ToString() == "NominalPower"|| e.Column.Header.ToString() == "CutoffFrequency" ||
                e.Column.Header.ToString() == "MaxPowerLoss" || e.Column.Header.ToString() == "OpenChanelResistance")
            {
                e.Column.Header = e.Column.Header.ToString().Humanize(LetterCasing.Title);
            }
        }

        private void UpdateTable()
        {
            int index = TypesComboBox.SelectedIndex;
            if (TypesComboBox.SelectedIndex != 5)
                TypesComboBox.SelectedIndex += 1;
            else
                TypesComboBox.SelectedIndex = 0;
            TypesComboBox.SelectedIndex = index;
        }

        private void NewElButton_Click(object sender, RoutedEventArgs e)
        {
            AddElementWindow win = new AddElementWindow();
            win.ShowDialog();
            UpdateTable();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dgr = null;
            if (sender != null)
            {
                dgr = sender as DataGridRow;
            }
            int i = TypesComboBox.SelectedIndex;
            switch (i)
            {
                case 0:
                    {
                        Element selected = dgr.Item as Element;
                        EditElementWindow eew = new EditElementWindow(selected.Id, 0);
                        eew.ShowDialog();
                        break;
                    }
                case 1:
                    {
                        Resistor selected = dgr.Item as Resistor;
                        EditElementWindow eew = new EditElementWindow(selected.ResId, 1);
                        eew.ShowDialog();
                        break;
                    }
                case 2:
                    {
                        Capacitor selected = dgr.Item as Capacitor;
                        EditElementWindow eew = new EditElementWindow(selected.CapId, 2);
                        eew.ShowDialog();
                        break;
                    }
                case 3:
                    {
                        Diode selected = dgr.Item as Diode;
                        EditElementWindow eew = new EditElementWindow(selected.DioId, 3);
                        eew.ShowDialog();
                        break;
                    }
                case 4:
                    {
                        FieldEffectTransistor selected = dgr.Item as FieldEffectTransistor;
                        EditElementWindow eew = new EditElementWindow(selected.FETId, 4);
                        eew.ShowDialog();
                        break;
                    }
                case 5:
                    {
                        BipolarTransistor selected = dgr.Item as BipolarTransistor;
                        EditElementWindow eew = new EditElementWindow(selected.BTId, 5);
                        eew.ShowDialog();
                        break;
                    }
            }
            UpdateTable();
        }

    }
}
