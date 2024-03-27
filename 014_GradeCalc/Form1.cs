using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _014_GradeCalc
{
    public partial class Form1 : Form
    {
        TextBox[] titles;
        ComboBox[] crds;
        ComboBox[] grds;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            double totalS = 0;  // 총점
            int totalC = 0;     // 학점수

            for (int i = 0; i < crds.Length; i++)
            {
                if (titles[i].Text != "")
                {
                    int crd = int.Parse(crds[i].SelectedItem.ToString());
                    totalC += crd;
                    totalS += crd * GetGrade(grds[i].SelectedItem.ToString());
                }
            }
            txtGrade.Text = (totalS / totalC).ToString("0.00");
        }

        private double GetGrade(string v)
        {
            double grade = 0;

            if (v == "A+") grade = 4.5;
            else if (v == "A0") grade = 4.0;
            else if (v == "B+") grade = 3.5;
            else if (v == "B0") grade = 3.0;
            else if (v == "C+") grade = 2.5;
            else if (v == "C0") grade = 2.0;
            else if (v == "D+") grade = 1.5;
            else if (v == "D0") grade = 1.0;
            else grade = 00;

            return grade;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt1.Text = "인체의구조와기능I";
            txt2.Text = "일반수학I";
            txt3.Text = "전기전자회로공학및실험";
            txt4.Text = "데이터사이언스";
            txt5.Text = "비주얼프로그래밍";
            txt6.Text = "설계및프로젝트기본I";
            txt7.Text = "영어I";
            txt8.Text = "디지털기술입문";

            // 학점 콤보박스의 배열
            crds = new ComboBox[] { crd1, crd2, crd3, crd4, crd5, crd6, crd7, crd8 };
            // 성적 콤보박스의 배열
            grds = new ComboBox[] { grd1, grd2, grd3, grd4, grd5, grd6, grd7, grd8 };
            // 교과목 텍스트박스의 배열
            titles = new TextBox[] { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8 };
            int[] arrCredit = { 1, 2, 3, 4, 5 };
            List<String> lstGrade = new List<String> { "A+", "A0", "B+", "B0", "C+", "C0", "D+", "D0", "F" };

            foreach (var combo in crds)
            {
                foreach (var i in arrCredit)
                    combo.Items.Add(i);
                combo.SelectedItem = 3;
            }

            foreach (var cb in grds)
            {
                foreach (var gr in lstGrade)
                    cb.Items.Add(gr);
                cb.SelectedItem = "B+";
            }
        }
    }
}
