using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Threading;

namespace EleTD
{
    class TowerGui
    {
        TileMap tilemap;
        GraphAdjacencyList TowerGraph;
        Texture2D panel;
        Random r = new Random(1);
        
        SpinButton spinButton;
        public static Color[] CollorPallet = { Color.Purple, new Color(1f, 0.98f, 0f), Color.Aqua, Color.YellowGreen, Color.OrangeRed, Color.Brown, Color.WhiteSmoke };
        
        Effect shader;
        List<int> EnemyLowTower=new List<int>();
        List<int> EnemyHighTower = new List<int>();

        public Texture2D[] MenuBallMedium = new Texture2D[6];

        public Texture2D[] MenuBallSmall = new Texture2D[6];
        public List<ElementalTypes> BoughtElementsList = new List<ElementalTypes>();
        public static SpriteFont font;

        public Tower CurrentlySelected = null; // This is True if We have currently CLicked a Tower
        public Tower CurrentlyClicked = null; // This is True if we have Selected a tower to be built.
        public Enemies EnemySelected = null; // If we have selected an Enemy this is the one where it goes.
        public Tower TowerToDisplay;

        public static SellButton sellButton;
        public Texture2D SButton;
        public Texture2D OnHoverPanel;
        public Texture2D SpinElementImage;
        public Texture2D SpinTowerImage;
        public Texture2D UpperBar;
        public DrawAble CircleEffect;

        public int Materia = 10;

        string DefaultTowerName;
        int DefaultTowerCost;

        Rectangle PanelRec;

        public List<Button> ButtonsToDisplay = new List<Button>();
        public List<Tower> TowersInGame;
        public List<Enemies> EnemiesInGame;

        public Vector2 WritingLocation; //Left of the Panel Where PIctures/Icons/Buttons Starts Being Displayed.
        public int Gold = 1000;

        Vector2 TowerDetailLoc = new Vector2(Game1.graphicsDev.Viewport.Width - 350, Game1.graphicsDev.Viewport.Height - 155); //Right Of Panel Where we Write Information.


        public TowerGui(Texture2D panel, Texture2D UpperBar, Texture2D SpinElementImage,
            Texture2D SpinTowerImage, Texture2D SButton, Texture2D onHoverPanel, List<Tower> TowersInGame,
            TileMap tilemap, GraphAdjacencyList TowerGraph, Effect shader, List<Enemies> ListOfEnemies)
        {

            BoughtElementsList.Add(ElementalTypes.Composite);
            DefaultTowerCost = 30;
            this.TowersInGame = TowersInGame;
            this.TowerGraph = TowerGraph;
            this.tilemap = tilemap;
            DefaultTowerName = "Base Tower";
            this.WritingLocation = new Vector2(25, 625);
            this.panel = panel;
            this.shader = shader;
            DisplayDefault();
            this.SButton = SButton;
            this.EnemiesInGame = ListOfEnemies;
            this.OnHoverPanel = onHoverPanel;
            this.SpinElementImage = SpinElementImage;
            this.SpinTowerImage = SpinTowerImage;
            this.PanelRec = new Rectangle(0, Game1.graphicsDev.Viewport.Height - 200, Game1.graphicsDev.Viewport.Width, 200);
            this.spinButton = new SpinButton(new ShadeAnimation(SpinElementImage), new ShadeAnimation(SpinTowerImage), new Vector2(570, 515));
            this.UpperBar = UpperBar;
        }


        private void GetEnemyElement() {
            EnemyLowTower.Clear();
            EnemyHighTower.Clear();
            int index = (int)EnemySelected.Type;
          
            for (int i = 0; i < 6; i++) {
                if (Enemies.damageTable[index, i] == 0.5f) {
                    EnemyLowTower.Add(i);
                }
                if (Enemies.damageTable[index, i] == 1.5f)
                {
                    EnemyHighTower.Add(i);
                }
            }

        }

        public void InitailizeIt(ContentManager Content)
        {

            MenuBallMedium[0] = Content.Load<Texture2D>(@"GUI/ElementMedium/Dark");
            MenuBallMedium[1] = Content.Load<Texture2D>(@"GUI/ElementMedium/Light");
            MenuBallMedium[2] = Content.Load<Texture2D>(@"GUI/ElementMedium/Water");
            MenuBallMedium[3] = Content.Load<Texture2D>(@"GUI/ElementMedium/Wind");
            MenuBallMedium[4] = Content.Load<Texture2D>(@"GUI/ElementMedium/Fire");
            MenuBallMedium[5] = Content.Load<Texture2D>(@"GUI/ElementMedium/Earth");

            MenuBallSmall[0] = Content.Load<Texture2D>(@"GUI/ElementSmall/Dark");
            MenuBallSmall[1] = Content.Load<Texture2D>(@"GUI/ElementSmall/Light");
            MenuBallSmall[2] = Content.Load<Texture2D>(@"GUI/ElementSmall/Water");
            MenuBallSmall[3] = Content.Load<Texture2D>(@"GUI/ElementSmall/Wind");
            MenuBallSmall[4] = Content.Load<Texture2D>(@"GUI/ElementSmall/Fire");
            MenuBallSmall[5] = Content.Load<Texture2D>(@"GUI/ElementSmall/Earth");
            HealthBar=Content.Load<Texture2D>(@"HealthBar");
        
        }

