using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D22_DClock
{
    public partial class Form1 : Form
    {
        // 필드 (멤버변수)
        private Graphics g;
        private bool aClockFlag = true; // 아날로그 시계
        private Point center;
        private double radius;
        private int hourHand; // 시침의 길이
        private int minHand;  // 분침의 길이
        private int secHand;  // 초짐의 길이

        private const int clinetSize = 300;  // client 크기
        private const int clockSize = 200;   // clock 크기

        private Font fDate;
        private Font fTime;
        private Brush bDate;
        private Brush bTime;

        private Point panelCenter;

        public Form1()
        {
            InitializeComponent();

            this.ClientSize = new Size(clinetSize, clinetSize + menuStrip1.Height);
            this.Text = "My Form Clock";

            // panel 지정
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Width = clockSize + 1;
            panel1.Height = clockSize + 1;
            panel1.Location = new Point(clinetSize / 2 - clockSize / 2,
                clinetSize / 2 - clockSize / 2 + menuStrip1.Height);

            g = panel1.CreateGraphics();  // panel1에서 그려주는 그래픽 객체

            aClockSetting();
            dClockSetting();
            timerSetting();
        }

        private void dClockSetting()
        {
            fDate = new Font("맑은 고딕", 12, FontStyle.Bold);
            fTime = new Font("맑은 고딕", 32, FontStyle.Bold|FontStyle.Italic);
            bDate = Brushes.SkyBlue;
            bTime = Brushes.SteelBlue;
        }

        private void timerSetting()
        {
            Timer t = new Timer();

            t.Enabled = true;
            t.Interval = 1000;
            t.Tick += T_Tick;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            DateTime c = DateTime.Now; // 현재 시간
            panel1.Refresh();

            if(aClockFlag == true)     // 아날로그
            {
                DrawClockFace();

                // 시계바늘 각도
                double radHr = (c.Hour % 12 + c.Minute / 60.0) * 30 * Math.PI / 180;
                double radMin = (c.Minute + c.Second / 60.0) * 6 * Math.PI / 180;
                double radSec = c.Second * 6 * Math.PI / 180;

                DrawHands(radHr, radMin, radSec);
            }
            else                       // 디지털
            {
                string date = DateTime.Today.ToString("D");
                string time = string.Format("{0:D2}:{1:D2}:{2:D2}", c.Hour, c.Minute, c.Second);

                g.DrawString(date, fDate, bDate, new Point(5, 70));
                g.DrawString(time, fTime, bTime, new Point(5, 100));
            }
        }

        private void DrawHands(double radHr, double radMin, double radSec)
        {
            
        }

        private void DrawClockFace()
        {
            const int penWidth = 30;

            Pen p = new Pen(Brushes.LightSteelBlue, penWidth);
            g.DrawEllipse(p, penWidth / 2 , penWidth / 2, clockSize - penWidth, clockSize - penWidth);
        }

        private void aClockSetting()
        {
            center = new Point(clinetSize / 2, clinetSize / 2 );
            radius = clockSize / 2;
            hourHand = (int)(radius * 0.45);
            minHand = (int)(radius * 0.55);
            secHand = (int)(radius * 0.65);
        }
    }
}
