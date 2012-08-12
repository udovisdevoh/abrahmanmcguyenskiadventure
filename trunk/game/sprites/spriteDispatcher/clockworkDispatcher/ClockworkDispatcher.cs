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
        #region Internal Methods
        /// <summary>
        /// Dispatch clockwork sprites
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchClockwork(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, Random random)
        {
            //double density = (random.NextDouble() * 0.1 + 0.05) * Program.monsterDensityAdjustment;
            //DispatchHardcodedTestContent(spritePopulation, random);

            #warning Implement DispatchClockwork();

            PlatformDispatcher.Dispatch(level, spritePopulation, waterInfo, random);
            PendulumDispatcher.Dispatch(level, spritePopulation, waterInfo, random);
            WheelDispatcher.Dispatch(level, spritePopulation, waterInfo, random);
            SeeSawDispatcher.Dispatch(level, spritePopulation, waterInfo, random);

            GeneratePlatformColors(spritePopulation, random);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Generate colors for platforms
        /// </summary>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        private static void GeneratePlatformColors(SpritePopulation spritePopulation, Random random)
        {
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

        /// <summary>
        /// Create hardcoded clockwork sprite
        /// </summary>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        private static void DispatchHardcodedTestContent(SpritePopulation spritePopulation, Random random)
        {
            Pendulum pendulum = new Pendulum(8, -17, random, 4, 2.4, 1.8, false, 0);
            spritePopulation.Add(pendulum);

            Platform platform = new Platform(8, -11, random, false, 2, false, 0, 0);
            spritePopulation.Add(platform);

            pendulum.AddChild(platform);




            Pendulum pendulum2 = new Pendulum(8, -17, random, 5, 2.7, 1.75, false, 2);
            spritePopulation.Add(pendulum2);

            pendulum.AddChild(pendulum2);

            Platform platform2 = new Platform(8, -11, random, false, 0, false, 0, 0);
            spritePopulation.Add(platform2);

            pendulum2.AddChild(platform2);





            //Flail attached to pendulum
            Pendulum pendulum3 = new Pendulum(20, -20, random, 4.0, 2.4, 1.5, false, 0);
            spritePopulation.Add(pendulum3);

            FlailBall flailBall = new FlailBall(20, -13, random, false, 1.0);
            spritePopulation.Add(flailBall);

            pendulum3.AddChild(flailBall);




            //Wheel attached to pendulum
            Pendulum pendulum4 = new Pendulum(40, -30, random, 12, 1.4, 1.0, false, 0);
            spritePopulation.Add(pendulum4);

            Wheel wheel = new Wheel(40, -18, random, 8.0, 0.0, -3.0, false, true, false, 0);
            spritePopulation.Add(wheel);

            pendulum4.AddChild(wheel);


            Platform wheelPlatform;

            wheelPlatform = new Platform(43, -15, random, false, 2, false, 0, 0);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(40, -19, random, false, 2, false, 0, 0);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(40, -17, random, false, 2, false, 0, 0);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(41, -18, random, false, 2, false, 0, 0);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(39, -18, random, false, 2, false, 0, 0);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);

            wheelPlatform = new Platform(37, -20, random, false, 2, false, 0, 0);
            spritePopulation.Add(wheelPlatform);
            wheel.AddChild(wheelPlatform);



            //Flail wheel
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





            //See saw that looks like a wheel
            SeeSaw seeSaw = new SeeSaw(60, -15, random, 4, 3.0, 1.0, 1.0, false, true, true, false, 0);
            spritePopulation.Add(seeSaw);

            Platform seeSawPlatform;

            seeSawPlatform = new Platform(61, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);








            //Nested see saw
            seeSaw = new SeeSaw(80, -15, random, 5, 3.0, 1.0, 1.0, false, true, false, false, 0);
            spritePopulation.Add(seeSaw);

            seeSawPlatform = new Platform(80, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            SeeSaw subSeeSaw = new SeeSaw(80, -15, random, 5, 3.0, 1.0, 1.0, false, true, false, false, 0);
            spritePopulation.Add(subSeeSaw);
            seeSaw.AddChild(subSeeSaw);

            seeSawPlatform = new Platform(80, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            subSeeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(80, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            subSeeSaw.AddChild(seeSawPlatform);









            //Complex structure with seesaw and pendulums
            seeSaw = new SeeSaw(100, -15, random, 5, 3.0, 1.0, 1.0, false, true, false, false, 0);
            spritePopulation.Add(seeSaw);

            seeSawPlatform = new Platform(80, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            subSeeSaw = new SeeSaw(80, -15, random, 5, 3.0, 1.0, 1.0, false, true, false, false, 0);
            spritePopulation.Add(subSeeSaw);
            seeSaw.AddChild(subSeeSaw);

            seeSawPlatform = new Platform(80, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            subSeeSaw.AddChild(seeSawPlatform);

            pendulum = new Pendulum(80, -15, random, 4, 3, 3);
            spritePopulation.Add(pendulum);
            subSeeSaw.AddChild(pendulum);

            SeeSaw subPendulumSeeSaw = new SeeSaw(80, -15, random, 5, 3.0, 1.0, 1.0, false, true, false, false, 0);
            spritePopulation.Add(subPendulumSeeSaw);
            pendulum.AddChild(subPendulumSeeSaw);

            seeSawPlatform = new Platform(80, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            subPendulumSeeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(80, -9, random, false, 2, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            subPendulumSeeSaw.AddChild(seeSawPlatform);




            //4 side see saw
            seeSaw = new SeeSaw(126, -15, random, 3, 3.0, 1.0, 1.0, false, true, false, false, 0);
            spritePopulation.Add(seeSaw);

            seeSawPlatform = new Platform(61, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);



            //Walking feet
            seeSaw = new SeeSaw(136, -15, random, 2, 3.0, 1.0, 1.0, true, true, false, false, 0);
            spritePopulation.Add(seeSaw);

            seeSawPlatform = new Platform(61, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);



            //Walking feet on wheel
            seeSaw = new SeeSaw(156, -15, random, 2, 3.0, 1.0, 1.0, true, true, true, false, 0);
            spritePopulation.Add(seeSaw);

            seeSawPlatform = new Platform(61, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);



            //Walking platform
            platform = new Platform(146, -9, random, true, 0, false, 0, 0);
            spritePopulation.Add(platform);



            //Resistant see saw
            seeSaw = new SeeSaw(-20, -15, random, 3.5, 3.0, 1.0, 3.0, false, true, false, true, 0);
            spritePopulation.Add(seeSaw);

            seeSawPlatform = new Platform(61, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);



            //Walking feet on wheel with tension
            seeSaw = new SeeSaw(-40, -15, random, 2, 3.0, 1.0, 2.0, true, true, true, true, 0);
            spritePopulation.Add(seeSaw);

            seeSawPlatform = new Platform(61, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);

            seeSawPlatform = new Platform(59, -9, random, false, 0, false, 0, 0);
            spritePopulation.Add(seeSawPlatform);
            seeSaw.AddChild(seeSawPlatform);




            //Elevator
            platform = new Platform(-50, -9, random, false, 0, true, 10, 1.0);
            spritePopulation.Add(platform);
        }
        #endregion
    }
}
