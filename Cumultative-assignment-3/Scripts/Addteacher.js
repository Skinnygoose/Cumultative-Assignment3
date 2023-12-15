// AJAX for author Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : "http://localhost:52095/api/TeacherData/AddTeacher/"

	//with POST data of authorname, bio, email, etc.

	var URL = "http://localhost:52095/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	

	var teacherfname = document.getElementById('teacherfname').value;
	var teacherlname= document.getElementById('teacherlname').value;
	var teacherid = document.getElementById('teacherid').value;
	var employeenumber = document.getElementById('employeenumber').value;
	var hiredate = document.getElementById('hiredate').value;
	var salary = document.getElementById('salary').value;



	var TeacherData = {
		"teacherfname": teacherfname,
		"teacherlname": teacherlname,
		"teacherid": teacherid,
		"employeenumber": employeenumber,
		"hiredate": hiredate,
		"salary": salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.
			alert("Teacher added successfully")

		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}