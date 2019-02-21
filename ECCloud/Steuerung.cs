using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ECCloud
{
    class Steuerung
    {
        private GUI dieGUI;
        private int sessionID = 0;
        public Steuerung(GUI pGUI)
        {
            dieGUI = pGUI;
        }

        public void Login(int sessionID, int ConnectionStatus)
        {
            MainWindow mainWindow = new MainWindow();
            this.sessionID = sessionID;
            mainWindow.Show();
            mainWindow.setSessionID(this.sessionID);
            mainWindow.setConnectionStatus(1);
        }

        private void checkUserValid()
        {
            
        }

        private bool checkIfFieldIsEmpty()
        {
            String user = dieGUI.getUserField();
            String pass = dieGUI.getPasswordField();
            Debug.Write(user+pass);
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                Debug.Write("String empty");
                return true;
            }
            return false;
        }
    }
}
