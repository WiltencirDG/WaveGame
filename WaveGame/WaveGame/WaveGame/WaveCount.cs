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
    public class WaveCount : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Point position { get; set; }
        int actual { get; set; }
        public int life { get; set; }
        public float time { get; set; }
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
            //Desenho para contador das ondas
            spriteBatch.Begin();
            spriteBatch.DrawString(waveFont, "Wave: " + actual, new Vector2(position.X, position.Y), Color.Black);
            spriteBatch.End();
            //Desenho para contador da vida
            spriteBatch.Begin();
            spriteBatch.DrawString(waveFont, "Life: " + this.life, new Vector2(position.X, position.Y+15), Color.Black);
            spriteBatch.End();
            //Desenho para contador do tempo restante do poder
            spriteBatch.Begin();
            spriteBatch.DrawString(waveFont, "Power: " + Math.Round(this.time/1000,2) + " s", new Vector2(position.X, position.Y + 30), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void winPoint()
        {
            //Aumenta a onda atual
            this.actual++;
        }

    }
}
