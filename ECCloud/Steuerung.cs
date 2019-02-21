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
        public Steuerung(GUI pGUI)
        {
            dieGUI = pGUI;
        }

        public void Login()
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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
