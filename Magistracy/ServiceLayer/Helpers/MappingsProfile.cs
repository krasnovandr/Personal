using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Helpers
{
    public class MappingsProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "MappingsProfile";
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
            Mapper.CreateMap<UserViewModel, ApplicationUser>();
            Mapper.CreateMap<LevelVote, LevelVoteViewModel>()
                .ForMember(dest => dest.SuggetedBy, opt => opt.MapFrom(src => src.SuggetedBy.Id))
                .ForMember(dest => dest.VoteBy, opt => opt.MapFrom(src => src.VoteBy.Id))
                .ForMember(dest => dest.SuggetedByUser, opt => opt.MapFrom(src => src.SuggetedBy))
                .ForMember(dest => dest.VoteByUser, opt => opt.MapFrom(src => src.VoteBy));

        }
    }
}
