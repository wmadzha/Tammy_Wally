using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System.Linq;

namespace Tammy_Wally.Core
{
    public interface IAzureTableSet<T> where T : TableEntity
    {
        T GetByRowKey(string rowKey);
        T GetByRowKey(string rowKey, string partitionkey);
        IEnumerable<T> GetAll();
        bool Add(T entity);
        bool AddOrReplace(T entity);
        bool Delete(T entity);
        bool Edit(T entity);
    }
    public class AzureTableSet<T> :
        IAzureTableSet<T> where T : TableEntity
    {
        private CloudTable Table { get; set; }
        private TableOperation Operation { get; set; }
        private CloudTableClient TableClient { get; set; }
        private string _TableName { get; set; }
        public AzureTableSet(AzureTableBuilder ctx)
        {
            _TableName = typeof(T).Name;
            Table = ctx.TableClient.GetTableReference(_TableName);
            TableClient = ctx.TableClient;

        }

        #region Crud
        // Todo Change From Void To Result
        public bool Add(T entity)
        {
            try
            {
                Operation = TableOperation.Insert(entity);
                Table.ExecuteAsync(Operation);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool AddOrReplace(T entity)
        {
            try
            {
                Operation = TableOperation.InsertOrReplace(entity);
                Table.ExecuteAsync(Operation);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(T entity)
        {
            try
            {
                Operation = TableOperation.Delete(entity);
                Table.ExecuteAsync(Operation);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public bool Edit(T entity)
        {
            try
            {
                Operation = TableOperation.Replace(entity);
                Table.ExecuteAsync(Operation);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public T GetByRowKey(string rowKey)
        {
            TableQuery<TableEntity> rangeQuery = new TableQuery<TableEntity>().Where(
            TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));
            TableContinuationToken tableContinuationToken = null;
            var queryResponse = Table.ExecuteQuerySegmentedAsync<TableEntity>(rangeQuery, tableContinuationToken, null, null);
            return queryResponse.Result.Results.FirstOrDefault() as T;
        }
        public T GetByRowKey(string rowKey, string partitionkey)
        {
            TableQuery<TableEntity> rangeQuery = new TableQuery<TableEntity>().Where(
                TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition(partitionkey, QueryComparisons.Equal, _TableName),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)));
            TableContinuationToken tableContinuationToken = null;
            var queryResponse = Table.ExecuteQuerySegmentedAsync<TableEntity>(rangeQuery, tableContinuationToken, null, null);
            return queryResponse.Result.Results.FirstOrDefault() as T;
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                List<TableEntity> Data = new List<TableEntity>();
                TableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(1), 10);
                TableContinuationToken tableContinuationToken = null;
                do
                {
                    TableQuery<TableEntity> query = new TableQuery<TableEntity>();
                    try
                    {
                        var queryResponse = Table.ExecuteQuerySegmentedAsync<TableEntity>(query, tableContinuationToken, null, null);
                        tableContinuationToken = queryResponse.Result.ContinuationToken;
                        Data.AddRange(queryResponse.Result);
                    }
                    catch (Exception exquery)
                    {
                        Console.WriteLine(exquery.InnerException);
                        return null;
                    }
                }
                while (tableContinuationToken != null);
                return Data as List<T>;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
