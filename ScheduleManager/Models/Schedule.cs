using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleManager.Models {
	public class Schedule {
		public int Id { get; set; }

		[Display(Name = "Materia")]
		public int SubjectId { get; set; }

		[Display(Name = "Materia")]
		public Subject? Subject { get; set; }

		[Display(Name = "Día de semana")]
		public DayOfWeek DayOfWeek { get; set; }

		[Display(Name = "Hora inicio")]
		public TimeSpan StartTime { get; set; }

		[Display(Name = "Hora fin")]
		public TimeSpan EndTime { get; set; }
		[Display(Name = "Aula")]
		public int RoomId { get; set; }

		[Display(Name = "Aula")]
		public Room? Room { get; set; }
		
		[Display(Name = "Grado")]
		public int GradeId { get; set; }

		[Display(Name = "Grado")]
		public Grade? Grade { get; set; }
		
		[Display(Name = "Maestro")]
		public int TeacherId { get; set; }

		[Display(Name = "Maestro")]
		public Teacher? Teacher { get; set; }


		[NotMapped] // Prevents EF from trying to create a DB column for this
		public string DayOfWeekSpanish =>
		new System.Globalization.CultureInfo("es-ES").DateTimeFormat.GetDayName(DayOfWeek);
	}
}
