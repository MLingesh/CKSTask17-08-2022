using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CKSTask.Models
{
    public class ResultResponse
    {
        public string ID { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public object[] result { get; set; }
    }
}