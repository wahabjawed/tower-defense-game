using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Sage_Engine;
namespace EleTD
{
    public static class Menu
    {
      public static Texture2D[] MenuBall = new Texture2D[6];
        static Vector2[] MenuBallPos = new Vector2[6];
        static Vector2[] MenuBallCenter = new Vector2[6];
        static Texture2D[] MenuButton = new Texture2D[3];
        static Vector2[] MenuButtonPos = new Vector2[3];
        static Texture2D menuScreen;
        static Texture2D Title;
        static int mIndex = 0;
        public static void LoadContent(ContentManager Content)
        {

            menuScreen = Content.Load<Texture2D>(@"Menu/MenuScreen");

            Title = Content.Load<Texture2D>(@"Menu/Title");

            MenuBall[0] = Content.Load<Texture2D>(@"Menu/Fire");
            MenuBall[1] = Content.Load<Texture2D>(@"Menu/wind");
            MenuBall[2] = Content.Load<Texture2D>(@"Menu/Water");
            MenuBall[3] = Content.Load<Texture2D>(@"Menu/Earth");
            MenuBall[4] = Content.Load<Texture2D>(@"Menu/Light");
            MenuBall[5] = Content.Load<Texture2D>(@"Menu/darkness");
            shaderEffect = Content.Load<Effect>(@"Shader/ZoomBlur");

            MenuBallPos[0] = new Vector2(100, 550);
            MenuBallPos[1] = new Vector2(300, 550);
            MenuBallPos[2] = new Vector2(500, 550);
            MenuBallPos[3] = new Vector2(700, 550);
            MenuBallPos[4] = new Vector2(900, 550);
            MenuBallPos[5] = new Vector2(1100, 550);

            MenuButton[0] = Content.Load<Texture2D>(@"Menu/start");
            MenuButton[1] = Content.Load<Texture2D>(@"Menu/instruction");
            MenuButton[2] = Content.Load<Texture2D>(@"Menu/end");

            MenuButtonPos[0] = new Vector2(500, 250);
            MenuButtonPos[1] = new Vector2(500, 350);
            MenuButtonPos[2] = new Vector2(500, 450);

            shaderEffect1 = Content.Load<Effect>(@"Shader/Select");


            for (int i = 0; i < 6; i++)
            {
                MenuBallCenter[i] = new Vector2(MenuBallPos[i].X + (MenuBall[i].Width / 2) - 30, MenuBallPos[i].Y + (MenuBall[i].Height / 2) - 20);

            }

        }

        public static float TimeSinceSpawn;
        static float elapsedTime;
        static bool swit = true;
        private static Effect shaderEffect;
        private static Effect shaderEffect1;
        public static void Update(GameTime gameTime)
        {

            Camera.Position = Vector2.Zero;

            TimeSinceSpawn += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (TimeSinceSpawn >= 500)
            {
                TimeSinceSpawn = 0;
                EffectsManager.MenuSparkEffect(MenuBallCenter[0], Color.OrangeRed, Color.Yellow);
                EffectsManager.MenuSparkEffect(MenuBallCenter[1], Color.YellowGreen, Color.SandyBrown);
                EffectsManager.MenuSparkEffect(MenuBallCenter[2], Color.Aqua, Color.Blue);
                EffectsManager.MenuSparkEffect(MenuBallCenter[3], Color.BurlyWood, Color.Brown);
                EffectsManager.MenuSparkEffect(MenuBallCenter[4], Color.WhiteSmoke, Color.LightGoldenrodYellow);
                EffectsManager.MenuSparkEffect(MenuBallCenter[5], Color.BlueViolet, Color.Blue);
            }


            if (elapsedTime < 2000 && swit)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsedTime > 1900)
                {
                    swit = false;

                }

            }
            else if (elapsedTime > 0)
            {
                elapsedTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTime < 100)
                {
                    elapsedTime = 0;
                    swit = true;

                }
            }
            shaderEffect.Parameters["BlurAmount"].SetValue(elapsedTime / 30000);
            shaderEffect.Parameters["Center"].SetValue(0.5f);
            shaderEffect.Parameters["Brightness"].SetValue(elapsedTime / 30000);

            if (Game1.currentState == Game1.States.menu)
            {
                if (InputHandler.KeyPressed(Keys.Up))
                {
                    mIndex--;
                    Game1.sound.playSound("MenuButtonClick");
                }
                else if (InputHandler.KeyPressed(Keys.Down))
                {
                    mIndex++;
                    Game1.sound.playSound("MenuButtonClick");
                }

                if (mIndex > 2)
                {
                    mIndex = 0;
                }
                else if (mIndex < 0)
                {
                    mIndex = 2;
                }


                if (InputHandler.KeyReleased(Keys.Enter))
                {
                    if (mIndex == 0)
                    {
                        Game1.sound.pauseSound("MainMenu");
                        Game1.currentState = Game1.States.arcade;
                        EffectsManager.ClearEffects();
                    }
                    else if (mIndex == 1)
                    {
                        Game1.sound.pauseSound("MainMenu");

                        Game1.currentState = Game1.States.instructions;
                        Game1.sound.playSound("PauseScreen");
                        //Game1.sound.pauseSound("Instruction");

                        EffectsManager.ClearEffects();
                    }
                    else
                    {
                        Game1.Exited = true;
                    }
                }
            }
            
            EffectsManager.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(menuScreen, new Rectangle(0, 0, Game1.graphicsDev.Viewport.Width, Game1.graphicsDev.Viewport.Height), Color.White);

            spriteBatch.End();

            EffectsManager.Draw(spriteBatch);


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            for (int i = 0; i < MenuButton.Length; i++)
            {


                spriteBatch.Draw(MenuButton[i], new Rectangle((int)MenuButtonPos[i].X, (int)MenuButtonPos[i].Y, MenuButton[i].Width, MenuButton[i].Height), Color.White);

            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            shaderEffect1.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Draw(MenuButton[mIndex], new Rectangle((int)MenuButtonPos[mIndex].X, (int)MenuButtonPos[mIndex].Y, MenuButton[mIndex].Width, MenuButton[mIndex].Height), Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            for (int i = 0; i < 6; i++)
            {

                spriteBatch.Draw(MenuBall[i], new Rectangle((int)MenuBallPos[i].X, (int)MenuBallPos[i].Y, MenuBall[i].Width, MenuBall[i].Height), Color.White);

            }
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            shaderEffect.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Draw(Title, new Rectangle(200, 10, Title.Width, Title.Height), Color.White);
            spriteBatch.End();


        }

    }
}
