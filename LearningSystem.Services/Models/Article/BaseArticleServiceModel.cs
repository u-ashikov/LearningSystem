namespace LearningSystem.Services.Models.Article
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Automapper;
    using System;

    public class BaseArticleServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
		public string Title { get; set; }

		public string Content { get; set; }

		public DateTime PublishDate { get; set; }

		public string Author { get; set; }

		public void ConfigureMapping(Profile profile)
		{
			profile.CreateMap<Article, BaseArticleServiceModel>()
				.ForMember(dest => dest.Author, cfg => cfg.MapFrom(src => src.Author.Name));
		}
	}
}
