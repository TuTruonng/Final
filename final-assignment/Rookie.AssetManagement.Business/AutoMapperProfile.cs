using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;

namespace Rookie.AssetManagement.Business
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            FromDataAccessorLayer();
            FromPresentationLayer();
        }

        private void FromPresentationLayer()
        {
            CreateMap<CreateUserDto, User>() // <Source, Dest>
                .ForMember(dst => dst.FullName, opt => opt.MapFrom(
                    src => string.Format("{0} {1}", src.FirstName, src.LastName)))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.DOB, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dst => dst.JoinedDate, opt => opt.MapFrom(src => src.JoinedDate))
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dst => dst.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => true)) // True - Not disable
                .ForMember(dst => dst.ChangePassword, opt => opt.MapFrom(src => false)) // False - Require to change password after login
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EditUserDto, User>() // <Source, Dest>
                .ForMember(dst => dst.DOB, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dst => dst.JoinedDate, opt => opt.MapFrom(src => src.JoinedDate))
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreateAssetDto, Asset>() // <Source, Dest>
              .ForMember(dst => dst.AssetName, opt => opt.MapFrom(src => src.AssetName))
              .ForMember(dst => dst.InstalledDate, opt => opt.MapFrom(src => src.InstalledDate))
              .ForMember(dst => dst.Specification, opt => opt.MapFrom(src => src.Specification))
              .ForMember(dst => dst.Location, opt => opt.MapFrom(src => src.Location))
              .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EditAssetDto, Asset>() // <Source, Dest>
                .ForMember(dst => dst.AssetName, opt => opt.MapFrom(src => src.AssetName))
                .ForMember(dst => dst.Specification, opt => opt.MapFrom(src => src.Specification))
                .ForMember(dst => dst.InstalledDate, opt => opt.MapFrom(src => src.InstalledDate))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private void FromDataAccessorLayer()
        {
            CreateMap<User, UserDto>()
                .ForMember(dst => dst.StaffCode, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.DateOfBirth, opt => opt.MapFrom(src => src.DOB))
                .ForMember(dst => dst.Type, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Asset, AssetDto>()
                .ForMember(dst => dst.AssetCode, opt => opt.MapFrom(src => $"{src.Category.Prefix}1{src.AssetId:D5}"))
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dst => dst.State, opt => opt.MapFrom(src => src.State.Name))
                .ReverseMap();

            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dst => dst.AssignmentNumber, opt => opt.MapFrom(src => src.AssignmentId))
                .ForMember(dst => dst.AssetCode, opt => opt.MapFrom(src => $"{src.Asset.Category.Prefix}1{src.AssetId:D5}"))
                .ForMember(dst => dst.AssetName, opt => opt.MapFrom(src => src.Asset.AssetName))
                .ForMember(dst => dst.AssignedTo, opt => opt.MapFrom(src => src.AssignedToUser.UserName))
                .ForMember(dst => dst.AssignedBy, opt => opt.MapFrom(src => src.AssignedByUser.UserName))
                .ForMember(dst => dst.AssignedDate, opt => opt.MapFrom(src => src.AssignedDate))
                .ForMember(dst => dst.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
        }
    }
}
