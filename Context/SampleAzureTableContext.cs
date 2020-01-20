using Microsoft.WindowsAzure.Storage.Table;
using Tammy_Wally.Core;
namespace Tammy_Wally.Context
{
    public partial class SampleAzureTableContext : AzureTableContext
    {
        public SampleAzureTableContext(string storageName, string storageKey) : base(storageName, storageKey)
        {
            MapObjects();
        }
        public SampleAzureTableContext(AzureStorageAccount acc) : base(acc)
        {
            MapObjects();
        }
        private void MapObjects()
        {
            Builder
              .MapProjectEntity(this)
              .MapAnyBusinessModelEntity(this);
        }
    }
}
