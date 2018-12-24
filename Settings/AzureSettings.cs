using Microsoft.WindowsAzure.Storage;
namespace Tammy_Wally
{
    internal static class AzureSettings
    {
        public static CloudStorageAccount SetupAccount()
        {
            Microsoft.WindowsAzure.Storage.Auth.StorageCredentials cred =
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                     StorageSettings.AzureStorageAccountName(false),
                    StorageSettings.AzureStorageKey(false)
                    );
            CloudStorageAccount acc = new CloudStorageAccount(cred, StorageSettings.AzureStorageAccountName(false), StorageSettings.EndPointSuffix, true);
            return acc;
        }
    }
}
