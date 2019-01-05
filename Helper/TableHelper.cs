using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
namespace Tammy_Wally
{
    internal static class CloudTableClientHelper
    {
        public static CloudTable New(CloudStorageAccount sa, string TableName)
        {
            CloudTableClient tc = sa.CreateCloudTableClient();
            CloudTable ct = tc.GetTableReference(TableName);
            ct.CreateIfNotExistsAsync();    
            return ct;
        }
    }
}
