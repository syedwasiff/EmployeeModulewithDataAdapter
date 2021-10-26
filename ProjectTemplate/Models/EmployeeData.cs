using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTemplate.Models
{
    public class EmployeeData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string DateOfBirth { get; set; }
        public int DepartmentID{ get; set; }
        public string DepartmentName { get; set; }
        public string CityName { get; set; }

    }
}