using AutoMapper;
using ServiceLayer.Helpers;

namespace AudioNetwork.Web
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new MappingsProfile()));
        }
    }


}
