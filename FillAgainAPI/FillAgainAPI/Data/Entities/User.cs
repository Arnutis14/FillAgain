using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string limit { get; set; }

        [DataMember]
        public string jsonData { get; set; }

    }

}
