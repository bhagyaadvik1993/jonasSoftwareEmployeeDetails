using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using Serilog;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
         private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
             try
            {
                return await _employeeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting all employees");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeByCodeAsync(string employeeCode)
        {
            try
            {
                return await _employeeRepository.GetByCodeAsync(employeeCode);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting employee by code {EmployeeCode}", employeeCode);
                throw;
            }
        }
        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                return await _employeeRepository.SaveEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while saving employee {EmployeeCode}", employee.EmployeeCode);
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                return await _employeeRepository.UpdateEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while updating employee {EmployeeCode}", employee.EmployeeCode);
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
             try
            {
                return await _employeeRepository.DeleteEmployeeAsync(employeeCode);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting employee {EmployeeCode}", employeeCode);
                throw;
            }
        }

    }
}
