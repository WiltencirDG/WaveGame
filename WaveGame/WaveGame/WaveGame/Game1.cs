using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WaveGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Beater principal;
        List<_2ndGrader> ndGraders = new List<_2ndGrader>();
        WaveCount waveCount;
        Random rand = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            principal = new Beater(this);
            waveCount = new WaveCount(this);

            for(int i = 1; i != 10; i++)
            {
                ndGraders.Add
                    (
                        new _2ndGrader(this, new Point(rand.Next(500),rand.Next(300)))
                    );
            }
        }
        
        protected override void Initialize()
        {
            principal.Initialize();
            waveCount.Initialize();

            for (int i = 1; i != ndGraders.Count; i++)
            {
                ndGraders[i].Initialize();
            }

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            for (int i = 1; i != ndGraders.Count; i++)
            {
                ndGraders[i].LoadContent(this);
            }

            waveCount.LoadContent(this);
            principal.LoadContent(this);
        }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();    
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                principal.Move(Beater.Directions.Up,gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                principal.Move(Beater.Directions.Down, gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                principal.Move(Beater.Directions.Left, gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                principal.Move(Beater.Directions.Right, gameTime);

            for (int i = 1; i != ndGraders.Count; i++)
            {
                ndGraders[i].Move(principal.position.X, principal.position.Y, gameTime);
            }          

            waveCount.Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            waveCount.Draw(gameTime);
            principal.Draw(gameTime);

            for (int i = 1; i != ndGraders.Count; i++)
            {
                ndGraders[i].Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
