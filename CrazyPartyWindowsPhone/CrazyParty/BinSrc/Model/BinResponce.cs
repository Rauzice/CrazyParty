using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CrazyParty.BinSrc.Model
{
    [DataContract]
    public class BinResponce
    {
        [DataMember]
        public int code { get; set; }
    }
}
