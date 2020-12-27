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
using System.Windows.Shapes;

namespace VatnikCWApp
{
    /// <summary>
    /// Логика взаимодействия для ElementTypeChangeWindow.xaml
    /// </summary>
    public partial class ElementTypeChangeWindow : Window
    {
        ElTypes Type;
        string Name;
        int Id;
        List<Resistor> ResList;
        List<Capacitor> CapList;
        List<Diode> DioList;
        List<FieldEffectTransistor> FETList;
        List<BipolarTransistor> BTList;

        public ElementTypeChangeWindow(int id, int type, string name)
        {
            InitializeComponent();
            Type = (ElTypes)type;
            Name = name;

            switch (Type)
            {
                case ElTypes.Resistor:
                    ResList = new List<Resistor>();
                    ResList.Add(new Resistor());
                    ResList[0].ResId=Id;
                    ResList[0].Name = Name;
                    ElementDataGrid.ItemsSource = ResList;
                    break;

                case ElTypes.Capacitor:
                    CapList = new List<Capacitor>();
                    CapList.Add(new Capacitor());
                    CapList[0].CapId = Id;
                    CapList[0].Name = Name;
                    ElementDataGrid.ItemsSource = CapList;
                    break;

                case ElTypes.Diode:
                    DioList = new List<Diode>();
                    DioList.Add(new Diode());
                    DioList[0].DioId = Id;
                    DioList[0].Name = Name;
                    ElementDataGrid.ItemsSource = DioList;
                    break;

                case ElTypes.Field_Effect_Transistor:
                    FETList = new List<FieldEffectTransistor>();
                    FETList.Add(new FieldEffectTransistor());
                    FETList[0].FETId = Id;
                    FETList[0].Name = Name;
                    ElementDataGrid.ItemsSource = FETList;
                    break;

                case ElTypes.Bipolar_Transistor:
                    BTList = new List<BipolarTransistor>();
                    BTList.Add(new BipolarTransistor());
                    BTList[0].BTId = Id;
                    BTList[0].Name = Name;
                    ElementDataGrid.ItemsSource = BTList;
                    break;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Type)
            {
                case ElTypes.Resistor:
                    MainWindow.db.Insert(ResList[0]);
                    break;

                case ElTypes.Capacitor:
                    MainWindow.db.Insert(CapList[0]);
                    break;

                case ElTypes.Diode:
                    MainWindow.db.Insert(DioList[0]);
                    break;

                case ElTypes.Field_Effect_Transistor:
                    MainWindow.db.Insert(FETList[0]);
                    break;

                case ElTypes.Bipolar_Transistor:
                    MainWindow.db.Insert(BTList[0]);
                    break;
            }

            this.Close();
        }
    }
}
