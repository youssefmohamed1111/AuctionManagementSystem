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
    public partial class Profile : Form
    {
        string orcl = "Data Source=orcl; User Id=scott; Password=tiger;";
        public string email = "";
        public string oldPass = "";
        public SellerDashboard sd = null;
        public BuyerDashboard bd = null;
        public Label mainAppLabel = null; 

        OracleConnection conn;
        public Profile()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Profile_Load(object sender, EventArgs e)
        {
            foreach (var ctr in this.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox tx = (TextBox)ctr;
                    tx.KeyPress += new KeyPressEventHandler(this.textbox_press);
                }
            }
            conn = new OracleConnection(orcl);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select FIRST_NAME, LAST_NAME, DATE_OF_BIRTH, PASSWORD from users where email = :email";
            cmd.Parameters.Add("email", email);
            
            OracleDataReader dr = cmd.ExecuteReader();

            oldPass = "";

            if(dr.Read())
            {
                textBox1.Text = dr[0].ToString();  
                textBox2.Text = dr[1].ToString();
                textBox3.Text = email;
                dateTimePicker1.Value = Convert.ToDateTime(dr[2]);
                textBox6.Text = dr[3].ToString();
                oldPass = dr[3].ToString();
            }

        }

        private void textbox_press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool changePassword = false;
            bool canChangePass = true;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;


            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                MessageBox.Show("Please make sure that email, first name and last name are not empty.");

            if (textBox4.Text != "")
                changePassword = true;

            if (changePassword)
            {
                if (textBox4.Text != textBox5.Text)
                {
                    MessageBox.Show("Please make sure to enter indentical passwords");
                    canChangePass = false;
                    return;
                }

                if (textBox6.Text != oldPass)
                {
                    MessageBox.Show("Your old passwords are not identical");
                    canChangePass = false;
                    return;
                }
            }

            if (changePassword && canChangePass)
            {
                cmd.CommandText = "Update Users set FIRST_NAME = :FIRST, LAST_NAME = :LAST, EMAIL=:EMAIl, PASSWORD = :PASSWORD , DATE_OF_BIRTH=:DOB where Email=:EMAIL";
                cmd.Parameters.Add("FIRST", textBox1.Text);
                cmd.Parameters.Add(":LAST", textBox2.Text);
                cmd.Parameters.Add(":EMAIL", textBox3.Text);
                cmd.Parameters.Add(":PASSWORD", textBox4.Text);
                cmd.Parameters.Add("DOB", Convert.ToDateTime(dateTimePicker1.Value));
                

                cmd.ExecuteNonQuery();

                if (sd != null)
                {
                    sd.FirstName = textBox1.Text;
                    sd.email = textBox3.Text;
                    mainAppLabel.Text = textBox1.Text;
                }
                else
                {
                    bd.FirstName = textBox1.Text;
                    bd.email = textBox3.Text;
                    mainAppLabel.Text = textBox1.Text;
                } 

            } else
            {
                cmd.CommandText = "Update Users set FIRST_NAME = :FIRST, LAST_NAME = :LAST, EMAIL=:EMAIL, DATE_OF_BIRTH=:DOB where EMAIL=:EMAIL";
                cmd.Parameters.Add("FIRST", textBox1.Text);
                cmd.Parameters.Add("LAST", textBox2.Text);
                cmd.Parameters.Add("EMAIL", textBox3.Text);
                cmd.Parameters.Add("DOB", Convert.ToDateTime(dateTimePicker1.Value));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Details Updated Successfully!");

                if (sd != null)
                {
                    sd.FirstName = textBox1.Text;
                    sd.email = textBox3.Text;
                    mainAppLabel.Text = textBox1.Text;

                } else
                {
                    bd.FirstName = textBox1.Text;
                    bd.email = textBox3.Text;
                    mainAppLabel.Text = textBox1.Text;
                }
                

            }
                
        }
    }
}
