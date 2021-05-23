using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Models;
using BLL.Models.In;
using BLL.Models.Out;
using DAL.Entities;

namespace BLL.Configs
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //CreateMap<User, UserModel>()
            //    .ForMember(dest => dest.Roles, opt => opt.MapFrom((src, dest, destMember, context) => (string[])context.Items["roles"]))
            //    .ReverseMap();

            CreateMap<User, UserTokenModel>()
                .ForMember(x => x.Roles, opt => opt.MapFrom((src, dest, destMember, context) => (string)context.Items["role"]))
                .ForMember(x => x.Token, opt => opt.MapFrom((src, dest, destMember, context) => (string)context.Items["token"]))
                .ReverseMap();

            CreateMap<UserRegisterModel, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.Email))
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.FirstName + " " + y.LastName))
                .ForMember(x => x.ImageLink, opt => opt.Condition(y => !String.IsNullOrEmpty(y.ImageLink)));

            CreateMap<User, UserPreviewModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status.UserStatus))
                .ForMember(x => x.Tags, opt => opt.MapFrom(y => y.TagDescriptions.Select(t => t.Tag.TagName)));

            CreateMap<User, UserProfileModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status.UserStatus));

            CreateMap<EditUserModel, User>();
        }
    }
}
