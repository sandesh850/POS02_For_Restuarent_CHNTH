using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS02_For_Restuarent
{
    public partial class frmManagementCenterNavbtns : Form
    {
        public frmManagementCenterNavbtns()
        {
            InitializeComponent();
        }

        private void btnLoginConfig_Click(object sender, EventArgs e)
        {
            frmUpdateLogin update = new frmUpdateLogin();
            update.ShowDialog();
        }

        private void btnKitchenStockRelease_Click(object sender, EventArgs e)
        {
            frmKitchenStockRelease release = new frmKitchenStockRelease();
            release.ShowDialog();
        }
    }
}
