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
    public partial class frmAddBarcode_Items : Form
    {
        public frmAddBarcode_Items()
        {
            InitializeComponent();
        }

        private void tbxprice_Click(object sender, EventArgs e)
        {
            lblPrice_of_a_single_item.Visible = true;
        }

        private void tbxprice_Leave(object sender, EventArgs e)
        {
            lblPrice_of_a_single_item.Visible = false;
        }

        private void frmAddBarcode_Items_Load(object sender, EventArgs e)
        {
            lblPrice_of_a_single_item.Visible = false;
            lblPricethatIsretrievedfromthevendor.Visible = false;
        }

        private void frmAddBarcode_Items_Click(object sender, EventArgs e)
        {
            lblPrice_of_a_single_item.Visible = false;
        }

        private void tbxQty_Click(object sender, EventArgs e)
        {
            lblTotal_Items.Visible = true;
        }

        private void tbxQty_TextChanged(object sender, EventArgs e)
        {
            lblTotal_Items.Visible = true;
        }

        private void tbxprice_TextChanged(object sender, EventArgs e)
        {
            lblPrice_of_a_single_item.Visible = true;
        }

        private void tbxQty_Leave(object sender, EventArgs e)
        {
            lblTotal_Items.Visible = false;
        }

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            /// The following code use to count the Barcode Item ID (BitemID)
            if (Program.ds.Tables["TblLastBitemID_dst"] != null)
            {
                Program.ds.Tables["TblLastBitemID_dst"].Clear();
            }

            Program.da = new SqlDataAdapter("SELECT TOP 1 BitemID FROM TblBarcode_Items ORDER BY BitemID DESC",Program.con);
            Program.da.Fill(Program.ds, "TblLastBitemID_dst");

            int BitemID = 0; 

            foreach (DataRow LastBitemID in Program.ds.Tables["TblLastBitemID_dst"].Rows)
            {
                BitemID = Convert.ToInt32(LastBitemID["BitemID"]);


            }

            BitemID = BitemID + 1;

           /// The following code use to store the data in database (TblBarcode_Items)
           if(tbxBarcode.Text != string.Empty && tbxItemName.Text != string.Empty && tbxQty.Text != string.Empty && tbxTotalstockPrice.Text != string.Empty && tbxprice.Text != string.Empty)
           {
                try
                {
                    using (SqlConnection con = SQLCon.GetConnection())
                    {
                        var query = "INSERT INTO TblBarcode_Items VALUES(@BitemID,@Barcode,@Price,@ItemName,@Qty)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@BitemID", BitemID);
                            cmd.Parameters.AddWithValue("@Barcode", tbxBarcode.Text);
                            cmd.Parameters.AddWithValue("@Price", tbxprice.Text);
                            cmd.Parameters.AddWithValue("@ItemName", tbxItemName.Text);
                            cmd.Parameters.AddWithValue("@Qty", tbxQty.Text);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();//testing
                            con.Close();

                        }

                        /// The following code use to store the data in database (TblStockManagementDetailsBarcodeItems)
                        if (Program.ds.Tables["TblStockID_dst"] != null)
                        {
                            Program.ds.Tables["TblStockID_dst"].Clear();
                        }


                        string date = DateTime.Now.ToShortDateString();
                        string time = DateTime.Now.ToShortTimeString();

                        ///Following code use for calculate stock ID
                        int StockID = 0;

                        Program.da = new SqlDataAdapter("SELECT TOP 1 StockID FROM TblStockManagementDetailsBarcodeItems ORDER BY StockID DESC",Program.con);
                        Program.da.Fill(Program.ds,("TblStockID_dst"));

                        if(Program.ds.Tables["TblStockID_dst"].Rows.Count == 0)
                        {
                            StockID = StockID + 1;
                        }
                        else
                        {
                            int stockID_retrieveFromDBS = Convert.ToInt32(Program.ds.Tables["TblStockID_dst"].Rows[0]["StockID"]);
                            int calculated_StockID = stockID_retrieveFromDBS + 1;
                            StockID += calculated_StockID;
                        }
                        //MessageBox.Show(StockID.ToString());
                        var query02 = "INSERT INTO TblStockManagementDetailsBarcodeItems VALUES(@BarcodeItemID,@Barcode,@ItemName,@TotalStockQTY," +
                            "@TotalStockPrice,@SellingPriceOfSingleItem,@StockID,@SingleItemPriceThatIsRetrievedFromTheVendor,@Date,@Time)";

                        using (SqlCommand cmd = new SqlCommand(query02, con))
                        {
                            cmd.Parameters.AddWithValue("@BarcodeItemID", BitemID);
                            cmd.Parameters.AddWithValue("@Barcode", tbxBarcode.Text);
                            cmd.Parameters.AddWithValue("@ItemName", tbxItemName.Text);
                            cmd.Parameters.AddWithValue("@TotalStockQTY", tbxQty.Text);
                            cmd.Parameters.AddWithValue("@TotalStockPrice", tbxTotalstockPrice.Text);
                            cmd.Parameters.AddWithValue("@SellingPriceOfSingleItem", tbxprice.Text);
                            cmd.Parameters.AddWithValue("@StockID", StockID);
                            cmd.Parameters.AddWithValue("@SingleItemPriceThatIsRetrievedFromTheVendor", tbxSingleItemPriceThatIsRetrievedFromTheVendor.Text);
                            cmd.Parameters.AddWithValue("@Date", date);
                            cmd.Parameters.AddWithValue("@Time", time);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();//testing
                            con.Close();

                        }
                    }



                    MessageBox.Show("Details successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbxBarcode.Clear();
                    tbxQty.Clear();
                    tbxprice.Clear();
                    tbxItemName.Clear();
                    tbxTotalstockPrice.Clear();
                    tbxSingleItemPriceThatIsRetrievedFromTheVendor.Clear();
                    lblTotal_Items.Visible = false;
                    lblPricethatIsretrievedfromthevendor.Visible = false;
                    lblPrice_of_a_single_item.Visible = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              
           }
           else
           {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }

          
        }

        private void tbxSingleItemPriceThatIsRetrievedFromTheVendor_TextChanged(object sender, EventArgs e)
        {
            lblPricethatIsretrievedfromthevendor.Visible = true;
        }

        private void tbxSingleItemPriceThatIsRetrievedFromTheVendor_Leave(object sender, EventArgs e)
        {
            lblPricethatIsretrievedfromthevendor.Visible =  false;
        }

        private void tbxSingleItemPriceThatIsRetrievedFromTheVendor_Click(object sender, EventArgs e)
        {
            lblPricethatIsretrievedfromthevendor.Visible = true;
        }
    }
    
}
