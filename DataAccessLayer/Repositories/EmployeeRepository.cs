using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
	  private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            var employees = await _employeeDbWrapper.FindAsync(e => e.EmployeeCode.Equals(employeeCode));
            return employees?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var existingEmployee = await _employeeDbWrapper.FindAsync(e =>
                e.SiteId.Equals(employee.SiteId) &&
                e.CompanyCode.Equals(employee.CompanyCode) &&
                e.EmployeeCode.Equals(employee.EmployeeCode));

            var itemRepo = existingEmployee?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;

                return await _employeeDbWrapper.UpdateAsync(itemRepo);
            }

            return await _employeeDbWrapper.InsertAsync(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            return await _employeeDbWrapper.UpdateAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            var employees = await _employeeDbWrapper.FindAsync(e => e.EmployeeCode.Equals(employeeCode));
            var itemRepo = employees?.FirstOrDefault();
            if (itemRepo != null)
            {
                return await _employeeDbWrapper.DeleteAsync(itemRepo);
            }

            return false;
        }
    }
}