using AutoMapper;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Mappers
{
    public static class ApiResourceMapper
    {
        internal static IMapper Mapper { get; }

        static ApiResourceMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }
        
        public static ApiResource ToModel(this Entities.ApiResource entity)
        {
            return entity == null ? null : Mapper.Map<ApiResource>(entity);
        }

        public static Entities.ApiResource ToEntity(this ApiResource model)
        {
            return model == null ? null : Mapper.Map<Entities.ApiResource>(model);
        }
    }

    public class ApiResourceMapperProfile : Profile
    {
        public ApiResourceMapperProfile()
        {
            CreateMap<Entities.ApiResource, ApiResource>(MemberList.Destination)
                .ConstructUsing(src => new ApiResource())
                .ForMember(x => x.ApiSecrets, opts => opts.MapFrom(x => x.Secrets))
                .ReverseMap();

            CreateMap<Entities.ApiResourceClaim, string>()
                .ConstructUsing(x => x.Type)
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

            CreateMap<Entities.ApiResourceProperty, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<Entities.ApiResourceScope, Scope>(MemberList.Destination)
                .ConstructUsing(src => new Scope())
                .ReverseMap();

            CreateMap<Entities.ApiResourceScopeClaim, string>()
               .ConstructUsing(x => x.Type)
               .ReverseMap()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

            CreateMap<Entities.ApiResourceSecret, Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
                .ReverseMap();
        }
    }
}
