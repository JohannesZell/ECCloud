using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace EccCloudWeb
    
{
    public class Test : Attribute
    {
        [WebMethod]
        public static void TestMethod()
        {
            
        }
    }
}
