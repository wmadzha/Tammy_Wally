
namespace Tammy_Wally
{
    public static class StorageSettings
    {
        public static string AzureStorageAccountName(bool IsLive)
        { 
            if(IsLive)
              return "LiveDataAzureStorageTableName";
            else
              ret  "DevAzureStorageTableName";
        }
        public static string AzureStorageKey(bool IsLive)
        { 
            if(IsLive)
              return "LiveKey";
            else
              return  "DevKey";
        }
        public static string EndPointSuffix { get { return "core.windows.net"; } }
    }
}
