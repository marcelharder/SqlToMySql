using SqlToMySql.Data.models;
using SqlToMySql.Data.SQLEntities;

namespace SqlToMySql.helpers;

    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Operative, Class_Procedure>().ForMember(dest => dest.ProcedureId, opt => opt.Ignore());

            CreateMap<eusur_cabg, Class_CABG>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Valves, Class_Valve>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }


    }