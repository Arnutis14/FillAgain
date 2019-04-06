using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    [DataContract]
    public class Transaction
    {
        [DataMember]
        public Cup cupId { get; set; }

        [DataMember]
        public string amount { get; set; }

        [DataMember]
        public string shopId { get; set; }

        [DataMember]
        public string Time { get; set; }
    }
}
