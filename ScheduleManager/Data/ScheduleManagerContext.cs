using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Models;

namespace ScheduleManager.Data {
	public class ScheduleManagerContext : DbContext {
		public ScheduleManagerContext(DbContextOptions<ScheduleManagerContext> options)
			: base(options) {
		}

		public DbSet<ScheduleManager.Models.Schedule> Schedule { get; set; } = default!;
	    public DbSet<ScheduleManager.Models.Grade> Grade { get; set; } = default!;
	    public DbSet<ScheduleManager.Models.Subject> Subject { get; set; } = default!;
	    public DbSet<ScheduleManager.Models.Teacher> Teacher { get; set; } = default!;
	}
}
