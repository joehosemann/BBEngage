using System;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace Engage.View
{
    public partial class DeleteRecycled : Form
    {
        public DeleteRecycled()
        {
            InitializeComponent();

            new Outlook().DeleteAll();

            btnOK.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
