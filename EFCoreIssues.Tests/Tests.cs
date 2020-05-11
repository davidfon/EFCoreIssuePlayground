using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace EFCoreIssues.Tests
{
    public class Tests
    {
        [Fact]
        public void Diff()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CustomDbContext>()
                .UseInMemoryDatabase("in-memory-db");
            using var dbContext = new CustomDbContext(dbContextOptionsBuilder.Options);

            var person = new Person(new PersonName("First", "M", "Last", "First M Last"));
            var permissionType = new PermissionType(new PermissionTypeData("example permission", 1));
            var personPermission = new PersonPermission(person.Id, permissionType.Id);
            dbContext.Add(person);
            dbContext.Add(personPermission);
            dbContext.Add(permissionType);
            dbContext.SaveChanges();

            var result1 = dbContext.PersonPermissions.Where(e => e.Id == personPermission.Id).Select(MapFromDomain1).FirstOrDefault();
            var result2 = dbContext.PersonPermissions.Where(e => e.Id == personPermission.Id).Select(MapFromDomain2).FirstOrDefault();

            //Fails
            Assert.Equal(result1.PersonFirstName, result2.PersonFirstName);         // result2.PersonFirstName == "First M Last"
            Assert.Equal(result1.PersonLastName, result2.PersonLastName);           // result2.PersonFirstName == "M"
            Assert.Equal(result1.PersonMiddleInitial, result2.PersonMiddleInitial); // result2.PersonFirstName == null
            Assert.Equal(result1.PersonFullName, result2.PersonFullName);           // result2.PersonFirstName == "Last"
        }

        public class Result
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public Guid PermissionTypeId { get; set; }
            public string PermissionTypeName { get; set; }
            public int PermissionTypeLevel { get; set; }
            public string PersonFirstName { get; set; }
            public string PersonMiddleInitial { get; set; }
            public string PersonLastName { get; set; }
            public string PersonFullName { get; set; }
        }

        public static Expression<Func<PersonPermission, Result>> MapFromDomain1
        {
            get
            {
                return entity => new Result
                {
                    Id = entity.Id,
                    PersonId = entity.PersonId,
                    PermissionTypeId = entity.PermissionTypeId,
                    PermissionTypeName = entity.PermissionType.PermissionTypeData.Name,
                    PermissionTypeLevel = entity.PermissionType.PermissionTypeData.Level,
                    PersonFirstName = entity.Person.PersonName.FirstName,
                    PersonMiddleInitial = entity.Person.PersonName.MiddleInitial,
                    PersonLastName = entity.Person.PersonName.LastName,
                    PersonFullName = entity.Person.PersonName.FullName,
                };
            }
        }

        public static Expression<Func<PersonPermission, Result>> MapFromDomain2
        {
            get
            {
                return entity => new Result
                {
                    Id = entity.Id,
                    PersonId = entity.PersonId,
                    PersonFirstName = entity.Person.PersonName.FirstName,
                    PersonMiddleInitial = entity.Person.PersonName.MiddleInitial,
                    PersonLastName = entity.Person.PersonName.LastName,
                    PersonFullName = entity.Person.PersonName.FullName,
                    PermissionTypeId = entity.PermissionTypeId,
                    PermissionTypeName = entity.PermissionType.PermissionTypeData.Name,
                    PermissionTypeLevel = entity.PermissionType.PermissionTypeData.Level,
                };
            }
        }
    }
}
