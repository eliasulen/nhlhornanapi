using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Util
{
    public static class LinkBuilder
    {
        public static string BuildNo = "33f4bcacaa52eed691a6f0671c4cde69850f3c31_1521478002";
        public static string LogoLink = $"https://www-league.nhlstatic.com/nhl.com/builds/site-core/{BuildNo}/images/logos/team/current/";
        public static string PortraitLink = $"https://nhl.bamcontent.com/images/headshots/current/168x168/";

        public static string GetLogoLink(int teamId)
        =>
           $"{LogoLink}team-{teamId}-dark.svg";

        public static string GetPortraitLink(int playerId)
            => $"{PortraitLink}{playerId}.jpg";
    }
}
