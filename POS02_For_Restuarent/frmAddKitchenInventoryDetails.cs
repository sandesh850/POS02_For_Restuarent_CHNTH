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
                if(tbxItemName.Text != string.Empty || tbxQTY.Text != string.Empty || cmbUnit.Text != "Select The Unit" || tbxUnitPrice.Text != string.Empty || tbxTotalQtyPrice.Text != string.Empty)
                {
                    ///Following code use for calculate stock ID
                    int StockID = 0;

                    Program.da = new SqlDataAdapter("SELECT TOP 1 StockID FROM TblStockManagementDetailsKitchenInventory ORDER BY StockID DESC", Program.con);
                    Program.da.Fill(Program.ds, ("TblStockID_dst"));

                    if (Program.ds.Tables["TblStockID_dst"].Rows.Count == 0)
                    {
                        StockID = StockID + 1;
                    }
                    else
                    {
                        int stockID_retrieveFromDBS = Convert.ToInt32(Program.ds.Tables["TblStockID_dst"].Rows[0]["StockID"]);
                        int calculated_StockID = stockID_retrieveFromDBS + 1;
                        StockID += calculated_StockID;
                    }

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


                    /// The following code use for insert data into database (TblStockManagementDetailsKitchenInventory)
                    var date = DateTime.Now.ToShortDateString();
                    var time = DateTime.Now.ToShortTimeString();

                    using (SqlConnection con = SQLCon.GetConnection())
                    {
                        var query = "INSERT INTO TblStockManagementDetailsKitchenInventory VALUES(@RecordID,@ITEM_NAME,@QTY,@Unit,@Unit_Price" +
                            ",@TotalQTYPrice,@StockID,@Date,@Time)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@RecordID", RecordID);
                            cmd.Parameters.AddWithValue("@ITEM_NAME", tbxItemName.Text);
                            cmd.Parameters.AddWithValue("@QTY", tbxQTY.Text);
                            cmd.Parameters.AddWithValue("@Unit", cmbUnit.Text);
                            cmd.Parameters.AddWithValue("@Unit_Price", tbxUnitPrice.Text);
                            cmd.Parameters.AddWithValue("@TotalQTYPrice", tbxTotalQtyPrice.Text);
                            cmd.Parameters.AddWithValue("@StockID", StockID);
                            cmd.Parameters.AddWithValue("@Date", date);
                            cmd.Parameters.AddWithValue("@Time", time);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // MessageBox.Show("Details successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //cmbUnit.Text = "Select The Unit";
                            //tbxItemName.Clear();
                            //tbxQTY.Clear();
                            //tbxUnitPrice.Clear();
                            //tbxTotalQtyPrice.Clear();

                        }

                    }

                    /// The following code use for insert data into database (TblStockManagementDKInventory_use_for_functions)
                    using (SqlConnection con = SQLCon.GetConnection())
                    {
                        var query = "INSERT INTO TblStockManagementDKInventory_use_for_functions VALUES(@RecordID,@ITEM_NAME,@QTY,@Unit,@Unit_Price" +
                            ",@TotalQTYPrice,@StockID,@Date,@Time)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@RecordID", RecordID);
                            cmd.Parameters.AddWithValue("@ITEM_NAME", tbxItemName.Text);
                            cmd.Parameters.AddWithValue("@QTY", tbxQTY.Text);
                            cmd.Parameters.AddWithValue("@Unit", cmbUnit.Text);
                            cmd.Parameters.AddWithValue("@Unit_Price", tbxUnitPrice.Text);
                            cmd.Parameters.AddWithValue("@TotalQTYPrice", tbxTotalQtyPrice.Text);
                            cmd.Parameters.AddWithValue("@StockID", StockID);
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
                else
                {
                    MessageBox.Show("Please fill in all fields", "",MessageBoxButtons.OK, MessageBoxIcon.Information);
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
