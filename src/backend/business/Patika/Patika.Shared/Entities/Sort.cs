using Patika.Shared.Enums;

namespace Patika.Shared.Entities
{
	public class Sort
	{
		public string Column { get; set; }
		public SortType SortType { get; set; }
		public override string ToString() => $"{Column}{(SortType == SortType.ASC ? "" : " DESC")}";
	}
}