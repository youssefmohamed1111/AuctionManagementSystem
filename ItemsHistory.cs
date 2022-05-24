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
    public partial class ItemsHistory : Form
    {
        public string email = "buyer";
        public string orcl = "Data Source=orcl; User Id=scott;password=tiger;";
        public OracleConnection conn;
        OracleDataAdapter ad;
        DataSet ds;

        public ItemsHistory()
        {
            InitializeComponent();
        }

        private void ItemsHistory_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(orcl);
            conn.Open();
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CmdStr = @"Select ITEM_ID AS ID, ITEM_NAME AS Name, 
                            min_bidding_amt AS Min_Bid,
                            DESCRIPTION AS Description,
                            TIME_LIMIT AS End_Date
                            from items where ITEM_USER_EMAIL=:email and items.ICATEGORYID=:Category";
            ad = new OracleDataAdapter(CmdStr, conn);
            ad.SelectCommand.Parameters.Add("email", this.email);
            ad.SelectCommand.Parameters.Add("CATEGORY", comboBox1.SelectedIndex + 1);
            ds = new DataSet();
            ad.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            int n = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommandBuilder build = new OracleCommandBuilder(ad);
            ad.Update(ds.Tables[0]);


        }
    }
}
