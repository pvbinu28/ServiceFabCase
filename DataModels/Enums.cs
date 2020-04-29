using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public enum CaseType
    {
        Fraud = 1,
        TrafficLight = 2
    }

    public class CaseCategories
    {
        public const int HighWayIssues = 500;
        public const int CivilIssues = 1500;
        public const int CitizenIssues = 2500;
        public const string Common_Route = "common";

        public const string Case_Route_Fraud = "fraud";
        public const string Case_Rout_TrafficLight = "trafficlight";

    }
}

