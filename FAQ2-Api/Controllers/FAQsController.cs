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

    public class FAQsController : ControllerBase
    {       
        [HttpGet]
        public async Task<IEnumerable<FAQ>> GetFAQs()
        {
            //AG.GetAllFAQs-y metoda Context.cs-i mej
            return await AG.GetAllFAQs();
        }

        [HttpGet("{id}")]

        public async Task<FAQ> GetFAQ(int id)
        {    //es metody tramabanoren sxala ashxatelu                                          
            var fAQs = await AG.GetAllFAQs();
            var faq = new FAQ();
            await Task.Run(() =>
            {
                Parallel.ForEach(fAQs, f =>
               {
                   if (id == f.Id)
                       faq = f;
               });
            });
            return faq;
        }
                
        [HttpPost]
        public async Task<IEnumerable<Group>> PostFAQ([FromBody] FAQ faq)
        {
            await Task.Run(() => {
                foreach (Group g in AG.Groups)
                {
                    if (g.Id == faq.GroupId)
                        g.FAQs.Add(faq);
                }
            });

            return AG.Groups;
        }
        [HttpPut("{id}")]
        public async Task<FAQ> PutFAQ(int id, FAQ fAQ)
        {
            await Task.Run(() =>
            {
                Parallel.ForEach(AG.Groups, g =>
               {
                   if (fAQ.GroupId == g.Id)
                   {
                       Parallel.ForEach(g.FAQs, f =>
                       {
                         if (f.Id == id && f.GroupId == fAQ.GroupId)
                         {
                               f.Id = fAQ.Id;
                               f.Question = fAQ.Question;
                               f.Answer = fAQ.Answer;
                                                             
                         }
                       });
                   }
               });
            });    
            return fAQ;
        }
                  

            // DELETE api/<FAQsController>/5
            [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
