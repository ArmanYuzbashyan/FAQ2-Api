using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FAQ2_Api.Models;
using Microsoft.JSInterop.Infrastructure;

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

        [HttpGet("{search}")] // api/Faqs/"search", 
        public async Task<ActionResult<List<FAQ>>> GetFAQsSearch(string search)
        {
            var faqs = await AG.GetAllFAQs();
            var fq = new List<FAQ> { };
            await Task.Run(() => 
            {                
                foreach (FAQ f in faqs)
                {
                    if (f.Question.MySearch(search, StringComparison.CurrentCultureIgnoreCase))                 
                        fq.Add(f);                    
                };
            });
            return fq;
        }        

        [HttpPost] // id-n normal Identificator a stex
        public async Task<ActionResult<List<Group>>> PostFAQ(FAQ faq)        
        {
            var a = await AG.GetAllFAQs();
            faq.Id = MakeId.NewId(a);  // !!!!!  

            if (faq.Answer == null || faq.Question == null || faq.GroupId <= 0)
                return BadRequest();
            bool done = false;
            await Task.Run(() => 
            {                
                foreach (Group g in AG.Groups)                
                {
                    if (g.Id == faq.GroupId)
                    {
                        g.FAQs.Add(faq);
                        done = true;
                        break;
                    }
                     
                }
            });
            if (!done )
                return BadRequest();
            else return AG.Groups;
        }

        [HttpDelete("{id}")] // vercnum em FAQ-i id-n u jnjum
        public async Task<string> DeleteFAQ(int id) 
        {
            bool done = false;
            await Task.Run(() => 
            {               
                foreach (Group g in AG.Groups)
                {     
                    foreach (FAQ f in g.FAQs)
                    {
                          if (f.Id == id)
                          {
                              g.FAQs.Remove(f);
                            {
                                done = true;
                                break;
                            }
                          }
                    };
                    if (done)
                        break;
                };
            });
            if (!done) return "not exists";
            else return "deleted";
        }
    

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Group>>> PutFAQ(int id, FAQ fAQ) //
        {
            bool done = false;
            if (fAQ.Answer == null || fAQ.Question == null || fAQ.GroupId <=0 || id<=0)
                return BadRequest();

            await Task.Run(() =>
            {
                          
                foreach (Group g in AG.Groups)
                {
                   if (fAQ.GroupId == g.Id)
                   {                       
                       foreach (FAQ f in g.FAQs)
                       {
                         if (f.Id == id && f.GroupId == fAQ.GroupId)
                         {
                               f.Id = id;
                               f.Question = fAQ.Question;
                               f.Answer = fAQ.Answer;
                                done = true;
                                break;
                         }
                       };                       
                   }
                    if (done)
                        break;
                };
            });
            if (!done) return BadRequest(); 
            else return AG.Groups;
        } 
    }
}
