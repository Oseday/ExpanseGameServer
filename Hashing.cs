using System;
using System.Numerics;

namespace GameServer
{
    public static class Hash 
    {
        private static float GridSize = 1;
        private static uint dist = 1048575;//(uint)Math.Pow(2,21)-1;

        private static uint yMul = 2097151; //21 1 bits

        public static ulong Calc(Vector3 p) {
            p = p/GridSize;
            uint x = (uint)(p.X + dist)&yMul;
            uint y = (uint)(p.Y + dist)&yMul;
            ulong z = (ulong)(p.Z + dist)&yMul;
            return z*4398046511104 + y*2097152 + x;
        }
    }
}



/*

11111111111111111111
0000000000000000000000

111111111111111111111

111111111111111111111

111111111111111111111


local function AddTo1D(self,x,y,hasht)
	local s = abs(x) > abs(y) and x or y
    local n = 4*s^2 - 2*s
    if s >= 0 then
        n = n - x + y
    else
        n = n + x - y
    end
	if hasht[n]==nil then hasht[n]={} end
	insert(hasht[n],self)
end
*/