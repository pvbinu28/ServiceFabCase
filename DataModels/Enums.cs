using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CaseType
    {
        Infrastructure = 1,
        Domestic = 2,
        Immigration = 3
    }

    public class CaseCategories
    {
        public static int HighWayIssues = 1;
        public static int CivilIssues = 2;
        public static int CitizenIssues = 3;
    }
}

