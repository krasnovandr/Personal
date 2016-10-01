using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;
using ServiceLayer.Models.KnowledgeSession.Enums;

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
            Mapper.CreateMap<ApplicationUser, SessionUserViewModel>();
            Mapper.CreateMap<SessionUserViewModel, ApplicationUser>();

            Mapper.CreateMap<NodeStructureSuggestion, NodeStructureSuggestionViewModel>();
            Mapper.CreateMap<NodeStructureSuggestionViewModel, NodeStructureSuggestion>();


            Mapper.CreateMap<SessionNode, NodeViewModel>()
                   .ForMember(dest => dest.SuggestedBy, opt => opt.MapFrom(src => src.SuggestedBy));
            Mapper.CreateMap<NodeViewModel, SessionNode>()
                       .ForMember(dest => dest.SuggestedBy, opt => opt.MapFrom(src => src.SuggestedBy));

            Mapper.CreateMap<SessionNode, SuggestionNodeViewModel>()
              .ForMember(dest => dest.SuggestedBy, opt => opt.MapFrom(src => src.SuggestedBy.Id));

            Mapper.CreateMap<NodeStructureSuggestionVote, NodeStructureSuggestionVoteViewModel>();
            Mapper.CreateMap<NodeStructureSuggestionVoteViewModel, NodeStructureSuggestionVote>();
            //.ForMember(dest => dest.VoteBy, opt => opt.MapFrom(src => src.VoteBy));


            //.ForMember(dest => dest.SuggestedBy, opt => opt.MapFrom(src => src.SuggestedBy))
            //.ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

            //Mapper.CreateMap<NodeViewModel, SessionNodes>()
            //    .ForMember(dest => dest.DateCreation, opt => opt.MapFrom(src => src.CreationDate))
            //    .ForMember(dest => dest.SuggestedBy, opt => opt.MapFrom(src => src.CreatedBy));


            ////Mapper.CreateMap<ApplicationUser, UserViewModel>();
            //Mapper.CreateMap<ApplicationUser, UserViewModel>();
            //Mapper.CreateMap<UserViewModel, ApplicationUser>();

            //Mapper.CreateMap<LevelVote, LevelVoteViewModel>()
            //    .ForMember(dest => dest.SuggetedBy, opt => opt.MapFrom(src => src.SuggetedBy.Id))
            //    .ForMember(dest => dest.VoteBy, opt => opt.MapFrom(src => src.VoteBy.Id))
            //    .ForMember(dest => dest.SuggetedByUser, opt => opt.MapFrom(src => src.SuggetedBy))
            //    .ForMember(dest => dest.VoteByUser, opt => opt.MapFrom(src => src.VoteBy));

            //Mapper.CreateMap<Suggestion, SuggestionViewModel>()
            //    .ForMember(dest => dest.SuggestedBy, opt => opt.MapFrom(src => src.SuggestedBy))
            //    .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            //    .ForMember(dest => dest.VotesUp, opt => opt.MapFrom(src => src.Votes.Where(m => m.Type == (int)VoteTypes.Up)))
            //    .ForMember(dest => dest.VotesDown, opt => opt.MapFrom(src => src.Votes.Where(m => m.Type == (int)VoteTypes.Down)));

            //Mapper.CreateMap<Vote, SuggestionVoteViewModel>()
            //    .ForMember(dest => dest.VoteBy, opt => opt.MapFrom(src => src.VoteBy.Id))
            //    .ForMember(dest => dest.VoteByUser, opt => opt.MapFrom(src => src.VoteBy));

            //Mapper.CreateMap<Comment, CommentViewModel>()
            //    .ForMember(dest => dest.CommentBy, opt => opt.MapFrom(src => src.CommentBy));

        }
    }
}
