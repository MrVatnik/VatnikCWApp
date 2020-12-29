using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Excel = Microsoft.Office.Interop.Excel;


namespace VatnikCWApp
{
    /// <summary>
    /// Логика взаимодействия для ExcelWindow.xaml
    /// </summary>
    public partial class ExcelWindow : Window
    {
        public Excel.Application APP;
        public Excel.Workbook WB;
        public Excel.Worksheet WS;
        public string ExLocation = "";
        DataGrid dataGridMain;
        int GridType;
        public ExcelWindow(int type, DataGrid dataGrid)
        {
            InitializeComponent();
            dataGridMain = dataGrid;
            GridType = type;
            ExTextBox.Text = DateTime.UtcNow.Ticks.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            APP = new Excel.Application();
            //WB = APP.Workbooks.Open(ExLocation);
            WB = APP.Workbooks.Add();
            WS = (Excel.Worksheet)WB.Worksheets[1];

            ExLocation = ExTextBox.Text + ".xls";

            int i = 1;

            foreach (object ob in dataGridMain.Columns.Select(cs => cs.Header).ToList())
            {
                this.WS.Cells[1, i] = ob.ToString();
                i++;
            }

            i = 2;
            int j = 1;

            switch (GridType)
            {
                case 0:
                    foreach (Element field in dataGridMain.Items)
                    {
                        foreach (string s in field.ToStringList())
                        {
                            WS.Cells[j][i] = s;
                            j++;
                        }
                        j = 1;
                        i++;
                    }
                    break;
                case 1:
                    foreach (Resistor field in dataGridMain.Items)
                    {
                        foreach (string s in field.ToStringList())
                        {
                            WS.Cells[j][i] = s;
                            j++;
                        }
                        j = 1;
                        i++;
                    }
                    break;
                case 2:
                    foreach (Capacitor field in dataGridMain.Items)
                    {
                        foreach (string s in field.ToStringList())
                        {
                            WS.Cells[j][i] = s;
                            j++;
                        }
                        j = 1;
                        i++;
                    }
                    break;
                case 3:
                    foreach (Diode field in dataGridMain.Items)
                    {
                        foreach (string s in field.ToStringList())
                        {
                            WS.Cells[j][i] = s;
                            j++;
                        }
                        j = 1;
                        i++;
                    }
                    break;
                case 4:
                    foreach (FieldEffectTransistor field in dataGridMain.Items)
                    {
                        foreach (string s in field.ToStringList())
                        {
                            WS.Cells[j][i] = s;
                            j++;
                        }
                        j = 1;
                        i++;
                    }
                    break;
                case 5:
                    foreach (BipolarTransistor field in dataGridMain.Items)
                    {
                        foreach (string s in field.ToStringList())
                        {
                            WS.Cells[j][i] = s;
                            j++;
                        }
                        j = 1;
                        i++;
                    }
                    break;
            }
            

            object misValue = System.Reflection.Missing.Value;
            if (APP.ActiveWorkbook != null)
                APP.ActiveWorkbook.SaveAs(ExLocation, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            if (APP != null)
            {
                if (WB != null)
                {
                    if (this.WS != null)
                        Marshal.ReleaseComObject(this.WS);
                    WB.Close(false, Type.Missing, Type.Missing);
                    Marshal.ReleaseComObject(this.WB);
                }
                APP.Quit();
                Marshal.ReleaseComObject(this.APP);
            }


            this.Close();
        }
    }
}
