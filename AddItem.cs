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
using System.IO;


namespace AuctionManagementSystem
{
    public partial class AddItem : Form
    {
        string orcl= "Data Source=orcl; User Id=scott; Password=tiger;";
        OracleConnection conn;
        string path;
        public string email = "";
        int id;

        public AddItem()
        {
            InitializeComponent();
        }

        private int insert_item()
        {

            OracleCommand command = new OracleCommand();
            command.Connection = conn;
            command.CommandText =
            "insert into items values (:item_id, :email, :item_name, :min_bid, :description, :end_date, :img_path, :category_id)";
            command.Parameters.Add("item_id", this.id);
            command.Parameters.Add("email", this.email);
            command.Parameters.Add("item_name", textBox1.Text);
            command.Parameters.Add("min_bid", Int32.Parse(textBox2.Text, System.Globalization.NumberStyles.Number));
            command.Parameters.Add("description", textBox4.Text);
            command.Parameters.Add("end_date", dateTimePicker1.Value.Date);
            command.Parameters.Add("img_path", this.id + Path.GetExtension(this.path));
            command.Parameters.Add("category", comboBox1.SelectedIndex + 1);


            MessageBox.Show("insert done succesfully");
            int r = command.ExecuteNonQuery();
            return r;

        }

        private int id_no()
        {

            int max = 0;
            OracleCommand command = new OracleCommand();
            command.Connection = conn;
            command.CommandText = "select max(item_id) from items";

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

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files(*.jpg;*.jpeg;*.png;*.bmp)| *.jpg;*.jpeg;*.png;*.bmp"; // file types, that will be allowed to upload
                dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
                if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
                {
                    this.path = dialog.FileName; // get name of file
                    pictureBox1.ImageLocation = path;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void AddItem_Load(object sender, EventArgs e)
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

        private void AddItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
                MessageBox.Show("Please Add all info.");
            else
            {
                this.id = id_no();
                int r = insert_item();

                try
                {
                    File.Copy(this.path, "../../Resources/" + id + Path.GetExtension(this.path));

                } catch (Exception ex)
                {

                }
            }
                
       
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Item Name:")
                textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = "Item Name:";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Minimum Bidding:")
                textBox2.Text = "";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                textBox2.Text = "Minimum Bidding:";
            else
                textBox2.Text = string.Format("{0:#,##0}", int.Parse(textBox2.Text));


        }
        private void textbox_press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();

            }

        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Enter Description:")
                textBox4.Text = "";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
                textBox4.Text = "Enter Description:";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int num = 0;

            


            if (char.IsDigit(e.KeyChar))
            {
                
                if (int.TryParse(textBox2.Text, out num))
                {
                    if (num > 20000000)
                    {
                        e.Handled = true;
                    } else
                    {
                        e.Handled = false;
                    }
                } else
                {
                    e.Handled = false;
                }
            } else
            {
                if (e.KeyChar == '\b')
                {
                    e.Handled = false;

                } else
                    e.Handled = true;
            }

            
        }

    }
}
