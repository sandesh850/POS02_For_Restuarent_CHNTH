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
    public partial class frmSubLoginForManagementCenter : Form
    {
        public frmSubLoginForManagementCenter()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = SQLCon.GetConnection())
            {

                var selecting = "SELECT * FROM TblLogin_details";

                using (SqlCommand cmd = new SqlCommand(selecting, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader()) // initializing data reader to read the data)
                    {
                        if (reader.Read())
                        {
                            // capturing values
                            string username = reader["Username"].ToString();
                            string password = reader["password"].ToString();

                            // checking the login
                            if (username == tbxUserName.Text && password == tbxPassword.Text)
                            {

                                frmManagementCenterNavbtns navbtn = new frmManagementCenterNavbtns();
                                navbtn.ShowDialog();

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tbxPassword.Clear();
                                tbxUserName.Clear();
                            }
                        }

                    }
                }

            }
        }
    }
}
