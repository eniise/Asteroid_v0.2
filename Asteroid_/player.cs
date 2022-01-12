using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_
{
    public class player : Entity
    {
		public bool thrust;

		public player()
		{
			name = "player";
		}

		public void update()
		{
			if (thrust)
			{
				dx += (float)(Math.Cos(angle * 0.017453f) * 0.2);
				dy += (float)(Math.Sin(angle * 0.017453f) * 0.2);
			}
			else
			{
				dx *= float.Parse((0.99).ToString());
				dy *= float.Parse((0.99).ToString());
			}

			int maxSpeed = 15;
			float speed = (float)Math.Sqrt(dx * dx + dy * dy);
			if (speed > maxSpeed)
			{
				dx *= maxSpeed / speed;
				dy *= maxSpeed / speed;
			}

			x += dx;
			y += dy;

			if (x > 1200) x = 0; if (x < 0) x = 1200;
			if (y > 800) y = 0; if (y < 0) y = 800;
		}
	}
}
