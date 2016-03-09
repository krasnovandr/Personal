using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace AudioNetwork.Web
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new MyProfile()));
        }
    }

    public class MyProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "MyProfile";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<KnowledgeSession, KnowledgeSessionViewModel>();
            Mapper.CreateMap<Node, NodeViewModel>();
            Mapper.CreateMap<SessionNodeSuggestions, NodeViewModel>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.SuggestedBy))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.DateCreation));

            Mapper.CreateMap<NodeViewModel, SessionNodeSuggestions>()
                .ForMember(dest => dest.DateCreation, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.SuggestedBy, opt => opt.MapFrom(src => src.CreatedBy));


            //Mapper.CreateMap<ApplicationUser, UserViewModel>();
            Mapper.CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
