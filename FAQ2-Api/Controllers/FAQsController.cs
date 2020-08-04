using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FAQ2_Api.Models;


namespace FAQ2_Api.Controllers
{
    [Route("api/FAQs")]
    [ApiController]
    public class FAQsController : FirstController
    {
        // GET: api/<FAQsController>
        [HttpGet]
        public   async Task<IEnumerable<FAQ>> GetFAQs() //verncum em bolor group-eri Q,A-nery,
        {                                               //miaclnum em mi Listi mej u veradardznum ayd Listy
            var fAQs = new List<FAQ> { };
            await Task.Run(() =>
            {
                Parallel.ForEach(Groups, g => 
                {
                    fAQs.AddRange(g.FAQs);
                });
            });
            return fAQs;
        }

        // GET api/<FAQsController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<FAQ>> GetFAQ(int id, int groupId)
        {
            IEnumerable<FAQ> fAQ = new List<FAQ> { };  // gitem, vor petq chi senc cast-er anel
            var fAQs = new List<FAQ> { };              // bayc de senc stacvav  ¯\_(ツ)_/¯

            await Task.Run(() =>
            {
                Parallel.ForEach(Groups, g =>
                {
                    fAQs.AddRange(g.FAQs);
                });
                fAQ = from f in fAQs.AsParallel()
                            where id == f.Id && groupId == f.GroupId
                            select f;                
            });
            return fAQ;
        }

        // POST api/<FAQsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FAQsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FAQsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
