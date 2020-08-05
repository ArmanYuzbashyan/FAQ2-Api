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
        [HttpGet] // bolor FAQ-eri hamar
        public async Task<List<FAQ>> GetFAQs()
        {
            return await AG.GetAllFAQs(); //AG.GetAllFAQs-y metoda Context.cs-i mej
        }

        [HttpGet("{search}")] // api/Faqs/"search", 
        public async Task<List<FAQ>> GetFAQsSearch(string search)
        {
            var faqs = await AG.GetAllFAQs();
            var fq = new List<FAQ> { };
            await Task.Run(() => {                 
            Parallel.ForEach(faqs, f => {
                if (f.Question.Contains(search))
                    fq.Add(f);
            });
            });
            return fq;

        }

        //[HttpGet("{id}")]

        //public async Task<FAQ> GetFAQ(int id, [FromBody] string qts)
        //{                       
        //        var fAQs = await AG.GetAllFAQs();
        //        var faq = new FAQ();           
        //     await Task.Run(() =>
        //        {
        //            Parallel.ForEach(fAQs, f =>
        //           {
        //               if (id == f.Id)
        //                   faq = f;                       
        //           });
        //        });
        //    return faq;
        //}


        //[HttpPost] // id-n hasarak propa stex

        //public async Task<List<Group>> PostFAQ([FromBody] FAQ faq)
        //{
        //    await Task.Run(() => {
        //        foreach (Group g in AG.Groups)
        //        {
        //            if (g.Id == faq.GroupId)
        //                g.FAQs.Add(faq);
        //        }
        //    });

        //    return AG.Groups;
        //}


        [HttpPost] // id-n normal Identificator a stex
        public async Task<List<Group>> PostFAQ(FAQ faq)
        
        {
            var a = await AG.GetAllFAQs();
            faq.Id = a.Count();            
            await Task.Run(() => {
                
                foreach (Group g in AG.Groups)
                {
                    if (g.Id == faq.GroupId)                        
                        g.FAQs.Add(faq);
                }
            });

            return AG.Groups;
        }

        [HttpDelete("{id}")] // vercnum em FAQ-i id-n u jnjum
        public async void DeleteFAQ(int id) 
        {            
            await Task.Run(() => 
            {
                Parallel.ForEach(AG.Groups, g => {

                    Parallel.ForEach(g.FAQs, f =>

                      {
                          if (f.Id == id)
                          {
                              g.FAQs.Remove(f);
                          }
                      });
                });
            });
        }
    

        [HttpPut("{id}")]
        public async void PutFAQ(int id, FAQ fAQ) //
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
                               f.Id = id;
                               f.Question = fAQ.Question;
                               f.Answer = fAQ.Answer;
                                                             
                         }
                       });
                   }
                });
            });               
        } 
    }
}
