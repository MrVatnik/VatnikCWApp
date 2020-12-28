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
            Id = id;
            switch (Type)
            {
                case ElTypes.Resistor:
                    ResList = new List<Resistor>();
                    ITable<Resistor> Ress = MainWindow.db.GetTable<Resistor>();
                    Ress.Value(el => el.ResId, MainWindow.db.GetTable<Element>().ToList<Element>().Find(el=>el.Id==Id).Id).
                        Value(el=>el.Name,this.Name).Value(el => el.Resistance, 0).Value(el => el.NominalPower, 0).
                        Value(el => el.Type,ResTypes.Low_Power_Resistor).Insert();
                    ResList.Add(Ress.ToList<Resistor>().Last());
                    ElementDataGrid.ItemsSource = ResList;
                    break;

                case ElTypes.Capacitor:
                    CapList = new List<Capacitor>();
                    ITable<Capacitor> Caps = MainWindow.db.GetTable<Capacitor>();
                    Caps.Value(el => el.CapId, MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == Id).Id).
                        Value(el => el.Name, this.Name).Value(el => el.Capacity, 0).Value(el => el.Type, CapTypes.Ceramic).Insert();
                    CapList.Add(Caps.ToList<Capacitor>().Last());
                    ElementDataGrid.ItemsSource = CapList;
                    break;

                case ElTypes.Diode:
                    DioList = new List<Diode>();
                    ITable<Diode> Dios = MainWindow.db.GetTable<Diode>();
                    Dios.Value(el => el.DioId, MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == Id).Id).
                        Value(el => el.Name, this.Name).Value(el => el.MaxReVoltage, 0).Value(el => el.MaxForVoltage, 0).
                        Value(el => el.MaxReCurrent, 0).Value(el => el.MaxForCurrent, 0).Insert();
                    DioList.Add(Dios.ToList<Diode>().Last());

                    ElementDataGrid.ItemsSource = DioList;
                    break;

                case ElTypes.Field_Effect_Transistor:
                    FETList = new List<FieldEffectTransistor>();
                    ITable<FieldEffectTransistor> FETs = MainWindow.db.GetTable<FieldEffectTransistor>();
                    FETs.Value(el => el.FETId, MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == Id).Id).
                        Value(el => el.Name, this.Name).Value(el => el.MaxDSVoltage, 0).Value(el => el.MaxDSCurrent, 0).
                        Value(el => el.OpenChanelResistance, 0).Value(el => el.ReMVoltage, 0).Value(el => el.ReMCurrent, 0).Insert();
                    FETList.Add(FETs.ToList<FieldEffectTransistor>().Last());
                    ElementDataGrid.ItemsSource = FETList;
                    break;

                case ElTypes.Bipolar_Transistor:
                    BTList = new List<BipolarTransistor>();
                    ITable<BipolarTransistor> BTs = MainWindow.db.GetTable<BipolarTransistor>();
                    BTs.Value(el => el.BTId, MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == Id).Id).
                        Value(el => el.Name, this.Name).Value(el => el.MaxCEVoltage, 0).Value(el => el.MaxCCurrent, 0).
                        Value(el => el.CutoffFrequency, 0).Value(el => el.MaxPowerLoss, 0).Insert();
                    BTList.Add(BTs.ToList<BipolarTransistor>().Last());
                    ElementDataGrid.ItemsSource = BTList;
                    break;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Type)
            {
                case ElTypes.Resistor:
                    MainWindow.db.Update(ResList[0]);
                    break;

                case ElTypes.Capacitor:
                    MainWindow.db.Update(CapList[0]);
                    break;

                case ElTypes.Diode:
                    MainWindow.db.Update(DioList[0]);
                    break;

                case ElTypes.Field_Effect_Transistor:
                    MainWindow.db.Update(FETList[0]);
                    break;

                case ElTypes.Bipolar_Transistor:
                    MainWindow.db.Update(BTList[0]);
                    break;
            }

            this.Close();
        }

        private void ElementDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ResId" || e.Column.Header.ToString() == "CapId" ||
               e.Column.Header.ToString() == "DioId" || e.Column.Header.ToString() == "FETId" || e.Column.Header.ToString() == "BTId")
            {
                //e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }

            if (e.Column.Header.ToString() == "Name")
            {
                //e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only                
            }
        }
    }
}
