using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FAQ2_Api.Models;
using FAQ2_Api.Models.Methods;

namespace  FAQ2_Api.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    
    public class GroupsController : ControllerBase
    {   
        
        [HttpGet] 
        public  List<Group> GetGroups()
        {
            return GroupActions.GetGroups();
        }

        [HttpGet("{search}")] // "search" -- filter em anum anunnerov
        public async Task<List<Group>> GetGroupsSearch(string search)
        {
            return await GroupActions.GetGroupssSearch(search);            
        }         
        
        [HttpPost]  //  post em anum trvac group-y AG.Groups-i mej
        public async Task<ActionResult<List<Group>>> PostGroup(Group group) 
        {
            if (group.GroupName == null)
                return BadRequest();
          
           var done = await GroupActions.PostGroup(group,done:false);
            if (!done)
                return BadRequest();   
            else
                return GroupActions.GetGroups();
        }

        [HttpDelete("{id}")] // AG.Groups-ic trvac id-ov groupy jnjum em
        public async Task<String> DeleteGroup(int id)
        {
            bool done = await GroupActions.DeleteGroup(id, done: false);
            if (!done) return "not exists" ;
            else return "deleted";
        }

        [HttpPut("{id}")] // Trvac id-ov group-i anunn em poxum,,
                          //Questionneroi het gorc chunenq stex
        public async Task<ActionResult<List<Group>>> PutGroup(int id, Group group)
        {            
            if (id <= 0 || group.GroupName == null ) // NO FAQS 
                return BadRequest();

            bool done = await GroupActions.PutGroup(id, group, done: false);
            if (!done)
                return BadRequest();
            else 
                return AG.Groups;
        }             
    }
}
