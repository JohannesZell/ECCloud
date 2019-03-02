using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECCloud
{
    class EncryptionThread
    {
        public static void ThreadProc(CryptoStream cs, String inputFile)
        {
            FileStream fsIn = new FileStream(inputFile, FileMode.Open);
            //Debug.Print(fsIn.ToString());
            byte[] buffer = new byte[1048576];
            int read;
            while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
            {
                cs.Write(buffer, 0, read);
            }
            fsIn.Close();
        }
    }
}
