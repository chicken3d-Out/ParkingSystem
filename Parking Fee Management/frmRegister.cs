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
    public partial class frm_Register : Form
    {
        public frm_Register()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=parking_Lot_System.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(txt_Username.Text) == "" && Convert.ToString(txt_Password.Text) == "" && Convert.ToString(txt_ConfirmPassword.Text) == "")
            {
                MessageBox.Show("Username and Password fields are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToString(txt_Password.Text) == Convert.ToString(txt_ConfirmPassword.Text))
            {
                con.Open();
                string register = "INSERT INTO tbl_users VALUES ('"+txt_Username.Text+"','"+txt_Password.Text+"')";
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();

                txt_Username.Text = "";
                txt_Password.Text = "";
                txt_ConfirmPassword.Text = "";

                MessageBox.Show("Your Account has been Sucessfully Created","Registration Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                new frmLogin().Show();
                this.Hide();
                txt_Username.Focus();
            }
            else
            {
                MessageBox.Show("Password does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Password.Text = "";
                txt_ConfirmPassword.Text = "";
                txt_ConfirmPassword.Focus();
            }
        }

        private void chckBox_ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBox_ShowPass.Checked)
            {
                txt_Password.PasswordChar = '\0';
                txt_ConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txt_Password.PasswordChar = '*';
                txt_ConfirmPassword.PasswordChar = '*';
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Username.Text = "";
            txt_Password.Text = "";
            txt_ConfirmPassword.Text = "";
            txt_Username.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
            txt_Username.Focus();

        }
    } }
