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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class WaveCount : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Point position { get; set; }
        int actual { get; set; }
        public int life { get; set; }
        SpriteBatch spriteBatch;
        SpriteFont waveFont;

        public WaveCount(Game game)
            : base(game)
        {
            position = new Point(600, 0);
            actual = 1;
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public void LoadContent(Game game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            waveFont = game.Content.Load<SpriteFont>("waveFont");
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(waveFont, "Wave: " + actual, new Vector2(position.X, position.Y), Color.Black);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(waveFont, "Life: " + this.life, new Vector2(position.X, position.Y+15), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void winPoint()
        {
            this.actual++;
        }

    }
}
