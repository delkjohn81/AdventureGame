namespace Adventure
{
    partial class FrmInventory
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
            this.lblHPTitle = new System.Windows.Forms.Label();
            this.lblHP = new System.Windows.Forms.Label();
            this.lbInventory = new System.Windows.Forms.ListBox();
            this.btnUse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHPTitle
            // 
            this.lblHPTitle.AutoSize = true;
            this.lblHPTitle.Font = new System.Drawing.Font("Script MT Bold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHPTitle.Location = new System.Drawing.Point(13, 13);
            this.lblHPTitle.Name = "lblHPTitle";
            this.lblHPTitle.Size = new System.Drawing.Size(65, 33);
            this.lblHPTitle.TabIndex = 0;
            this.lblHPTitle.Text = "HP:";
            // 
            // lblHP
            // 
            this.lblHP.AutoSize = true;
            this.lblHP.Font = new System.Drawing.Font("Script MT Bold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHP.Location = new System.Drawing.Point(82, 13);
            this.lblHP.Name = "lblHP";
            this.lblHP.Size = new System.Drawing.Size(29, 33);
            this.lblHP.TabIndex = 1;
            this.lblHP.Text = "0";
            // 
            // lbInventory
            // 
            this.lbInventory.Font = new System.Drawing.Font("Rockwell Extra Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInventory.FormattingEnabled = true;
            this.lbInventory.ItemHeight = 19;
            this.lbInventory.Location = new System.Drawing.Point(12, 59);
            this.lbInventory.Name = "lbInventory";
            this.lbInventory.Size = new System.Drawing.Size(256, 270);
            this.lbInventory.TabIndex = 2;
            this.lbInventory.SelectedIndexChanged += new System.EventHandler(this.lbInventory_SelectedIndexChanged);
            // 
            // btnUse
            // 
            this.btnUse.Font = new System.Drawing.Font("Script MT Bold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUse.Location = new System.Drawing.Point(13, 345);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(255, 55);
            this.btnUse.TabIndex = 3;
            this.btnUse.Text = "Use/Equip";
            this.btnUse.UseVisualStyleBackColor = true;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // FrmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Adventure.Properties.Resources.InvBackground;
            this.ClientSize = new System.Drawing.Size(274, 524);
            this.Controls.Add(this.btnUse);
            this.Controls.Add(this.lbInventory);
            this.Controls.Add(this.lblHP);
            this.Controls.Add(this.lblHPTitle);
            this.Name = "FrmInventory";
            this.Text = "Inventory";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmInventory_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHPTitle;
        private System.Windows.Forms.Label lblHP;
        private System.Windows.Forms.ListBox lbInventory;
        private System.Windows.Forms.Button btnUse;
    }
}