﻿using AutoMapper;
using nhlhornanapi.Database.Entity;
using nhlhornanapi.Map;
using nhlhornanapi.Map.Odds;
using nhlhornanapi.Map.Player;
using nhlhornanapi.Map.Schedule;
using nhlhornanapi.Model.Odds;
using nhlhornanapi.Model.Player;
using nhlhornanapi.Model.Recent;
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
            CreateMap<JsonRecent, IEnumerable<DTORecent>>().ConvertUsing<RecentConverter>();
            CreateMap<IEnumerable<Match>, IEnumerable<DTOTeamOdds>>().ConvertUsing<TeamOddsConverter>();
            CreateMap<JsonPlayers, IEnumerable<DTOPlayerSalary>>().ConvertUsing<PlayerSalaryConverter>();
        }
    }
}
