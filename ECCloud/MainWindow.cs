using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECCloud
{
    public partial class MainWindow : Form
    {
        private bool status;
        private Connector con;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

            /*
            con = new Connector(44358, "175.0.0.1");
            status = con.establishConnection();
            if (status == false)
            {
                MessageBox.Show("Not able to Connect to Server!");
                toolStripConnection.Text = "Not connected!";
            }
            */
        }
    }
}
