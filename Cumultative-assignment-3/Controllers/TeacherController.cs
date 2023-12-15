using CUMULITATIVE_ASSIGN_1.Models;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUMULITATIVE_ASSIGN_1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teachers/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);


            return View(SelectedTeacher);

        }
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber, DateTime hiredate)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accesse");
            Debug.WriteLine(teacherfname);


            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = teacherfname;
            NewTeacher.teacherlname = teacherlname;

            NewTeacher.employeenumber = employeenumber;
            NewTeacher.hiredate = hiredate;



            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


        //GET : /Teacher/DeleteSelected/{id}
        public ActionResult DeleteSelected(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }






        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }


        //GET : /Teacher/Ajax_Method
        public ActionResult Ajax_Method()
        {
            return View();

        }

        //GET: /Author/Update/{id} 

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the Teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        public ActionResult update(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);


            return View(SelectedTeacher);

        }

        //POST:/Teacher/Update/{id}

        /// <summary>
        /// Receives a POST request containing information about an existing Teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated author.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="teacherfname">The updated first name of the teacher</param>
        /// <param name="teacherlname">The updated last name of the teacher</param>
        /// <param name="employeenumber">The updated employeenumber of the teacher.</param>
        /// <param name="hiredate">The updated hiredate of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the Teaacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"Teacherfname":"Christine",
        ///	"Teacherlname":"Bittle",
        ///	"employeenumber":"T41",
        ///	"Hiredate":"2023-05-02"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id ,string teacherfname, string teacherlname, string employeenumber, DateTime hiredate)
        {

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.teacherfname = teacherfname;
            TeacherInfo.teacherlname = teacherlname;

            TeacherInfo.employeenumber = employeenumber;
            TeacherInfo.hiredate = hiredate;



            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id,TeacherInfo);


            return RedirectToAction("Show/" + id);

        }
    }
}