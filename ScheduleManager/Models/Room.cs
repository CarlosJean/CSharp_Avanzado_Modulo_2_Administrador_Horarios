using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Models {
	public class Room {
		public int Id { get; set; }
		[Display(Name = "Descripción")]
		public string Description { get; set; }
		public ICollection<Schedule> Schedules { get; set; }
	}
}