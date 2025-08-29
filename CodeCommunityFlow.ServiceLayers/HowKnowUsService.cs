using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.Repository;
using CodeCommunityFlow.DomainModels;
using AutoMapper;

namespace CodeCommunityFlow.ServiceLayers
{
    public interface IHowknowService
    {
        List<HowKnowUsViewModel> GetHowKnowUs();
    }
    public class HowKnowUsService : IHowknowService
    {
        IHowKnowUsRepository howKnowUsRepository;
        IMapper _mapper;
        public HowKnowUsService(IMapper mapper)
        {
            howKnowUsRepository = new HowKnowUsRepository();
            _mapper = mapper;
        }
        public List<HowKnowUsViewModel> GetHowKnowUs()
        {
            var knows = howKnowUsRepository.GetHowKnowUs();
            return _mapper.Map<List<HowKnowUsViewModel>>(knows);
        }
    }
}
