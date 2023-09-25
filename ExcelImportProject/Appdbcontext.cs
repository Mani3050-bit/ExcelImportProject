using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExcelImportProject.Models;

namespace ExcelImportProject
{
    public class Appdbcontext:DbContext
    {
        public DbSet<EmployeeTbl> EmployeeTbl { get; set; }
        public DbSet<OrganisationTbl> OrganisationTbl { get; set; }

        public Appdbcontext(DbContextOptions<Appdbcontext> options)
            : base(options)
        {
        }


    }
}
