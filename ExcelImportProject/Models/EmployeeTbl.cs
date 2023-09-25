using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportProject.Models
{
    public class EmployeeTbl
    { 
           [Key]
        public int OrganisationNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        public OrganisationTbl Organisation { get; set; }
    }
}

    

