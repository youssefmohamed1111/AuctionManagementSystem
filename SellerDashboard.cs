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
    public partial class SellerDashboard : Form
    {
        public string email = "seller";
        public string FirstName = "Seller";
        Form activeForm;
        Profile userProfile;
        public SellerDashboard()
        {
            InitializeComponent();
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in this.panel1.Controls)
            {
                if (ctr.GetType() == typeof(Button))
                {
                    if (ctr.Name != "addItem")
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

            AddItem addItem = new AddItem();
            addItem.email = this.email;
            addItem.TopLevel = false;
            addItem.AutoScroll = true;
            this.panel3.Controls.Add(addItem);
            addItem.Show();
            activeForm = addItem;
        }

        private void profile_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in this.panel1.Controls)
            {
                if (ctr.GetType() == typeof(Button))
                {
                    if (ctr.Name != "profile")
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

            userProfile = new Profile();

            userProfile.TopLevel = false;
            userProfile.AutoScroll = true;
            this.panel3.Controls.Add(userProfile);
            userProfile.email = email;
            userProfile.sd = this;
            userProfile.mainAppLabel = this.label2;
            userProfile.Show();
            activeForm = userProfile;
            
        }

        private void allItems_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in this.panel1.Controls)
            {
                if (ctr.GetType() == typeof(Button))
                {
                    if (ctr.Name != "allItems")
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

            ItemsHistory allItems = new ItemsHistory();

            allItems.TopLevel = false;
            allItems.AutoScroll = true;
            this.panel3.Controls.Add(allItems);
            allItems.email = this.email;
            allItems.Show();
            activeForm = allItems;
        }

        private void BuyerDashboard_Load(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem();
            label2.Text = FirstName;
            addItem.email = this.email;
            addItem.TopLevel = false;
            addItem.AutoScroll = true;
            this.panel3.Controls.Add(addItem);
            addItem.Show();
            activeForm = addItem;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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
