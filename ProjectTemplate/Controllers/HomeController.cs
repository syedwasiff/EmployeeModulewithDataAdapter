﻿using ProjectTemplate.AppCode;
using ProjectTemplate.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult DepartmentPage()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult State()
        {
            return View();
        }
        public ActionResult City()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Employee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubDep(Department getdep)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Department"].ConnectionString);
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Department values(@DepartmentName)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@DepartmentName", getdep.DepartmentName);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }


            return Json(i);

        }

        [HttpPost]
        public ActionResult setState(State SetState)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Department"].ConnectionString);
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("insert into States values(@StateName)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@StateName", SetState.StateName);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }


            return Json(i);

        }

        [HttpPost]
        public ActionResult SetCity(City postCity)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Department"].ConnectionString);
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Cities values(@CityName, @StateName)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CityName", postCity.CityName);
                cmd.Parameters.AddWithValue("@StateName", postCity.StateName);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Json(i);
        }
        [HttpPost]
        public ActionResult EmpData(EmployeeData empDetail)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Department"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Employee values(@FirstName, @LastName, @Address, @MobileNo, @EmailId, @DOB, @DepartmentID, @CityName)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FirstName", empDetail.FirstName);
                cmd.Parameters.AddWithValue("@LastName", empDetail.LastName);
                cmd.Parameters.AddWithValue("@Address", empDetail.Address);
                cmd.Parameters.AddWithValue("@MobileNo", empDetail.MobileNo);
                cmd.Parameters.AddWithValue("@EmailId", empDetail.EmailId);
                cmd.Parameters.AddWithValue("@DOB", empDetail.DateOfBirth);
                cmd.Parameters.AddWithValue("@DepartmentID", empDetail.DepartmentID);
                cmd.Parameters.AddWithValue("@CityName", empDetail.CityName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }


            return Json(empDetail);

        }
        [HttpGet]
        public ActionResult GetEmpData(int Departmentid = 0)
        {

            List<EmployeeData> empdata = new List<EmployeeData>();
            try
            {
                string query = "select Employee.FirstName, Employee.LastName, Employee.Address, Employee.MobileNo, Employee.EmailId, Employee.DateOfBirth, Department.DepartmentName, Employee.CityName from Department inner join Employee on Employee.DepartmentID = Department.DepartmentID Where Department.DepartmentID=@Departmentid or @Departmentid=0;";
                DbLayer db = new DbLayer();
                SqlParameter[] param = {
                    new SqlParameter("@Departmentid", Departmentid)
                };
                DataTable empdt = db.GetData(query, param);
                if (empdt != null && empdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in empdt.Rows)
                    {
                        EmployeeData loadedData = new EmployeeData();
                        loadedData.FirstName = Convert.ToString(dr["FirstName"]);
                        loadedData.LastName = Convert.ToString(dr["LastName"]);
                        loadedData.Address = Convert.ToString(dr["Address"]);
                        loadedData.MobileNo = Convert.ToString(dr["MobileNo"]);
                        loadedData.EmailId = Convert.ToString(dr["EmailId"]);
                        loadedData.DateOfBirth = Convert.ToString(dr["DateOFBirth"]);
                        loadedData.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                        loadedData.CityName = Convert.ToString(dr["CityName"]);
                        empdata.Add(loadedData);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/views/Home/partialview/Employee.cshtml", empdata);
        }
        [HttpGet]
        public ActionResult getdep()
        {
            List<Department> dep = new List<Department>();
            try
            {
                string query = "select * from Department";
                DbLayer db = new DbLayer();
                DataTable depdt = db.GetData(query);
                if (depdt != null && depdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in depdt.Rows)
                    {
                        Department loadedData = new Department();
                        loadedData.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        loadedData.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                        dep.Add(loadedData);
                    };
                };
            }
            catch (Exception ex)
            {

            };
            return PartialView("~/Views/Home/partialView/Department.cshtml", dep);
        }
        [HttpGet]
        public ActionResult loadState()
        {

            List<State> state = new List<State>();
            try
            {
                string query = "select * from States";
                DbLayer db = new DbLayer();
                DataTable depdt = db.GetData(query);
                if (depdt != null && depdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in depdt.Rows)
                    {
                        State loadedState = new State();
                        loadedState.StateId = Convert.ToInt32(dr["StateId"]);
                        loadedState.StateName = Convert.ToString(dr["StateName"]);
                        state.Add(loadedState);
                    };
                };

            }
            catch (Exception ex)
            {

            };
            return PartialView("~/Views/Home/partialView/State.cshtml", state);
        }
        [HttpGet]
        public ActionResult LoadCities(int stateId = 0, string order = "")
        {
            List<City> loadcities = new List<City>();
            try
            {
                string sqlQuery = "SELECT Cities.CityId, Cities.CityName, States.StateName FROM States INNER JOIN Cities on Cities.StateId = States.StateId Where Cities.StateId = @stateId or @stateId=0; ";
                if (order == "asc")
                {
                    sqlQuery = "SELECT Cities.CityId, Cities.CityName, States.StateName FROM States INNER JOIN Cities on Cities.StateId = States.StateId Where Cities.StateId = @stateId or @stateId=0 order by CityName asc; ";
                }
                if (order == "desc")
                {
                    sqlQuery = "SELECT Cities.CityId, Cities.CityName, States.StateName FROM States INNER JOIN Cities on Cities.StateId = States.StateId Where Cities.StateId = @stateId or @stateId=0 order by CityName desc; ";
                }
                DbLayer db = new DbLayer();
                SqlParameter[] param =
                    {
                    new SqlParameter("@stateId", stateId),

                    };
                DataTable depdt = db.GetData(sqlQuery, param);
                if (depdt != null && depdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in depdt.Rows)
                    {
                        City loadedCities = new City();
                        loadedCities.CityId = Convert.ToInt32(dr["CityId"]);
                        loadedCities.CityName = Convert.ToString(dr["CityName"]);
                        loadedCities.StateName = Convert.ToString(dr["StateName"]);
                        loadcities.Add(loadedCities);
                    };
                }                
            }
            catch (Exception ex)
            {
                
            };
            return PartialView("~/Views/Home/partialView/Cities.cshtml", loadcities);
        }
        [HttpGet]
        public ActionResult loadStateforcities()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Department"].ConnectionString);
            List<State> stateforcities = new List<State>();
            try
            {
                SqlCommand getData = new SqlCommand("select * from States order by StateName asc;", con);
                getData.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = getData;
                con.Open();
                DataTable depdt = new DataTable();
                da.Fill(depdt);
                con.Close();

                if (depdt != null && depdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in depdt.Rows)
                    {
                        State loadedStateforcities = new State();
                        loadedStateforcities.StateId = Convert.ToInt32(dr["StateId"]);
                        loadedStateforcities.StateName = Convert.ToString(dr["StateName"]);
                        stateforcities.Add(loadedStateforcities);
                    };
                };
                con.Open();
                getData.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                };
            };
            return Json(stateforcities, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getdepfromemp()
        {
            List<EmployeeData> depdata = new List<EmployeeData>();
            try
            {
                string query = "select * from Department";
                DbLayer db = new DbLayer();
                DataTable empdt = db.GetData(query);
                if (empdt != null && empdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in empdt.Rows)
                    {
                        EmployeeData loadedData = new EmployeeData();
                        loadedData.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        loadedData.DepartmentName = Convert.ToString(dr["DepartmentName"]);

                        depdata.Add(loadedData);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(depdata, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getcitiesfromemp()
        {

            List<City> citydata = new List<City>();
            try
            {
                string query = "select * from Cities order by CityName asc";
                DbLayer db = new DbLayer();
                DataTable empdt = db.GetData(query);
                if (empdt != null && empdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in empdt.Rows)
                    {
                        City loadedData = new City();
                        loadedData.CityId = Convert.ToInt32(dr["CityId"]);
                        loadedData.CityName = Convert.ToString(dr["CityName"]);

                        citydata.Add(loadedData);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(citydata, JsonRequestBehavior.AllowGet);
        }
    };
};