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
        Texture2D texture { get; set; }
        SpriteBatch spriteBatch;
        Beater principal;
        List<_2ndGrader> ndGraders = new List<_2ndGrader>();
        WaveCount waveCount;
        Random rand = new Random();
        float elapsed;
        int qtdInimigos = 5;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            principal = new Beater(this);
            waveCount = new WaveCount(this);
            
            //Inicia os inimigos
            for (int i = 0; i != qtdInimigos; i++)
            {
                ndGraders.Add
                (
                    new _2ndGrader(this, new Point(rand.Next(600), rand.Next(300)))
                );
            }

        }
        
        protected override void Initialize()
        {
            principal.Initialize();
            waveCount.Initialize();

            for (int i = 0; i != ndGraders.Count; i++)
            {
                ndGraders[i].Initialize();
            }

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = this.Content.Load<Texture2D>("mapa");
            for (int i = 0; i != ndGraders.Count; i++)
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
            //Tempo decorrido
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //Movimentação do personagem principal e alguns atalhos.
            //Sair
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();    
            //Movimentação
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                principal.Move(Beater.Directions.Up,gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                principal.Move(Beater.Directions.Down, gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                principal.Move(Beater.Directions.Left, gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                principal.Move(Beater.Directions.Right, gameTime);
            //Especial e Ação
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                principal.shout();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                int direct = 3;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    direct = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    direct = 1;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    direct = 2;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    direct = 3;

                principal.Hit(direct);
            }
                
            //Tempo de Recuperação do Especial
            principal.recoveryPower -= elapsed;

            //Checar Vida e Colisões
            for (int i = 0; i < ndGraders.Count; i++)
            {
                //Checar vida
                if (ndGraders[i].CheckLife() > 0)
                {
                    //Seguir o personagem principal
                    ndGraders[i].Move(principal.position.X, principal.position.Y, gameTime);
                    if (elapsed > 150)
                    {
                        //Colisão
                        if (ndGraders[i].Bounds.Intersects(principal.Bounds))
                        {
                            //Se os inimigos encostarem no principal, principal leva dano
                            principal.takeDamage();

                            //Se acabar a vida do principal, acaba o jogo
                            if(principal.CheckLife() <= 0)
                            {
                                //"Game over"
                                this.Exit();
                            }
                        }

                        if (ndGraders[i].Bounds.Intersects(principal.HitBounds))
                        {
                            //Se o principal bater nos inimigos, eles levam dano
                            ndGraders[i].takeDamage();
                        }
                        
                        elapsed = 0;
                    }
                }
                else
                {
                    //Se a vida estiver zerada, remove da lista
                    ndGraders.Remove(ndGraders[i]);
                }
                
            }

            if (ndGraders.Count == 0)
            {
                //Se acabaram os inimigos, muda de onda
                waveCount.winPoint();
                ndGraders = new List<_2ndGrader>();
                //Reinicia os inimigos
                int vida = 5;
                if(waveCount.checkWave()%5 == 0)
                {
                    qtdInimigos = 1;
                    vida = 50;
                }else
                {
                    qtdInimigos = 5;
                    vida = 5;
                }
                for (int i = 0; i != qtdInimigos; i++)
                {
                    ndGraders.Add
                    (
                        new _2ndGrader(this, new Point(rand.Next(600), rand.Next(300)),vida)
                    );
                }

                for (int i = 0; i != ndGraders.Count; i++)
                {
                    ndGraders[i].Initialize();
                }

                for (int i = 0; i != ndGraders.Count; i++)
                {
                    ndGraders[i].LoadContent(this);
                }

            }

            waveCount.Update(gameTime);


            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                texture,
                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                Color.White
                );
            spriteBatch.End();
            

            waveCount.life = principal.CheckLife();
            waveCount.time = principal.CheckPower();
            waveCount.Draw(gameTime);
            principal.Draw(gameTime);

            if (ndGraders.Count > 0)
            {
                for (int i = 0; i != ndGraders.Count; i++)
                {
                    ndGraders[i].Draw(gameTime);
                }
            }
            
            base.Draw(gameTime);
        }
        
    }
}
