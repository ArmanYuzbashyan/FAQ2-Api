using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FAQ2_Api.Models;


namespace  FAQ2_Api.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupsController : FirstController
    {
        
        

        [HttpGet]
        public  IEnumerable<Group> GetGroups()
        {
            return  Groups;
        }


        [HttpGet("{id}")]
        public async Task<IEnumerable<Group>> Get(int id)       //Fing a group with id matching
        {
            var group = (IEnumerable<Group>)Groups;
            await Task.Run(() =>
            {
                var tempg = from g in Groups.AsParallel() where g.Id == id select g;
                group = tempg;
            });

            return group;

        }
        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
