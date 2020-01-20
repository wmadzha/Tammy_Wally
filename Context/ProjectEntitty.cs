using System;
using Microsoft.WindowsAzure.Storage.Table;
using Tammy_Wally.Core;
namespace Tammy_Wally.Context
{
    public partial class ProjectEntity : TableEntity
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        public Project()
        {
            this.RowKey = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.UtcNow;
            this.PartitionKey = "Projects";
        }
        public Project(ProjectDTO Data) : this()
        {
            this.ProjectDescription = Data.ProjectDescription;
            this.ProjectName = Data.ProjectName;
            this.ProjectId = Data.ProjectId;
        }
    }
    public static partial class Extensions
    {
        public static AzureTableBuilder MapProjectEntity(this AzureTableBuilder builder, SampleAzureTableContext context)
        {
            context.Project = new AzureTableSet<Project>(builder);
            return builder;
        }
    }
    public partial class SampleAzureTableContext
    {
        public virtual AzureTableSet<ProjectEntity> Project { get; set; }
    }
}
