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
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        int Id;
        ElTypes Type;
        public DeleteWindow(int id)
        {
            InitializeComponent();

            this.Id = id;
            Type = (MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == Id)).Type;
            switch (Type)
            {
                case ElTypes.Resistor:
                    List<Resistor> Ress = new List<Resistor>();
                    Ress.Add(MainWindow.db.GetTable<Resistor>().ToList<Resistor>().Find(el => el.ResId == Id));
                    DeletedElementDataGrid.ItemsSource=Ress;
                    break;
                case ElTypes.Capacitor:
                    List<Capacitor> Caps = new List<Capacitor>();
                    Caps.Add(MainWindow.db.GetTable<Capacitor>().ToList<Capacitor>().Find(el => el.CapId == Id));
                    DeletedElementDataGrid.ItemsSource = Caps;
                    break;
                case ElTypes.Diode:
                    List<Diode> Dios = new List<Diode>();
                    Dios.Add(MainWindow.db.GetTable<Diode>().ToList<Diode>().Find(el => el.DioId == Id));
                    DeletedElementDataGrid.ItemsSource = Dios;
                    break;
                case ElTypes.Field_Effect_Transistor:
                    List<FieldEffectTransistor> FETs = new List<FieldEffectTransistor>();
                    FETs.Add(MainWindow.db.GetTable<FieldEffectTransistor>().ToList<FieldEffectTransistor>().Find(el => el.FETId == Id));
                    DeletedElementDataGrid.ItemsSource = FETs;
                    break;
                case ElTypes.Bipolar_Transistor:
                    List<BipolarTransistor> BTs = new List<BipolarTransistor>();
                    BTs.Add(MainWindow.db.GetTable<BipolarTransistor>().ToList<BipolarTransistor>().Find(el => el.BTId == Id));
                    DeletedElementDataGrid.ItemsSource = BTs;
                    break;
            }

        }

        private void DeletedElementDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.IsReadOnly = true; // Makes the column as read only
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Type)
            {
                case ElTypes.Resistor:
                    MainWindow.db.Delete(MainWindow.db.GetTable<Resistor>().ToList<Resistor>().Find(el=>el.ResId == this.Id));
                    break;
                case ElTypes.Capacitor:
                    MainWindow.db.Delete(MainWindow.db.GetTable<Capacitor>().ToList<Capacitor>().Find(el => el.CapId == this.Id));
                    break;
                case ElTypes.Diode:
                    MainWindow.db.Delete(MainWindow.db.GetTable<Diode>().ToList<Diode>().Find(el => el.DioId == this.Id));
                    break;
                case ElTypes.Field_Effect_Transistor:
                    MainWindow.db.Delete(MainWindow.db.GetTable<FieldEffectTransistor>().ToList<FieldEffectTransistor>().
                        Find(el => el.FETId == this.Id));
                    break;
                case ElTypes.Bipolar_Transistor:
                    MainWindow.db.Delete(MainWindow.db.GetTable<BipolarTransistor>().ToList<BipolarTransistor>().
                        Find(el => el.BTId == this.Id));
                    break;
            }
            MainWindow.db.Delete(MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == this.Id));

            
            this.Close();
        }
    }
}