        float DelayBetweenSwitch;
        bool switcher = false;
        Rectangle MouseWorldRect;
        Rectangle MouseScreenRect;
        float TimeSinceMouseClicked;
        const int TimeToDelayClicks = 200;
        public void Update(GameTime gameTime, MouseState mouse)
        {
            MouseScreenRect = new Rectangle((int)mouse.X, (int)mouse.Y, 2, 2);
            MouseWorldRect = new Rectangle((int)mouse.X + (int)Camera.Position.X, (int)mouse.Y + (int)Camera.Position.Y, 2, 2);
            TimeSinceMouseClicked += gameTime.ElapsedGameTime.Milliseconds;

            if (switcher)
            {
                DelayBetweenSwitch += gameTime.ElapsedGameTime.Milliseconds;
            }
            if (DelayBetweenSwitch > 300)
            {
                DelayBetweenSwitch = 0;
                switcher = false;
                FlipFlopButtons();
                if (spinButton.isAnimation1)
                {
                    DisplayNewButtons();
                }
            }

            spinButton.Update(gameTime);
            //Handle Left Mouse Button Click
            if (TimeSinceMouseClicked > TimeToDelayClicks)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    MouseClicked();
                    TimeSinceMouseClicked = 0;
                }
            } //Handle Left Button Click
            if (mouse.RightButton == ButtonState.Pressed)
            {
                NullifyEverything();
            }

            //What Happens When You Press Escape.
            if (InputHandler.KeyPressed(Keys.Escape))
            {
                NullifyEverything();
            }

            //If We have Selected a Tower.
            if (CurrentlyClicked != null)
            {
                Vector2 TileAtClick = tilemap.GetCollTileAtPixel(new Vector2(MouseWorldRect.X, MouseWorldRect.Y));
                TowerToDisplay.Location = TileAtClick * 64;
                if (tilemap.GetCollCellIndex(TileAtClick) == 1)
                {
                    TowerToDisplay.spriteAnimation.TintColor = Color.LightGreen;
                    TowerToDisplay.TurretAnim.TintColor = Color.LightGreen;
                }
                else
                {
                    TowerToDisplay.spriteAnimation.TintColor = Color.Red;
                    TowerToDisplay.TurretAnim.TintColor = Color.Red;
                }
                TowerToDisplay.Update(gameTime);
                FlipFlopButtons();
            }

            //CurrentlySelected
            if (CurrentlySelected != null)
            {
                sellButton.Update(gameTime);
                if (CircleEffect != null)
                {
                    CircleEffect.Update(gameTime);
                }
            }

