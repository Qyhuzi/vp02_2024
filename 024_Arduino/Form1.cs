﻿using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace D014_Arduino
{
    public partial class Form1 : Form
    {
        SerialPort sPort = null;
        private double xCount = 200; // 차트 하나에 표시되는 데이터 수

        // 시뮬레이션에 사용
        Timer t = new Timer();
        Random r = new Random();
        bool simulationFlag = false;

        public Form1()
        {
            InitializeComponent();

            foreach (var port in SerialPort.GetPortNames())
                comboBox1.Items.Add(port);
            comboBox1.Text = "Select Port";

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 1023;

            progressBar1.Value = 500;

            CharSetting();
            InitSetting();
        }

        private void CharSetting()
        {
            chart1.Titles.Add("조도");
            chart2.Titles.Add("온도 / 습도");

            // Chart1 x축
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = xCount;
            chart1.ChartAreas[0].AxisX.Interval = xCount / 4;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // Chart1 y축
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 800;
            chart1.ChartAreas[0].AxisY.Interval = 200;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Black;

            // Chart2 x축
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = xCount;
            chart2.ChartAreas[0].AxisX.Interval = xCount / 4;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // Chart2 y축
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Maximum = 100;
            chart2.ChartAreas[0].AxisY.Interval = 20;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart2.ChartAreas[0].BackColor = Color.Black;

            // Series 디자인
            chart1.Series.Clear();
            chart1.Series.Add("lumi");
            
            chart2.Series.Clear();
            chart2.Series.Add("temp");
            chart2.Series.Add("humi");

            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].Color = Color.LightGreen;
            chart1.Series[0].BorderWidth = 2;

            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].Color = Color.LightBlue;
            chart2.Series[0].BorderWidth = 2;

            chart2.Series[1].ChartType = SeriesChartType.Line;
            chart2.Series[1].Color = Color.Orange;
            chart2.Series[1].BorderWidth = 2;
        }

        private void InitSetting()
        {
            this.Text = "Arduino Sensor Monitoring System";
            btnPortValue.BackColor = Color.Blue;
            btnPortValue.ForeColor = Color.White;
            btnPortValue.Text = "온도\n습도\n조도";
            btnPortValue.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

            label1.Text = "Connection Time : ";
            txtCount.TextAlign = HorizontalAlignment.Center;
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = false;
        }

        private void 시작ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simulationFlag = true;
            t.Interval = 1000;
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            int lumi = r.Next(800);     // 조도
            int temp = r.Next(10, 35);  // 온도
            int humi = r.Next(30, 80);  // 습도

            string s = string.Format("{0}\t{1}\t{2}", temp, humi, lumi);
            ShowValue(s);
        }

        static int skip = 0;     // 처음 받은 값 3개를 버림
        static int counter = 0;  // 차트의 표시 영역을 변경
        private void ShowValue(string s)
        {
            listBox1.Items.Add(DateTime.Now.ToString() + "\t>>\t" + s);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            if (++skip <= 3) return;
            else skip = 4;

            counter++;

            txtCount.Text = "counter : " + counter;

            string[] sub = new string[3];
            sub = s.Split('\t');

            double temp = double.Parse(sub[0]);
            double humi = double.Parse(sub[1]);
            int lumi = int.Parse(sub[2]);

            if (simulationFlag == false)
            {
                btnPortValue.Text = sPort.PortName
                + "\n온도 : " + temp + "ºC"
                + "\n습도 : " + humi + "%"
                + "\n조도 : " + lumi;
            }
            
            else
            {
                btnPortValue.Text = "Simulation"
                + "\n온도 : " + temp + "ºC"
                + "\n습도 : " + humi + "%"
                + "\n조도 : " + lumi;
            }

            progressBar1.Value = lumi;
            chart1.Series[0].Points.Add(lumi);
            chart2.Series[0].Points.Add(temp);
            chart2.Series[1].Points.Add(humi);

            // 차트 스크롤
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = (counter >= xCount) ? counter : xCount;

            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = (counter >= xCount) ? counter : xCount;

            if (counter > xCount)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(counter - xCount, counter);
                chart2.ChartAreas[0].AxisX.ScaleView.Zoom(counter - xCount, counter);
            }
        }

        private void 끝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.Stop();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sPort != null) return;

            ComboBox cb = sender as ComboBox;
            sPort = new SerialPort(cb.SelectedItem.ToString());
            sPort.Open();
            sPort.DataReceived += SPort_DataReceived;

            label1.Text = "Connection Time : " + DateTime.Now.ToString();
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
        }

        private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s = sPort.ReadLine();
            this.BeginInvoke(new Action(() => { ShowValue(s); }));
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            sPort.Close();

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            sPort.Open();
            skip = 0;

            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
        }
    }
}