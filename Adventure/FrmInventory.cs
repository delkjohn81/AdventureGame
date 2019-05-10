using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    public partial class FrmInventory : Form
    {
        protected Mob link;
        public event UseItemHandler UseItem;
        public FrmInventory(Mob link)
        {
            InitializeComponent();
            this.link = link;// store a copy of Link for future reference
            StartPosition = FormStartPosition.Manual;
            this.Top = 20;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - 290;
            this.Width = 290;
            KeyPreview = true;
            btnUse.Visible = false;
            UpdateUI(link);
        }

        private void UpdateUI(Mob link)
        {
            lbInventory.Items.Clear();
            foreach (Item item in link.Inventory)
            {
                lbInventory.Items.Add(item.Name);
                if (item == link.EquippedWeapon)
                {
                    lbInventory.SelectedIndex = lbInventory.Items.Count - 1;
                }
            }
            lblHP.Text = link.HP.ToString();
        }

        private void FrmInventory_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Close();
            }
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            int sel = lbInventory.SelectedIndex;
            if (sel > -1)
            {
                Item i = link.Inventory[sel];
                if (UseItem != null)
                {
                    UseItem(i);
                    UpdateUI(link);
                }
            }
        }

        private void lbInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = lbInventory.SelectedIndex;
            if (sel > -1)
            {
                btnUse.Visible = true;
            }
            else
            {
                btnUse.Visible = false;
            }
        }
    }
}
