using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_
{
    public class bullet : Entity
    {
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 rand();
		public bullet()
		{
			name = "bullet";
		}

		public void update()
		{
			dx = (float)(Math.Cos(angle * 0.017453f) * 6);
			dy = (float)(Math.Sin(angle * 0.017453f) * 6);
			x += dx;
			y += dy;

			if (x > 1200 || x < 0 || y > 800 || y < 0) life = true;
		}
	}
}
