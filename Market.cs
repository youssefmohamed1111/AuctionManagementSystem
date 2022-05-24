using AuctionManagementSystem.Properties;
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
using System.Collections;

namespace AuctionManagementSystem
{
    public partial class Market : Form
    {
        public string email = "";
        public string name = "";
        string orcl = "Data Source=orcl; User Id=scott; Password=tiger;";
        OracleConnection conn;
        Label zeroLabel = new Label();
        public BuyerDashboard parentForm;
        public Market()
        {
            InitializeComponent();
        }


        public int getItemsCtn()
        {
            int count = 0;
            OracleCommand ctn = new OracleCommand();
            ctn.Connection = conn;
            ctn.CommandText = "Select count(*) from items";
            OracleDataReader dr = ctn.ExecuteReader();

            if (dr.Read())
            {
                count = Convert.ToInt32(dr[0]);
            }

            return count;
        }

        public List<int> getBidItems()
        {
            List<int> arrayList = new List<int>();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"Select ITEM_ID from items I, Bid B where b.bitem_id = I.item_id and b.buser_email = :email";
            cmd.Parameters.Add("email", email);
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                arrayList.Add(Convert.ToInt32(dr[0]));
            }
            return arrayList;
        }

        public List<int> getReportItems()
        {
            List<int> arrayList = new List<int>();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"Select ITEM_ID from items I, Complaint C where C.CITEM_ID = I.item_id and C.CUSER_EMAIL = :email";
            cmd.Parameters.Add("email", email);
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                arrayList.Add(Convert.ToInt32(dr[0]));
            }
            return arrayList;
        }

        public List<Item> GetItems()
        {
            List<Item> items = new List<Item>();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetItems";
            cmd.Parameters.Add("cursor", OracleDbType.RefCursor, ParameterDirection.Output);

            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Item item = new Item(Convert.ToInt32(dr[0].ToString()), dr[2].ToString(), dr[4].ToString(), 
                    dr[1].ToString(), dr[5].ToString(), Convert.ToInt32(dr[3].ToString()), dr[6].ToString());
                items.Add(item);
            }

            return items;
        }
        
        public void zeroCtnLabel()
        {
            this.zeroLabel.AutoSize = true;
            this.zeroLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zeroLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.zeroLabel.Location = new System.Drawing.Point(167, 20);
            this.zeroLabel.Name = "label8";
            this.zeroLabel.Size = new System.Drawing.Size(434, 78);
            this.zeroLabel.TabIndex = 49;
            this.zeroLabel.Text = "Sorry, there are no items \r\navailable right now. ";
            this.zeroLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.zeroLabel.Visible = false;
            this.panel1.Controls.Add(zeroLabel);
        }


        
        // Fucntion to be called when Items are returned from DB. 
        private void createPicBox(int yPos, int randomized, string imgName)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            pictureBox.Location = new System.Drawing.Point(20, yPos + 20);
            pictureBox.Margin = new System.Windows.Forms.Padding(2);
            pictureBox.Name = "pictureBox" + randomized.ToString();
            pictureBox.Size = new System.Drawing.Size(119, 111);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 40;
            pictureBox.TabStop = false;
            try
            {
                pictureBox.Image = Image.FromFile("../../Resources/" + imgName);

            } catch (Exception ex)
            {
                
            }
            this.panel1.Controls.Add(pictureBox);
        }

        private void createItemLabel(int yPos, int randomized, string name)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            label.Location = new System.Drawing.Point(44, yPos + 143);
            label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label.Name = "labelName" + randomized.ToString();
            label.Size = new System.Drawing.Size(77, 16);
            label.TabIndex = 41;
            label.Text = name.ToUpper();
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel1.Controls.Add(label);
        }

        private void createItemDesc(int yPost, int randomized, string desc)
        {
            TextBox tx = new TextBox();
            tx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(60)))));
            tx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tx.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tx.ForeColor = System.Drawing.Color.YellowGreen;
            tx.Location = new System.Drawing.Point(165, yPost + 61);
            tx.Multiline = true;
            tx.ReadOnly = true;
            tx.Name = "textBox" + randomized.ToString();
            tx.Size = new System.Drawing.Size(409, 62);
            tx.TabIndex = 44;
            tx.Text = desc;
            tx.SelectionLength = 0;
            tx.TabStop = false;
            this.panel1.Controls.Add(tx);
        }

        private void createMinBid(int yPost, int randomized, int num)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Lucida Fax", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.ForeColor = System.Drawing.SystemColors.ScrollBar;
            label.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            label.Location = new System.Drawing.Point(592, yPost + 30);
            label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label.Name = "minBiding" + randomized.ToString();
            label.Size = new System.Drawing.Size(72, 13);
            label.TabIndex = 45;
            label.Text = "Minimum Bid: $" ;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel1.Controls.Add(label);
        }

        private void createTextBox(int yPost, int randomized, int minBid)
        {
            TextBox placeBid = new TextBox();
            placeBid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(60)))));
            placeBid.Location = new System.Drawing.Point(595, yPost + 52);
            placeBid.Margin = new System.Windows.Forms.Padding(2);
            placeBid.Name = "placeBid" + randomized.ToString();
            placeBid.Size = new System.Drawing.Size(180, 31);
            placeBid.TabIndex = 46;
            placeBid.TextChanged += new EventHandler((s, e) => textBoxChanged(s, e, minBid));
            placeBid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            this.panel1.Controls.Add(placeBid);
        }

        private void createBidBtn(int yPost, int randomized)
        {
            Button button = new Button();
            button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;         
            button.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.Location = new System.Drawing.Point(595, yPost + 86);
            button.Margin = new System.Windows.Forms.Padding(2);
            button.Name = "BidButton" + randomized.ToString();
            button.Size = new System.Drawing.Size(180, 40);
            button.TabIndex = 47;
            button.TabStop = false;
            button.Text = "Bid";
            button.UseVisualStyleBackColor = false;
            button.Enabled = false;
            button.Click += new EventHandler((s, e) => BidButton(s,e));
            this.panel1.Controls.Add(button);
        }

        private void createReportBtn(int yPost, int radomized, string itemName)
        {
            Button btn = new Button();
            btn.BackColor = System.Drawing.Color.Maroon;
            btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.Font = new System.Drawing.Font("Lucida Fax", 7F);
            btn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btn.Image = global::AuctionManagementSystem.Properties.Resources.icons8_flag_2_20;
            btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Location = new System.Drawing.Point(595, yPost + 136);
            btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btn.Name = "reportButton" + radomized.ToString();
            btn.Size = new System.Drawing.Size(127, 30);
            btn.TabIndex = 41;
            btn.TabStop = false;
            btn.Text = "Report";
            btn.UseVisualStyleBackColor = false;
            btn.Click += new EventHandler((s, e) => Report_Button(s, e, itemName));
            this.panel1.Controls.Add(btn);

        }

        private void createRemainingDays(int yPost, int randomized, string days)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Lucida Fax", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            label.Location = new System.Drawing.Point(285, yPost + 143);
            label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label.Name = "reminaingDaysDesc" + randomized.ToString();
            label.Size = new System.Drawing.Size(117,16);
            label.TabIndex = 50;
            label.Text = "Bid closes on: " + days ;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel1.Controls.Add(label);
        }

        private void createHR(int yPost, int randomized)
        {
            Label label = new Label();
            label.BackColor = System.Drawing.SystemColors.GrayText;
            label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.Location = new System.Drawing.Point(20, yPost + 176);
            label.Name = "hr" + randomized.ToString();
            label.Size = new System.Drawing.Size(745, 1);
            label.TabIndex = 48;
            this.panel1.Controls.Add(label);
        }

        private void createElementsArray(int size, List<Item> items)
        {
            int yPosition = 0;


            for (int i = 0; i < items.Count; i++)
            {
                int randomized = items[i].Id;
                createPicBox(yPosition, randomized, items[i].Img);
                createItemLabel(yPosition, randomized, items[i].Name);
                createHR(yPosition, randomized);
                createItemDesc(yPosition, randomized, items[i].Description);
                createMinBid(yPosition, randomized, items[i].MinBid);
                createTextBox(yPosition, randomized, items[i].MinBid);
                createRemainingDays(yPosition, randomized, items[i].Remaining);
                createBidBtn(yPosition, randomized);
                createReportBtn(yPosition, randomized, items[i].Name);

                // ItemDescription:
                Label ItemDescriptionLabel = new Label();
                ItemDescriptionLabel.AutoSize = true;
                ItemDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ItemDescriptionLabel.ForeColor = System.Drawing.Color.Coral;
                ItemDescriptionLabel.Location = new System.Drawing.Point(164, yPosition + 36);
                ItemDescriptionLabel.Name = "itemDescLabel" + randomized.ToString();
                ItemDescriptionLabel.Size = new System.Drawing.Size(106, 16);
                ItemDescriptionLabel.TabIndex = 43;
                ItemDescriptionLabel.Text = "Item Description:";
                this.panel1.Controls.Add(ItemDescriptionLabel);



                // ItemDescription
                yPosition += 176;
            }

        }

        private void createGUI(int count)
        {
            List<int> bids = getBidItems();
            List<int> reports = getReportItems();
            List<int> removeIDs = new List<int>();

            if (count == 0 || count == bids.Count || count == reports.Count || count == (reports.Count + bids.Count))
            {
                this.zeroLabel.Visible = true;   
            } 
            else
            {
                List<Item> items = GetItems();
                for (int i = 0; i < items.Count; i++)
                {
                    if (bids.Contains(items[i].Id) || reports.Contains(items[i].Id))
                    {
                        removeIDs.Add(i);
                    }
                }
                

                foreach (int index in removeIDs)
                {
                    items.RemoveAt(index);
                }
                createElementsArray(count, items);
            }

        }

        private void textBoxChanged(object sender, EventArgs e, int minBid)
        {
            var textbox = sender as TextBox;
            Button bidBtn = (Button)this.panel1.Controls["BidButton" + textbox.Name[textbox.Name.Length -1 ].ToString()];
            long bidAmount = 0;
            bool is64Num = Int64.TryParse(textbox.Text, out bidAmount);

            if (is64Num)
            {
                if (bidAmount < minBid)
                {
                    textbox.ForeColor = Color.Red;
                    bidBtn.Enabled = false;
                }

                else
                {
                    textbox.ForeColor = Color.Green;
                    bidBtn.Enabled = true;
                }
                    
            }


        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void Market_Load(object sender, EventArgs e)
        {
            zeroCtnLabel();
            conn = new OracleConnection(orcl);
            conn.Open();

            int count = getItemsCtn();
            
            createGUI(count);

        }

        private void Report_Button(object sender, EventArgs e, string itemName)
        {
            var btn = (Button)sender;

            ComplaintForm fr = new ComplaintForm();
            fr.BuyerEmail = this.email;
            fr.ItemID = Convert.ToInt32(btn.Name[btn.Name.Length - 1].ToString());
            fr.ItemName = itemName;
            Form op = new Form();
            op.ClientSize = new Size(965, 509);
            op.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            op.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            op.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            op.Opacity = 0.8;
            fr.opacityForm = op;
            op.Show();
            fr.ParentForm = this.parentForm;
            fr.FirstName = this.name;
            fr.ShowDialog();
            
        }

        private void BidButton(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            OracleCommand cmd = new OracleCommand();
            int itemId = Convert.ToInt32(btn.Name[btn.Name.Length - 1].ToString());
            TextBox text = (TextBox)this.panel1.Controls["placeBid" + btn.Name[btn.Name.Length - 1].ToString()];
            cmd.Connection = conn;
            cmd.CommandText = "insert into BId values (:BITEM_ID, :BUSER_EMAIL, :BIDDING_AMOUNT, :BIDDING_DATE)";
            cmd.Parameters.Add("BITEM_ID", itemId);
            cmd.Parameters.Add("BUSER_EMAIL", this.email);
            cmd.Parameters.Add("BIDDING_AMOUNT", text.Text);
            cmd.Parameters.Add("BIDDING_DATE", DateTime.Now);
            int r = cmd.ExecuteNonQuery();

            parentForm.Close();
            parentForm.Dispose();

            BuyerDashboard bd = new BuyerDashboard();
            bd.email = this.email;
            bd.FirstName = this.name;
            bd.ShowDialog();
            
        }
    }

    public class Item
    {
        public int Id;
        public string Name;
        public string Description;
        public string SellerEmail;
        public string Remaining;
        public int MinBid;
        public string Img;

        public Item(int Id, string Name, string Desc, string SellerEmail, string Remaining, int MinBid, string img)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Desc;
            this.SellerEmail = SellerEmail;
            this.Remaining = Remaining;
            this.MinBid = MinBid;
            this.Img = img;
        }
    }
}
