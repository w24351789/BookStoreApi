using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CountryApp;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{

    public class CountriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            return await Mediator.Send(new GetCountries.Query());
        }
        [HttpGet("{countryid}")]
        public async Task<ActionResult<Country>> GetCountry(int countryid)
        {
            return await Mediator.Send(new GetCountry.Query { CountryId = countryid});
        }
        [HttpGet("authors/{authorid}")]
        public async Task <ActionResult<Country>> GetCountryOfAnAuthor(int authorid)
        {
            return await Mediator.Send(new GetCountryOfAnAuthor.Query { AuthorId = authorid });
        }
        [HttpGet("{countryId}/authors")]
        public async Task <ActionResult<List<Author>>> GetAuthorsFromCountry(int countryid)
        {
            return await Mediator.Send(new GetAuthorsFromCountry.Query { CountryId = countryid });
        }
    }
}