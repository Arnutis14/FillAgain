using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    [DataContract]
    public class Shop
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string location { get; set; }
    }
}
