using PenelopePie.Data.AzureTable.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tammy_Wally.BusinessLogic
{
    public class Application
    {
        public Application(SampleAzureTableContext NoSqldbContext)
        {
            // Sample Add
            NoSqldbContext.AnyBusinessModel.Add(new AnyBusinessModelEntity() { AnyId = Guid.NewGuid() });
            // Sample Get
          var allProjects = NoSqldbContext.Project.GetAll();

        }
    }
}
