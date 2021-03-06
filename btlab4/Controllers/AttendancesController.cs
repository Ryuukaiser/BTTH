using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using btlab4.Models;
using Microsoft.AspNet.Identity;

namespace btlab4.Controllers
{
    public class AttendancesController : ApiController
    {
        BigSchoolContext context = new BigSchoolContext();

       [HttpPost]
       public IHttpActionResult Attend(Course attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            if (context.Attendances.Any(p => p.Attendee == userID && p.CourseId == attendanceDto.Id))
            {
                // return BadRequest("The attendance already exists!");
                context.Attendances.Remove(context.Attendances.SingleOrDefault(p =>p.Attendee == userID && p.CourseId == attendanceDto.Id));
                context.SaveChanges();
                return Ok("cancel");
            }
            var attendance = new Attendance() { CourseId = attendanceDto.Id, Attendee = User.Identity.GetUserId() };
            context.Attendances.Add(attendance);
            context.SaveChanges();
            return Ok();
        }
    }
}
