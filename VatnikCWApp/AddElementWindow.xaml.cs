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
using System.Windows.Shapes;

namespace VatnikCWApp
{
    /// <summary>
    /// Логика взаимодействия для AddElementWindow.xaml
    /// </summary>
    public partial class AddElementWindow : Window
    {
        List<Resistor> ResList;
        List<Capacitor> CapList;
        List<Diode> DioList;
        List<FieldEffectTransistor> FETList;
        List<BipolarTransistor> BTList;

        List<string> comboList = new List<string>{
            "Resistors",
            "Capacitors",
            "Diodes",
            "Field Effect Transistors",
            "Bipolar Transistors"
        };

        public AddElementWindow()
        {
            InitializeComponent();

            TypeComboBox.ItemsSource = comboList;
            TypeComboBox.SelectedIndex = 0;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int i = TypeComboBox.SelectedIndex;
            switch (i)
            {
                case 0:
                    ResList[0].Insert((float)double.Parse(PriceTextBox.Text.Replace('.', ',')), MainWindow.db);
                    break;
                case 1:
                    CapList[0].Insert((float)double.Parse(PriceTextBox.Text.Replace('.', ',')), MainWindow.db);
                    break;
                case 2:
                    DioList[0].Insert((float)double.Parse(PriceTextBox.Text.Replace('.', ',')), MainWindow.db);
                    break;
                case 3:
                    FETList[0].Insert((float)double.Parse(PriceTextBox.Text.Replace('.', ',')), MainWindow.db);
                    break;
                case 4:
                    BTList[0].Insert((float)double.Parse(PriceTextBox.Text.Replace('.', ',')), MainWindow.db);
                    break;
            }
            this.Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = TypeComboBox.SelectedIndex;
            switch (i)
            {
                case 0:
                    ResList = new List<Resistor>();
                    ResList.Add(new Resistor());
                    ElementDataGrid.ItemsSource = ResList;
                    break;
                case 1:
                    CapList = new List<Capacitor>();
                    CapList.Add(new Capacitor());
                    ElementDataGrid.ItemsSource = CapList;
                    break;
                case 2:
                    DioList = new List<Diode>();
                    DioList.Add(new Diode());
                    ElementDataGrid.ItemsSource = DioList;
                    break;
                case 3:
                    FETList = new List<FieldEffectTransistor>();
                    FETList.Add(new FieldEffectTransistor());
                    ElementDataGrid.ItemsSource = FETList;
                    break;
                case 4:
                    BTList = new List<BipolarTransistor>();
                    BTList.Add(new BipolarTransistor());
                    ElementDataGrid.ItemsSource = BTList;
                    break;
            }
            
        }

        private void ElementDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ResId" || e.Column.Header.ToString() == "CapId" ||
                e.Column.Header.ToString() == "DioId" || e.Column.Header.ToString() == "FETId" || e.Column.Header.ToString() == "BTId")
            {
                 e.Cancel = true;   // For not to include 
                //e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
    }
}
