using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Parking_Fee_Management
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=parking_Lot_System.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void btn_Login_Click(object sender, EventArgs e)
        {
            
            con.Open();
            string login = "SELECT * FROM tbl_users WHERE username='" + txt_Username.Text + "' and password= '" + txt_Password.Text + "'";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                new dashboard().Show();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Username.Text = "";
                txt_Password.Text = "";
                txt_Username.Focus();
                con.Close(); 
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Username.Text = "";
            txt_Password.Text = "";
            txt_Username.Focus();
        }

        private void chckBox_ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBox_ShowPass.Checked)
            {
                txt_Password.PasswordChar = '\0';
            }
            else
            {
                txt_Password.PasswordChar = '*';          
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new frm_Register().Show();
            this.Hide();
            txt_Username.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
