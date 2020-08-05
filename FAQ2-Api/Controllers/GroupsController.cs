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
        
        [HttpGet] // Context.cs-i mej haytararel em static List <group> AG.Groups prop,
                  //dra mej a amboxj informacias
        public  IEnumerable<Group> GetGroups()
        {
            return  AG.Groups;
        }

        [HttpGet("{search}")] // https://localhost:44346/api/Groups/"search" -- filter em anum anunnerov
        public async Task<IEnumerable<Group>> GetFAQsSearch(string search)
        {            
            var gn = new List<Group> { };
            await Task.Run(() => {
                Parallel.ForEach(AG.Groups, g => {
                if (g.GroupName.Contains(search))
                    gn.Add(g);
                });
            });
            return gn;

        }


        //[HttpGet("{id}")] // id-ov search
        //public async Task<IEnumerable<Group>> Get(int id)       
        //{
        //    var group = (IEnumerable<Group>)AG.Groups;
        //    await Task.Run(() =>
        //    {
        //        var tempg = from g in AG.Groups.AsParallel() where g.Id == id select g;
        //        group = tempg;
        //    });

        //    return group;

        //}



        //[HttpPost]  // id-n uxxaki propa stex
        //public async Task<IEnumerable<Group>> PostGroup(Group group)//PostGroup(int id, Group group) 
        //{
        //    await Task.Run(()=> {
        //        //group.Id = id;
        //        AG.Groups.Add(group);

        //    });

        //    return AG.Groups;
        //}


        [HttpPost]  // normal id -ov post em anum trvac group-y AG.Groups-i mej
        public async Task<IEnumerable<Group>> PostGroup(Group group) 
        {
            await Task.Run(() => {
                //group.Id = id;
                group.Id = AG.Groups.Count();
                foreach (FAQ f in group.FAQs)
                {
                    f.GroupId = group.Id;
                }
                AG.Groups.Add(group);

            });

            return AG.Groups;
        }




        [HttpDelete("{id}")] // AG.Groups-ic trvac id-ov groupy jnjum em
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

        [HttpPut("{id}")] // Trvac id-ov group-i anunn em poxum,,
                          //Questionneroi het gorc chunenq stex
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
