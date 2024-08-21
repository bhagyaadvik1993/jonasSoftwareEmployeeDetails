using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public async IEnumerable<Company> GetAll()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Company GetByCode(string companyCode)
        {
            return await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode))?.FirstOrDefault();
        }

        public async bool SaveCompany(Company company)
        {
            var itemRepo =await _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return await _companyDbWrapper.Update(itemRepo);
            }

            return await _companyDbWrapper.Insert(company);
        }
        public async bool DeleteCompany(Company company){
            var itemRepo = await _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                return await _companyDbWrapper.DeleteAsync(itemRepo);
            }
            return false;
        }
    }
   

}
