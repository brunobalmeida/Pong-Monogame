using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    /// <summary>
    /// Score system class 
    /// </summary>
    public class ScoreSystem : DrawableGameComponent
    {
        private const int SCORETOWIN = 2;
        private const string P1NAME = "Player A";
        private const string P2NAME = "Player B";
        private int scoreP1;
        private int scoreP2;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D scoreTex;
        private SoundEffect winSound;
        private Vector2 p1NamePosition;
        private Vector2 p2NamePosition;
        private Vector2 p1ScorePosition;
        private Vector2 p2ScorePosition;
        private Vector2 winnerNamePosition;
        private Vector2 stage;
        private Vector2 position;
        private bool gameWon = false;
        private string winnerName;
        private bool disableWinSound = true;

        //Acessibility for private variables
        public int ScoreP1
        {
            get
            {
                return scoreP1;
            }
            set
            {
                scoreP1 = value;
            }
        }
        public int ScoreP2
        {
            get
            {
                return scoreP2;
            }
            set
            {
                scoreP2 = value;
            }
        }
        public bool GameWon
        {
            get
            {
                return gameWon;
            }
        }


        /// <summary>
        /// Method to set up the variables to restart the game
        /// </summary>
        public void Restart()
        {
            scoreP1 = 0;
            scoreP2 = 0;
            disableWinSound = true;
            gameWon = false;
            winnerName = "";
        }

        /// <summary>
        /// ScoreSystem Class custom constructor
        /// </summary>
        /// <param name="game">Main game variable</param>
        /// <param name="spriteBatch">Main spriteBatch</param>
        /// <param name="font">Font Used</param>
        /// <param name="scoreTex">Score Texture2D</param>
        /// <param name="winSound">Winning Sound Effect</param>
        /// <param name="position">Vector2 Position</param>
        /// <param name="stage">Vector2 Stage</param>
        public ScoreSystem(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            Texture2D scoreTex,
            SoundEffect winSound,
            Vector2 position,
            Vector2 stage) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.scoreTex = scoreTex;
            this.winSound = winSound;
            this.position = position;
            this.stage = stage;
            p1NamePosition = new Vector2(0, (position.Y + (scoreTex.Height / 2) - font.MeasureString(P1NAME).Y / 2));
            p2NamePosition = new Vector2(stage.X - font.MeasureString(P2NAME).X - 50, (position.Y + (scoreTex.Height / 2) - font.MeasureString(P2NAME).Y / 2));
            p1ScorePosition = new Vector2(font.MeasureString(P1NAME).X + 20, (position.Y + (scoreTex.Height / 2) - font.MeasureString(P1NAME).Y / 2));
            p2ScorePosition = new Vector2(stage.X - 30, (position.Y + (scoreTex.Height / 2) - font.MeasureString(P2NAME).Y / 2));
            winnerNamePosition = new Vector2(stage.X / 2, stage.Y / 2);
        }

        /// <summary>
        /// Update override method
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            if (scoreP1 == SCORETOWIN)
            {
                winnerName = P1NAME;
                gameWon = true;
                if (disableWinSound)
                {
                    winSound.Play();
                    disableWinSound = false;
                }
                
            }
            else if (scoreP2 == SCORETOWIN)
            {
                winnerName = P2NAME;
                gameWon = true;
                if (disableWinSound)
                {
                    winSound.Play();
                    disableWinSound = false;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw override method
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(scoreTex, position, Color.White);
            spriteBatch.DrawString(font, P1NAME, p1NamePosition, Color.Black);
            spriteBatch.DrawString(font, Convert.ToString(scoreP1), p1ScorePosition, Color.Black);
            spriteBatch.DrawString(font, P2NAME, p2NamePosition, Color.Black);
            spriteBatch.DrawString(font, Convert.ToString(scoreP2), p2ScorePosition, Color.Black);
            if (gameWon)
            {
                spriteBatch.DrawString(font, winnerName + " Won the game\nPress space to restart", winnerNamePosition, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
