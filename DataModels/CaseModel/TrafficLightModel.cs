using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataModels.CaseModel
{
    public class TrafficLightModel: CaseBaseModel
    {
        public string TrafficLightIssue { get; set; }
        public string LocationDetails { get; set; }
    }
}
