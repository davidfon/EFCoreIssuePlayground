using System;

namespace EFCoreIssues
{
    public class PersonPermission
    {
        public Guid Id { get; private set; }
        public Guid PermissionTypeId { get; private set; }
        public PermissionType PermissionType { get; private set; }
        public Guid PersonId { get; private set; }
        public Person Person { get; private set; }
        
        private PersonPermission()
        {

        }

        public PersonPermission(Guid employeeId, Guid permissionTypeId)
        {
            Id = Guid.NewGuid();
            PersonId = employeeId;
            PermissionTypeId = permissionTypeId;
        }
    }
}
