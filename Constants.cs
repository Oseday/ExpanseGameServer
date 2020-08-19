using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class Constants
    {
        public const int TICKS_PER_SEC = 30;
        public const float MS_PER_TICK = 1000f / TICKS_PER_SEC;

        
        public const int PHYS_TICKS_PER_SEC = 10;
        public const float PHYS_MS_PER_TICK = 1000f / PHYS_TICKS_PER_SEC;
    }
}
