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
    public partial class reciept : Form
    {
        public reciept()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=parking_Lot_System.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void btnPaid_Click(object sender, EventArgs e)
        {

                con.Open();
                string registerCarHistory = "INSERT INTO carParked VALUES ('" + lblReceiptLPlate.Text + "','" + lblBrand.Text + "','"+ lblColor.Text+ "'," +
                    "'"+ lblRecieptTimeIn .Text+ "','"+ lblReceiptTimeOut .Text+ "','"+ lblRecieptDuration.Text+ "','"+ lblRecieptAmount.Text+ "'," +
                    "'"+ lblRecieptDate.Text+ "','"+ lblReceiptNo.Text+ "')";
                cmd = new OleDbCommand(registerCarHistory, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Car Log Has Been Successfully Entered", "Log Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            

            
        }
    }
}
