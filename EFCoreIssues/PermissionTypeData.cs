
namespace EFCoreIssues
{
	public class PermissionTypeData
	{
		public string Name { get; private set; }
		public int Level { get; private set; }

		public PermissionTypeData(string name, int level)
		{
			Name = name;
			Level = level;
		}
	}
}
