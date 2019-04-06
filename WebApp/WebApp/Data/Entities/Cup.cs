using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    [DataContract]
    public class Cup
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string limit { get; set; }

        [DataMember]
        public Boolean isBlocked { get; set; }

        [DataMember]
        public string preferedDrink { get; set; }

        [DataMember]
        public string owner { get; set; }
    }
}
