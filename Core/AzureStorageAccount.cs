
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
namespace PenelopePie.Data.AzureTable.Core
{
    public class AzureStorageAccount
    {
        private StorageCredentials _Cred { get; set; }
        public CloudStorageAccount StorageAccount { get; set; }
        public AzureStorageAccount(string storageName, string storageAccountKey)
        {
            this._Cred = new StorageCredentials(storageName, storageAccountKey);
            this.StorageAccount = new CloudStorageAccount(this._Cred, storageName, "core.windows.net", true);
        }
    }
}
