namespace BgeniiusUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BirthCountryAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "BirthCountry", c => c.String());
            DropStoredProcedure("dbo.Department_Insert");
            DropStoredProcedure("dbo.Department_Update");
            DropStoredProcedure("dbo.Department_Delete");
            CreateStoredProcedure(
                "dbo.Department_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                        Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        InstructorID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Department]([Name], [Budget], [StartDate], [InstructorID])
                      VALUES (@Name, @Budget, @StartDate, @InstructorID)
                      
                      DECLARE @DepartmentID int
                      SELECT @DepartmentID = [DepartmentID]
                      FROM [dbo].[Department]
                      WHERE @@ROWCOUNT > 0 AND [DepartmentID] = scope_identity()
                      
                      SELECT t0.[DepartmentID], t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );
            
            CreateStoredProcedure(
                "dbo.Department_Update",
                p => new
                    {
                        DepartmentID = p.Int(),
                        Name = p.String(maxLength: 50),
                        Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        InstructorID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Department]
                      SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID
                      WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );
            
            CreateStoredProcedure(
                "dbo.Department_Delete",
                p => new
                    {
                        DepartmentID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Department]
                      WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Department_Delete");
            DropStoredProcedure("dbo.Department_Update");
            DropStoredProcedure("dbo.Department_Insert");
            DropColumn("dbo.Person", "BirthCountry");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
