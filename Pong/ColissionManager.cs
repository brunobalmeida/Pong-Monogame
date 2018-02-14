using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace PongGame
{
    /// <summary>
    /// Class to manage the collision 
    /// </summary>
    public class CollisionManager : GameComponent
    {
        private Ball ball;
        private Bat bat;
        private Bat batTwo;
        private SoundEffect hit;

        /// <summary>
        /// Collision Manager custom constructor
        /// </summary>
        /// <param name="game">Main game variable</param>
        /// <param name="ball">Ball class instance</param>
        /// <param name="bat">Bat class instance</param>
        /// <param name="batTwo">Bat class instance Bat 2</param>
        /// <param name="hit">Hit Sound Effect</param>
        public CollisionManager (Game game,
            Ball ball, 
            Bat bat,
            Bat batTwo,
            SoundEffect hit) : base(game)
        {
            this.ball = ball;
            this.bat = bat;
            this.batTwo = batTwo;
            this.hit = hit;
        }

        /// <summary>
        /// Update override method
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            if (ball.GetBound().Intersects(bat.GetBound()))
            {
                ball.speed = new Vector2(Math.Abs(ball.speed.X), ball.speed.Y);
                hit.Play();
            }

            if (ball.GetBound().Intersects(batTwo.GetBound()))
            {
                ball.speed = new Vector2(-Math.Abs(ball.speed.X), ball.speed.Y);
                hit.Play();
            }

            base.Update(gameTime);
        }

    }
}
