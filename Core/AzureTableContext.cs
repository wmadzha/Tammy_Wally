using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace Tammy_Wally.Core
{
    public abstract class AzureTableContext
    {
        public AzureTableBuilder Builder { get; set; }
        public AzureTableContext(AzureStorageAccount acc)
        {
            Builder = new AzureTableBuilder(acc);
        }
        public AzureTableContext(string stoageName, string Key)
        {
            AzureStorageAccount acc = new AzureStorageAccount(stoageName, Key);
            Builder = new AzureTableBuilder(acc);
        }
    }
    public class AzureTableBuilder
    {
        private AzureStorageAccount TableAccount { get; set; }
        public CloudTableClient TableClient { get; set; }
        public AzureTableBuilder(AzureStorageAccount acc)
        {
            this.TableAccount = acc;
            TableClient = this.TableAccount.StorageAccount.CreateCloudTableClient();
        }
    }
}
