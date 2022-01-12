using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_
{
    public class Animation : Entity
    {

		public float Frame;
		public float speed;
		public Sprite sprite = new Sprite();
		public List<IntRect> frames = new List<IntRect>();

		public Animation()
		{
		}

		public Animation(Texture t, int x, int y, int w, int h, int count, float Speed)
		{
			Frame = 0F;
			speed = Speed;

			for (int i = 0; i < count; i++)
			{
				frames.Add(new IntRect(x + i * w, y, w, h));
			}

			sprite.Texture = t;
			sprite.Origin = new Vector2f(w / 2, h / 2);
			sprite.TextureRect = frames[0];
		}


		public void update()
		{
			Frame += speed;
			int n = frames.Count;
			if (Frame >= n)
			{
				Frame -= n;
			}
			if (n > 0)
			{
				sprite.TextureRect = (frames[(int)Frame]);
			}
		}

		public bool isEnd()
		{
			if (Frame >= frames.Count)
				return true;
			else
				return false;
			//return Frame + speed >= frames.Count;
		}
	}
}
