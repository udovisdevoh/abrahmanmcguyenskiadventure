using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Clockwork dispatcher
    /// </summary>
    internal static class ClockworkDispatcher
    {
        internal static void DispatchClockwork(Level level, SpritePopulation spritePopulation, level.WaterInfo waterInfo, Random random)
        {
            Pendulum pendulum = new Pendulum(8, -17, random, 4, 1.0, 1.8, false, 0);
            spritePopulation.Add(pendulum);

            Platform platform = new Platform(8, -11, random, false, 2);
            spritePopulation.Add(platform);

            pendulum.AddChild(platform);




            Pendulum pendulum2 = new Pendulum(8, -17, random, 5, 1.7, 1.75, false, 2);
            spritePopulation.Add(pendulum2);

            pendulum.AddChild(pendulum2);

            Platform platform2 = new Platform(8, -11, random, false, 0);
            spritePopulation.Add(platform2);

            pendulum2.AddChild(platform2);






            Pendulum pendulum3 = new Pendulum(20, -20, random, 4.0, 1.0, 1.5, false, 0);
            spritePopulation.Add(pendulum3);

            FlailBall flailBall = new FlailBall(20, -13, random, false, 1.0);
            spritePopulation.Add(flailBall);

            pendulum3.AddChild(flailBall);





            Pendulum pendulum4 = new Pendulum(40, -30, random, 12, 0.4, 1.0, false, 0);
            spritePopulation.Add(pendulum4);
            
            Wheel wheel = new Wheel(40, -18, random, 8.0, 0.0, -3.0, false, true, false, 0);
            spritePopulation.Add(wheel);

            pendulum4.AddChild(wheel);


            Platform wheelPlatform;

            wheelPlatform = new Platform(43, -15, random, false, 2);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(40, -19, random, false, 2);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(40, -17, random, false, 2);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(41, -18, random, false, 2);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(39, -18, random, false, 2);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(37, -20, random, false, 2);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);



            Wheel flailWheel = new Wheel(40, -18, random, 3.0, 0.0, -6.0, false, false, true, 0);
            spritePopulation.Add(flailWheel);

            FlailBall flailBall2;
            flailBall2 = new FlailBall(40, -18, random, false, 0);
            spritePopulation.Add(flailBall2);
            flailWheel.AddChild(flailBall2);

            flailBall2 = new FlailBall(40, -18, random, false, 0);
            spritePopulation.Add(flailBall2);
            flailWheel.AddChild(flailBall2);

            flailBall2 = new FlailBall(40, -18, random, false, 0);
            spritePopulation.Add(flailBall2);
            flailWheel.AddChild(flailBall2);

            wheel.AddChild(flailWheel);






            SeeSaw seeSaw = new SeeSaw(60, -15, random, 4, 3.0, 3.0, false, true, 0);
            spritePopulation.Add(seeSaw);

            
            Platform seeSawPlatform;

            seeSawPlatform = new Platform(61, -9, random, false, 2);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 2);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);







            //We generate colors for platforms
            foreach (AbstractSprite sprite in spritePopulation.AllSpriteList)
            {
                if (sprite is AbstractBearing)
                {
                    AbstractBearing bearing = (AbstractBearing)sprite;

                    if (bearing.ParentNode == null)
                        bearing.GenerateColoredPlatformSurface(random);
                }
            }
        }
    }
}
