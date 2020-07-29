using AutoMapper;
using IssueNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Utils
{
    public class ModelDTOProfiles : Profile
    {
        public ModelDTOProfiles()
        {
            CreateMap<UserWriteDto, User>();
            CreateMap<User, UserReadDTO>();

            CreateMap<ProjectWriteDTO, Project>();
            CreateMap<Project, ProjectReadDTO>();

            CreateMap<IssueWriteDTO, Issue>();
            CreateMap<Issue, IssueReadDTO>();
        }
    }
}
