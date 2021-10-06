using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace ExcelExport
{
    public partial class Form1 : Form
    {
        List<Flat> Flats;
        RealEstateEntities context = new RealEstateEntities();
        

        Excel.Application xlApp; 
        Excel.Workbook xlWB; 
        Excel.Worksheet xlSheet; 

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application(); //Excel indítása
                xlWB = xlApp.Workbooks.Add(Missing.Value); // Új munkafüzet
                xlSheet = xlWB.ActiveSheet; // Új munkalap
                CreateTable(); //Tábla létrehozása
                xlApp.Visible = true;
                xlApp.UserControl = true;


            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1} ", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                //Hiba esetén alkalmazás bezárása
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;

            }
        
        }
        private void CreateTable()
        {
            //tömb, ami tartalmazza a fejléceket
            string[] headers = new string[]
            {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"
            };
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i+1] = headers[i];
                
            }
            object[,] values = new object[Flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Code;
                values[counter, 2] = f.Code;
                values[counter, 3] = f.Code;
                values[counter, 4] = "";
                values[counter, 5] = f.Code;
                values[counter, 6] = f.Code;
                values[counter, 7] = f.Code;
                values[counter, 8] = f.Code;
            }

            xlSheet.get_Range(
                GetCell(2, 1),
                GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            // Formatting table
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length)); 
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            int lastRowID = xlSheet.UsedRange.Rows.Count;

            Excel.Range tablerange = xlSheet.get_Range(GetCell(1, 1), GetCell(lastRowID, headers.Length));
            tablerange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            Excel.Range firstcolumn = xlSheet.get_Range(GetCell(1, 1), GetCell(lastRowID, 1));
            firstcolumn.Font.Bold = true;
            firstcolumn.Interior.Color = Color.LightYellow;
            Excel.Range lastcolumn = xlSheet.get_Range(GetCell(1, headers.Length), GetCell(lastRowID, headers.Length));
            lastcolumn.Interior.Color = Color.LightGreen;
        }
       

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend>0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }



        private void LoadData() 
        {
            Flats = context.Flats.ToList();
        }

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }
    }
}
