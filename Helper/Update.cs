using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
namespace Tammy_Wally
{
    internal static class Update
    {
        public static async Task<bool> SingleOperationAsync<T>(string TableName, string OperationName, string ID) where T : TableEntity, new()
        {
            try
            {
                CloudStorageAccount storageAccount = AzureSettings.SetupAccount();
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                tableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(1), 10);
                CloudTable table = tableClient.GetTableReference(TableName);
                TableOperation retrieveOperation = TableOperation.Retrieve<T>(TableName, ID);

                TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);
                if (retrievedResult != null)
                {
                    var Entity = PerformOperation(OperationName, (T)retrievedResult.Result);
                    TableOperation updateOperation = TableOperation.InsertOrReplace((T)Entity);
                    await  table.ExecuteAsync(updateOperation);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Result = ex.Message;
                return false;
            }
        }
        private static TableEntity PerformOperation(string OperationName, TableEntity Data)
        {
            if(OperationName == "Default")
            {
                TableEntity tbl = new TableEntity();
                return tbl;
            }
            else
            {
                TableEntity tbl = new TableEntity();
                return tbl;
            }
        }



    }
}
