using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.Repository
{ 
    public interface IHowKnowUsRepository
    {
        List<HowKnowUs> GetHowKnowUs();
    }
    public class HowKnowUsRepository : IHowKnowUsRepository
    {
        CodeCommunityFlowDbContext db;
        public HowKnowUsRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public List<HowKnowUs> GetHowKnowUs()
        {
            List<HowKnowUs> howKnowUs = db.HowKnowUs.ToList();
            return howKnowUs;
        }
    }
}
