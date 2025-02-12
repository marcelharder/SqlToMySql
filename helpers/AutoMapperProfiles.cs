using SqlToMySql.Data.models;

namespace SqlToMySql.helpers;

    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Operative, Class_Procedure>().ForMember(dest => dest.ProcedureId, opt => opt.Ignore());
        }


    }