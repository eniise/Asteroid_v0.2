using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_
{
    public class asteroid : Entity
    {
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int srand(UInt32 seed);

		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 rand();
		public asteroid()
		{
			dx = rand() % 8 - 4;
			dy = rand() % 8 - 4;
			name = "asteroid";
		}

		public new void update()
		{
			x += dx;
			y += dy;

			if (x > 1200)
				x = 0;
			if (x < 0)
				x = 1200;
			if (y > 800)
				y = 0;
			if (y < 0)
				y = 800;
		}

	}
}
