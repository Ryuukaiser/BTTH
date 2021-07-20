using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btlab4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace btlab4.Controllers
{
    public class CoursesController : Controller
    {
        BigSchoolContext context = new BigSchoolContext();
        // GET: Courses
        public ActionResult Create()

        {
          
            Course objCourse = new Course();
            objCourse.ListCategory = context.Categories.ToList();
            return View(objCourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objCourse)
        {
            ModelState.Remove("LecturerId");
            if(!ModelState.IsValid)
            {
                objCourse.ListCategory = context.Categories.ToList();
                return View("Create", objCourse);

            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LecturerId = user.Id;
            context.Courses.Add(objCourse);
            context.SaveChanges();
            return RedirectToAction("index", "Home");

        }

        public ActionResult Attending()
        {
             ApplicationUser currentUser  = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
             var listAttendances = context.Attendances.Where(p => p.Attendee == currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach (Attendance temp in listAttendances)
            {
                Course objCourse = temp.Course;
                objCourse.LectureName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LecturerId).Name;
                courses.Add(objCourse);
            }   
            return View(courses);
        }
        public ActionResult Mine()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var courses = context.Courses.Where(c => c.LecturerId == currentUser.Id && c.DateTime > DateTime.Now).ToList(); 
            foreach (Course i in courses)
            {
                i.LectureName = currentUser.Name;
            }
            return View(courses);
        }
        public ActionResult DeleteMine(int id)
        {
            BigSchoolContext context = new BigSchoolContext();

            Course deletedCourse = context.Courses.FirstOrDefault(p => p.Id == id);

            return View(deletedCourse);
        }
        [HttpPost]
        public ActionResult DeleteMine(Course x)
        {
            BigSchoolContext context = new BigSchoolContext();

            Attendance deletedAttendance = context.Attendances.FirstOrDefault(p => p.CourseId == x.Id);
            if (deletedAttendance != null)
            {
                context.Attendances.Remove(deletedAttendance);
                context.SaveChanges();
            }

            Course deletedCourse = context.Courses.FirstOrDefault(p => p.Id == x.Id);
            if (deletedCourse != null)
            {
                context.Courses.Remove(deletedCourse);
                context.SaveChanges();
            }

            return RedirectToAction("Mine", "Courses");
        }
        public ActionResult EditMine(int id)
        {

            BigSchoolContext context = new BigSchoolContext();
            Course editCourse = context.Courses.FirstOrDefault(p => p.Id == id);

            if (editCourse != null)
            {
                editCourse.ListCategory = context.Categories.ToList();
            }
            return View(editCourse);
        }
        [HttpPost]
        public ActionResult EditMine([Bind(Include = "Id, Place, Datetime, CategoryId")] Course x)
        {
            BigSchoolContext context = new BigSchoolContext();
            Course editCourse = context.Courses.FirstOrDefault(p => p.Id == x.Id);
            if (editCourse != null)
            {
                editCourse.Place = x.Place;
                editCourse.DateTime = x.DateTime;
                editCourse.CatergoryId = x.CatergoryId;
                context.SaveChanges();
            }
            return RedirectToAction("Mine", "Courses");
        }

        public ActionResult LectureIamGoing()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());     
            var listFollwee = context.Followings.Where(p => p.FollowerId ==currentUser.Id).ToList();
            var listAttendances = context.Attendances.Where(p => p.Attendee ==currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach (var course in listAttendances)
            {
                foreach (var item in listFollwee)
                {
                    if (item.FolloweeId == course.Course.LecturerId)
                    {
                        Course objCourse = course.Course;
                        objCourse.LectureName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LecturerId).Name;
                        courses.Add(objCourse);
                    }
                }

            }
            return View(courses);
        }
    }
}