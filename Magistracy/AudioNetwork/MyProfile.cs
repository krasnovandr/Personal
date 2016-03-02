using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Models;
using ServiceLayer.Models;

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
            Mapper.CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
