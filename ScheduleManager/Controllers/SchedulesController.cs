using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers {
	public class SchedulesController : Controller {
		private readonly ScheduleManagerContext _context;

		public SchedulesController(ScheduleManagerContext context) {
			_context = context;
		}

		// GET: Schedules
		public async Task<IActionResult> Index() {
			var scheduleManagerContext = _context.Schedule.Include(s => s.Grade).Include(s => s.Room).Include(s => s.Subject).Include(s => s.Teacher);


			return View(await scheduleManagerContext.ToListAsync());
		}

		// GET: Schedules/Details/5
		public async Task<IActionResult> Details(int? id) {
			if (id == null) {
				return NotFound();
			}

			var schedule = await _context.Schedule
				.Include(s => s.Grade)
				.Include(s => s.Room)
				.Include(s => s.Subject)
				.Include(s => s.Teacher)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (schedule == null) {
				return NotFound();
			}

			return View(schedule);
		}

		// GET: Schedules/Create
		public IActionResult Create() {
			ViewData["GradeId"] = new SelectList(_context.Set<Grade>(), "Id", "Description");
			ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Description");
			ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "Id", "Description");
			ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Fullname");

			var daysOfWeeks = new[] {
				new { Id = Convert.ToInt32(DayOfWeek.Monday),  DayOfWeek = "Lunes"},
				new { Id = Convert.ToInt32(DayOfWeek.Tuesday),  DayOfWeek = "Martes"},
				new { Id = Convert.ToInt32(DayOfWeek.Wednesday),  DayOfWeek = "Miércoles"},
				new { Id = Convert.ToInt32(DayOfWeek.Thursday),  DayOfWeek = "Jueves"},
				new { Id = Convert.ToInt32(DayOfWeek.Friday),  DayOfWeek = "Viernes"}
			};

			ViewData["DaysOfWeek"] = new SelectList(daysOfWeeks.ToList(), "Id", "DayOfWeek");


			return View();
		}

		// POST: Schedules/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,SubjectId,DayOfWeek,StartTime,EndTime,RoomId,GradeId,TeacherId")] Schedule schedule) {

			bool isThereCoincidence = _context.Schedule.Any(s =>
				s.DayOfWeek == schedule.DayOfWeek &&
				s.StartTime < schedule.EndTime &&
				s.EndTime > schedule.StartTime);

			if (isThereCoincidence) {
				// Agregamos el mensaje en español al ModelState
				ModelState.AddModelError(string.Empty, "El horario seleccionado coincide con otra clase ya programada en el aula seleccionada.");
			}

			var teachersWeeklyHours = _context
				.Schedule
				.Where(s => s.TeacherId == schedule.TeacherId).ToList()
				.Sum(s => (s.EndTime - s.StartTime).TotalHours) + schedule.EndTime.Subtract(schedule.StartTime).TotalHours;

			var teacher = await _context.Teacher.FirstAsync(t => t.Id == schedule.TeacherId);

			if (teachersWeeklyHours > teacher.MaxHours) {
				ModelState.AddModelError(string.Empty, "Este horario hace que se exceda la cantidad de horas máximas del maestro seleccionado.");
			}

			if (ModelState.IsValid) {
				_context.Add(schedule);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["GradeId"] = new SelectList(_context.Set<Grade>(), "Id", "Description");
			ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Description");
			ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "Id", "Description");
			ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Fullname");

			var daysOfWeeks = new[] {
				new { Id = Convert.ToInt32(DayOfWeek.Monday),  DayOfWeek = "Lunes"},
				new { Id = Convert.ToInt32(DayOfWeek.Tuesday),  DayOfWeek = "Martes"},
				new { Id = Convert.ToInt32(DayOfWeek.Wednesday),  DayOfWeek = "Miércoles"},
				new { Id = Convert.ToInt32(DayOfWeek.Thursday),  DayOfWeek = "Jueves"},
				new { Id = Convert.ToInt32(DayOfWeek.Friday),  DayOfWeek = "Viernes"}
			};

			ViewData["DaysOfWeek"] = new SelectList(daysOfWeeks.ToList(), "Id", "DayOfWeek", schedule.DayOfWeek);
			return View(schedule);
		}

		// GET: Schedules/Edit/5
		public async Task<IActionResult> Edit(int? id) {
			if (id == null) {
				return NotFound();
			}

			var schedule = await _context.Schedule
				.Include(s => s.Grade)
				.Include(s => s.Room)
				.Include(s => s.Subject)
				.Include(s => s.Teacher)
				.FirstOrDefaultAsync(s => s.Id == id);

			if (schedule == null) {
				return NotFound();
			}

			var daysOfWeeks = new[] {
				new { Id = Convert.ToInt32(DayOfWeek.Monday),  DayOfWeek = "Lunes"},
				new { Id = Convert.ToInt32(DayOfWeek.Tuesday),  DayOfWeek = "Martes"},
				new { Id = Convert.ToInt32(DayOfWeek.Wednesday),  DayOfWeek = "Miércoles"},
				new { Id = Convert.ToInt32(DayOfWeek.Thursday),  DayOfWeek = "Jueves"},
				new { Id = Convert.ToInt32(DayOfWeek.Friday),  DayOfWeek = "Viernes"}
			};

			ViewData["DaysOfWeek"] = new SelectList(daysOfWeeks.ToList(), "Id", "DayOfWeek", daysOfWeeks[Convert.ToInt32(schedule.DayOfWeek) - 1]);

			ViewData["GradeId"] = new SelectList(_context.Set<Grade>(), "Id", "Description", schedule.Grade.Description);
			ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Description", schedule.Room.Description);
			ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "Id", "Description", schedule.Subject.Description);
			ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Fullname", schedule.Teacher.Fullname);
			return View(schedule);
		}

		// POST: Schedules/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectId,DayOfWeek,StartTime,EndTime,RoomId,GradeId,TeacherId")] Schedule schedule) {
			if (id != schedule.Id) {
				return NotFound();
			}

			bool isThereCoincidence = _context.Schedule.Any(s =>
				s.TeacherId != schedule.TeacherId &&
				s.DayOfWeek == schedule.DayOfWeek &&
				s.StartTime < schedule.EndTime &&
				s.EndTime > schedule.StartTime);

			if (isThereCoincidence) {
				// Agregamos el mensaje en español al ModelState
				ModelState.AddModelError(string.Empty, "El horario seleccionado coincide con otra clase ya programada para este profesor.");
			}

			var teachersWeeklyHours = _context
				.Schedule
				.Where(s => s.TeacherId == schedule.TeacherId).ToList()
				.Sum(s => (s.EndTime - s.StartTime).TotalHours) + schedule.EndTime.Subtract(schedule.StartTime).TotalHours;

			var teacher = await _context.Teacher.FirstAsync(t => t.Id == schedule.TeacherId);

			if (teachersWeeklyHours > teacher.MaxHours) {
				ModelState.AddModelError(string.Empty, "Este horario hace que se exceda la cantidad de horas máximas del maestro seleccionado.");
			}

			if (ModelState.IsValid) {
				try {
					_context.Update(schedule);
					await _context.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException) {
					if (!ScheduleExists(schedule.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			schedule = await _context.Schedule
				.Include(s => s.Grade)
				.Include(s => s.Room)
				.Include(s => s.Subject)
				.Include(s => s.Teacher)
				.FirstOrDefaultAsync(s => s.Id == id);


			ViewData["GradeId"] = new SelectList(_context.Set<Grade>(), "Id", "Description", schedule.Grade.Description);
			ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Description", schedule.Room.Description);
			ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "Id", "Description", schedule.Subject.Description);
			ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Fullname", schedule.Teacher.Fullname);

			var daysOfWeeks = new[] {
				new { Id = Convert.ToInt32(DayOfWeek.Monday),  DayOfWeek = "Lunes"},
				new { Id = Convert.ToInt32(DayOfWeek.Tuesday),  DayOfWeek = "Martes"},
				new { Id = Convert.ToInt32(DayOfWeek.Wednesday),  DayOfWeek = "Miércoles"},
				new { Id = Convert.ToInt32(DayOfWeek.Thursday),  DayOfWeek = "Jueves"},
				new { Id = Convert.ToInt32(DayOfWeek.Friday),  DayOfWeek = "Viernes"}
			};

			ViewData["DaysOfWeek"] = new SelectList(daysOfWeeks.ToList(), "Id", "DayOfWeek", schedule.DayOfWeek);

			ViewData["DayOfWeek"] = new SelectList(_context.Set<Subject>(), "Id", "DayOfWeek", schedule.DayOfWeek);
			return View(schedule);
		}

		// GET: Schedules/Delete/5
		public async Task<IActionResult> Delete(int? id) {
			if (id == null) {
				return NotFound();
			}

			var schedule = await _context.Schedule
				.Include(s => s.Grade)
				.Include(s => s.Room)
				.Include(s => s.Subject)
				.Include(s => s.Teacher)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (schedule == null) {
				return NotFound();
			}

			return View(schedule);
		}

		// POST: Schedules/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id) {
			var schedule = await _context.Schedule.FindAsync(id);
			if (schedule != null) {
				_context.Schedule.Remove(schedule);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ScheduleExists(int id) {
			return _context.Schedule.Any(e => e.Id == id);
		}
	}
}
