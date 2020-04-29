using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataModels.CaseModel
{
    public class FraudCaseModel: CaseBaseModel
    {
       [DataMember]
       public string FullName { get; set; }

        [DataMember]
        public string DetailsAboutPerson { get; set; }
    }
}
