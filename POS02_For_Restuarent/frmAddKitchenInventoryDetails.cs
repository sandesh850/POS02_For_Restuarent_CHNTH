using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS02_For_Restuarent
{
    public partial class frmAddKitchenInventoryDetails : Form
    {
        public frmAddKitchenInventoryDetails()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                /// This code use to calculate RecordID value
                if (Program.ds.Tables["TblRecordID_dst"] != null)
                {
                    Program.ds.Tables["TblRecordID_dst"].Clear();
                }

                Program.da = new SqlDataAdapter("SELECT TOP 1 RecordID FROM TblStockManagementDetailsKitchenInventory ORDER BY RecordID DESC", Program.con);
                Program.da.Fill(Program.ds, "TblRecordID_dst");

                var RecordID = 0;

                if (Program.ds.Tables["TblRecordID_dst"].Rows.Count == 0)
                {

                    RecordID = RecordID + 1;
                }
                else
                {
                    RecordID = Convert.ToInt16(Program.ds.Tables["TblRecordID_dst"].Rows[0]["RecordID"]) + 1;
                }


                /// The following code use for insert data into database
                var date = DateTime.Now.ToShortDateString();
                var time = DateTime.Now.ToShortTimeString();

                using (SqlConnection con = SQLCon.GetConnection())
                {
                    var query = "INSERT INTO TblStockManagementDetailsKitchenInventory VALUES(@RecordID,@ITEM_NAME,@QTY,@Unit,@Unit_Price" +
                        ",@TotalQTYPrice,@Date,@Time)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RecordID", RecordID);
                        cmd.Parameters.AddWithValue("@ITEM_NAME", tbxItemName.Text);
                        cmd.Parameters.AddWithValue("@QTY", tbxQTY.Text);
                        cmd.Parameters.AddWithValue("@Unit", cmbUnit.Text);
                        cmd.Parameters.AddWithValue("@Unit_Price", tbxUnitPrice.Text);
                        cmd.Parameters.AddWithValue("@TotalQTYPrice", tbxTotalQtyPrice.Text);
                        cmd.Parameters.AddWithValue("@Date", date);
                        cmd.Parameters.AddWithValue("@Time", time);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Details successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbUnit.Text = "Select The Unit";
                        tbxItemName.Clear();
                        tbxQTY.Clear();
                        tbxUnitPrice.Clear();
                        tbxTotalQtyPrice.Clear();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


          
        }

        private void tbxUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if(tbxUnitPrice.Text != string.Empty)
            {
                double unitprice = Convert.ToDouble(tbxUnitPrice.Text);
                int qty = Convert.ToInt32(tbxQTY.Text);

                double calculatedval = unitprice * qty;

                tbxTotalQtyPrice.Text = calculatedval.ToString();
            }
           

        }
    }
}
