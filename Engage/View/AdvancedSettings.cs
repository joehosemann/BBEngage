using System;
using System.Windows.Forms;

namespace Engage.View
{
    public partial class AdvancedSettings : Form
    {
        private const int OffsetMaximum = 5;
        
        public AdvancedSettings()
        {
            InitializeComponent();

            numFutureOffset.Maximum = OffsetMaximum;
            numHistoricOffset.Maximum = OffsetMaximum;
            
            if (Properties.Settings.Default.DefaultOffsets)
            {
                defaultCheckBox();
                checkBox1.Checked = true;
            }

            checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            button1.Click += new EventHandler(button1_Click);
        }

        void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultOffsets = checkBox1.Checked;
            Properties.Settings.Default.HistoricalOffset = (int)numHistoricOffset.Value;
            Properties.Settings.Default.FutureOffset = (int)numFutureOffset.Value;
        }

        void defaultCheckBox()
        {
            numHistoricOffset.Value = 2;
            numFutureOffset.Value = 2;
            //numHistoricOffset.ReadOnly = true;
            //numFutureOffset.ReadOnly = true;
            numHistoricOffset.Enabled = false;
            numFutureOffset.Enabled = false;
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                defaultCheckBox();
            }
            else
            {
                numHistoricOffset.Enabled = true;
                numFutureOffset.Enabled = true;
                // numHistoricOffset.ReadOnly = false;
                //numFutureOffset.ReadOnly = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Outlook().DeleteAll();
        }

        private bool _isDeleteRecycledOpen;


        /// <summary>
        /// Initalizes the settings form.
        /// </summary>
        void InitalizeDeleteRecycledForm()
        {

            if (!_isDeleteRecycledOpen)
            {
                var dr = new DeleteRecycled();
                dr.Closed += dr_Closed;
                
                dr.ShowDialog();
                _isDeleteRecycledOpen = true;
            }
        }

        void dr_Closed(object sender, EventArgs e)
        {
            _isDeleteRecycledOpen = false;
        }


    }
}
