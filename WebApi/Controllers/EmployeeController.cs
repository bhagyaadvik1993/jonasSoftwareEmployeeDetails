using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;
using Serilog;

namespace WebApi.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
             _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async IEnumerable<CompanyDto> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                 return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting all employees");
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{employeeCode}")]
        public async Task<IHttpActionResult> Get(string employeeCode)
         {
            try{
                 var employee = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            if (employee == null)
            {
                return NotFound();
            }
            return  _mapper.Map<CompanyDto>(employee);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting employee by code {EmployeeCode}", employeeCode);
                return InternalServerError(ex);
            }
           
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Employee employee)
        {
            try
            {
                var result = await _employeeService.SaveEmployeeAsync(employee);
                if (result)
                {
                    return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeCode }, employee);
                }
                return BadRequest("Could not save employee");
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while saving employee {EmployeeCode}", employee.EmployeeCode);
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{employeeCode}")]
        public async Task<IHttpActionResult> Put(string employeeCode, [FromBody] Employee employee)
        {
            if (employee.EmployeeCode != employeeCode)
            {
                return BadRequest("Employee code mismatch");
            }
            try{

                var result = await _employeeService.UpdateEmployeeAsync(employee);
                if (result)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return BadRequest("Could not update employee");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while updating employee {EmployeeCode}", employeeCode);
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{employeeCode}")]
        public async Task<IHttpActionResult> Delete(string employeeCode)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(employeeCode);
                if (result)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return BadRequest("Could not delete employee");
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting employee {EmployeeCode}", employeeCode);
                return InternalServerError(ex);
            }
        }
    }
}