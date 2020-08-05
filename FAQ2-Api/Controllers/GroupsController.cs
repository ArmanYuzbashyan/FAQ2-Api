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
    
    public class GroupsController : ControllerBase
    {   
        
        [HttpGet]
        public  IEnumerable<Group> GetGroups()
        {
            return  AG.Groups;
        }


        [HttpGet("{id}")]
        public async Task<IEnumerable<Group>> Get(int id)       //Fing a group with id matching
        {
            var group = (IEnumerable<Group>)AG.Groups;
            await Task.Run(() =>
            {
                var tempg = from g in AG.Groups.AsParallel() where g.Id == id select g;
                group = tempg;
            });

            return group;

        }
        // POST api/<ValuesController>
        [HttpPost] // [HttpPost("{id}")]
        public async Task<IEnumerable<Group>> PostGroup(Group group)//PostGroup(int id, Group group) 
        {
            await Task.Run(()=> {
                //group.Id = id;
                AG.Groups.Add(group);
            
            });

            return AG.Groups;
        }

        [HttpDelete("{id}")]
        public async void DeleteGroup(int id)
        {
            await Task.Run(() => {
                Parallel.ForEach(AG.Groups, g =>
                {
                    if (g.Id == id)
                        AG.Groups.Remove(g);
                });
            });
        }

        [HttpPut("{id}")]
        public async void PutGroup(int id, Group group)
        {
            await Task.Run(() =>
            {
                Parallel.ForEach(AG.Groups, g =>
                {
                    if (id == g.Id)
                    {
                        g.GroupName = group.GroupName; // NO FAQs !!!!
                    }
                });
            });
        }

                
        
    }
}
