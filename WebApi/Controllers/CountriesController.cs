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
        [HttpGet("{countryId}")]
        public async Task<ActionResult<Country>> GetCountry(int countryId)
        {
            return await Mediator.Send(new GetCountry.Query { CountryId = countryId});
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

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCountry(CreateCountry.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{countryId}")]
        public async Task <ActionResult<Unit>> DeleteCountry(int countryId)
        {
            return await Mediator.Send(new DeleteCountry.Command { CountryId = countryId });
        }
    }
}