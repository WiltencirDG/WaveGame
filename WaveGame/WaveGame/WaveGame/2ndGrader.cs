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
   
    public class _2ndGrader : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum Directions { Up, Down, Left, Right}
        public Point position { get; set; }
        public int speed { get; set; }
        Texture2D texture { get; set; }
        SpriteBatch spriteBatch;
        int frames;
        int framesY;
        int qtdFrames;
        float elapsed;


        public _2ndGrader(Game game)
            : base(game)
        {
            position = new Point(200, 300);
            speed = 1;
            frames = 0;
            qtdFrames = 4;
        }

        public _2ndGrader(Game game, Point pos)
            : base(game)
        {
            position = pos;
            speed = 1;
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
                        
            if (elapsed > 200)
            {
                if (frames >= qtdFrames-1)
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
                new Rectangle(position.X, position.Y, texture.Width/ qtdFrames, texture.Height/ qtdFrames),new Rectangle((texture.Width / qtdFrames) *frames,(texture.Height / qtdFrames) *framesY, texture.Width / qtdFrames, texture.Height /qtdFrames),
                Color.White
                );
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Move(float X, float Y, GameTime gameTime)
        {
            if (position.X < X)
            {
                position = new Point(position.X + speed, position.Y);
                framesY = 2;
                this.Update(gameTime);
            }
            if (position.Y < Y)
            {
                position = new Point(position.X, position.Y + speed);
                framesY = 0;
                this.Update(gameTime);
            }
            if (position.X > X)
            {
                position = new Point(position.X - speed, position.Y);
                framesY = 1;
                this.Update(gameTime);
            }
            if (position.Y > Y)
            {
                position = new Point(position.X, position.Y - speed);
                framesY = 3;
                this.Update(gameTime);
            }            
        }
    }
}