
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
namespace Tammy_Wally.Core
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
