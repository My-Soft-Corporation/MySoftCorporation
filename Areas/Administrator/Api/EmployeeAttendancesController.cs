using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MySoft.Employee.Entities.Attendance;
using MySoftCorporation.Data.Entities;

namespace MySoftCorporation.Areas.Administrator.Api
{
    public class EmployeeAttendancesController : ApiController
    {
        private MySoftCorporationDbContext db = new MySoftCorporationDbContext();

        // GET: api/EmployeeAttendances
        public IQueryable<EmployeeAttendance> GetEmployeeAttendances()
        {
            return db.EmployeeAttendances;
        }

        // GET: api/EmployeeAttendances/5
        [ResponseType(typeof(EmployeeAttendance))]
        public async Task<IHttpActionResult> GetEmployeeAttendance(int id)
        {
            EmployeeAttendance employeeAttendance = await db.EmployeeAttendances.FindAsync(id);
            if (employeeAttendance == null)
            {
                return NotFound();
            }

            return Ok(employeeAttendance);
        }

        // PUT: api/EmployeeAttendances/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployeeAttendance(int id, EmployeeAttendance employeeAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeAttendance.Id)
            {
                return BadRequest();
            }

            db.Entry(employeeAttendance).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeAttendances
        [ResponseType(typeof(EmployeeAttendance))]
        public async Task<IHttpActionResult> PostEmployeeAttendance(EmployeeAttendance employeeAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeAttendances.Add(employeeAttendance);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = employeeAttendance.Id }, employeeAttendance);
        }

        // DELETE: api/EmployeeAttendances/5
        [ResponseType(typeof(EmployeeAttendance))]
        public async Task<IHttpActionResult> DeleteEmployeeAttendance(int id)
        {
            EmployeeAttendance employeeAttendance = await db.EmployeeAttendances.FindAsync(id);
            if (employeeAttendance == null)
            {
                return NotFound();
            }

            db.EmployeeAttendances.Remove(employeeAttendance);
            await db.SaveChangesAsync();

            return Ok(employeeAttendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeAttendanceExists(int id)
        {
            return db.EmployeeAttendances.Count(e => e.Id == id) > 0;
        }
    }
}