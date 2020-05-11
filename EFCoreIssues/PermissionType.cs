using System;

namespace EFCoreIssues
{
    public class PermissionType
    {
		public Guid Id { get; private set; }
		public PermissionTypeData PermissionTypeData { get; private set; }

		private PermissionType() { }

		public PermissionType(PermissionTypeData permissionTypeData)
		{
			Id = Guid.NewGuid();
			PermissionTypeData = permissionTypeData;
		}
	}
}
