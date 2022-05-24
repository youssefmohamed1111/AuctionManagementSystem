using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionManagementSystem
{
    public partial class BuyerDashboard : Form
    {
        Profile userProfile;
        Market marketplace;
        Form activeForm;

        public string FirstName = "Buyer";
        public string email = "buyer";
        public BuyerDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in this.panel1.Controls)
            {
                if (ctr.GetType() == typeof(Button))
                {
                    if (ctr.Name != "button1" )
                    {
                        ctr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(60)))));
                        ctr.Enabled = true;
                    } else
                    {
                        ctr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
                        ctr.Enabled = false;
                    }
                }
            }
            this.panel1.Controls.Remove(activeForm);
            activeForm.Close();
            activeForm.Dispose();

            userProfile = new Profile();

            userProfile.TopLevel = false;
            userProfile.AutoScroll = true;
            this.panel3.Controls.Add(userProfile);
            userProfile.email = email;
            userProfile.bd = this;
            userProfile.mainAppLabel = this.label2;
            userProfile.Show();
            activeForm = userProfile;
        }

        private void market_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in this.panel1.Controls)
            {
                if (ctr.GetType() == typeof(Button))
                {
                    if (ctr.Name != "market")
                    {
                        ctr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(60)))));
                        ctr.Enabled = true;
                    }
                    else
                    {
                        ctr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
                        ctr.Enabled = false;
                    }
                }

            }
            this.panel1.Controls.Remove(activeForm);
            activeForm.Close();
            activeForm.Dispose();

            marketplace = new Market();

            marketplace.TopLevel = false;
            marketplace.email = email;
            marketplace.AutoScroll = true;
            marketplace.parentForm = this;
            marketplace.name = this.FirstName;
            this.panel3.Controls.Add(marketplace);

            marketplace.Show();
            activeForm = marketplace;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BuyerDashboard_Load(object sender, EventArgs e)
        {
            marketplace = new Market();
            this.label2.Text = FirstName;
            marketplace.TopLevel = false;
            marketplace.AutoScroll = true;
            marketplace.email = email;
            marketplace.parentForm = this;
            this.panel3.Controls.Add(marketplace);
            marketplace.name = this.FirstName;
            marketplace.Show();
            activeForm = marketplace;
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