            UpdateButtons(gameTime);
            DisplayOnHoverToolBar();
        }

        ButtonTower onHoverButton;
        bool DisplayHoverToolBar = false;
        private void DisplayOnHoverToolBar()
        {
            if(spinButton.isAnimation1)
            {
                if (MouseScreenRect.Intersects(PanelRec))
                {
                    foreach (Button b in ButtonsToDisplay)
                    {
                        if (b.CheckIfClicked(MouseWorldRect))
                        {
                            DisplayHoverToolBar = true;
                            onHoverButton = b as ButtonTower;
                            return;
                        }
                    }
                }
            }
            DisplayHoverToolBar = false;
        }

        private void UpdateButtons(GameTime gameTime)
        {
            foreach (Button b in ButtonsToDisplay)
            {
                b.Update(gameTime);
            }
        }



        private void NullifyEverything()
        {
            if (CurrentlySelected != null)
            {
                CurrentlySelected.spriteAnimation.Shader = null;
                CurrentlySelected.TurretAnim.Shader = null;
            }
            if (EnemySelected != null)
            {
                EnemySelected.spriteAnimation.Shader = null;
                EnemySelected = null;
            }
            CurrentlySelected = null;
            CurrentlyClicked = null;
            TowerToDisplay = null;
            FlipFlopButtons();

        }

        private void DisplayElements()
        {
            ButtonsToDisplay.Clear();

            Vector2 DisplayLocation = WritingLocation;
            for (int i = 0; i < 6; i++)
            {
                ButtonElement be = new ButtonElement(new SpriteAnimation(MenuBallMedium[i]), DisplayLocation, (ElementalTypes)i);
                DisplayLocation.X += be.spriteAnimation.CurrentAnimation.CurrentRect.Width + 15;
                ButtonsToDisplay.Add(be);
            }
        }

        private void DisplayDefault()
        {
            ButtonsToDisplay.Clear();
            Tower t = TowerGraph.Search(DefaultTowerName);
            SpriteAnimation anim = new SpriteAnimation(t.ButtonImage);
            anim.AddAnimations("Default", new FrameAnimation(1, t.ButtonImage.Width, t.ButtonImage.Height, 1, 0, 0, new Point(1, 1)));

            ButtonTower b = new ButtonTower(anim, new Vector2(WritingLocation.X, WritingLocation.Y), t, DefaultTowerCost);
            ButtonsToDisplay.Add(b);
        }


        private bool CheckIfElementsRequirementMet(Tower SourceTower, Tower DestinationTower)
        {
            List<Edge> Edges = SourceTower.TowersThisCanBeUpgradedInto;
            Edge ForTowerToBuilt = null;
            foreach (Edge e in Edges)
            {
                if (e.Destination == DestinationTower)
                {
                    ForTowerToBuilt = e;
                    break;
                }
            }

            ElementalTypes[] TypesOfElementsRequired = new ElementalTypes[ForTowerToBuilt.ElementsRequiredToCreate.Count];
            ForTowerToBuilt.ElementsRequiredToCreate.CopyTo(TypesOfElementsRequired);
            List<ElementalTypes> TypeOfElementsRequiredList = new List<ElementalTypes>(TypesOfElementsRequired);

            
            ElementalTypes[] TypesOfElementsBought = new ElementalTypes[BoughtElementsList.Count];
            BoughtElementsList.CopyTo(TypesOfElementsBought);
            List<ElementalTypes> TypesOfElementsBoughtList = new List<ElementalTypes>(TypesOfElementsBought);


            for(int i = TypeOfElementsRequiredList.Count -1; i>=0; i--)
            {
                ElementalTypes TypeReq = TypeOfElementsRequiredList[i];
               for(int j = TypesOfElementsBoughtList.Count -1 ; j>=0; j--)
               {
                   ElementalTypes TypeBought = TypesOfElementsBoughtList[j];

                   if(TypeBought == TypeReq)
                   {
                       TypeOfElementsRequiredList.RemoveAt(i);
                       TypesOfElementsBoughtList.RemoveAt(j);
                       break;
                   }
               }
            }

            if(TypeOfElementsRequiredList.Count == 0)
            {
                return true;
                
            }

            return false;
        }

        //How A Left MouseClick is Handled.
        private void MouseClicked()
        {
            //Code To CheckIf We ClickedOnAnEnemy.
            foreach (Enemies enem in EnemiesInGame)
            {
                if (enem.CheckIfClicked(MouseWorldRect))
                {
                    NullifyEverything();
                    EnemySelected = enem;
                    if(!(EnemySelected.Type.ToString().Equals("Composite"))){
                    GetEnemyElement();
                    }
                    EnemySelected.spriteAnimation.Shader = shader;
                    return;
                }
            }

            //Check if the SpinButtonIsClicked,
            if (spinButton.CheckIfClicked(MouseWorldRect))
            {
                SpinButtonClicked();
            }
            //If We are on the the Tower SelectionScreen.
            if (spinButton.isAnimation1)
            {
                //Check If any of the towers have been Clicked.
                foreach (Tower T in TowersInGame)
                {
                    if (T.CheckIfClicked(MouseWorldRect))
                    {

                        NullifyEverything();
                        CircleEffect = new DrawAble(new SpriteAnimation(CreateCircle((int)T.TowerRange)), T.Location, 0, 0);
                        Vector2 CricleEffectLocation =  CircleEffect.Location;
                        CricleEffectLocation.X -= CircleEffect.spriteAnimation.CurrentAnimation.CurrentRect.Width / 2;
                        CricleEffectLocation.Y -= CircleEffect.spriteAnimation.CurrentAnimation.CurrentRect.Height / 2;
                        CricleEffectLocation.X += T.spriteAnimation.CurrentAnimation.CurrentRect.Width / 2;
                        CricleEffectLocation.Y += T.spriteAnimation.CurrentAnimation.CurrentRect.Height / 2;
                        CircleEffect.Location = CricleEffectLocation;
                        CircleEffect.spriteAnimation.TintColor = Color.Red; 
                        CurrentlySelected = T;
                        CurrentlySelected.spriteAnimation.Shader = shader;
                        CurrentlySelected.TurretAnim.Shader = shader;
                        CreateSellButton();
                        DisplayNewButtons();
                        return;
                    }
                }
                //How To Handle A ButtonClick in TowerSection.
                foreach (Button b in ButtonsToDisplay)
                {
                    ButtonTower BT = b as ButtonTower; //We Know it is of the ButtonTower Kind.

                    if (b.CheckIfClicked(MouseWorldRect))
                    {
                        if (CurrentlySelected != null && CurrentlyClicked == null) //This Statement is True If We Want To update a tower.
                        {
                            Tower t = BT.Value;
                            if (CheckIfElementsRequirementMet(CurrentlySelected, t))
                            {
                                if (Gold >= BT.GoldToCreate)
                                {
                                    Game1.sound.playSound("OnTowerCreation");
                                    //if (r.Next(1, 9) < 4)
                                    //{
                                    //    Game1.sound.playSound("FoolishDecision");
                                    //}
                                    //else if (r.Next(1, 9) > 6)
                                    //{
                                    //    Game1.sound.playSound("SoYouThink");
                                    //}
                                    Gold -= BT.GoldToCreate;
                                    TowersInGame.Add(t.Copy(CurrentlySelected.Location, BT.GoldToCreate / 2));
                                    TowersInGame.Remove(CurrentlySelected);
                                    NullifyEverything();
                                    FlipFlopButtons();
                                }
                                else
                                {
                                    EffectsManager.NoGoldEffect();
                                }
                            }
                            else
                            {
                                EffectsManager.NotEnoughElements();
                            }
                        }
                        else // This Runs When We are making a base Tower
                        {
                            CurrentlyClicked = BT.Value;
                            TowerToDisplay = CurrentlyClicked.Copy(BT.Location, BT.GoldToCreate / 2);
                        }
                        return;
                    }
                } // End Of Button Looping Code for Tower Panel.

                // This Runs When We have Clicked On a Button to Create a tower and we are not intersecting with the panel. It shows the red and green tower.
                if (CurrentlyClicked != null && !(PanelRec.Intersects(MouseScreenRect)))
                {
                    Vector2 TileAtClick = tilemap.GetCollTileAtPixel(new Vector2(MouseWorldRect.X, MouseWorldRect.Y));
                    if (tilemap.GetCollCellIndex(TileAtClick) == 1)
                    {
                        if (Gold >= DefaultTowerCost)
                        {
                            //This Code will Create A Default Tower if you have selected it.C:\Users\Arsinx\documents\visual studio 2010\Projects\EleTD\EleTD\EleTD\EffectsManager.cs
                            Gold -= DefaultTowerCost;
                            Game1.sound.playSound("OnTowerCreation");
                            //if (r.Next(1, 9) < 4)
                            //{
                            //    Game1.sound.playSound("FoolishDecision");
                            //}
                            //else if (r.Next(1, 9) > 6)
                            //{
                            //    Game1.sound.playSound("SoYouThink");
                            //} 
                            TileAtClick *= 64;
                            TowersInGame.Add(CurrentlyClicked.Copy(TileAtClick, DefaultTowerCost));  // We are not Dividing the Tower Cost By 2 Because the basic tower has full Sell price.
                            tilemap.SetCollisionTile((int)TileAtClick.X, (int)TileAtClick.Y, 0);
                            CurrentlyClicked = null;
                            TowerToDisplay = null;
                            return;
                        }
                        else
                        {
                            EffectsManager.NoGoldEffect();
                        }
                    }
                    else
                    {
                        EffectsManager.CantBuildEffect();
                        Game1.sound.playSound("CannotBuildTower");
                        return;
                    }
                } // End OF Selected Tower Display Code.

                //If We Click the Sell Button.
                if (CurrentlySelected != null)
                {
                    if (sellButton.CheckIfClicked(MouseWorldRect))
                    {
                        TowersInGame.Remove(sellButton.Value);
                        Game1.sound.playSound("OnTowerSell");
                        if (r.Next(1,9) >6)
                        {
                         
                            Game1.sound.playSound("FoolishDecision");
                        }
                        
                        EffectsManager.WritingEffect(sellButton.Value.CenterofSprite, sellButton.Value.SellPrice);
                        Gold += sellButton.Value.SellPrice;
                        NullifyEverything();
                        FlipFlopButtons();
                        return;
                    }
                }// End of Sell Code
            }
            else //If Elements Screen is the main screen.
            {
                //How to handle elements if we are in the screen for Elements.
                foreach(Button b in ButtonsToDisplay)
                {
                    if(b.CheckIfClicked(MouseWorldRect))
                    {
                        ButtonElement BE = b as ButtonElement;
                        if (Materia > 0)
                        {
                            BoughtElementsList.Add(BE.Type);
                            Materia--;
                            return;
                        }
                        else {
                            EffectsManager.NotEnoughMateria();
                        }
                    }
                }

                //TowerClick In Elements Section.
                foreach (Tower T in TowersInGame)
                {
                    if (T.CheckIfClicked(MouseWorldRect))
                    {
                        SpinButtonClicked();
                        CurrentlySelected = T;
                        CurrentlySelected.spriteAnimation.Shader = shader;
                        CurrentlySelected.TurretAnim.Shader = shader;
                        CreateSellButton();
                        DisplayNewButtons();
                        
                        return;
                    }
                }
            }
            NullifyEverything();
            FlipFlopButtons();
        }

        private void SpinButtonClicked()
        {

            spinButton.GetAnimation.doCollapse = true;
            spinButton.switchAnim = true;
            NullifyEverything();
            switcher = true;

        }

        private void DisplayNewButtons()
        {
            if (CurrentlySelected == null)
                return;
            ButtonsToDisplay.Clear();

            Tower s = CurrentlySelected;
            //s.DrawRange = false;


            Vector2 DisplayLocation = WritingLocation;
            foreach (Edge e in s.TowersThisCanBeUpgradedInto)
            {
                if (s.TowerName != e.Destination.TowerName)
                {
                    Tower T = e.Destination;
                    SpriteAnimation anim = new SpriteAnimation(T.ButtonImage);
                    anim.AddAnimations("Default", new FrameAnimation(1, T.ButtonImage.Width, T.ButtonImage.Height, 1, 0, 0, new Point(1, 1)));

                    ButtonTower b = new ButtonTower(anim, DisplayLocation, T, e.GoldCost);
                    ButtonsToDisplay.Add(b);
                    DisplayLocation.X += b.spriteAnimation.CurrentAnimation.CurrentRect.Width + 2;
                }
            }
        }


        private void CreateSellButton()
        {
            SellButton sb = new SellButton(SButton, CurrentlySelected, new Vector2(TowerDetailLoc.X + 190, TowerDetailLoc.Y));
            sellButton = sb;
        }

        void FlipFlopButtons()
        {
            if (spinButton.isAnimation1)
            {
                DisplayDefault();
            }
            else
            {
                DisplayElements();
            }
        }





        Vector2 LocationForHoverPanel = new Vector2(Game1.graphicsDev.Viewport.Width - 350, 0);
        Vector2 WhereToWriteFont1 = new Vector2(Game1.graphicsDev.Viewport.Width - 390, 30);
        Vector2 WhereToWriteFont2 = new Vector2(Game1.graphicsDev.Viewport.Width - 320, 30);
        Vector2 WhereToWriteFont3 = new Vector2(Game1.graphicsDev.Viewport.Width - 240, 30);
        Vector2 WhereToWriteFont4 = new Vector2(Game1.graphicsDev.Viewport.Width - 150, 30);
        Vector2 WhereToWriteFont5 = new Vector2(Game1.graphicsDev.Viewport.Width - 55, 30);
        private Texture2D HealthBar;
        public void Draw(SpriteBatch spriteBatch)
        {
            if(panel != null)
            {
                if (CurrentlySelected != null)
                {
                    CircleEffect.Draw(spriteBatch);
                }
                spriteBatch.Begin();
                spriteBatch.Draw(UpperBar, new Vector2(828, 10), Color.White); // Draw Upper Panel.
                spriteBatch.Draw(panel, PanelRec, Color.White); // Draw Lower Panel.
                //Write Information in lower Panel
                spriteBatch.DrawString(ButtonTower.font, "" + Gold, WhereToWriteFont1, Color.Gold);
                spriteBatch.DrawString(ButtonTower.font, "" + Game1.Life, WhereToWriteFont2, Color.Green);
                spriteBatch.DrawString(ButtonTower.font, "" + Materia, WhereToWriteFont3, Color.Violet);
                spriteBatch.DrawString(ButtonTower.font, "" + ((int)((Game1.DelayBetweenSpawns - Game1.TimeElaspedSinceWaveKilled) / 1000) == 5 ? 0 : (int)((Game1.DelayBetweenSpawns - Game1.TimeElaspedSinceWaveKilled) / 1000)) + "", WhereToWriteFont4, Color.Red);
                spriteBatch.DrawString(ButtonTower.font, "" + Game1.WavesElasped+"", WhereToWriteFont5, Color.WhiteSmoke);
                
                if (EnemySelected != null) //Writing about enemy Information.
                {
                    spriteBatch.Draw(EnemySelected.spriteAnimation.texture, new Rectangle(440, 600, 100, 100), EnemySelected.spriteAnimation.GetDefaultAnimation().animations[0], Color.White);
                    //spriteBatch.DrawString(ButtonTower.font, "Element Type: ", new Vector2(Game1.graphicsDev.Viewport.Width - 730, PanelRec.Y + 120), Color.WhiteSmoke);

                    spriteBatch.DrawString(ButtonTower.font, "Type: "+EnemySelected.Type.ToString(), new Vector2(Game1.graphicsDev.Viewport.Width - 568, PanelRec.Y + 78), CollorPallet[(int)EnemySelected.Type]);
                    
                    if ((int)EnemySelected.Type==6)
                    {
                        spriteBatch.DrawString(ButtonTower.font, "100% Damage From All ", new Vector2(Game1.graphicsDev.Viewport.Width - 575, PanelRec.Y + 90), CollorPallet[(int)EnemySelected.Type]);
                    
                    }
                    else {
                        Vector2 test = new Vector2(Game1.graphicsDev.Viewport.Width - 575, PanelRec.Y + 90);
                        for (int i = 0; i < EnemyLowTower.Count; i++)
                        {
                            spriteBatch.DrawString(ButtonTower.font, "50% Damage From " + ((ElementalTypes) EnemyLowTower[i]).ToString(), test, CollorPallet[EnemyLowTower[i]]);
                            test.Y += 12;
                        }

                        for (int i = 0; i < EnemyLowTower.Count; i++)
                        {
                            spriteBatch.DrawString(ButtonTower.font, "150% Damage From " + ((ElementalTypes)EnemyHighTower[i]).ToString(), test, CollorPallet[EnemyHighTower[i]]);
                            test.Y += 12;
                        }
                    
                    }
                    
                    
                    spriteBatch.Draw(HealthBar, new Vector2(Game1.graphicsDev.Viewport.Width - 730, PanelRec.Y + 160), new Rectangle(0, 0, 320, 12), Color.Black);
                   spriteBatch.Draw(HealthBar, new Vector2(Game1.graphicsDev.Viewport.Width - 730, PanelRec.Y + 160), new Rectangle(0, 0, (int)((((float)EnemySelected.health) / (float)(EnemySelected.totalHealth)) * 320), 12), Color.GreenYellow);
                     spriteBatch.DrawString(ButtonTower.font, EnemySelected.health + " / " + EnemySelected.totalHealth, new Vector2(Game1.graphicsDev.Viewport.Width - 600, PanelRec.Y + 155), CollorPallet[(int)EnemySelected.Type]);
                
                
                }
                spriteBatch.End();
            }

            //Draw Spin Buttons.
            spinButton.Draw(spriteBatch);
            
            //Draw Buttons
            foreach (Button b in ButtonsToDisplay)
            {
                b.Draw(spriteBatch);
            }
            
             if (spinButton.isAnimation1)
            {
                //If we have Clicked An Icon that it will draw;
                DrawClickedTowerIcon(spriteBatch);
                //If we have clicked a tower its Information will be displayed here.
                DrawSelectedTowerInfo(spriteBatch);

                if (DisplayHoverToolBar)
                {
                    spriteBatch.Begin();
                    Vector2 WritingForPanel = LocationForHoverPanel;
                    WritingForPanel.Y = PanelRec.Y - 85;
                    WritingForPanel.X += 2;
                    //Drawing Text for Panel.
                    Rectangle OnHoverPanelRec = new Rectangle(Game1.graphicsDev.Viewport.Width - 350, PanelRec.Y - 85, 350, 115);
                    spriteBatch.Draw(OnHoverPanel, OnHoverPanelRec, Color.WhiteSmoke);
                    
                    //Writing For The Panel
                    if (onHoverButton != null)
                    {
                        spriteBatch.DrawString(ButtonTower.font, onHoverButton.Value.TowerName.ToString(), new Vector2(WritingForPanel.X + (OnHoverPanelRec.Width / 2) - ButtonTower.font.MeasureString(onHoverButton.Value.TowerName).X - 2, WritingForPanel.Y), Color.WhiteSmoke);

                        spriteBatch.DrawString(ButtonTower.font, " (Gold: " + onHoverButton.GoldToCreate + ")", new Vector2(WritingForPanel.X + (OnHoverPanelRec.Width / 2) - ButtonTower.font.MeasureString(onHoverButton.Value.TowerName).X + ButtonTower.font.MeasureString(onHoverButton.Value.TowerName).X + 4, WritingForPanel.Y), Color.Gold);

                        WritingForPanel.Y += 20;
                        WritingForPanel.X += 3;

                        Vector2 WritingElementalRequirements = WritingForPanel;
                        spriteBatch.DrawString(ButtonTower.font, "Requirements: ", WritingElementalRequirements, Color.WhiteSmoke);


                        WritingElementalRequirements.X += 107;
                        Edge EdgeChosen = null;

                        foreach (Edge e in onHoverButton.Value.TowersThisCanBeUpgradedInto)
                        {
                            if (e.Destination == onHoverButton.Value)
                            {
                                EdgeChosen = e;
                            }
                        }

                        if (EdgeChosen != null)
                        {
                            foreach (ElementalTypes type in EdgeChosen.ElementsRequiredToCreate)
                            {
                                spriteBatch.DrawString(ButtonTower.font, type.ToString(), WritingElementalRequirements, CollorPallet[(int)type]);
                                WritingElementalRequirements.X += 50;
                            }
                        }
                        else
                        {
                            WritingElementalRequirements.X += 20;
                            spriteBatch.DrawString(ButtonTower.font, "None", WritingElementalRequirements, Color.WhiteSmoke);
                        }

                        WritingForPanel.Y += 15;
                        spriteBatch.DrawString(ButtonTower.font, "Type: ", WritingForPanel, Color.WhiteSmoke);
                        spriteBatch.DrawString(ButtonTower.font, onHoverButton.Value.Type.ToString(), new Vector2(WritingForPanel.X + 50, WritingForPanel.Y), CollorPallet[(int)onHoverButton.Value.Type]);

                        WritingForPanel.Y += 15;
                        spriteBatch.DrawString(ButtonTower.font, "Range: " + onHoverButton.Value.TowerRange.ToString(), WritingForPanel, Color.WhiteSmoke);

                        WritingForPanel.Y += 15;
                        spriteBatch.DrawString(ButtonTower.font, "AoE: " + onHoverButton.Value.Aoe.ToString(), WritingForPanel, Color.WhiteSmoke);

                        WritingForPanel.Y += 15;
                        spriteBatch.DrawString(ButtonTower.font, "Damage: " + onHoverButton.Value.Damage.ToString(), WritingForPanel, Color.WhiteSmoke);

                        WritingForPanel.Y += 15;
                        spriteBatch.DrawString(ButtonTower.font, "Rate Of Attack: " + onHoverButton.Value.DelayBetweenShots, WritingForPanel, Color.WhiteSmoke);
                    }
                        spriteBatch.End();
                }
                
            }
            else
            {
                Vector2 DarkColoumn = new Vector2(Game1.graphicsDev.Viewport.Width - 300, PanelRec.Y + 90);
                Vector2 LightColoumn = new Vector2(DarkColoumn.X + 50, DarkColoumn.Y);
                Vector2 WaterColoumn = new Vector2(LightColoumn.X +50, LightColoumn.Y);
                Vector2 WindColoumn = new Vector2(WaterColoumn.X + 50, WaterColoumn.Y);
                Vector2 FireColoumn = new Vector2(WindColoumn.X + 50, WindColoumn.Y);
                Vector2 EarthColoumn = new Vector2(FireColoumn.X + 50, FireColoumn.Y);
                spriteBatch.Begin();
                spriteBatch.DrawString(ButtonTower.font, "Elements Available", new Vector2(DarkColoumn.X -850, DarkColoumn.Y - 30), Color.WhiteSmoke);
                
                spriteBatch.DrawString(ButtonTower.font, "Elements Bought", new Vector2(DarkColoumn.X +25, DarkColoumn.Y-30), Color.WhiteSmoke);
                
                spriteBatch.DrawString(ButtonTower.font, "Level 1:", new Vector2(DarkColoumn.X-65,DarkColoumn.Y), Color.WhiteSmoke);
                spriteBatch.DrawString(ButtonTower.font, "Level 2:", new Vector2(DarkColoumn.X-65, DarkColoumn.Y+33), Color.WhiteSmoke);
                spriteBatch.DrawString(ButtonTower.font, "Level 3:", new Vector2(DarkColoumn.X-65, DarkColoumn.Y+66), Color.WhiteSmoke);
                //Vector2 
                foreach (ElementalTypes type in BoughtElementsList)
                {
                    
                    if (type == 0)
                    {
                        spriteBatch.Draw(MenuBallSmall[(int)type], DarkColoumn, Color.WhiteSmoke);
                        DarkColoumn.Y += 30;    
                    }
                    else if (type == (ElementalTypes)1)
                    {
                        spriteBatch.Draw(MenuBallSmall[(int)type], LightColoumn, Color.WhiteSmoke);
                        LightColoumn.Y += 30;    
                    }
                    else if (type == (ElementalTypes)2)
                    {
                        spriteBatch.Draw(MenuBallSmall[(int)type], WaterColoumn, Color.WhiteSmoke);
                        WaterColoumn.Y += 30;
                    }
                    else if (type == (ElementalTypes)3)
                    {
                        spriteBatch.Draw(MenuBallSmall[(int)type], WindColoumn, Color.WhiteSmoke);
                        WindColoumn.Y += 30;
                    }
                    else if (type == (ElementalTypes)4)
                    {
                        spriteBatch.Draw(MenuBallSmall[(int)type], FireColoumn, Color.WhiteSmoke);
                        FireColoumn.Y += 30;
                    }
                    else if (type == (ElementalTypes)5)
                    {
                        spriteBatch.Draw(MenuBallSmall[(int)type], EarthColoumn, Color.WhiteSmoke);
                        EarthColoumn.Y += 30;
                    }
                    
                }

                spriteBatch.End();

            }

        }

        private void DrawSelectedTowerInfo(SpriteBatch spriteBatch)
        {
            if (CurrentlySelected != null)
            {

                sellButton.Draw(spriteBatch);

                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Tower Upgrades", new Vector2(85, 572), Color.LightBlue);
                spriteBatch.DrawString(font, CurrentlySelected.TowerName, TowerDetailLoc, Color.Red);
                spriteBatch.DrawString(ButtonTower.font, "Element Type:", new Vector2(TowerDetailLoc.X + 5, TowerDetailLoc.Y + 30), Color.WhiteSmoke);

                spriteBatch.DrawString(ButtonTower.font, CurrentlySelected.Type.ToString(), new Vector2(TowerDetailLoc.X + 125, TowerDetailLoc.Y + 30), CollorPallet[(int)CurrentlySelected.Type]); //Composit


                spriteBatch.DrawString(ButtonTower.font, "Range: " + CurrentlySelected.TowerRange, new Vector2(TowerDetailLoc.X + 5, TowerDetailLoc.Y + 45), Color.WhiteSmoke);
                spriteBatch.DrawString(ButtonTower.font, "Splash: ", new Vector2(TowerDetailLoc.X + 5, TowerDetailLoc.Y + 60), Color.WhiteSmoke);

                if (CurrentlySelected.Aoe <= 500)
                {
                    spriteBatch.DrawString(ButtonTower.font, "Small", new Vector2(TowerDetailLoc.X + 60, TowerDetailLoc.Y + 60), Color.WhiteSmoke);
                }
                else if (CurrentlySelected.Aoe > 500 && CurrentlySelected.Aoe <= 1000)
                {
                    spriteBatch.DrawString(ButtonTower.font, "Medium", new Vector2(TowerDetailLoc.X + 60, TowerDetailLoc.Y + 60), Color.WhiteSmoke);
                }
                else if (CurrentlySelected.Aoe > 1000 && CurrentlySelected.Aoe <= 1500)
                {
                    spriteBatch.DrawString(ButtonTower.font, "Large", new Vector2(TowerDetailLoc.X + 60, TowerDetailLoc.Y + 60), Color.WhiteSmoke);
                }
                else if (CurrentlySelected.Aoe > 1500 && CurrentlySelected.Aoe <= 2000)
                {
                    spriteBatch.DrawString(ButtonTower.font, "Massive", new Vector2(TowerDetailLoc.X + 60, TowerDetailLoc.Y + 60), Color.WhiteSmoke);
                }

                spriteBatch.DrawString(ButtonTower.font, "Sell Price: " + CurrentlySelected.SellPrice, new Vector2(TowerDetailLoc.X + 225, TowerDetailLoc.Y + 65), Color.Gold);

                spriteBatch.DrawString(ButtonTower.font, "Damage: " + CurrentlySelected.Damage, new Vector2(TowerDetailLoc.X + 5, TowerDetailLoc.Y + 75), Color.WhiteSmoke);
                spriteBatch.DrawString(ButtonTower.font, "Rate of Attack: " + 1000 / CurrentlySelected.DelayBetweenShots + " Shots Per Second", new Vector2(TowerDetailLoc.X + 5, TowerDetailLoc.Y + 90), Color.WhiteSmoke);


                spriteBatch.End();
            }
        }


        private void DrawClickedTowerIcon(SpriteBatch spriteBatch)
        {
            if (CurrentlyClicked != null && !(PanelRec.Intersects(MouseScreenRect)))
            {
                TowerToDisplay.Draw(spriteBatch);
            }
        }


        private Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(Game1.graphicsDev, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Black * 0f;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {

                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.Red;
            }

            texture.SetData(data);
            return texture;
        }
    }
}