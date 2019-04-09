using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
namespace Tammy_Wally
{
    internal static class Get
    {
        public static class List
        {
            public async static Task< List<T>> All<T>(string TableName) where T : TableEntity, new()
            {
                try
                {
                    List<T> Data = new List<T>();
                    CloudStorageAccount storageAccount = AzureSettings.SetupAccount();
                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                    tableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(1), 10);
                    CloudTable table = tableClient.GetTableReference(TableName);                   
                    //await table.CreateIfNotExistsAsync();
                    TableContinuationToken tableContinuationToken = null;
                    do
                    {
                        TableQuery<T> query = new TableQuery<T>();
                        try
                        {
                            var queryResponse = await table.ExecuteQuerySegmentedAsync<T>(query, tableContinuationToken, null, null);
                            tableContinuationToken = queryResponse.ContinuationToken;
                            Data.AddRange(queryResponse.Results);
                        }
                        catch (Exception exquery)
                        {
                            Console.WriteLine(exquery.InnerException);
                            return null;
                        }
                    }
                    while (tableContinuationToken != null);
                    return Data;
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                    return null;
                }
            }
        }
        public static class Single
        {
            public async static Task<T> Data<T>(string TableName, string PropertyName, string RowKey) where T : TableEntity, new()
            {
                TableQuery<T> rangeQuery = new TableQuery<T>().Where(
                TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, TableName),
                TableOperators.And,
                TableQuery.GenerateFilterCondition(PropertyName, QueryComparisons.Equal, RowKey)));
                CloudStorageAccount storageAccount = AzureSettings.SetupAccount();
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference(TableName);
                await table.CreateIfNotExistsAsync();
                TableContinuationToken tableContinuationToken = null;
                var queryResponse = await table.ExecuteQuerySegmentedAsync<T>(rangeQuery, tableContinuationToken, null, null);
                return queryResponse.Results.FirstOrDefault();
            }
        }
    }
}
