using DataModels.CaseModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataModels
{
    [DataContract]
    [KnownType(typeof(FraudCaseModel))]
    public class CaseBaseModel
    {
        [DataMember]
        [BsonId]
        public string Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int CaseTypeId { get; set; }

        [DataMember]
        public string CaseType { get; set; }

        [DataMember]
        public int StatusId { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public List<History> History { get; set; }
    }

    [DataContract]
    public class History
    {
        [DataMember]
        public DateTime StatusDate { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int StatusId { get; set; }
    }
}
