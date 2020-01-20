using System;
using Microsoft.WindowsAzure.Storage.Table;
using Tammy_Wally.Core;
namespace Tammy_Wally.Context
{
    public partial class AnyBusinessModelEntity : TableEntity
    {
        public Guid AnyId { get; set; }
        public string AnyName { get; set; }
        public string AnyDescription { get; set; }
        public Project()
        {
            this.RowKey = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.UtcNow;
            this.PartitionKey = "AnyBusinessModel";
        }
        public Project(AnyBusinessModelDTO Data) : this()
        {
            this.AnyName = Data.AnyName;
            this.AnyDescription = Data.AnyDescription;
        }
    }
    public static partial class Extensions
    {
        public static AzureTableBuilder MapAnyBusinessModelEntity(this AzureTableBuilder builder, SampleAzureTableContext context)
        {
            context.AnyBusinessModel = new AzureTableSet<Project>(builder);
            return builder;
        }
    }
    public partial class SampleAzureTableContext
    {
        public virtual AzureTableSet<AnyBusinessModelEntity> AnyBusinessModel { get; set; }
    }
}
