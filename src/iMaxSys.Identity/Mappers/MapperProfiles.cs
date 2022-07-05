using System;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.Entities;

namespace iMaxSys.Identity.Mappers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<DepartmentModel, Department>();
            CreateMap<MenuModel, Menu>();
        }
    }
}

