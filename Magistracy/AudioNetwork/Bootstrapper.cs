using System.Web.Http;
using System.Web.Mvc;
using DataLayer.Interfaces;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using MusicRecognition.Interfaces;
using MusicRecognition.Services;
using ServiceLayer.Helpers;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using TextMining;
using Unity.Mvc4;
using VkService;

namespace AudioNetwork.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IdentityUser, ApplicationUser>();
            container.RegisterType<IMusicRepository, MusicRepository>();
            container.RegisterType<IPlaylistRepository, PlaylistRepository>();
            container.RegisterType<IConversationRepository, ConversationRepository>();
            container.RegisterType<IWallRepository, WallRepository>();

            container.RegisterType<IStatisticsRepository, StatisticsRepository>();
            container.RegisterType<IStatisticsService, StatisticsService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IConversationService, ConversationService>();
            container.RegisterType<IMusicService, MusicService>();
            container.RegisterType<IPlaylistService, PlaylistService>();
            container.RegisterType<IWallService, WallService>();
            container.RegisterType<IUploadService, UploadService>();
            container.RegisterType<IUnitOfWork, EfUnitOfWork>();
            container.RegisterType<IKnowledgeSessionService, KnowledgeSessionService>();
            container.RegisterType<IKnowledgeSessionMemberService, KnowledgeSessionMemberService>();
            //container.RegisterType<ILevelVoteService, LevelVoteService>();
            //container.RegisterType<ISuggestionVoteService, SuggestionVoteService>();
            container.RegisterType<ISuggestionService, SuggestionService>();
            //container.RegisterType<IHistoryService, HistoryService>();
            container.RegisterType<IVoteFinishHelper, VoteFinishHelper>();
            container.RegisterType<INodeModificationService, NodeModificationService>();
            container.RegisterType<INodeService, NodeService>();
            container.RegisterType<ICommentsService, CommentsService>();
            container.RegisterType<INodeResourceService, NodeResourceService>();
            container.RegisterType<ITextMiningApi, TextMiningApi>();
            container.RegisterType<ITextMiningService, TextMiningService>();
            container.RegisterType<ITextMergeSuggestionService, TextMergeSuggestionService>();
            container.RegisterType<IRecognitionService, RecognitionService>();
            container.RegisterType<IVkAudioService, VkAudioService>();
        }
    }
}