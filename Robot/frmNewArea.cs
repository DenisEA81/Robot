using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robot
{
    public partial class frmNewArea : Form
    {
        public DialogResult ShowResult = DialogResult.Cancel;

        public frmNewArea()
        {
            InitializeComponent();
        }

        private void frmNewArea_Load(object sender, EventArgs e)
        {

        }

        private void nudLevelFieldSizeX_Leave(object sender, EventArgs e)
        {            
            try
            {
                if (((NumericUpDown)sender).Value % 16 != 0) throw new Exception("Размеры поля должны быть кратны 16.");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                ((NumericUpDown)sender).Value = 16;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ShowResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
