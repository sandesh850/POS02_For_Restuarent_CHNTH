namespace POS02_For_Restuarent
{
    partial class frmManagementCenterNavbtns
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagementCenterNavbtns));
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoginConfig = new System.Windows.Forms.Button();
            this.btnKitchenStockRelease = new System.Windows.Forms.Button();
            this.btnWeeklyReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 50);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select an action ";
            // 
            // btnLoginConfig
            // 
            this.btnLoginConfig.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnLoginConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginConfig.Font = new System.Drawing.Font("Perpetua", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginConfig.Location = new System.Drawing.Point(126, 134);
            this.btnLoginConfig.Name = "btnLoginConfig";
            this.btnLoginConfig.Size = new System.Drawing.Size(331, 61);
            this.btnLoginConfig.TabIndex = 8;
            this.btnLoginConfig.Text = "Login Configuration";
            this.btnLoginConfig.UseVisualStyleBackColor = false;
            this.btnLoginConfig.Click += new System.EventHandler(this.btnLoginConfig_Click);
            // 
            // btnKitchenStockRelease
            // 
            this.btnKitchenStockRelease.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnKitchenStockRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKitchenStockRelease.Font = new System.Drawing.Font("Perpetua", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKitchenStockRelease.Location = new System.Drawing.Point(126, 230);
            this.btnKitchenStockRelease.Name = "btnKitchenStockRelease";
            this.btnKitchenStockRelease.Size = new System.Drawing.Size(331, 61);
            this.btnKitchenStockRelease.TabIndex = 9;
            this.btnKitchenStockRelease.Text = "Kitchen Stock Release";
            this.btnKitchenStockRelease.UseVisualStyleBackColor = false;
            this.btnKitchenStockRelease.Click += new System.EventHandler(this.btnKitchenStockRelease_Click);
            // 
            // btnWeeklyReport
            // 
            this.btnWeeklyReport.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnWeeklyReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeeklyReport.Font = new System.Drawing.Font("Perpetua", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeeklyReport.Location = new System.Drawing.Point(126, 321);
            this.btnWeeklyReport.Name = "btnWeeklyReport";
            this.btnWeeklyReport.Size = new System.Drawing.Size(331, 61);
            this.btnWeeklyReport.TabIndex = 10;
            this.btnWeeklyReport.Text = "Weekly Report";
            this.btnWeeklyReport.UseVisualStyleBackColor = false;
            // 
            // frmManagementCenterNavbtns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(596, 430);
            this.Controls.Add(this.btnWeeklyReport);
            this.Controls.Add(this.btnKitchenStockRelease);
            this.Controls.Add(this.btnLoginConfig);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManagementCenterNavbtns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoginConfig;
        private System.Windows.Forms.Button btnKitchenStockRelease;
        private System.Windows.Forms.Button btnWeeklyReport;
    }
}