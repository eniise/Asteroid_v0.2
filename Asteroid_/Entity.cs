using SFML.Graphics;
using SFML.System;

namespace Asteroid_
{
    public class Entity : System.IDisposable
    {
		public float x;
		public float y;
		public float dx;
		public float dy;
		public float R;
		public float angle;
		public bool life;
		public string name;
		public Animation anim;

		public Entity()
		{
			life = true;
		}

		public void settings(Animation a, int X, int Y, float Angle = 0F, int radius = 1)
		{
			anim = a;
			x = X;
			y = Y;
			angle = Angle;
			R = radius;
		}

		public virtual void update()
		{
		}

		public void draw(RenderWindow app)
		{
			anim.sprite.Position = new Vector2f(x, y);
			anim.sprite.Rotation = angle + 90;
			app.Draw(anim.sprite);

			//CircleShape circle = new CircleShape(R);
			//circle.FillColor = new Color(255, 0, 0, 170);
			//circle.Position = new Vector2f(x, y);
			//circle.Origin = new Vector2f(R, R);
			//app.Draw(circle);
		}

		public virtual void Dispose()
		{
			x = 0;
			y = 0;
			dx = 0;
			dy = 0;
			R = 0;
			angle = 0;
			life = false;
			//anim = null;
		}
	}
}
