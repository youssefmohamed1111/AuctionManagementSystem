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
    public partial class Login : Form
    {
         string orcl = "Data Source=orcl; User Id=scott; Password=tiger;";
        OracleConnection conn;
        public SingupLoginHolder parentForm;

        private string loginCheck(string email, string password) {

            OracleCommand command = new OracleCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PasswordChecker";
            
            command.Parameters.Add("UserEmail", OracleDbType.Varchar2, 200,email, ParameterDirection.Input);

            command.Parameters.Add("uPassword", OracleDbType.Varchar2, 255).Direction = ParameterDirection.Output;
            command.Parameters.Add("uAccountType", OracleDbType.Varchar2, 255).Direction = ParameterDirection.Output;

            try
            {
                command.ExecuteNonQuery();
                string pass = command.Parameters["uPassword"].Value.ToString();

                if (pass.Trim() != password)
                {
                    MessageBox.Show("Incorrect Password!");
                    return null;
                }
                else
                {
                    return command.Parameters["uAccountType"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Email Doesn't exist!");
            }

            
            return null;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Please Enter your email and password");
            }
            else
            {
                
                string type = loginCheck(textBox3.Text, textBox4.Text);
                


                if(type != null)
                {
                    OracleCommand cd = new OracleCommand();
                    cd.Connection = conn;
                    cd.CommandText = "Select FIRST_NAME from users where email=:email";
                    cd.Parameters.Add("email", textBox3.Text);
                    OracleDataReader dr = cd.ExecuteReader();
                    string localfirst = "";
                    if (dr.Read())
                    {
                        localfirst = dr[0].ToString();
                    }
                    type = type.Trim();
                    if (type == "Seller")
                    {
                        
                        parentForm.Hide();
                        SellerDashboard sd = new SellerDashboard();
                        sd.email = textBox3.Text;
                        sd.FirstName = localfirst;
                        sd.ShowDialog();
                        
                        parentForm.Close();
                    }

                    else if (type == "Buyer")
                    {
                        parentForm.Hide();
                        BuyerDashboard bd = new BuyerDashboard();
                        bd.email = textBox3.Text;
                        bd.FirstName = localfirst;
                        bd.ShowDialog();
                        parentForm.Close();
                    }

                    else
                    {
                        parentForm.Hide();
                        AdminPanel admin = new AdminPanel();
                        admin.ShowDialog();
                        parentForm.Close();
                    }
                }


            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Enter your email address:")
                textBox3.Text = "";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
                textBox3.Text = "Enter your email address:";
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Enter your password:")
                textBox4.Text = "";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
                textBox4.Text = "Enter your password:";
        }

        private void textbox_press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();

            }

        }
    }
}
