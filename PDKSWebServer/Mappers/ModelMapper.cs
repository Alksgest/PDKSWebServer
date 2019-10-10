using System.Collections.Generic;
using AutoMapper;
using PDKSWebServer.Dtos;
using PDKSWebServer.Models;

namespace PDKSWebServer.Mappers
{
    internal class ModelMapper
    {
        private static ModelMapper _modelMapper;
        internal static ModelMapper GetMapper =>
            _modelMapper ?? (_modelMapper = new ModelMapper());

        private readonly IMapper _mapper;

        private ModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();

                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<CategoryDto, Category>();

                cfg.CreateMap<Article, ArticleDto>();
                cfg.CreateMap<ArticleDto, Article>()
                    .ForMember(m => m.Category, 
                        opt => opt.MapFrom(x =>_mapper.Map<CategoryDto, Category>(x.Category)));

                //cfg.CreateMap<Article, ArticleDto>()
                //    .AfterMap((article, articleDto) =>
                //    {
                //        foreach (var ac in article.ArticleCategories)
                //        {
                //            articleDto.Categories.Add(Map<Category, CategoryDto>(ac.Category));
                //        }
                //    });

                //cfg.CreateMap<ArticleDto, Article>()
                //.AfterMap((dto, article) =>
                //    {
                //        foreach(var catDto in dto.Categories)
                //        {
                //            article.ArticleCategories.Add(new ArticleCategory
                //            {
                //                Article = article,
                //                ArticleId = article.ArticleID,
                //                Category = Map<CategoryDto, Category>(catDto),
                //                CategoryId = catDto.CategoryId
                //            });
                //        }
                //    });
            });

            _mapper = new Mapper(config);
        }

        internal TDest Map<TSource, TDest>(TSource source)
        {
            if (source == null)
                return default;
            return _mapper.Map<TSource, TDest>(source);
        }

        internal IEnumerable<TDest> MapList<TSource, TDest>(IEnumerable<TSource> source)
        {
            if (source == null)
                return null;

            List<TDest> res = new List<TDest>();

            foreach (var obj in source)
            {
                res.Add(Map<TSource, TDest>(obj));
            }
            return res;
        }
    }
}
