using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidTools
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void RefreshDeviceList()
        {
            List<string> deviceList = ADBHelper.GetDeviceList();

            comboBoxDevices.Items.Clear();

            if (deviceList.Count == 0)
                return;

            foreach (string curItem in deviceList)
                comboBoxDevices.Items.Add(curItem);

            comboBoxDevices.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshDeviceList();
        }

        private void comboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelDeviceInfo.Text = "Model:" + ADBHelper.GetDeviceModel(comboBoxDevices.SelectedItem.ToString());
        }
    }
}
