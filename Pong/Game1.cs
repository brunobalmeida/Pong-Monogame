using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball;
        Texture2D ballTex;
        Vector2 stage;
        Vector2 ballPos;
        Vector2 ballSpeed;
        Bat bat;
        Bat bat2;
        int player;
        ScoreSystem scoreSystem;
        private SpriteFont font;

        /// <summary>
        /// Game class constructor
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Score texture needs to come before ball creation

            //Winning/Score
            Texture2D scoreTex = this.Content.Load<Texture2D>("images/Scorebar");
            stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight - scoreTex.Height);
            SoundEffect winSound = this.Content.Load<SoundEffect>("sounds/applause1");
            Vector2 position = new Vector2(0, (graphics.PreferredBackBufferHeight - scoreTex.Height));
            this.font = this.Content.Load<SpriteFont>("fonts/SpriteFont1");
            scoreSystem = new ScoreSystem(this, spriteBatch, font, scoreTex, winSound, position, stage);
            this.Components.Add(scoreSystem);

            //Ball creation
            ballTex = this.Content.Load<Texture2D>("Images/ball");
            ballPos = new Vector2(stage.X / 2 - ballTex.Width / 2, stage.Y / 2 - ballTex.Height / 2);
            SoundEffect hit = this.Content.Load<SoundEffect>("sounds/click");
            SoundEffect miss = this.Content.Load<SoundEffect>("sounds/ding");

            ball = new Ball(this, spriteBatch, ballTex, ballPos, ballSpeed, stage, hit, miss, scoreSystem);
            this.Components.Add(ball);

            

            //Player creation
            player = 1; 
            Texture2D batTex = this.Content.Load<Texture2D>("Images/BatLeft");
            Vector2 batSpeed = new Vector2(0, 4);
            Vector2 batPos = new Vector2(batTex.Width * (float)1.2f, stage.Y / 2 - batTex.Height / 2);

            bat = new Bat(this, spriteBatch, batTex, batPos, batSpeed, stage, player);
            this.Components.Add(bat);

            player = 2;
            Texture2D batTexPTwo = this.Content.Load<Texture2D>("Images/BatRight");
            Vector2 batSpeedPTwo = new Vector2(0, 4);
            Vector2 batPosPTwo = new Vector2(stage.X - batTexPTwo.Width * (float)2.4f, stage.Y / 2 - batTexPTwo.Height / 2);

            bat2 = new Bat(this, spriteBatch, batTexPTwo, batPosPTwo, batSpeedPTwo, stage, player);
            this.Components.Add(bat2);

            CollisionManager cm = new CollisionManager(this, ball, bat, bat2, hit);
            this.Components.Add(cm);
            

        } 

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState ks = Keyboard.GetState();
            if (scoreSystem.GameWon && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                scoreSystem.Restart();
                bat.RestartPos();
                bat2.RestartPos();

            }
            

            //TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
