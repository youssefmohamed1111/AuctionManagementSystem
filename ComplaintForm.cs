using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace AuctionManagementSystem
{
    public partial class ComplaintForm : Form
    {
        public string BuyerEmail;
        public string FirstName;
        public int ItemID; 
        public string ItemName;
        public BuyerDashboard ParentForm;
        public Form opacityForm;
        OracleConnection con;
        public ComplaintForm()
        {
            InitializeComponent();
        }

        private int id_no()
        {

            int max = 0;
            OracleCommand command = new OracleCommand();
            command.Connection = con;
            command.CommandText = "select max(complaint_id) from complaint";

            OracleDataReader reader = command.ExecuteReader();
            try
            {

                if (reader.Read())
                {
                    max = Convert.ToInt32(reader[0]) + 1;
                }


            }
            catch (Exception ex)
            {
                max = 1;
            }
            return max;
        }

        private void ComplaintForm_Load(object sender, EventArgs e)
        {
            string orcl = "Data Source=orcl; User Id=scott;Password=tiger;";
            con = new OracleConnection(orcl);
            con.Open();
            label1.Text = "For Item: " + ItemName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int maxid = id_no();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into COMPLAINT values (:cid, :description, :buyerID, :itemID)";
            cmd.Parameters.Add(":cid", maxid);
            cmd.Parameters.Add("description", textBox3.Text);
            cmd.Parameters.Add("buyerID", this.BuyerEmail);
            cmd.Parameters.Add("itemID", this.ItemID);
            int r = cmd.ExecuteNonQuery();

            MessageBox.Show("Thanks for your complaint, it has been sent for review.");

            this.Close();
            this.Dispose();
        }

        private void ComplaintForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
            con.Dispose();
            opacityForm.Close();
            opacityForm.Dispose();
            this.ParentForm.Close();
            this.ParentForm.Dispose();
            BuyerDashboard bd = new BuyerDashboard();
            bd.FirstName = "";
            bd.email = this.BuyerEmail;
            bd.ShowDialog();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Enter your complaint: ")
            {
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Enter your complaint: ";
            } 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                button2.Enabled = false;
            } else
            {
                button2.Enabled=true;
            }
        }
    }
}
