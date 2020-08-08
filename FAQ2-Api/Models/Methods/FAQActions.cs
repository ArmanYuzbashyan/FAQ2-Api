using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace FAQ2_Api.Models.Methods
{
    public static class FAQActions
    {
        public static async Task<List<FAQ>> GetFAQsSearch(string search)
        {
            var faqs = await AG.GetAllFAQs();
            var fq = new List<FAQ> { };
            await Task.Run(() =>
            {
                foreach (FAQ f in faqs)
                {

                    if (f.Question.MySearch(search, StringComparison.CurrentCultureIgnoreCase))
                        fq.Add(f);

                    // primitive variant
                    // var lowG = g.GroupName.ToLower();
                    // var lowS = search.ToLower();
                    // if (lowG.Contains(lowS))
                    //    gn.Add(g);

                };
            });
            return fq;
        }
        public static async Task<bool> PostFAQ(FAQ faq, bool done)
        {
            var a = await AG.GetAllFAQs();
            await Task.Run(() =>
            {
                var a1 = new List<IdAble> { };
                foreach (FAQ f in a) { a1.Add(f); }

                faq.Id = MakeId.NewId(a1);  // !!!!!  

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
            return done;
        }
        public static async Task<bool> DeleteFAQ(int id, bool done) 
        {
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
            return done;
        }
        public static async Task <bool> PutFAQ(int id, FAQ fAQ, bool done)
        {
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
            return done;
        }
    }
}
