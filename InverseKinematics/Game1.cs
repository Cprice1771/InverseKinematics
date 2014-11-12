#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace InverseKinematics
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _boneTexture;
        Texture2D _effectorTexture;
        List<Bone> bones;

        public Game1()
            : base()
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

            this.IsMouseVisible = true;
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

            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1500;
            graphics.ApplyChanges();

            _boneTexture = Content.Load<Texture2D>("arm");
            _effectorTexture = Content.Load<Texture2D>("effector");

            bones = new List<Bone>();
            for (int i = 0; i < 4; i++)
            {
                if(i != 0)
                    bones.Add(new Bone(_boneTexture, new Vector2(500, 400), new Effector(_effectorTexture), bones[i - 1]));
                else
                    bones.Add(new Bone(_boneTexture, new Vector2(700, 500), new Effector(_effectorTexture), null));
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            bool done = false;

            for (int i = 0; i < 20; i++)
            {
                for (int j = 3; j >= 0 ; j--)
                {
                    bones[j].UpdatePosition();

                    for (int k = 3; k >= 0; k--)
                        bones[k].Update(gameTime);

                    if (Vector2.Distance(bones[0].Effector.Position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 10)
                    {
                        done = true;
                        break;
                    }
                }
                
                if (done)
                    break;
            }

          
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for(int i = 3; i >= 0; i--)
                bones[i].Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
