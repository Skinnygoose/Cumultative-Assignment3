using CUMULITATIVE_ASSIGN_1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CUMULITATIVE_ASSIGN_1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the Teachers table of our blog database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/SchoolData/Teachers</example>   
        /// <returns>
        /// A list of teacher objects.
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                double salary = Convert.ToDouble(ResultSet["salary"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherid = teacherid;
                NewTeacher.teacherfname = teacherfname;
                NewTeacher.teacherlname = teacherlname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;

                //Add the Teacher Name to the List
                teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return teachers;
        }


        /// <summary>
        /// Returns an individual teacher from the database by specifying the primary key teacherid
        /// </summary>
        /// <param name="id">the teacher ID in the database</param>
        /// <returns>A teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                double salary = Convert.ToDouble(ResultSet["salary"]);


                NewTeacher.teacherid = teacherid;
                NewTeacher.teacherfname = teacherfname;
                NewTeacher.teacherlname = teacherlname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;
            }


            return NewTeacher;
        }


        /// <summary>
        /// Adds an Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the author's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/Teacher/AddTeacher
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"teacherfname":"Christine",
        ///	"teacherlname":"Bittle",
        ///	"teacherid":"22",
        ///	"employeenumber":"T65"
        ///	"hiredate":"11-5-2024"
        ///	"salary":"45"
        ///	
        /// }
        /// </example>

        [HttpPost]
        public string AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            Debug.WriteLine(NewTeacher.teacherfname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, teacherid, employeenumber, hiredate, salary) values (@teacherfname, @teacherlname, @teacherid, @employeenumber, @hiredate, @salary)";
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@teacherid", NewTeacher.teacherid);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

            return "Successfully added teacher";
        }



        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the Teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Debug.WriteLine(AuthorInfo.AuthorFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname, teacherid=@teacherid, employeenumber=@employeenumber, hiredate=@hiredate, salary= @salary where teacherid=@teacherid";
            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.teacherlname);
            ////cmd.Parameters.AddWithValue("@teacherid", TeacherInfo.teacherid);
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", TeacherInfo.hiredate);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.salary);
            cmd.Parameters.AddWithValue("@teacherid", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
    }
}
