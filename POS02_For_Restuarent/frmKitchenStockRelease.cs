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
    public partial class frmKitchenStockRelease : Form
    {
        double availableQty = 0;

        public frmKitchenStockRelease()
        {
            InitializeComponent();
        }

        private void tbxSearchByUsingName_Click(object sender, EventArgs e)
        {
            tbxSearchByUsingName.Text = string.Empty;
        }

        private void tbxSearchByUsingName_Leave(object sender, EventArgs e)
        {
            tbxSearchByUsingName.Text = "Search";
        }

        private void frmKitchenStockRelease_Load(object sender, EventArgs e)
        {
            //cmbUnit.DropDownStyle = ComboBoxStyle.Simple;

            if (Program.ds.Tables["TblItemNames_dst"] != null)
            {
                Program.ds.Tables["TblItemNames_dst"].Clear();
            }

            Program.da = new System.Data.SqlClient.SqlDataAdapter("SELECT ITEM_NAME FROM TblStockManagementDKInventory_use_for_functions", Program.con);
            Program.da.Fill(Program.ds, "TblItemNames_dst");

            foreach (DataRow itemName in Program.ds.Tables["TblItemNames_dst"].Rows)
            {
                string itemNameValue = itemName["ITEM_NAME"].ToString();

                if (!lbxItemNames.Items.Contains(itemNameValue))
                {
                    lbxItemNames.Items.Add(itemNameValue);
                }
            }
        }

        private void lbxItemNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.ds.Tables["Tbl_Items_details_dst"] != null)
            {
                Program.ds.Tables["Tbl_Items_details_dst"].Clear();


            }

            Dictionary<string,int> StockDetails = new Dictionary<string,int>();// this use to store the available qty of selected stock item

            Program.da = new System.Data.SqlClient.SqlDataAdapter("SELECT ITEM_NAME,Unit,QTY,StockID FROM TblStockManagementDKInventory_use_for_functions WHERE ITEM_NAME='" + lbxItemNames.SelectedItem + "'  ", Program.con);
            Program.da.Fill(Program.ds, "Tbl_Items_details_dst");

            foreach (DataRow data in Program.ds.Tables["Tbl_Items_details_dst"].Rows)
            {
                tbxItemName.Text = data["ITEM_NAME"].ToString();
                cmbUnit.Text = data["Unit"].ToString();

                string itemName = data["ITEM_NAME"].ToString();
                int qty =Convert.ToInt16(data["QTY"]);

                if (StockDetails.ContainsKey(itemName))
                {
                    StockDetails[itemName] += qty;
                }
                else
                {
                    StockDetails.Add(itemName, qty);
                }

            }

            foreach (var itm in StockDetails)
            {
                tbxAvailableStock.Text = itm.Value.ToString();
                availableQty = Convert.ToDouble(tbxAvailableStock.Text);
            }

            //tbxStockID.Text = Program.ds.Tables["Tbl_Items_details_dst"].Rows[0]["StockID"].ToString();
            //string message = "";

            //foreach (var item in StockDetails)
            //{
            //    message += $"Item: {item.Key}\n  Qty: {item.Value}\n";
            //}

            //MessageBox.Show(message);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
           
            if(tbxItemName.Text == string.Empty)
            {
                MessageBox.Show("Please select an item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (tbxReleaseQty.Text == string.Empty)
            {
                MessageBox.Show("Please Enter the Release Qty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (availableQty < Convert.ToDouble(tbxReleaseQty.Text))
                {
                    MessageBox.Show("Insufficient stock available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(availableQty >= Convert.ToDouble(tbxReleaseQty.Text))
                {
                    /// This code use to calculate RecordID value
                    if (Program.ds.Tables["TblRecordID_dst"] != null)
                    {
                        Program.ds.Tables["TblRecordID_dst"].Clear();
                    }

                    Program.da = new SqlDataAdapter("SELECT TOP 1 RecordID FROM TblKitchenStockRelease ORDER BY RecordID DESC", Program.con);
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

                    /// The following code use for store data in database (TblKitchenStockRelease)
                    string date = DateTime.Now.ToShortDateString();
                    string time = DateTime.Now.ToShortTimeString();

                    using (SqlConnection con = SQLCon.GetConnection())
                    {
                        var insertQuery = "INSERT INTO TblKitchenStockRelease VALUES(@RecordID,@ITEM_NAME,@Unit,@ReleaseQty,@Date,@Time)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@RecordID", RecordID);
                            cmd.Parameters.AddWithValue("@ITEM_NAME", tbxItemName.Text);
                            cmd.Parameters.AddWithValue("@Unit", cmbUnit.Text);
                            cmd.Parameters.AddWithValue("@ReleaseQty", tbxReleaseQty.Text);
                            cmd.Parameters.AddWithValue("@Date", date);
                            cmd.Parameters.AddWithValue("@Time", time);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    /// The following code use for reduse the release qty from TblStockManagementDKInventory_use_for_functions
                    if (Program.ds.Tables["TblDetails_dst"] != null)
                    {
                        Program.ds.Tables["TblDetails_dst"].Clear();
                    }

                    Program.da = new SqlDataAdapter("SELECT QTY,StockID FROM TblStockManagementDKInventory_use_for_functions WHERE ITEM_NAME='" + tbxItemName.Text+"' ", Program.con);
                    Program.da.Fill(Program.ds, "TblDetails_dst");

                    foreach(DataRow data in Program.ds.Tables["TblDetails_dst"].Rows)
                    {
                        int valuefromDB = Convert.ToInt32(data["QTY"]);
                        int releaseQty = Convert.ToInt32(tbxReleaseQty.Text);

                        if (valuefromDB == 0 || valuefromDB < releaseQty)
                        {
                            
                        }
                        else
                        {
                            int stockID = Convert.ToInt16(data["StockID"]);
                            using (SqlConnection con = SQLCon.GetConnection())
                            {
                                var update = "UPDATE TblStockManagementDKInventory_use_for_functions SET QTY = @qty WHERE StockID = @Stockid";
                                using(SqlCommand cmd = new SqlCommand(update, con))
                                {
                                    cmd.Parameters.AddWithValue("@qty",valuefromDB - Convert.ToInt16(tbxReleaseQty.Text));
                                    cmd.Parameters.AddWithValue("@Stockid",stockID);

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                  
                                }
                            }
                        }
                    }


                    MessageBox.Show("Successfully Released", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbUnit.Text = "Please Select";
                    tbxItemName.Clear();
                    tbxReleaseQty.Clear();
                    tbxAvailableStock.Clear();

                }

               
            }

        }
    }
}
