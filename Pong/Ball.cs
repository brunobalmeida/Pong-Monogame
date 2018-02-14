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
    /// Class to manage game ball
    /// </summary>
    public class Ball : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        public Vector2 speed;
        private Vector2 stage;
        private bool ballMoving = false;
        private Random random;
        private SoundEffect hit;
        private SoundEffect miss;
        private ScoreSystem scoreSystem;

        /// <summary>
        /// Ball Class custom constructor
        /// </summary>
        /// <param name="game">Main game variable</param>
        /// <param name="spriteBatch">Main spriteBatch</param>
        /// <param name="tex">Ball Texture 2D </param>
        /// <param name="position">Ball vector 2 position</param>
        /// <param name="speed">Ball Vector 2 Speed</param>
        /// <param name="stage">Ball vector2 stage</param>
        /// <param name="hit">Hit Sound Effect</param>
        /// <param name="miss">Miss Sound Effect</param>
        /// <param name="scoreSystem">ScoreSystem</param>
        public Ball (Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage,
            SoundEffect hit,
            SoundEffect miss,
            ScoreSystem scoreSystem) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            random = new Random();
            this.hit = hit;
            this.miss = miss;
            this.scoreSystem = scoreSystem;
        }

        /// <summary>
        /// Method to set ball's initial position and random speed 
        /// </summary>
        public void Start()
        {
            position = new Vector2(stage.X / 2f - (tex.Width / 2), stage.Y / 2f - (tex.Height / 2));
            int speedX = random.Next(3, 9);
            int speedY = random.Next(3, 9);
            int directionX = random.Next(0, 2);
            int directionY = random.Next(0, 2);
            
            if (directionX == 1)
                speedX = -speedX;
            if (directionY == 1)
                speedY = -speedY;
            speed = new Vector2(speedX, speedY);
        }

        /// <summary>
        /// Update override method
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            //If ball is inside game screen it can move
            if (ballMoving)
            {
                position += speed;
            }


            KeyboardState ks = Keyboard.GetState();
            //Press enter for the ball to start moving
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !ballMoving && !scoreSystem.GameWon)
            {
                Start();
                ballMoving = true;
            }
            //Top wall
            if (position.Y < 0)
            {
                speed.Y = Math.Abs(speed.Y);
                hit.Play();
            }
            // Bottom wall
            if (position.Y + tex.Height > stage.Y)
            {
                speed.Y = -Math.Abs(speed.Y);
                hit.Play();
            }

            //Detects if ball hit left or right for scoring
            if (ballMoving)
            {
                //Right wall
                if (position.X > stage.X)
                {
                    ++scoreSystem.ScoreP1;
                    ballMoving = false;
                    Start();
                    miss.Play();
                }
                //Left wall
                if (position.X + tex.Width < 0)
                {
                    ++scoreSystem.ScoreP2;
                    ballMoving = false;
                    Start();
                    miss.Play();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw override method
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Method to get rectangle boundaries 
        /// </summary>
        /// <returns>Returns new rectangle with boundaries arguments</returns>
        public Rectangle GetBound()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }

    }
}
