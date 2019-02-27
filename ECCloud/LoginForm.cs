using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECCloud
{
    public partial class GUI : Form
    {
        private Connector con;
        private Steuerung dieSteuerung;
        private bool status = false;
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
            string User = TB_Username.Text;
            string Password = TB_Password.Text;
            HashAlgorithm sha = new SHA1CryptoServiceProvider();
            int PWHash = Password.GetHashCode();
            string message = con.CallRestMethod("http://localhost:53859/api/CloudAPI/CheckUser?User=" + User + "&PWHash=" + PWHash);
            if (message == "0")
            {
                MessageBox.Show("Please check user or password!");
                
            }
            else
            {
                //this.Close();
                dieSteuerung.Login(Convert.ToInt16(message), 1); //1 stands for connected
            }

            
        }

        private void Init()
        {

        }

        private void GUI_Load(object sender, EventArgs e)
        {
            con = new Connector();
            con.hostName = "127.0.0.1";
            con.hostPort = 53859;
            status = con.establishConnection();
        }
    }
}
