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
    public partial class GUI : Form
    {
        private Steuerung dieSteuerung;
        public GUI()
        {
            InitializeComponent();
            dieSteuerung = new Steuerung(this);
        }

        public void showMessage(string v)
        {
            MessageBox.Show(v);
        }

        public string getUserField()
        {
            return TB_Username.Text.ToString();
        }

        public string getPasswordField()
        {
            return TB_Password.Text.ToString();
        }

        private void B_Login_Click(object sender, EventArgs e)
        {
            dieSteuerung.Login();
        }
    }
}
