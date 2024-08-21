using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;
using Serilog;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async IEnumerable<CompanyDto> GetAll()
        {
            try
            {
                 var items = await _companyService.GetAllCompanies();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting all companies");
                return InternalServerError(ex);
            }
           
        }

        // GET api/<controller>/5
        public async CompanyDto Get(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCode(companyCode);
                 return _mapper.Map<CompanyDto>(item);
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting company by code {CompanyCode}", companyCode);
                return InternalServerError(ex);
            }
            
        }

        // POST api/<controller>
        [HttpPost]
        public async IHttpActionResult Post([FromBody]CompanyDto companyDto)
        {
            try{
                if(companyDto == null){
                return BadRequest("Company data is null.")
            }
            var company = await _mapper.Map<Company>(companyDto);
            _companyService.SaveCompany(company);

            return CreatedAtRoute("DefaultAPi", new {id = company.CopanyId }, companyDto);

            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while saving company {CompanyCode}", companyDto.companyCode);
                return InternalServerError(ex);
            }
            
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async IHttpActionResult Put(int id, [FromBody]CompanyDto companyDto)
        {
            try{
                if(companyDto == null || companyDto.CompanyIdv!= id)
                {
                    return BadRequest("Company data is invalid");
                }
                var existingCompany = await _companyService.GetCompanyByCode(id);
                if(existingCompany == null){
                    return NotFound();
                }
                _mapper.Map(companyDto, existingCompany);
                _companyService.SaveCompany(existingCompany); //Since SaveCompany has update functionality

                return StatusCode(HttpStatusCode.NoContent);
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while updating company {CompanyCode}", companyDto.companyCode);
                return InternalServerError(ex);
            }
           
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async IHttpActionResult Delete(int id)
        {
            try{
                var company = await _companyService.GetCompanyByCode(id);
                if(company === null){
                    return NotFound();
                }
            await _companyService.DeleteCompany(id);

                return StatusCode(HttpStatusCode.NoContent);
            }catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting company {CompanyCode}", id);
                return InternalServerError(ex);
            }
            
        }
    }
}