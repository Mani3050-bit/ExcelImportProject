using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportProject.Models
{
    public class OrganisationTbl
    {
     [Key]
        public int OrganisationNumber { get; set; }
        public string OrganisationName { get; set; }
        public string AddressLine1  { get; set; }
        public string Town { get; set; }
        public string AddressLine2  { get; set; }
        public string AddressLine3   { get; set; }
        public string AddressLine4   { get; set; }
        public string Postcode { get; set; }
        public int ID { get; set; }
        public ICollection<EmployeeTbl> Employees { get; set; }

    }

   

}

