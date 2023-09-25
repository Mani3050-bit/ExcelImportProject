using ExcelImportProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportProject.Controller
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelImportController : ControllerBase
    {
        private readonly Appdbcontext _dbcontext;
   
    public  ExcelImportController(Appdbcontext dbcontext)
    {
        _dbcontext=dbcontext;

    }

[HttpPost("import")]
public async Task<ActionResult> ImportExcel(IFormFile file)
        {
            //var package = new ExcelPackage();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (file==null || file.Length==0)
            {
                return BadRequest("Invalid File");
            }
            try
            {
                using (var stream=new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                            {
                        foreach (var worksheet in package.Workbook.Worksheets)
                        {
                            if (worksheet.Name == "OrganisationTbl")
                            {
                                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                                {
                                    //int organisationNumber = int.Parse(worksheet.Cells[row, organisationNumberColumnIndex].Value.ToString());
                                   
                                    if (int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var organisationnumber))
                                    {
                                        int organisationNumber = int.Parse(worksheet.Cells[row, organisationNumberColumnIndex].Value.ToString());

                                        var item = new OrganisationTbl
                                        {


                                            OrganisationNumber = organisationnumber,
                                            OrganisationName = worksheet.Cells[row, 2].Value.ToString(),
                                            AddressLine1 = worksheet.Cells[row, 3].Value.ToString(),
                                            Town = worksheet.Cells[row, 4].Value.ToString(),
                                            AddressLine2 = worksheet.Cells[row, 5].Value.ToString(),
                                            AddressLine3 = worksheet.Cells[row, 6].Value.ToString(),
                                            AddressLine4 = worksheet.Cells[row, 7].Value.ToString(),
                                            Postcode = worksheet.Cells[row, 8].Value.ToString(),
                                            ID = int.Parse(worksheet.Cells[row, 9].Value.ToString()),
                                        };

                                        _dbcontext.OrganisationTbl.Add(item);
                                    }
                                }
                            }

                            else if (worksheet.Name == "EmployeeTbl")
                            {
                                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                                {
                                    var organisationnumber = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                                    var item = new EmployeeTbl
                                    {
                                        OrganisationNumber = organisationnumber,
                                        FirstName = worksheet.Cells[row, 2].Value.ToString(),
                                        LastName = worksheet.Cells[row, 3].Value.ToString(),
                                    };

                                    _dbcontext.EmployeeTbl.Add(item);
                                }
                            }
                            // Add similar code for other tabs/tables
                        }

                        await _dbcontext.SaveChangesAsync();
                    }
                }

                return Ok("Data imported successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
        [HttpGet("OrganisationTbl")]
        [SwaggerOperation("Get data from Table1")]
        public async Task<ActionResult<IEnumerable<OrganisationTbl>>> GetTable1Data()
        {
            var data = await _dbcontext.OrganisationTbl.ToListAsync();
            return Ok(data);
        }

        [HttpGet("EmployeeTbl")]
        [SwaggerOperation("Get data from Table2")]
        public async Task<ActionResult<IEnumerable<EmployeeTbl>>> GetTable2Data()
        {
            var data = await _dbcontext.EmployeeTbl.ToListAsync();
            return Ok(data);
        }
    }
            }
        



