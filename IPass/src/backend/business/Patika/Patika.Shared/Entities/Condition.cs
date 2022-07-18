using Patika.Shared.Enums;
using System.Collections.Generic;

namespace Patika.Shared.Entities
{
	public class Condition
	{
		public string PropertyName { get; set; }
		public ConditionOperator Operator { get; set; } = ConditionOperator.Equal;
		public List<string> Values { get; set; } = new List<string>();
	}
}