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
        public  List<Group> GetGroups()
        {
            return  AG.Groups;
        }

        [HttpGet("{search}")] // "search" -- filter em anum anunnerov
        public async Task<List<Group>> GetFAQsSearch(string search)
        {            
            var gn = new List<Group> { };
            await Task.Run(() =>
            {                
                foreach (Group g in AG.Groups)
                {
                    if (g.GroupName.MySearch(search, StringComparison.CurrentCultureIgnoreCase))
                        gn.Add(g);

                    // primitive variant
                    // var lowG = g.GroupName.ToLower();
                    // var lowS = search.ToLower();
                    // if (lowG.Contains(lowS))
                    //    gn.Add(g);
                };
            });
            return gn;

        }         
        
        [HttpPost]  // normal id -ov post em anum trvac group-y AG.Groups-i mej
        public async Task<ActionResult<List<Group>>> PostGroup(Group group) 
        {
            if (group.GroupName == null)
                return BadRequest();
            bool done = false;   
            await Task.Run(() =>
            {
                List<IdAble> IdAbleGroup = new List<IdAble> { };
                foreach (Group g in AG.Groups)
                {
                    IdAbleGroup.Add(g);
                }
                group.Id = MakeId.NewId(IdAbleGroup);

                AG.Groups.Add(group);//(new Group { GroupName = group.GroupName }); 
                done = true;
            });
            if (done == false)
                return BadRequest();
            else return AG.Groups;
        }

        [HttpDelete("{id}")] // AG.Groups-ic trvac id-ov groupy jnjum em
        public async Task<String> DeleteGroup(int id)
        {
            bool done = false;
            await Task.Run(() => {
                foreach (Group g in AG.Groups)
                {
                    if (g.Id == id)
                    {
                        AG.Groups.Remove(g);
                        done = true;
                        break;
                    }
                };
            });
            if (!done) return "not exists" ;
            else return "deleted";
        }

        [HttpPut("{id}")] // Trvac id-ov group-i anunn em poxum,,
                          //Questionneroi het gorc chunenq stex
        public async Task<ActionResult<List<Group>>> PutGroup(int id, Group group)
        {
            bool done = false;
            if (id <= 0 || group.GroupName == null || group.FAQs.Count != 0) // NO FAQS 
                return BadRequest();

            await Task.Run(() =>
            {                
                foreach (Group g in AG.Groups)
                {
                    if (id == g.Id)
                    {
                        g.GroupName = group.GroupName;                        
                        done = true;
                        break;// NO FAQs !!!!
                    }
                };
            });
            if (!done) return BadRequest();
            else return AG.Groups;
        }              
        
    }
}
