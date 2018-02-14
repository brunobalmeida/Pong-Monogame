using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    /// <summary>
    /// Class to manage game bats
    /// </summary>
    public class Bat : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 stage;
        private int player;


        /// <summary>
        /// Bat Class custom constructor
        /// </summary>
        /// <param name="game">Main game variable</param>
        /// <param name="spriteBatch">Main spriteBatch</param>
        /// <param name="tex">Bat Texture 2D</param>
        /// <param name="position">Bat vector2 position</param>
        /// <param name="speed">Bat vector2 speed</param>
        /// <param name="stage">Bat vector2 stage</param>
        /// <param name="player">Integer player variable</param>
        public Bat(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage, 
            int player) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            this.player = player; 
        }


        /// <summary>
        ///  Update override method
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();

            if (player == 1)
            {
                if (kState.IsKeyDown(Keys.A))
                {
                    position -= speed;
                }
                if (kState.IsKeyDown(Keys.Z))
                {
                    position += speed;
                }
            }

            if (player == 2)
            {
                if (kState.IsKeyDown(Keys.Up))
                {
                    position -= speed;
                }
                if (kState.IsKeyDown(Keys.Down))
                {
                    position += speed;
                }
            }

            if (position.Y + tex.Height > stage.Y)
            {
                position.Y = stage.Y - tex.Height;
            }
            else if (position.Y < 0)
            {
                position.Y = 0;
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

        /// <summary>
        /// Method to restart bat position
        /// </summary>
        public void RestartPos()
        {
            position.Y = stage.Y / 2 - tex.Height / 2;
        }
    }
}
