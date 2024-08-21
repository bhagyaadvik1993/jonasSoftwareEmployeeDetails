using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using Serilog;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            try{
                var res = _companyRepository.GetAll();
                return _mapper.Map<IEnumerable<CompanyInfo>>(res);
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting all companies");
                throw;
            }
            
        }

        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            try{
                var result = _companyRepository.GetByCode(companyCode);
                return _mapper.Map<CompanyInfo>(result);
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting company by code {CompanyCode}", companyCode);
                throw;
            }
            
        }
         public async bool SaveCompany(Company company)
        {
            try
            {
                return await _companyRepository.SaveCompany(company);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while saving company {CompanyCode}", company.companyCode);
                throw;
            }
        }
        public async bool DeleteCompany(Company company){
            try{
                return await _companyRepository.DeleteCompany(company);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting company {CompanyCode}", company.companyCode);
                throw;
            }
        }
    }
}
