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

    public class Beater : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum Directions { Up, Down, Left, Right }
        public Point position { get; set; }
        public int speed { get; set; }
        Texture2D texture { get; set; }
        SpriteBatch spriteBatch;
        int frames;
        int framesY;
        int qtdFrames;
        float elapsed;


        public Beater(Game game)
            : base(game)
        {
            position = new Point(200, 300);
            speed = 5;
            frames = 0;
            qtdFrames = 4;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public void LoadContent(Game game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = game.Content.Load<Texture2D>("2ndGraders");
        }

        public override void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                framesY = 3;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                framesY = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                framesY = 2;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                framesY = 1;

            if (elapsed > 200)
            {
                if (frames >= qtdFrames - 1)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture,
                new Rectangle(position.X, position.Y, texture.Width / qtdFrames, texture.Height / qtdFrames), new Rectangle((texture.Width / qtdFrames) * frames, (texture.Height / qtdFrames) * framesY, texture.Width / qtdFrames, texture.Height / qtdFrames),
                Color.White
                );
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Move(Directions d, GameTime gameTime)
        {
            switch (d)
            {
                case Directions.Up: position = new Point(position.X, position.Y - speed); this.Update(gameTime); break;
                case Directions.Down: position = new Point(position.X, position.Y + speed); this.Update(gameTime); break;
                case Directions.Left: position = new Point(position.X - speed, position.Y); this.Update(gameTime); break;
                case Directions.Right: position = new Point(position.X + speed, position.Y); this.Update(gameTime); break;
            }
        }

    }
}
