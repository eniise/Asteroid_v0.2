using SFML.Graphics;
using SFML.System;
using SFML;
using System;
using System.Collections.Generic;
using SFML.Window;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Asteroid_
{
	class Program
	{
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int srand(UInt32 seed);

		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 rand();
		public static  bool isCollide(Entity a, Entity b)
		{
			return (b.x - a.x) * (b.x - a.x) +
				   (b.y - a.y) * (b.y - a.y) <
				   (a.R + b.R) * (a.R + b.R);
		}
		public static int W = 1200;
		public static int H = 800;
		static uint Width = 1200;
		static uint Height = 800;
		public static float DEGTORAD = 0.017453f;
		static void Main(string[] args)
        {
			VideoMode app = new VideoMode(Width, Height);
			RenderWindow renderWindow = new RenderWindow(app,"Enise Akkuş meteorları!");
			renderWindow.SetFramerateLimit(60);
			Texture t1, t2, t3, t4, t5, t6, t7;
			t1 = new Texture(Application.StartupPath + @"\images\spaceship.png");
			t2 = new Texture(Application.StartupPath + @"\images\background.jpg");
			t3 = new Texture(Application.StartupPath + @"\images\explosions\type_C.png");
			t4 = new Texture(Application.StartupPath + @"\images\rock.png");
			t5 = new Texture(Application.StartupPath + @"\images\fire_blue.png");
			t6 = new Texture(Application.StartupPath + @"\images\rock_small.png");
			Animation sRock = new Animation(t4, 0, 0, 64, 64, 16, 0.2f);
			Animation sBullet = new Animation(t5, 0, 0, 32, 64, 16, 0.8f);
			Animation sPlayer = new Animation(t1, 40, 0, 40, 40, 1, 0);
			Animation sRock_small = new Animation(t6, 0, 0, 64, 64, 16, 0.2f);
			Animation sExplosion_ = new Animation(t3, 0, 0, 256, 256, 48, 0.5f);
			sRock.sprite.Position = new Vector2f(400, 400);
			Sprite background = new Sprite(t2);
			Sprite sPlayer_ = new Sprite(t1);
			Sprite sExplosion = new Sprite(t3);
			sPlayer_.TextureRect = new IntRect(40, 0, 40, 40);
			sPlayer_.Origin = new Vector2f(20, 20);
			List<Entity> entities = new List<Entity>();
			for (int i = 0; i < 15; i++)
			{
				asteroid a = new asteroid();
				a.settings(sRock, rand() % W, rand() % H, rand() % 360, 25);
				entities.Add(a);
			}
			player p = new player();
			p.settings(sPlayer, 200, 200, 0, 20);
			entities.Add(p);
			float frame = 0;
			float animSpeed = 0.4f;
			int frameCount = 20;
			float x = 300, y = 300;
			float dx = 0, dy = 0, angle = 0;
			bool thrust;
            while (renderWindow.IsOpen)
            {
				renderWindow.Closed += (s, a) => renderWindow.Close();
				if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
				{
					bullet b = new bullet();
					b.settings(sBullet, (int)p.x, (int)p.y, p.angle, 10);
					entities.Add(b);
				}
				if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
					p.angle += 3;
				if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
					p.angle -= 3;
				if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
					p.thrust = true;
				else
					p.thrust = false;
				for (int j = 0; j < entities.Count; j++)
				{
					Entity a = entities[j];
					for(int i=0; i<entities.Count; i++)
                    {
						Entity b = entities[i];
						if(a.name == "asteroid"&&b.name == "bullet")
                        {
                            if (isCollide(a, b))
                            {
								a.life = false;// at
								b.life = false;
								//Entity e = new Entity();
								//e.settings(sExplosion_, (int)a.x, (int)a.y);
								//e.name = "explosion";
								//entities.Add(e);
								for (int k = 0; i < 2; k++)
								{
									if (a.R == 15) continue;
									Entity e_ = new asteroid();
									e_.settings(sRock_small, (int)a.x, (int)a.y, rand() % 360, 15);
									entities.Add(e_);
								}
							}
                        }
						if (a.name == "player" && b.name == "asteroid")
						{
							if (isCollide(a, b))
							{
								b.life = false;
								//Entity e = new Entity();
								//e.settings(sExplosion_, (int)a.x, (int)a.y);
								//e.name = "explosion";
								//entities.Add(e);
								p.settings(sPlayer, 1200 / 2, 800 / 2, 0, 20);
								p.dx = 0; p.dy = 0;
							}
						}
					}
				}
				if (rand() % 150 == 0)
				{
					asteroid a = new asteroid();
					a.settings(sRock, 0, rand() % 800, rand() % 360, 25);
					entities.Add(a);
				}
				if (p.thrust)
                {
					p.dx += (float)Math.Cos(angle * DEGTORAD)*0.2f;
					p.dy += (float)Math.Sin(angle * DEGTORAD)*0.2f;
				}
				else
                {
					p.dx *= 0.99f;
					p.dy *= 0.99f;
                }
				int maxspeed = 15;
				float speed = (float)Math.Sqrt(dx * dx + dy * dy);
				if(speed > maxspeed)
                {
					p.dx *= maxspeed / speed;
					p.dy *= maxspeed / speed;
                }
				p.x += dx;
				p.y += dy;
				if (p.x > W) p.x = 0; if (p.x < 0) p.x = W;
				if (p.y > H) p.y = 0; if (p.y < 0) p.y = H;
				p.anim.sprite.Position = new Vector2f(x, y);
				p.anim.sprite.Rotation = angle + 90;
				for (int j = 0; j < entities.Count; j++)
				{
					//if (entities[j].name == "explosion")
					//{
					//	entities[j].update();
					//	entities[j].anim.update();
					//	if (entities[j].anim.isEnd() == true)
					//	{
					//		entities[j].Dispose();
					//		entities.Remove(entities[j]);
					//		continue;
					//	}
					//}
					if (entities[j].name == "asteroid")
					{
						asteroid e = (asteroid)entities[j];
						if (e.name == "asteroid")
						{
							e.update();
							e.anim.update();
							if (e.life == false)
							{
								entities.Remove(e);
								e.Dispose();
							}
						}
					}
					if (entities[j].name == "bullet")
					{
						bullet e = (bullet)entities[j];
						if (e.name == "bullet")
						{
							e.update();
							e.anim.update();
							if (e.life == false)
							{
								entities.Remove(e);
								e.Dispose();
							}
						}
					}
					//if (entities[j].name == "explosion")
					//{
					//	if (entities[j].anim.isEnd())
					//	{
					//		entities[j].life = true;
					//	}
					//}
				}
				p.update();
				p.anim.update();
				renderWindow.DispatchEvents();
				renderWindow.Clear();
				renderWindow.Draw(background);
				foreach(Entity e in entities)
                {
					e.anim.update();
					//if(e.name != "explosion")
						e.draw(renderWindow);

                }
				renderWindow.Display();
			}
		}
    }
}
