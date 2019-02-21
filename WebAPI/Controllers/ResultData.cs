using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPI.Controllers
{
    public class ResultData : Exception
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
