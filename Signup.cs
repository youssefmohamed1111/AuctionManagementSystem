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
    public partial class Signup : Form
    {
        string orcl = "Data Source=orcl; User Id=scott; Password=tiger;";
        OracleConnection conn;

        
        public SingupLoginHolder parentForm;
        public Signup()
        {
            InitializeComponent();
        }

    
        private int insertUser()
        {

            // Creating Command Object
            OracleCommand command = new OracleCommand();
            command.Connection = conn;
            command.CommandText = "insert into users (Email, Password, First_Name, Last_name, Account_Type, Date_of_birth) values (:email, :password, :first, :last, :type, :dob)";
            
            // Parameters
            command.Parameters.Add("email", textBox3.Text);
            command.Parameters.Add("password", textBox4.Text);
            command.Parameters.Add("first", textBox1.Text);
            command.Parameters.Add("last", textBox2.Text);
            command.Parameters.Add("type", comboBox1.SelectedItem.ToString());
            command.Parameters.Add("dob", dateTimePicker1.Value.Date);

            // Execution of command & returning the result
            int r = command.ExecuteNonQuery();
            return r;
        }

        public void RemoveText(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        public void AddText(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
                textBox1.Text = "Enter text here...";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool canSignUp = true;

            if (textBox5.Text != textBox4.Text)
            {
                MessageBox.Show("Please Enter Matching Password");
                canSignUp = false;
            } else if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedIndex.ToString() == "") {
                MessageBox.Show("Please make sure to enter all details correctly");
                canSignUp = false;
            } else
            {
                canSignUp = true;
            }

            if (canSignUp)
            {
                try
                {
                    int r = insertUser();

                    if (comboBox1.SelectedItem.ToString() == "Buyer")
                    {
                        parentForm.Hide();
                        BuyerDashboard bd = new BuyerDashboard();
                        bd.email = textBox3.Text;
                        bd.FirstName = textBox1.Text;
                        bd.ShowDialog();
                        parentForm.Close();
                    }
                    else
                    {
                        parentForm.Hide();
                        SellerDashboard sd = new SellerDashboard();
                        sd.email = textBox3.Text;
                        sd.FirstName = textBox1.Text;
                        sd.ShowDialog();
                        parentForm.Close();
                    }
                } catch (Exception ex) {
                    MessageBox.Show("Error, this email is already registered");
                }
            }

        }

        private void Signup_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            //Handle Enter Key.

            foreach (var ctr in this.Controls)
            {
                if (ctr is TextBox )
                {
                    TextBox tx = (TextBox)ctr;
                    tx.KeyPress += new KeyPressEventHandler(this.textbox_press);
                }
            }
            // Done
            conn = new OracleConnection(orcl);
            conn.Open();
        }

        private void Signup_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Dispose();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "First name:")
                textBox1.Text = "";
        
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = "First name:";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Last name:")
                textBox2.Text = "";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                textBox2.Text = "Last name:";
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

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Enter your password:")
                textBox5.Text = "";

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
                textBox5.Text = "Enter your password:";
        }

        private void textbox_press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                button1.PerformClick();

            }

        }
    }
}
