using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
namespace Tammy_Wally
{
    internal static class Add
    {
        public static bool New(string TableName, ITableEntity DataModel)
        {
            CloudStorageAccount sa = AzureSettings.SetupAccount();
            CloudTableClient tc = sa.CreateCloudTableClient();
            CloudTable ct = tc.GetTableReference(TableName);
            ct.CreateIfNotExistsAsync();            
            TableOperation b = TableOperation.InsertOrReplace(DataModel);
            try
            {
                ct.ExecuteAsync(b);
                return true;
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                // Call Logger
                return false;
            }
        }
    }
}
