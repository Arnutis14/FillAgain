using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    [DataContract]
    public class Friendship
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string friend1 { get; set; }

        [DataMember]
        public string friend2 { get; set; }

    }
}
