using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
     public interface ICompanyRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByCodeAsync(string employeeCode);
        Task<bool> SaveEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(string employeeCode);
    }
}