using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.App_Start
{
    using System.Text.RegularExpressions;
    using AutoMapper;
    using Models.ViewModels;
    using Snippy.Models;

    public class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Snippet, ConciseSnippetViewModel>()
                .ForMember(vm => vm.Labels, opt => opt.MapFrom(t => t.Labels));

            Mapper.CreateMap<Snippet, DetailsSnippetViewModel>()
                .ForMember(vm => vm.Labels, opt => opt.MapFrom(t => t.Labels))
                .ForMember(vm => vm.Comments, opt => opt.MapFrom(t => t.Comments))
                .ForMember(vm => vm.LanguageName, opt => opt.MapFrom(t => t.Language.Name))
                .ForMember(vm => vm.AuthorUsername, opt => opt.MapFrom(t => t.Author.UserName));

            Mapper.CreateMap<Label, ConciseLabelViewModel>()
               .ForMember(vm => vm.SnippetsCount, opt => opt.MapFrom(t => t.Snippets.Count));

            Mapper.CreateMap<Comment, ConciseCommentViewModel>()
                .ForMember(vm => vm.AuthorUsername, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(vm => vm.SnippetId, opt => opt.MapFrom(t => t.Snippet.Id))
                .ForMember(vm => vm.SnippetTitle, opt => opt.MapFrom(t => t.Snippet.Title));
        }
    }
}