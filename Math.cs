using System;

namespace GameServer {
	public static class Math {
		public static uint mod(uint x, uint y) {
			return x - y * (uint)MathF.Floor(x/y);
		}
		public static float mod(float x, float y) {
			return x - y * MathF.Floor(x/y);
		}
		public static int mod(int x, int y) {
			return x - y * (int)MathF.Floor(x/y);
		}
		public static long mod(long x, long y) {
			return x - y * (long)MathF.Floor(x/y);
		}
	}
}