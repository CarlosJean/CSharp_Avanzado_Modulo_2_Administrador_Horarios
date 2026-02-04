using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Models {
	public class Teacher {
		public int Id { get; set; }

		[Display(Name = "Nombre completo")]
		public string Fullname { get; set; }
		public ICollection<Schedule> Schedules { get; set; }
	}
}