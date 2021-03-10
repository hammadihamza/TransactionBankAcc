using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TransactionBankAcc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-NSRE279; Initial Catalog=Accounts; Integrated Security=true");
        SqlCommand cmd;
        SqlTransaction tr;
        void LoadAmount()
        {
            cmd = new SqlCommand("select amount from Clients where id =1");
            con.Open();
            label1.Text = cmd.ExecuteScalar().ToString();
            con.Close();
            cmd = new SqlCommand("select amount from Clients where id =2");
            con.Open();
            label2.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAmount();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                tr = con.BeginTransaction();
                cmd = new SqlCommand("Update Clients set Amount=Amount+1000 where id=1",con,tr);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Update Clients set Amount=Amount-1000 where id=2", con, tr);
                cmd.ExecuteNonQuery();
                tr.Commit();
                MessageBox.Show("Transaction Completed!");
                con.Close();
                LoadAmount();
            }
            catch(Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
