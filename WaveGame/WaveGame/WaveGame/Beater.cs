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
        public enum States { Idle, Walking }
        public Point position { get; set; }
        public int speed { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle HitBounds { get; set; }
        Texture2D texture { get; set; }
        SpriteBatch spriteBatch;
        States estados;
        int frames;
        int framesY;
        int qtdFrames;
        float elapsed;
        int life;

        SoundEffect hit;
        SoundEffectInstance toHit;
        SoundEffect take;
        SoundEffectInstance toTake;
        SoundEffect power;
        SoundEffectInstance toPower;

        public Beater(Game game)
            : base(game)
        {
            position = new Point(200, 300);
            speed = 5;
            frames = 0;
            qtdFrames = 4;
            life = 15;
            //Bounds = new Rectangle(position.X, position.Y, this.texture.Width, this.texture.Height);
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public void LoadContent(Game game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = game.Content.Load<Texture2D>("2ndGraders");

            //Dar Dano
            hit = game.Content.Load<SoundEffect>("beater_hit");
            toHit = hit.CreateInstance();

            //Levar Dano
            take = game.Content.Load<SoundEffect>("beater_take");
            toTake = take.CreateInstance();

            //Power
            power = game.Content.Load<SoundEffect>("power");
            toPower = power.CreateInstance();
        }
        
        public override void Update(GameTime gameTime)
        {
            Bounds = new Rectangle(this.position.X, this.position.Y, this.texture.Width / qtdFrames /2 , this.texture.Height / qtdFrames /2);
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed > 70)
            {
                HitBounds = new Rectangle(this.position.X, this.position.Y, this.texture.Width / qtdFrames / 2, this.texture.Height / qtdFrames / 2);
            }

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

        public void Hit()
        {
            this.HitBounds = new Rectangle(this.position.X, this.position.Y, this.texture.Width, this.texture.Height / qtdFrames / 2);
        }

        public void takeDamage()
        {
            this.life = this.life - 1;
            if (toTake.State != SoundState.Playing)
                toTake.Play();
        }

        public int CheckLife()
        {
            return this.life;
        }

        public void shout()
        {
            if (toPower.State != SoundState.Playing)
                toPower.Play();
            HitBounds = new Rectangle(this.position.X, this.position.Y, this.texture.Width, this.texture.Height);
        }

    }
}
