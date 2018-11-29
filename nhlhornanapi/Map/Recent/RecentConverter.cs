using AutoMapper;
using nhlhornanapi.Model.Recent;
using nhlhornanapi.Util;
using System.Collections.Generic;
using System.Linq;

namespace nhlhornanapi.Map.Schedule
{
    internal class RecentConverter : ITypeConverter<JsonRecent, IEnumerable<DTORecent>>
    {
        public IEnumerable<DTORecent> Convert(JsonRecent source, IEnumerable<DTORecent> destination, ResolutionContext context)
        {
            List<DTORecent> recent = new List<DTORecent>();

           

            return recent;
        }
    }
}