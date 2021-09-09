using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Parking_Fee_Management
{
    public partial class dashboard : Form
    {
        string currentTimeIn;
        public dashboard()
        {
            InitializeComponent();
        }
        int indexRow;
        int indexColumn;
        int parkingFee;
        int currentOccupied = 0;
        int currentAvailable = 100;
        int receiptNumber = 0;

        private void carAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                //the occupied space increment by one
                currentOccupied++;
                //the available decrement by one
                currentAvailable--;

                //check if the textbox in empty
                if (txtLiscensePlate.Text == "" && txtBrand.Text == "" && txtColour.Text == "")
                {
                    MessageBox.Show("Please Fill The Blank Data", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //the occupied space increment by one
                    currentOccupied--;
                    //the available decrement by one
                    currentAvailable++;
                }
                else if (txtLiscensePlate.Text == "" && txtBrand.Text == "")
                {
                    MessageBox.Show("Please Fill The Blank Data", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentOccupied--;
                    //the available decrement by one
                    currentAvailable++;
                }
                else if (txtBrand.Text == "" && txtColour.Text == "")
                {
                    MessageBox.Show("Please Fill The Blank Data", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentOccupied--;
                    //the available decrement by one
                    currentAvailable++;
                }
                else if (txtLiscensePlate.Text == "" && txtColour.Text == "")
                {
                    MessageBox.Show("Please Fill The Blank Data", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentOccupied--;
                    //the available decrement by one
                    currentAvailable++;
                }
                else if (txtLiscensePlate.Text == "")
                {
                    MessageBox.Show("Please Fill The Blank Data", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentOccupied--;
                    //the available decrement by one
                    currentAvailable++;
                }
                else if (txtBrand.Text == "")
                {
                    MessageBox.Show("Please Fill The Blank Data", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentOccupied--;
                    //the available decrement by one
                    currentAvailable++;
                }
                else if (txtColour.Text == "")
                {
                    MessageBox.Show("Please Fill The Blank Data", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentOccupied--;
                    //the available decrement by one
                    currentAvailable++;
                }
                else
                {
                    //adds data to the datagridview
                    dataGridView.Rows.Add(txtLiscensePlate.Text, txtBrand.Text, txtColour.Text, currentTimeIn, "N/A", "N/A", "N/A");

                }
                //updates the occupied and available parking space
                if (currentOccupied > 100)
                {
                    MessageBox.Show("The Parking Lot Reached the Limit", "No More Space!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (currentOccupied >= 10)
                {
                    lblOccupied.Text = Convert.ToString(currentOccupied);
                    currentOccupied = Convert.ToInt32(lblOccupied.Text);
                    lblAvailable.Text = Convert.ToString(currentAvailable);
                    currentAvailable = Convert.ToInt32(lblAvailable.Text);
                }
                else if (currentOccupied > 0)
                {
                    lblOccupied.Text = Convert.ToString(currentOccupied);
                    currentOccupied = Convert.ToInt32(lblOccupied.Text);
                    lblAvailable.Text =Convert.ToString(currentAvailable);
                    currentAvailable = Convert.ToInt32(lblAvailable.Text);
                }
                //blanks the textbox
                txtLiscensePlate.Text = "";
                txtBrand.Text = "";
                txtColour.Text = "";

                dataGridView.CurrentCell.Selected = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Action not possible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentDate.Text = DateTime.Now.ToLongDateString();
            currentTimeIn= Convert.ToString(currentTime.Text = DateTime.Now.ToLongTimeString());
        }

        private void btnCarExit_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow getRowColor = dataGridView.Rows[indexRow];
                System.Drawing.Color color = getRowColor.DefaultCellStyle.BackColor;
                if (color == Color.Red)
                {
                    MessageBox.Show("This car has already left the parking lot", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //checks if there is a selected item
                else if (dataGridView.SelectedCells.Count <= 0)
                {
                    MessageBox.Show("You did not select a row!", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //compute the time duration
                    DateTime inTime = DateTime.Parse(currentTimeIn);
                    TimeSpan duration = DateTime.Now.Subtract(inTime);
                    int timeDuration = (duration.Hours);

                    //compute the time the car  exit
                    string timeExit = DateTime.Now.ToLongTimeString();

                    //compute the parkingFee
                    if (timeDuration <= 3)
                    {
                        parkingFee = 50;
                    }
                    else if (timeDuration <= 19)
                    {
                        int hr;
                        double add_Charge, total;
                        hr = timeDuration - 3;
                        int x = 1;
                        while (x <= hr)
                        {
                            add_Charge = x * 25;
                            total = add_Charge + 50;
                            x++;
                            parkingFee = Convert.ToInt32(total);
                        }
                    }
                    else if (timeDuration > 19)
                    {
                        parkingFee = 500;
                    }

                    //update value on the selected row
                    DataGridViewRow newDataAppend = dataGridView.Rows[indexRow];
                    newDataAppend.Cells[4].Value = timeExit;
                    newDataAppend.Cells[5].Value = timeDuration + " hr";
                    newDataAppend.Cells[6].Value = parkingFee;

                    //update the back color on timed out car
                    DataGridViewRow rowColor = dataGridView.Rows[indexRow];
                    rowColor.DefaultCellStyle.BackColor = Color.Red;

                    //update the available parking space
                    currentOccupied = Convert.ToInt32(lblOccupied.Text);
                    if (currentOccupied == 0)
                    {
                        lblOccupied.Text = Convert.ToString(currentOccupied - 1);
                        currentOccupied = Convert.ToInt32(lblOccupied.Text);
                        lblAvailable.Text = Convert.ToString(currentAvailable + 1);
                        currentAvailable = Convert.ToInt32(lblAvailable.Text);
                    }
                    else if (currentOccupied > 0)
                    {
                        lblOccupied.Text = Convert.ToString(currentOccupied - 1);
                        currentOccupied = Convert.ToInt32(lblOccupied.Text);
                        lblAvailable.Text = Convert.ToString(currentAvailable + 1);
                        currentAvailable = Convert.ToInt32(lblAvailable.Text);
                    }
                    else if (currentOccupied >= 10)
                    {
                        lblOccupied.Text = Convert.ToString(currentOccupied - 1);
                        currentOccupied = Convert.ToInt32(lblOccupied.Text);
                        lblAvailable.Text = Convert.ToString(currentAvailable + 1);
                        currentAvailable = Convert.ToInt32(lblAvailable.Text);
                    }
                    else if (currentOccupied > 100)
                    {
                        MessageBox.Show("The Parking Lot Reached the Limit", "No More Space!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //this code shows the receipt form
                    receiptNumber++;
                    reciept receiptForm = new reciept();

                    if (receiptNumber >= 1)
                    {
                        receiptForm.lblReceiptNo.Text = "PRK00" + receiptNumber;
                    }
                    else if (receiptNumber >= 10)
                    {
                        receiptForm.lblReceiptNo.Text = "PRK0" + receiptNumber;
                    }
                    else if (receiptNumber == 100)
                    {
                        receiptForm.lblReceiptNo.Text = Convert.ToString("PRK" + receiptNumber);
                    }
                    else if (receiptNumber > 100)
                    {
                        MessageBox.Show("The Parking Lot Reached the Limit", "No More Space!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    DataGridViewRow getData = dataGridView.Rows[indexRow];

                    receiptForm.lblRecieptDate.Text = DateTime.Now.ToShortDateString();
                    receiptForm.lblBrand.Text = Convert.ToString(getData.Cells[1].Value);
                    receiptForm.lblColor.Text = Convert.ToString(getData.Cells[2].Value);
                    receiptForm.lblReceiptLPlate.Text = Convert.ToString(getData.Cells[0].Value);
                    receiptForm.lblRecieptTimeIn.Text = Convert.ToString(getData.Cells[3].Value);
                    receiptForm.lblReceiptTimeOut.Text = timeExit;
                    receiptForm.lblRecieptDuration.Text = Convert.ToString(timeDuration) + " hr";
                    receiptForm.lblRecieptAmount.Text = Convert.ToString("PhP " + parkingFee);

                    receiptForm.Show();

                }                 
            }
            catch (Exception)
            {
                MessageBox.Show("No car has entered the parking lot yet!", "This action is Impossible!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //how to get the selected row
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView.Rows[indexRow];

            indexColumn = e.ColumnIndex;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //checks if there is a selected item
                if (dataGridView.SelectedCells.Count <= 0)
                {
                    MessageBox.Show("You did not select a row!", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //ask the user if he/she wants to delete the selected row
                else if (MessageBox.Show("Are you sure you want to delete this row?", "Delete Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //delete row from the datagridview
                    int rowIndex = dataGridView.CurrentCell.RowIndex;
                    dataGridView.Rows.RemoveAt(rowIndex);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No car has entered the parking lot yet!", "This action is Impossible!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //log out button
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Quit?", "Exit the Program", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }   
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count <= 0)
            {
                MessageBox.Show("There is no data to be exported!", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (MessageBox.Show("Are you sure you want to Export data now to Excel?", "Export To Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //saving datagrid inputs to microsoft excel
                saveFileDialog1.InitialDirectory = "C:";
                saveFileDialog1.Title = "Save as Excel File";
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "Excel Files(2013)|*.xlsx";

                if(saveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);

                    //Change properties of the workbook
                    ExcelApp.Columns.ColumnWidth = 20;

                    //Storing header Part in Excel
                    for( int i = 1; i < dataGridView.Columns.Count + 1; i++)
                    {
                        ExcelApp.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
                    }

                    //Storing each row and column value to excel sheet
                    for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    {
                        for(int j = 0; j <dataGridView.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();

                    MessageBox.Show("Data Has Been Successfully Exported to Excel File", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
