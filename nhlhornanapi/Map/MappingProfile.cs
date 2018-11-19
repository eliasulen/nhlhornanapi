using AutoMapper;
using nhlhornanapi.Database.Entity;
using nhlhornanapi.Map;
using nhlhornanapi.Map.Odds;
using nhlhornanapi.Map.Schedule;
using nhlhornanapi.Model.Odds;
using nhlhornanapi.Model.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Match, DTOOdds>();
            CreateMap<JsonSchedule, IEnumerable<DTOSchedule>>().ConvertUsing<ScheduleConverter>();
            CreateMap<IEnumerable<Match>, IEnumerable<DTOTeamOdds>>().ConvertUsing<TeamOddsConverter>();
        }
    }
}
