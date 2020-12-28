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
    /// Логика взаимодействия для EditElementWindow.xaml
    /// </summary>
    public partial class EditElementWindow : Window
    {
        int SelectedType;
        /*0-element
         * 1-resistor
         * 2-capacitor
         * 3-diode
         * 4-FET
         * 5-Bipolar T*/

        int Id;
        List<Element> ElList;
        List<Resistor> ResList;
        List<Capacitor> CapList;
        List<Diode> DioList;
        List<FieldEffectTransistor> FETList;
        List<BipolarTransistor> BTList;


        public EditElementWindow(int id, int type)
        {
            InitializeComponent();
            SelectedType = type;
            Id = id;

            switch (SelectedType)
            {
                case 0:/*element*/
                    ElList = new List<Element>();
                    ElList.Add(MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == Id));
                    EEDataGrid.ItemsSource = ElList;
                    break;
                case 1:/*resistor*/
                    ResList = new List<Resistor>();
                    ResList.Add(MainWindow.db.GetTable<Resistor>().ToList<Resistor>().Find(el => el.ResId == Id));
                    EEDataGrid.ItemsSource = ResList;
                    break;
                case 2:/*capacitor*/
                    CapList = new List<Capacitor>();
                    CapList.Add(MainWindow.db.GetTable<Capacitor>().ToList<Capacitor>().Find(el => el.CapId == Id));
                    EEDataGrid.ItemsSource = CapList;
                    break;
                case 3:/*diode*/
                    DioList = new List<Diode>();
                    DioList.Add(MainWindow.db.GetTable<Diode>().ToList<Diode>().Find(el => el.DioId == Id));
                    EEDataGrid.ItemsSource = DioList;
                    break;
                case 4:/*FET*/
                    FETList = new List<FieldEffectTransistor>();
                    FETList.Add(MainWindow.db.GetTable<FieldEffectTransistor>().ToList<FieldEffectTransistor>().Find(el => el.FETId == Id));
                    EEDataGrid.ItemsSource = FETList;
                    break;
                case 5:/*Bipolar T*/
                    BTList = new List<BipolarTransistor>();
                    BTList.Add(MainWindow.db.GetTable<BipolarTransistor>().ToList<BipolarTransistor>().Find(el => el.BTId == Id));
                    EEDataGrid.ItemsSource = BTList;
                    break;
            }
            

        }

        


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            switch (SelectedType)
            {
                case 0:/*element*/
                    {
                        Element previous = MainWindow.db.GetTable<Element>().ToList<Element>().Find(el => el.Id == Id);
                        Element current = ElList[0];
                        if(current.Type==previous.Type)
                            MainWindow.db.Update(current);
                        else
                        {
                            switch (previous.Type)
                            {
                                case ElTypes.Resistor:
                                    MainWindow.db.Delete(
                                        MainWindow.db.GetTable<Resistor>().ToList<Resistor>().Find(El => El.ResId == previous.Id)
                                        );
                                    break;

                                case ElTypes.Capacitor:
                                    MainWindow.db.Delete(
                                        MainWindow.db.GetTable<Capacitor>().ToList<Capacitor>().Find(El => El.CapId == previous.Id)
                                        );
                                    break;

                                case ElTypes.Diode:
                                    MainWindow.db.Delete(
                                        MainWindow.db.GetTable<Diode>().ToList<Diode>().Find(El => El.DioId == previous.Id)
                                        );
                                    break;

                                case ElTypes.Field_Effect_Transistor:
                                    MainWindow.db.Delete(
                                        MainWindow.db.GetTable<FieldEffectTransistor>().ToList<FieldEffectTransistor>().
                                        Find(El => El.FETId == previous.Id)
                                        );
                                    break;

                                case ElTypes.Bipolar_Transistor:
                                    MainWindow.db.Delete(
                                        MainWindow.db.GetTable<BipolarTransistor>().ToList<BipolarTransistor>().Find(El => El.BTId == previous.Id)
                                        );
                                    break;

                            }

                            ElementTypeChangeWindow etcw = new ElementTypeChangeWindow(Id, (int)current.Type, current.Name);
                            etcw.Show();
                        }


                        break;
                    }



                case 1:/*resistor*/
                    MainWindow.db.Update(ResList[0]);
                    break;
                case 2:/*capacitor*/
                    MainWindow.db.Update(CapList[0]);
                    break;
                case 3:/*diode*/
                    MainWindow.db.Update(DioList[0]);
                    break;
                case 4:/*FET*/
                    MainWindow.db.Update(FETList[0]);
                    break;
                case 5:/*BipolarT*/
                    MainWindow.db.Update(BTList[0]);
                    break;
            }


            this.Close();
        }

        private void EEDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
