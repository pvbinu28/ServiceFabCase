using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public interface IDbSettings
    {
        string OrderCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class DbSettingsModel : IDbSettings
    {
        public string OrderCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
