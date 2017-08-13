using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sage_Editor
{
    public partial class LayerForm : Form
    {
        int LayerCount;
        public LayerForm(int LayerCount)
        {
            this.LayerCount = LayerCount;
            InitializeComponent();
        }

        public bool OkPressed;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int width;
            int height;
            if ((int.TryParse(txtLayrWidth.Text, out width)) && int.TryParse(txtLayHeight.Text, out height)
                && (width >= 1) && (height >= 1))
            {
                
                OkPressed = true;
                Close();
            }
            else
            {
                MessageBox.Show("Pleae Enter Valid Numbers in Width and Height \r\nmake sure the number is not negative");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            OkPressed = false;
            Close();

        }

    

        private void LayerForm_Load(object sender, EventArgs e)
        {
            txtLayerName.Text = "Layer" + (LayerCount+1); 
            txtLayHeight.Text = "" + 100;
            txtLayrWidth.Text = "" + 100;
        }

        
    }
}
