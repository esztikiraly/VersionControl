using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using webszolg.MnbServiceReference;

namespace webszolg
{
    public partial class Form1 : Form
    {
        BindingList<Entities.RateData> Rates = new BindingList<Entities.RateData>();
        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "EUR";
            RefreshData();
        }

        private void RefreshData()
        {
            comboBox1.DataSource = Currencies;
            Rates.Clear();
            GetExchangefv();
            dataGridView1.DataSource = Rates;
            chartRateData.DataSource = Rates;
            
            
        }

        private void GetExchangefv()
        {

            
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            var response = mnbService.GetExchangeRates(request);
           var result = response.GetExchangeRatesResult;

            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new Entities.RateData();
                Rates.Add(rate);


                rate.Date = DateTime.Parse(element.GetAttribute("date"));


                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");


                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;

                var series = chartRateData.Series[0];
                series.ChartType = SeriesChartType.Line;
                series.XValueMember = "Date";
                series.YValueMembers = "Value";
                series.BorderWidth = 2;

                var legend = chartRateData.Legends[0];
                legend.Enabled = false;

                var chartArea = chartRateData.ChartAreas[0];
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisY.MajorGrid.Enabled = false;
                chartArea.AxisY.IsStartedFromZero = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
        


        }
    }
       

