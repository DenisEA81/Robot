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
    public partial class frmSettings : Form
    {
        public DialogResult ShowResult = DialogResult.Cancel;
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ShowResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trbVolumeMusic_ValueChanged(object sender, EventArgs e)
        {
            lblVolumeMusic.Text = trbVolumeMusic.Value.ToString() + "%";
        }

        private void trbVolumeBackSound_ValueChanged(object sender, EventArgs e)
        {
            lblVolumeBackSound.Text = trbVolumeBackSound.Value.ToString() + "%";
        }

        private void trbVolumeCommand_ValueChanged(object sender, EventArgs e)
        {
            lblVolumeCommand.Text = trbVolumeCommand.Value.ToString() + "%";
        }
    }
}
