using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataLayer;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenaltyController : ControllerBase
    {
        public readonly iSQL _SQL;

        public PenaltyController(iSQL SQLDataHelper)
        {
            this._SQL = SQLDataHelper;
        }

        // GET: api/Penalty
        [HttpGet]
        public List<Country> Get()
        {
            List<Country> countryList = _SQL.GetCountries();
            return countryList;
        }

        // GET: api/Penalty/5
        [HttpGet("{CountryID}", Name = "Get")]
        public List<Holidays> Get(int countryID)
        {
            List<Holidays> holidayList = _SQL.GetHolidays(countryID);
            return holidayList;
        }

        // POST: api/Penalty
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Penalty/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
