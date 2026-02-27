using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Models {
	public class Teacher {
		public int Id { get; set; }

		[Display(Name = "Nombre completo")]
		public string Fullname { get; set; }
		[Display(Name = "Especialidad")]
		public string Speciality { get; set; }
		[Display(Name = "Cantidad de horas máximas")]
		public int MaxHours { get; set; }
		public ICollection<Schedule>? Schedules { get; set; }
	}
}