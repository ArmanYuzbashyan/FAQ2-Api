using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FAQ2_Api.Models;
using Microsoft.JSInterop.Infrastructure;
using FAQ2_Api.Models.Methods;

namespace FAQ2_Api.Controllers
{
    [Route("api/FAQs")]
    [ApiController]

    public class FAQsController : ControllerBase
    {       
        [HttpGet] // bolor FAQ-eri hamar
        public async Task<ActionResult<List<FAQ>>> GetFAQs()
        {
            return await AG.GetAllFAQs(); //AG.GetAllFAQs-y metoda Context.cs-i mej
        }

        [HttpGet("{p}")] // api/Faqs/"search", 
        public async Task<ActionResult<Page<FAQ>>> GetFAQsSearch(int p)//string search)
        {
            if (p <= 0)
                return BadRequest();
            var ToReturn = await FAQActions.GetFAQsSearch(p);
            if (p > ToReturn.PageCount)
                return BadRequest();
            return ToReturn;
        }        

        [HttpPost] 
        public async Task<ActionResult<List<Group>>> PostFAQ(FAQ faq)        
        {  
            if (faq.Answer == null || faq.Question == null || faq.GroupId <= 0)
                return BadRequest();

            bool done = await FAQActions.PostFAQ(faq, done: false);            
            if (!done )
                return BadRequest();
            else
                return AG.Groups;
        }

        [HttpDelete("{id}")] // vercnum em FAQ-i id-n u jnjum
        public async Task<string> DeleteFAQ(int id) 
        {
            bool done = await FAQActions.DeleteFAQ(id, done: false);            
            if (!done) 
                return "not exists";
            else
                return "deleted";
        }    

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Group>>> PutFAQ(int id, FAQ fAQ) //
        {            
            if (fAQ.Answer == null || fAQ.Question == null || fAQ.GroupId <=0 || id<=0)
                return BadRequest();
            bool done = await FAQActions.PutFAQ(id, fAQ);
            if (!done)
                return BadRequest(); 
            else
                return AG.Groups;
        } 
    }
}
