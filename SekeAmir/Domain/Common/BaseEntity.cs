using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
	public abstract class BaseEntity<TKey>
	{
		[Key]
		public TKey Id { get; set; }
		public bool IsActive { get; set; }
		public DateTime DateTime { get; set; }
		public DateTime UpdateDateTime { get; set; }
	}

	public abstract class BaseEntity : BaseEntity<int>
	{

	}
}
