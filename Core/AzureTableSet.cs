using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System.Linq;
using System.Threading.Tasks;

namespace TammyWally.Core
{
    public interface IAzureTableSet<T> where T : ITableEntity
    {
        Task<ITableEntity> GetByRowKey(string rowKey, string partitionKey);
        IEnumerable<ITableEntity> GetAll();
        bool Add(T entity);
        bool AddOrReplace(T entity);
        bool Delete(T entity);
        bool Edit(T entity);
    }
    public class AzureTableSet<T> :
        IAzureTableSet<T> where T : ITableEntity
    {
        private CloudTable Table { get; set; }
        private TableOperation Operation { get; set; }
        private CloudTableClient TableClient { get; set; }
        private string _TableName { get; set; }
        public AzureTableSet(AzureTableBuilder ctx)
        {
            _TableName = typeof(T).Name;

            Table = ctx.TableClient.GetTableReference(_TableName);
            Table.CreateIfNotExistsAsync();
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
        public async Task<ITableEntity> GetByRowKey(string partitionKey, string rowKey)
        {
            Operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var result = await Table.ExecuteAsync(Operation);
            return result.Result as ITableEntity;
        }

        public IEnumerable<ITableEntity> GetAll()
        {
            List<ITableEntity> Data = new List<ITableEntity>();
            TableContinuationToken tableContinuationToken = null;
            TableQuery query = new TableQuery();
            TableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(1), 10);
            var queryResponse = Table.ExecuteQuerySegmentedAsync(query, tableContinuationToken, null, null);
            tableContinuationToken = queryResponse.Result.ContinuationToken;
            Data.AddRange(queryResponse.Result);
            return Data;
        }
        #endregion
    }
}
