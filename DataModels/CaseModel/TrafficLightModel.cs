using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataModels.CaseModel
{
    [DataContract]
    public class TrafficLightModel: CaseBaseModel
    {
        [DataMember]
        public string TrafficLightIssue { get; set; }

        [DataMember]
        public string LocationDetails { get; set; }
    }
}
