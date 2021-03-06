﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Util
{
    public static class Globals
    {
        public static int UtcOffset = DateUtil.UtcOffsetStandard;

        public static void SetUtcOffset(int utcOffset) => UtcOffset = utcOffset;

        public static class Points
        {
            public static int Win = 2;
            public static int OtLoss = 1;
            public static int Loss = 0;
        }
    }

}
