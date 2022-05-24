using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace AuctionManagementSystem
{
    public partial class AdminPanel : Form
    {

        CrystalReport1 CR;
        CrystalReport2 CR2;
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = CR;
        }

      
        private void AdminPanel_Load(object sender, EventArgs e)
        {
            
           
                CR = new CrystalReport1();
                CR2 = new CrystalReport2();

            foreach (ParameterDiscreteValue v in CR2.ParameterFields[0].DefaultValues)
                comboBox1.Items.Add(v.Value);
           
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            CR2.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer1.ReportSource = CR2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SingupLoginHolder s = new SingupLoginHolder();
            s.ShowDialog();
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BuyerDashboard bd = new BuyerDashboard();
            bd.ShowDialog();
            this.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellerDashboard bd = new SellerDashboard();
            bd.ShowDialog();
            this.Close();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminPanel bd = new AdminPanel();
            bd.ShowDialog();
            this.Close();
        }
    }
}
