using Microsoft.WindowsAzure.Storage;
namespace Tammy_Wally
{
    internal static class AzureSettings
    {
        public static CloudStorageAccount SetupAccount()
        {
            Microsoft.WindowsAzure.Storage.Auth.StorageCredentials cred =
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                     StorageSettings.AzureStorageAccountName,
                    StorageSettings.AzureStorageKey
                    );
            CloudStorageAccount acc = new CloudStorageAccount(cred, StorageSettings.AzureStorageAccountName, StorageSettings.EndPointSuffix, true);
            return acc;
        }
    }
}
