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
using Sage_Engine;

namespace EleTD
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        int newGametime=5000;
        int newGameWaitTime = 0;

        public static GraphicsDevice graphicsDev;

        int totaltime = 0;
        public static SoundLibrary sound = new SoundLibrary();

        public static Effect flipEffect;

        //int no = 10000;
        int g = 0;
        int timer = 0;

        int instructionCounter = 1;
        // int sIndex = 0;
        Texture2D background;
        Texture2D[] logoAnim;

        Cue menusong;
        // game Game;

        public enum States
        {
            logo,
            menu,
            arcade,
            pause,
            instructions

        }

        Texture2D instructionScreen;

        public static States currentState;

        SpriteBatch spriteBatch;
        TileMap Map;
        List<Enemies> listOfEnemies = new List<Enemies>();

        Queue<Queue<Enemies>> Waves = new Queue<Queue<Enemies>>();

        Queue<Enemies> SpawnQueue = new Queue<Enemies>();

        List<Tower> TowersCurrentlyInGame = new List<Tower>();

        SpriteFont font;
        TowerGui towerGui;
        GraphAdjacencyList TowerGraph = new GraphAdjacencyList();

        Dictionary<String, Enemies> enemiesWeHaveMade = new Dictionary<string, Enemies>();

        const int DelaYBetweenWaves = 2000;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
            ;
        }

        Rectangle Top;
        Rectangle Bottom;
        Rectangle Left;
        Rectangle Right;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sound.Initailise();

            currentState = States.logo;

            logoAnim = new Texture2D[41];

            Components.Add(new InputHandler(this));


            base.Initialize();
            graphicsDev = graphics.GraphicsDevice;

            AstarMovement movement = new AstarMovement(Map, new Vector2(8, 0));
            Enemies.Initialise(movement.FindPath(new Vector2(3, 0), new Vector2(8, 0)), Content.Load<Texture2D>("Greenbar"));

            towerGui = new TowerGui(Content.Load<Texture2D>(@"GUI/toolbar-latest"), Content.Load<Texture2D>(@"GUI/upper-toolbar"), Content.Load<Texture2D>(@"GUI/element"), Content.Load<Texture2D>(@"GUI/TOWERS"), Content.Load<Texture2D>("GUI/SellButton"), Content.Load<Texture2D>("GUI/onHover3"), TowersCurrentlyInGame, Map, TowerGraph, Content.Load<Effect>(@"Shader/Select"), listOfEnemies);
            towerGui.InitailizeIt(Content);
            Menu.LoadContent(Content);
            CreateEnemyDictionary();

            //Uncomment to Create Enemies.
            Wave();
            Camera.Initialise(GraphicsDevice);

            Top = new Rectangle(0, 0, Camera.ScreenWidth, 40);
            Bottom = new Rectangle(0, Camera.ScreenHeight - 40, Camera.ScreenWidth, 40);
            Left = new Rectangle(0, 0, 40, Camera.ScreenHeight);
            Right = new Rectangle(Camera.ScreenWidth - 40, 0, 40, Camera.ScreenHeight);

        }

        private void Wave()
        {
            for (int i = 0; i < 22; i++)
            {
                //SpawnQueue.Enqueue(enemiesWeHaveMade["EarthCreep"].Copy(100, 20));
                SpawnQueue.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(45, 13, 1, ElementalTypes.Composite));
            }


            // for corresponding waves
            Queue<Enemies> wave2 = new Queue<Enemies>();

            for (int i = 0; i < 23; i++)
            {
                wave2.Enqueue(enemiesWeHaveMade["Earth1"].Copy(45, 15, 2, ElementalTypes.Earth));

            }

            Waves.Enqueue(wave2);


            Queue<Enemies> wave3 = new Queue<Enemies>();
            for (int i = 0; i < 30; i++)
            {
                wave3.Enqueue(enemiesWeHaveMade["Electric"].Copy(45, 22, 2, ElementalTypes.Light));

            }

            Waves.Enqueue(wave3);


            Queue<Enemies> wave4 = new Queue<Enemies>();
            for (int i = 0; i < 30; i++)
            {
                wave4.Enqueue(enemiesWeHaveMade["Fire1"].Copy(55, 60, 2, ElementalTypes.Fire));
            }
            Waves.Enqueue(wave4);
            

            Queue<Enemies> wave5 = new Queue<Enemies>();
            for (int i = 0; i < 35; i++)
            {
                wave5.Enqueue(enemiesWeHaveMade["Earth2"].Copy(60, 150, 2, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave5);



            Queue<Enemies> wave6 = new Queue<Enemies>();
            for (int i = 0; i < 35; i++)
            {
                wave6.Enqueue(enemiesWeHaveMade["Darkness1"].Copy(60, 150, 2, ElementalTypes.Wind));
            }
            Waves.Enqueue(wave6);



            Queue<Enemies> wave7 = new Queue<Enemies>();
            for (int i = 0; i < 35; i++)
            {
                wave7.Enqueue(enemiesWeHaveMade["Darkness2"].Copy(60, 200, 2, ElementalTypes.Light));
            }
            Waves.Enqueue(wave7);



            Queue<Enemies> wave8 = new Queue<Enemies>();
            for (int i = 0; i < 45; i++)
            {
                wave8.Enqueue(enemiesWeHaveMade["EarthDarkness"].Copy(60, 220, 2, ElementalTypes.Earth));
            }
            Waves.Enqueue(wave8);



            Queue<Enemies> wave9 = new Queue<Enemies>();
            for (int i = 0; i < 45; i++)
            {
                wave9.Enqueue(enemiesWeHaveMade["Creep7"].Copy(60, 240, 2, ElementalTypes.Fire));
            }
            Waves.Enqueue(wave9);



            Queue<Enemies> wave10 = new Queue<Enemies>();
            for (int i = 0; i < 50; i++)
            {
                wave10.Enqueue(enemiesWeHaveMade["Fire1"].Copy(65, 260, 3, ElementalTypes.Fire));
            }
            Waves.Enqueue(wave10);



            Queue<Enemies> wave11 = new Queue<Enemies>();
            for (int i = 0; i < 50; i++)
            {
                wave11.Enqueue(enemiesWeHaveMade["Creep8"].Copy(65, 280, 3, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave11);


            Queue<Enemies> wave12 = new Queue<Enemies>();
            for (int i = 0; i < 50; i++)
            {
                wave12.Enqueue(enemiesWeHaveMade["Creep5"].Copy(65, 280, 3, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave12);



            Queue<Enemies> wave13 = new Queue<Enemies>();
            for (int i = 0; i < 50; i++)
            {
                wave13.Enqueue(enemiesWeHaveMade["Fire1"].Copy(68, 280, 3, ElementalTypes.Light));
            }
            Waves.Enqueue(wave13);



            Queue<Enemies> wave14 = new Queue<Enemies>();
            for (int i = 0; i < 50; i++)
            {
                wave14.Enqueue(enemiesWeHaveMade["EarthDarkness"].Copy(68, 300, 3, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave14);



            Queue<Enemies> wave15 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave15.Enqueue(enemiesWeHaveMade["Electric"].Copy(68, 220, 4, ElementalTypes.Fire));
            }
            Waves.Enqueue(wave15);



            Queue<Enemies> wave16 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave16.Enqueue(enemiesWeHaveMade["Darkness2"].Copy(60, 260, 4, ElementalTypes.Dark));
            }
            Waves.Enqueue(wave16);

            Queue<Enemies> wave17 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave17.Enqueue(enemiesWeHaveMade["Darkness1"].Copy(60, 280, 5, ElementalTypes.Dark));
            }
            Waves.Enqueue(wave17);



            Queue<Enemies> wave18 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave18.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(50, 290, 5, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave18);



            Queue<Enemies> wave19 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave19.Enqueue(enemiesWeHaveMade["Earth1"].Copy(50, 310, 5, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave19);



            Queue<Enemies> wave20 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave20.Enqueue(enemiesWeHaveMade["Earth2"].Copy(50, 330, 14, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave20);



            Queue<Enemies> wave21 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave21.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(50, 340, 14, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave21);
            
            Queue<Enemies> wave22 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave22.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(50, 380, 16, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave22);



            Queue<Enemies> wave23 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave23.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(50, 420, 20, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave23);



            Queue<Enemies> wave24 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave24.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(50, 80, 20, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave24);



            Queue<Enemies> wave25 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave25.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(50, 80, 20, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave25);



            Queue<Enemies> wave26 = new Queue<Enemies>();
            for (int i = 0; i < 23; i++)
            {
                wave26.Enqueue(enemiesWeHaveMade["EarthSprite"].Copy(50, 80, 20, ElementalTypes.Composite));
            }
            Waves.Enqueue(wave26);



        }

        private void CreateEnemyDictionary()
        {
            
            SpriteAnimation anim = new SpriteAnimation(creep1);
            anim.AddAnimations("Right", new FrameAnimation(
            10,
            creep1.Width / 6,
            creep1.Height,
            6,
            0,
            0,
            new Point(6, 1)));
            Enemies enemy1 = new Enemies(anim, new Vector2(240, 1), 100, 0, 0);

            SpriteAnimation anim2 = new SpriteAnimation(creep2);
            anim2.AddAnimations("Right", new FrameAnimation(
            10,
            creep2.Width / 4,
            creep2.Height,
            4,
            0, 0,
            new Point(4, 1)));

            Enemies enemey2 = new Enemies(anim2,
            new Vector2(240, 1), 50, 0, 0);

            SpriteAnimation anim3 = new SpriteAnimation(creep3);
            anim3.AddAnimations("Right", new FrameAnimation(
            10,
            creep3.Width / 6,
            creep3.Height,
            6,
            0,
            0,
            new Point(6, 1)));
            Enemies enemy3 = new Enemies(anim3, new Vector2(240, 1), 100, 0, 0);

            SpriteAnimation anim4 = new SpriteAnimation(creep4);
            anim4.AddAnimations("Right", new FrameAnimation(
            10,
            creep4.Width / 4,
            creep4.Height,
            4,
            0,
            0,
            new Point(4, 1)));

            Enemies enemy4 = new Enemies(anim4, new Vector2(240, 1), 100, 0, 0);

            SpriteAnimation anim5 = new SpriteAnimation(creep5);
            anim5.AddAnimations("Right", new FrameAnimation(
            10,
            creep5.Width / 6,
            creep5.Height,
            6,
            0,
            0,
            new Point(6, 1)));

            Enemies enemy5 = new Enemies(anim5, new Vector2(240, 1), 100, 0, 0);

            SpriteAnimation anim6 = new SpriteAnimation(creep6);
            anim6.AddAnimations("Right", new FrameAnimation(
            10,
            creep6.Width / 4,
            creep6.Height,
            4,
            0,
            0,
            new Point(4, 1)));

            Enemies enemy6 = new Enemies(anim6, new Vector2(240, 1), 100, 0, 0);

            SpriteAnimation anim7 = new SpriteAnimation(creep7);
            anim7.AddAnimations("Right", new FrameAnimation(
            10,
            creep7.Width / 6,
            creep7.Height,
            6,
            0,
            0,
            new Point(6, 1)));

            Enemies enemy7 = new Enemies(anim7, new Vector2(10, 10), 100, 0, 0);

            SpriteAnimation anim8 = new SpriteAnimation(creep8);
            anim8.AddAnimations("Right", new FrameAnimation(
            10,
            creep8.Width / 4,
            creep8.Height,
            4,
            0,
            0,
            new Point(4, 1)));

            Enemies enemy8 = new Enemies(anim8, new Vector2(240, 1), 100, 0, 0);
            
            SpriteAnimation animEarth = new SpriteAnimation(EarthSprite);
            animEarth.AddAnimations("Right", new FrameAnimation(
            6,
            EarthSprite.Width / 2,
            EarthSprite.Height,
            2,
            0,
            0,
            new Point(2, 1)));

            Enemies EarthEnemy = new Enemies(animEarth, new Vector2(240, 1), 100, 0, 0);
            SpriteAnimation animDarkness = new SpriteAnimation(DarknessSprite);
            Enemies DarkEnemy = new Enemies(animDarkness, Vector2.Zero, 75, 0, 100);

            SpriteAnimation animDarkness2 = new SpriteAnimation(DarknessSprite2);
            Enemies DarkEnemy2 = new Enemies(animDarkness2, Vector2.Zero, 10, 0, 100);

            SpriteAnimation animEarthDarkness = new SpriteAnimation(earthdarkness);
            animEarthDarkness.AddAnimations("D", new FrameAnimation(16, earthdarkness.Width / 4, earthdarkness.Height, 4, 0, 0, new Point(4, 1)));
            Enemies EarthDarkEnemey = new Enemies(animEarthDarkness, Vector2.Zero, 0, 0, 100);

            SpriteAnimation AnimEarth1 = new SpriteAnimation(earth1);
            Enemies EarthEnemey1 = new Enemies(AnimEarth1, Vector2.Zero, 10, 0, 100);

            SpriteAnimation AnimEarth2 = new SpriteAnimation(earth2);
            Enemies EarthEnemy2 = new Enemies(AnimEarth2, Vector2.Zero, 0, 0, 10);

            SpriteAnimation AnimElectric = new SpriteAnimation(electric1);
            AnimElectric.AddAnimations("D", new FrameAnimation(3, electric1.Width, electric1.Height / 4, 4, 0, 0, new Point(1, 4)));
            Enemies ElectricEnem = new Enemies(AnimElectric, Vector2.Zero, 0, 0, 0);

            SpriteAnimation AnimFire1 = new SpriteAnimation(fire1);
            AnimFire1.AddAnimations("D", new FrameAnimation(8, fire1.Width, fire1.Height / 2, 2, 0, 0, new Point(1, 2)));
            Enemies FireEmen = new Enemies(AnimFire1, Vector2.Zero, 0, 0, 0);

            enemiesWeHaveMade.Add("EarthSprite", EarthEnemy);
            enemiesWeHaveMade.Add("Electric", ElectricEnem);
            enemiesWeHaveMade.Add("Earth2", EarthEnemy2);
            enemiesWeHaveMade.Add("Earth1", EarthEnemey1);
            enemiesWeHaveMade.Add("EarthDarkness", EarthDarkEnemey);
            enemiesWeHaveMade.Add("Darkness2", DarkEnemy2);
            enemiesWeHaveMade.Add("Darkness1", DarkEnemy);
            enemiesWeHaveMade.Add("EarthCreep", EarthEnemy);
            enemiesWeHaveMade.Add("Creep1", enemy1);
            enemiesWeHaveMade.Add("Creep2", enemey2);
            enemiesWeHaveMade.Add("Creep3", enemy3);
            enemiesWeHaveMade.Add("Creep4", enemy4);
            enemiesWeHaveMade.Add("Creep5", enemy5);
            enemiesWeHaveMade.Add("Creep6", enemy6);
            enemiesWeHaveMade.Add("Creep7", enemy7);
            enemiesWeHaveMade.Add("Creep8", enemy8);
            enemiesWeHaveMade.Add("Fire1", FireEmen);

        }

        Texture2D earthdarkness;
        Texture2D earth1;
        Texture2D earth2;
        Texture2D electric1;
        Texture2D fire1;
        Texture2D lightdarknesswater;
        Texture2D water1;
        Texture2D water2;
        Texture2D wind1;
        Texture2D DarknessSprite2;
        Texture2D DarknessSprite;
        Texture2D EarthSprite;
        Texture2D creep1;
        Texture2D creep2;
        Texture2D creep3;
        Texture2D creep4;
        Texture2D creep5;
        Texture2D creep6;
        Texture2D creep7;
        Texture2D creep8;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            instructionScreen = Content.Load<Texture2D>(@"instruction1");
            for (int i = 0; i < 41; i++)
            {

                logoAnim[i] = Content.Load<Texture2D>("SilverSages/Silver Sages" + (10000 + i));
            }
            background = logoAnim[0];

            EarthSprite = Content.Load<Texture2D>("Sprites/EarthSprite");

            flipEffect = Content.Load<Effect>(@"Shader/CoinFlip");


            Dictionary<int, string> Textures;
            Map = TileMap.ReadInMap(Content.RootDirectory + "/EleMapRevised.map", out Textures);
            Map.LoadImages(Content, Textures);

            creep1 = Content.Load<Texture2D>("Sprites/creep-1-redSprite");
            creep2 = Content.Load<Texture2D>("Sprites/creep-2-redSprite");
            creep3 = Content.Load<Texture2D>("Sprites/creep-1-blueSprite");
            creep4 = Content.Load<Texture2D>("Sprites/creep-2-blueSprite");
            creep5 = Content.Load<Texture2D>("Sprites/creep-1-greenSprite");
            creep6 = Content.Load<Texture2D>("Sprites/creep-2-greenSprite");
            creep7 = Content.Load<Texture2D>("Sprites/creep-1-yellowSprite");
            creep8 = Content.Load<Texture2D>("Sprites/creep-2-yellowSprite");

            DarknessSprite = Content.Load<Texture2D>("Sprites/darkness1");
            DarknessSprite2 = Content.Load<Texture2D>("Sprites/darkness2");
            earthdarkness = Content.Load<Texture2D>("Sprites/earth-darkenss");
            earth1 = Content.Load<Texture2D>("Sprites/earth1");
            earth2 = Content.Load<Texture2D>("Sprites/earth2");
            electric1 = Content.Load<Texture2D>("Sprites/electric1");
            fire1 = Content.Load<Texture2D>("Sprites/fire1");
            lightdarknesswater = Content.Load<Texture2D>("Sprites/light-darkness-earth");
            water1 = Content.Load<Texture2D>("Sprites/water1");
            water2 = Content.Load<Texture2D>("Sprites/water2");
            wind1 = Content.Load<Texture2D>("Sprites/wind1");
            InitializeBullets();
            InitlizeGraphs();
            InitializeEffects();

            font = Content.Load<SpriteFont>("Fonrt");
            ButtonTower.smallerFont = Content.Load<SpriteFont>("SpriteFont1");
            ButtonTower.font = Content.Load<SpriteFont>("MediumSizedFont");
            TowerGui.font = font;
            EffectsManager.font = ButtonTower.smallerFont;
        }

        private void InitializeEffects()
        {

            EffectsManager.Initialisze(Content, GraphicsDevice);

        }


        private void InitlizeGraphs()
        {

            SpriteAnimation[] Base = new SpriteAnimation[7];
            SpriteAnimation[] cannon = new SpriteAnimation[7];
            Edge temp;
            for (int i = 1; i < 8; i++)
            {
                Texture2D bases = Content.Load<Texture2D>("Bases/turret-" + i + "-base");
                Texture2D turret = Content.Load<Texture2D>("Bases/turret-" + i + "-cannon");
                Base[i - 1] = new SpriteAnimation(bases);
                Base[i - 1].AddAnimations("d", new FrameAnimation(1, bases.Width, bases.Height, 1, 0, 0, new Point(1, 1)));
                cannon[i - 1] = new SpriteAnimation(turret);
                cannon[i - 1].AddAnimations("d", new FrameAnimation(1, turret.Width, turret.Height, 1, 0, 0, new Point(1, 1)));
            }

            //Ceate Base Tower
            SpriteAnimation AnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/basetower_base"));
            SpriteAnimation AnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/basetower_turret"));
           
            //Fire Tower.
              SpriteAnimation fireAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/fire_base"));
            SpriteAnimation fireAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/fire_turret"));
            TowerGraph.CreateTwoTowers("Base Tower", "Fire", 60, new TowerDataHolder(AnimBase, AnimTurret, ElementalTypes.Composite, Content.Load<Texture2D>("Buttons/Arrow-b"), 198, 550, 1, 75),
           new TowerDataHolder(fireAnimBase, fireAnimTurret, ElementalTypes.Fire, Content.Load<Texture2D>("Buttons/Fire-b"), 200, 500, 7, 125), ElementalTypes.Fire);
            Tower BaseTower = TowerGraph.Search("Base Tower");

            //Texture2D baseTex = Content.Load<Texture2D>("Projectiles/basetower_projectile");

            BaseTower.SpecificBullet = new Bullet(new SpriteAnimation(Content.Load<Texture2D>("Projectiles/basetower_projectile")), Vector2.Zero, 100, 10, 100);
            BaseTower.SpecificBullet.AoeEffect = new NoAoeBase();
            
            Tower FireTower = TowerGraph.Search("Fire");
            Texture2D fireTex = Content.Load<Texture2D>("Projectiles/fire_projectile");
            FrameAnimation fireFrames = new FrameAnimation(5,fireTex.Width/4,fireTex.Height,4,0,0,new Point(4,1));
            SpriteAnimation firesp = new SpriteAnimation(fireTex);
            firesp.AddAnimations("D", fireFrames); 
            FireTower.SpecificBullet = new Bullet(firesp);
            FireTower.SpecificBullet.AoeEffect = new StraightAoeFire(5);
            FireTower.AutoRotate=true;
            //End Fire Tower.

            //Start Creation of Water/BarfiTower
            SpriteAnimation waterAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/water_base"));
            SpriteAnimation waterAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/water_turret"));
            TowerGraph.CreateTwoTowers("Base Tower", "Water", 60, null, new TowerDataHolder(waterAnimBase, waterAnimTurret, ElementalTypes.Water,
                Content.Load<Texture2D>("Buttons/Water-b"), 200, 450 , 7, 78), ElementalTypes.Water);
            Tower WaterT = TowerGraph.Search("Water");
            WaterT.SpecificBullet = new Bullet(new SpriteAnimation(Content.Load<Texture2D>("Projectiles/barfi_projectile")), Vector2.Zero, 100, 10, 100);
            WaterT.SpecificBullet.AoeEffect = new StraightAoeFrost(2);
            WaterT.SpecificBullet.isGrow = true;
            //End Creation of water/Barfi Tower

            //start of wind tower
            SpriteAnimation windAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/wind_base"));
            SpriteAnimation windAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/wind_turret"));
            TowerGraph.CreateTwoTowers("Base Tower", "Wind", 60, null,
                new TowerDataHolder(windAnimBase, windAnimTurret, ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Wind"),210, 400, 2, 125 ), ElementalTypes.Wind);
            Tower WindT = TowerGraph.Search("Wind");
            WindT.SpecificBullet = new Bullet(new SpriteAnimation(Content.Load<Texture2D>("Bases/wind_turret")));
            WindT.SpecificBullet.AoeEffect = new StraightAoeDamage(5);
            WindT.AutoRotate = true;
            WindT.BaseRotate = true;
            //end of wind tower


            //Start Creation of Ligntinig/Tower
            SpriteAnimation lightAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/lightning_base"));
            SpriteAnimation lightAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/lightning_turret"));
            TowerGraph.CreateTwoTowers("Base Tower", "Light", 60, null, new TowerDataHolder(lightAnimBase, lightAnimTurret, ElementalTypes.Light,
                Content.Load<Texture2D>("Buttons/Light-b"), 220, 400, 10, 0), ElementalTypes.Light);
            Tower LightT = TowerGraph.Search("Light");
            LightT.SpecificBullet = new Bullet(new SpriteAnimation(Content.Load<Texture2D>("Projectiles/Light_Projectile")), Vector2.Zero, 100, 10, 100);
            LightT.SpecificBullet.AutoRotate = true;
            LightT.SpecificBullet.AoeEffect = new NoAoeLight();
            //End Creation of Lightning/Tower


            // Start Construciton of DarkTower
            SpriteAnimation darkAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/darkness_base"));
            SpriteAnimation darkAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/dark_turret"));
            Texture2D Dark_Projectile = Content.Load<Texture2D>("Projectiles/Darkness_Projectile");
            SpriteAnimation DarkBulletAnim = new SpriteAnimation(Dark_Projectile);
            DarkBulletAnim.AddAnimations("Key", new FrameAnimation(3, Dark_Projectile.Width / 4, Dark_Projectile.Height, 4, 0, 0, new Point(4, 1)));
            TowerGraph.CreateTwoTowers("Base Tower", "Dark", 60, null, new TowerDataHolder(darkAnimBase, darkAnimTurret, ElementalTypes.Dark,
                Content.Load<Texture2D>("Buttons/Dark-b"), 220, 600, 9, 0), ElementalTypes.Dark);
            Tower DarkTower = TowerGraph.Search("Dark");
            Bullet DarkBullet = new Bullet(DarkBulletAnim);
            DarkTower.SpecificBullet = DarkBullet;
            DarkTower.SpecificBullet.AoeEffect = new NoAoeDark();
            //End Construction of DarkTower.

            //TowerGraph.CreateTwoTowers("Base Tower", "Light", 60, null, new TowerDataHolder(Base[2], cannon[2], ElementalTypes.Light, Content.Load<Texture2D>("Buttons/Light-b"), 110, 100, 40, 0));

            //Earth Tower Construction:
            SpriteAnimation EarthAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/earth_basic_base"));
            SpriteAnimation EarthAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/earth_basic_turret"));
            Bullet EarthBullet = new Bullet(new SpriteAnimation(Content.Load<Texture2D>("Projectiles/rock_projectile")));
            EarthBullet.AutoRotate = true;
            EarthBullet.AoeEffect = new StraightAoeEarth(2);

            TowerGraph.CreateTwoTowers("Base Tower", "Earth", 60, null,
                new TowerDataHolder(EarthAnimBase, EarthAnimTurret, ElementalTypes.Earth, Content.Load<Texture2D>("Buttons/Earth-b"), 210, 1300, 15, 110), ElementalTypes.Earth);
            Tower EarthT = TowerGraph.Search("Earth");
            EarthT.AutoRotate = true;
            EarthT.SpecificBullet = EarthBullet;
            //End Earth Construction

            //pyro

            SpriteAnimation PyroAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/pyro-lava"));
            TowerGraph.CreateTwoTowers("Fire", "Pyro", 120, null,
                new TowerDataHolder(PyroAnimBase, PyroAnimBase, ElementalTypes.Earth, Content.Load<Texture2D>("Buttons/Pyro"), 250, 450, 4, 0),
            ElementalTypes.Earth, ElementalTypes.Fire);
            Tower PyroT = TowerGraph.Search("Pyro");
            Texture2D PyroTex = Content.Load<Texture2D>("Projectiles/Pyro_Projectile");
            SpriteAnimation PyroAnim = new SpriteAnimation(PyroTex);
            PyroAnim.AddAnimations("D", new FrameAnimation(12, PyroTex.Width / 6, PyroTex.Height, 6, 0, 0, new Point(6, 1)));

            PyroT.SpecificBullet = new Bullet(PyroAnim, Vector2.Zero, 100, 40, 100);
            PyroT.AutoRotate = true;
            PyroT.SpecificBullet.AoeEffect = new StraightAoeFire(5);




            TowerGraph.CreateTwoTowers("Fire", "Grazer", 130,
                null, new TowerDataHolder(windAnimBase, windAnimTurret, ElementalTypes.Light, Content.Load<Texture2D>("Buttons/Grazer"), 250, 200, 25 , 0), ElementalTypes.Fire, ElementalTypes.Light);
            Tower GrazerT = TowerGraph.Search("Grazer");
            Texture2D GrazerTex = Content.Load<Texture2D>("Projectiles/Pyro_Projectile");
            SpriteAnimation GrazerAnim = new SpriteAnimation(GrazerTex);
            GrazerAnim.AddAnimations("D", new FrameAnimation(12, PyroTex.Width / 6, PyroTex.Height, 6, 0, 0, new Point(6, 1)));

            GrazerT.SpecificBullet = new Bullet(GrazerAnim, Vector2.Zero, 100, 40, 100);
            GrazerT.AutoRotate = true;
            GrazerT.SpecificBullet.AoeEffect = new StraightAoeFire(5);

            //gumpowder
            SpriteAnimation GunAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/gunpowder-base"));
            SpriteAnimation GunAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/gunpowder-turret"));

            TowerGraph.CreateTwoTowers("Fire", "Gunpowder",120, null,
                new TowerDataHolder(GunAnimBase, GunAnimTurret, ElementalTypes.Fire, Content.Load<Texture2D>("Buttons/gunpowder"), 260, 600, 28, 0),
            ElementalTypes.Dark, ElementalTypes.Fire);
            Tower GunT = TowerGraph.Search("Gunpowder");
            Texture2D GunTex = Content.Load<Texture2D>("Projectiles/Pyro_Projectile");
            SpriteAnimation GunAnim = new SpriteAnimation(GunTex);
            GunAnim.AddAnimations("D", new FrameAnimation(12, GunTex.Width / 6, GunTex.Height, 6, 0, 0, new Point(6, 1)));

            GunT.SpecificBullet = new Bullet(GunAnim, Vector2.Zero, 100, 40, 100);
            GunT.AutoRotate = false;
            GunT.SpecificBullet.AoeEffect = new NoAoeGun();




            TowerGraph.CreateTwoTowers("Fire", "Steam", 110,
                null, new TowerDataHolder(GunAnimBase, GunAnimTurret, ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Meltdown"), 220, 100, 35, 0), ElementalTypes.Water, ElementalTypes.Fire);
            Tower SteamT = TowerGraph.Search("Steam");
            SteamT.SpecificBullet = new Bullet(GunAnim, Vector2.Zero, 100, 40, 100);
            SteamT.AutoRotate = false;
            SteamT.SpecificBullet.AoeEffect = new NoAoeGun();

            //heatwave
            SpriteAnimation heatAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/heatwave_base"));
            SpriteAnimation heatAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/heatwave_turret"));

             TowerGraph.CreateTwoTowers("Fire", "Heatwave", 120,
                null, new TowerDataHolder(heatAnimBase, heatAnimTurret, ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Heatwave-b"), 260, 600, 34, 0), ElementalTypes.Fire, ElementalTypes.Wind);
           
              
            Tower HeatT = TowerGraph.Search("Heatwave");
           Texture2D HeatTex = Content.Load<Texture2D>("Projectiles/Pyro_Projectile");
            SpriteAnimation HeatAnim = new SpriteAnimation(HeatTex);
            HeatAnim.AddAnimations("H", new FrameAnimation(12, HeatTex.Width / 6, HeatTex.Height, 6, 0, 0, new Point(6, 1)));
            HeatT.SpecificBullet = new Bullet(HeatAnim, Vector2.Zero, 100, 40, 100);
            HeatT.AutoRotate = true;
            HeatT.SpecificBullet.AoeEffect = new NoAoeGun();
            
            
            
            
          
            
           
            //sandstorm
           SpriteAnimation sandAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/sandstrom_base"));
            SpriteAnimation sandAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/sandstrom_turret"));

            TowerGraph.CreateTwoTowers("Earth", "Sandstorm", 125, null, new TowerDataHolder(sandAnimBase, sandAnimTurret, ElementalTypes.Earth, Content.Load<Texture2D>("Buttons/Sandstorm"), 210, 500, 35, 0), ElementalTypes.Wind, ElementalTypes.Earth);

            Tower SandT = TowerGraph.Search("Sandstorm");
            Texture2D SandTex = Content.Load<Texture2D>("Projectiles/Pyro_Projectile");
            SpriteAnimation SandAnim = new SpriteAnimation(SandTex);
            SandAnim.AddAnimations("G", new FrameAnimation(12, SandTex.Width / 6, SandTex.Height, 6, 0, 0, new Point(6, 1)));
            SandT.SpecificBullet = new Bullet(SandAnim, Vector2.Zero, 100, 40, 100);
            SandT.AutoRotate = false;
            SandT.SpecificBullet.AoeEffect = new NoAoeGun();



            //earth
            TowerGraph.CreateTwoTowers("Earth", "Tidal", 135,
                null, new TowerDataHolder(sandAnimBase, sandAnimTurret, ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Tsunami"), 250, 250, 30, 0), ElementalTypes.Earth, ElementalTypes.Water);
            Tower TidalT = TowerGraph.Search("Tidal");
            TidalT.SpecificBullet = new Bullet(SandAnim, Vector2.Zero, 100, 40, 100);
            TidalT.AutoRotate = false;
            TidalT.SpecificBullet.AoeEffect = new NoAoeGun();

            
            //shardTower
            
           
            SpriteAnimation shardAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/shard_base"));
            SpriteAnimation shardAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/shard_turret"));
            TowerGraph.CreateTwoTowers("Earth", "Shard", 150, null, new TowerDataHolder(shardAnimBase, shardAnimTurret, ElementalTypes.Earth, Content.Load<Texture2D>("Buttons/shard"), 200, 250, 5, 0), ElementalTypes.Light, ElementalTypes.Earth);
            Tower ShardT = TowerGraph.Search("Shard");

            Texture2D shardTex = Content.Load<Texture2D>("Projectiles/shard_particle");
            FrameAnimation shardFrames = new FrameAnimation(5, shardTex.Width, shardTex.Height/5, 5, 0, 0, new Point(1, 5));
            SpriteAnimation shardsp = new SpriteAnimation(shardTex);
            shardsp.AddAnimations("E", shardFrames);
            ShardT.SpecificBullet = new Bullet(shardsp);
            ShardT.SpecificBullet.AoeEffect = new StraightAoeDamage(5);
            ShardT.AutoRotate = true;
            ShardT.BaseRotate = true;
            





            //poison
            SpriteAnimation PoisonAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/poison"));
            TowerGraph.CreateTwoTowers("Earth", "Poison", 130, null, new TowerDataHolder(PoisonAnimBase, PoisonAnimBase, ElementalTypes.Dark, Content.Load<Texture2D>("Buttons/Poison"), 250, 600, 35, 0), ElementalTypes.Dark, ElementalTypes.Earth);
            Tower poisonT = TowerGraph.Search("Poison");
            Texture2D poisonTex = Content.Load<Texture2D>("Projectiles/Pyro_Projectile");
            SpriteAnimation poisonAnim = new SpriteAnimation(poisonTex);
            poisonAnim.AddAnimations("D", new FrameAnimation(12, poisonTex.Width / 6, poisonTex.Height, 6, 0, 0, new Point(6, 1)));
            poisonT.AutoRotate = true;
            poisonT.SpecificBullet = new Bullet(PyroAnim, Vector2.Zero, 100, 40, 100);

            //TowerGraph.CreateTwoTowers("Earth", "Pyro", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));

            temp = TowerGraph.CreateEdge(TowerGraph.Search("Earth"), TowerGraph.Search("Pyro"), 50);
            TowerGraph.Search("Earth").InsertEdge(temp);

            //dark
            //TowerGraph.CreateTwoTowers("Dark", "Poison", 39, null, new TowerDataHolder(Base[3], cannon[3], 200, 600, 3));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Dark"), TowerGraph.Search("Poison"), 50);
            TowerGraph.Search("Dark").InsertEdge(temp);


            SpriteAnimation BeelzeAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/beelze-base"));
            SpriteAnimation BeelzeAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/sword_turret"));
            TowerGraph.CreateTwoTowers("Dark", "Beelze", 150, null, new TowerDataHolder(BeelzeAnimBase, BeelzeAnimTurret, ElementalTypes.Dark, Content.Load<Texture2D>("Buttons/Beelze - dark"), 230, 500, 36, 0), ElementalTypes.Dark);
            Tower BeelzeT = TowerGraph.Search("Beelze");
            Texture2D BeelzeTex = Content.Load<Texture2D>("Bases/beelze-turret");
            SpriteAnimation BeelzeAnim = new SpriteAnimation(BeelzeTex);;
            BeelzeT.BaseRotate = true;
            BeelzeT.SpecificBullet = new Bullet(BeelzeAnim, Vector2.Zero, 100, 40, 100);
            BeelzeT.AutoRotate = true;
            BeelzeT.SpecificBullet.AoeEffect = new NoAoeBeelze();
            BeelzeT.SpecificBullet.AutoRotate = true;


            //TowerGraph.CreateTwoTowers("Dark", "Beelze", 25, null, new TowerDataHolder(Base[2], cannon[2], ElementalTypes.Dark, Content.Load<Texture2D>("Buttons/Acid-b"), 110, 100, 40, 0));

            //TowerGraph.CreateTwoTowers("Dark", "Gunpowder", 39, null, new TowerDataHolder(Base[3], cannon[3], 200, 600, 3));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Dark"), TowerGraph.Search("Gunpowder"), 50);
            TowerGraph.Search("Dark").InsertEdge(temp);
            TowerGraph.CreateTwoTowers("Dark", "Styx", 115, null, new TowerDataHolder(Base[2], cannon[2], ElementalTypes.Dark, Content.Load<Texture2D>("Buttons/Styx"), 110, 150, 20, 0), ElementalTypes.Dark);


            // Start Construciton of VortexTower
            SpriteAnimation vortexAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/vortex_base"));
            SpriteAnimation vortexAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/vortex_turret"));
            Texture2D Vortex_Projectile = Content.Load<Texture2D>("Projectiles/could_vortex");
            SpriteAnimation VortexBulletAnim = new SpriteAnimation(Vortex_Projectile);
            VortexBulletAnim.AddAnimations("Key", new FrameAnimation(3, Vortex_Projectile.Width / 3, Vortex_Projectile.Height, 3, 0, 0, new Point(3, 1)));
            TowerGraph.CreateTwoTowers("Dark", "Vortex", 200, null, new TowerDataHolder(vortexAnimBase, vortexAnimTurret, ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Vortex"), 210,300, 40, 0), ElementalTypes.Wind, ElementalTypes.Dark);
            Tower VortexTower = TowerGraph.Search("Vortex");
            Bullet VortexBullet = new Bullet(VortexBulletAnim);
            VortexTower.SpecificBullet = VortexBullet;
            VortexTower.SpecificBullet.AutoRotate = true;
            VortexTower.SpecificBullet.AoeEffect = new NoAoeVortex();
            //End Construction of VortexTower.

            //            TowerGraph.CreateTwoTowers("Dark", "Vortex", 39, null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Dark, Content.Load<Texture2D>("Buttons/Acid-b"), 200, 600, 3, 0));

            //light

            // TowerGraph.CreateTwoTowers("Light", "Tidal", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Light"), TowerGraph.Search("Tidal"), 50);
            TowerGraph.Search("Light").InsertEdge(temp);

            TowerGraph.CreateTwoTowers("Light", "Polar", 160,
                null, new TowerDataHolder(waterAnimBase, waterAnimTurret, ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Polar"), 260, 500, 28, 0), ElementalTypes.Light,ElementalTypes.Water);
            Tower PolarT = TowerGraph.Search("Polar");
            PolarT.SpecificBullet = new Bullet(new SpriteAnimation(Content.Load<Texture2D>("Projectiles/barfi_projectile")), Vector2.Zero, 100, 10, 100);
            PolarT.SpecificBullet.AoeEffect = new StraightAoeFrost(1);
            PolarT.SpecificBullet.isGrow = true;
            
            //temptest
            SpriteAnimation tempestAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/tempest_base"));
            SpriteAnimation tempestAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/tempest_turret"));
            TowerGraph.CreateTwoTowers("Light", "Temptest", 145,
                null, new TowerDataHolder(tempestAnimBase, tempestAnimTurret, ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Tempest"), 210, 100, 20, 0), ElementalTypes.Light,ElementalTypes.Wind);
            Tower TempestTower = TowerGraph.Search("Temptest");
            TempestTower.AutoRotate=true;

            
            //TowerGraph.CreateTwoTowers("Light", "Shard", 39, null, new TowerDataHolder(Base[3], cannon[3], 200, 600, 3));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Light"), TowerGraph.Search("Shard"), 50);
            TowerGraph.Search("Light").InsertEdge(temp);
            //TowerGraph.CreateTwoTowers("Light", "Beelze", 39, null, new TowerDataHolder(Base[3], cannon[3], 200, 600, 3));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Light"), TowerGraph.Search("Beelze"), 50);
            TowerGraph.Search("Light").InsertEdge(temp);

            //wind 

            //TowerGraph.CreateTwoTowers("Wind", "Heatwave", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Wind"), TowerGraph.Search("Heatwave"), 50);
            TowerGraph.Search("Wind").InsertEdge(temp);

            //TowerGraph.CreateTwoTowers("Wind", "Temptest", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Wind"), TowerGraph.Search("Temptest"), 50);
            TowerGraph.Search("Wind").InsertEdge(temp);

            //TowerGraph.CreateTwoTowers("Wind", "Vortex", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Wind"), TowerGraph.Search("Vortex"), 50);
            TowerGraph.Search("Wind").InsertEdge(temp);

            
            //whirlpool
            SpriteAnimation whilrpoolAnimBase = new SpriteAnimation(Content.Load<Texture2D>("Bases/whirlpool_base"));
            SpriteAnimation whirlpooltAnimTurret = new SpriteAnimation(Content.Load<Texture2D>("Bases/whirlpool_turret"));
            TowerGraph.CreateTwoTowers("Wind", "Whirlpool", 170,
                null, new TowerDataHolder(whilrpoolAnimBase, whirlpooltAnimTurret, ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Water-ele"), 300, 800, 40, 0), ElementalTypes.Wind, ElementalTypes.Water);
            Tower WhilrpoolTower = TowerGraph.Search("Whirlpool");
            WhilrpoolTower.AutoRotate = true;



            //TowerGraph.CreateTwoTowers("Wind", "Sandstorm", 39, null, new TowerDataHolder(Base[3], cannon[3], 200, 600, 3));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Wind"), TowerGraph.Search("Sandstorm"), 50);
            TowerGraph.Search("Wind").InsertEdge(temp);

            //water

            //TowerGraph.CreateTwoTowers("Water", "Steam", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Water"), TowerGraph.Search("Steam"), 50);
            TowerGraph.Search("Water").InsertEdge(temp);

            //TowerGraph.CreateTwoTowers("Water", "Tidal", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Water"), TowerGraph.Search("Tidal"), 50);
            TowerGraph.Search("Water").InsertEdge(temp);

            //TowerGraph.CreateTwoTowers("Water", "Polar", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Water"), TowerGraph.Search("Polar"), 50);
            TowerGraph.Search("Water").InsertEdge(temp);

            //TowerGraph.CreateTwoTowers("Water", "Styx", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Water"), TowerGraph.Search("Styx"), 50);
            TowerGraph.Search("Water").InsertEdge(temp);

            //TowerGraph.CreateTwoTowers("Water", "Whirlpool", 25, null, new TowerDataHolder(Base[2], cannon[2], Content.Load<Texture2D>("Buttons/Acid-b"),110, 100, 40));
            temp = TowerGraph.CreateEdge(TowerGraph.Search("Water"), TowerGraph.Search("Whirlpool"), 50);
            TowerGraph.Search("Water").InsertEdge(temp);


            TowerGraph.CreateTwoTowers("Pyro", "Lava", 350,
               null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Earth, Content.Load<Texture2D>("Buttons/Lava-b"), 350, 350, 175, 0), ElementalTypes.Fire, ElementalTypes.Earth, ElementalTypes.Fire, ElementalTypes.Earth);

            TowerGraph.CreateTwoTowers("Lava", "Magma", 880,
               null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Earth, Content.Load<Texture2D>("Buttons/Magma"), 500, 800, 500, 0), ElementalTypes.Fire, ElementalTypes.Earth, ElementalTypes.Fire, ElementalTypes.Earth, ElementalTypes.Fire, ElementalTypes.Earth);
            //aoe
            TowerGraph.CreateTwoTowers("Sandstorm", "Dune", 425,
              null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Sandstorm"), 350, 300, 195, 0), ElementalTypes.Wind, ElementalTypes.Earth, ElementalTypes.Wind, ElementalTypes.Earth);

            TowerGraph.CreateTwoTowers("Dune", "Tomb", 1025,
              null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Tomb"), 550, 200, 400, 0), ElementalTypes.Wind, ElementalTypes.Earth, ElementalTypes.Wind, ElementalTypes.Earth, ElementalTypes.Wind, ElementalTypes.Earth);

            TowerGraph.CreateTwoTowers("Tidal", "Flood", 395,
               null, new TowerDataHolder(Base[2], cannon[2], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Flood-b"), 380, 420, 190, 0), ElementalTypes.Water, ElementalTypes.Earth, ElementalTypes.Water, ElementalTypes.Earth);

            TowerGraph.CreateTwoTowers("Flood", "Tsunami", 950,
              null, new TowerDataHolder(Base[2], cannon[2], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Tsunami"), 110, 100, 40, 0), ElementalTypes.Water, ElementalTypes.Earth, ElementalTypes.Water, ElementalTypes.Earth, ElementalTypes.Water, ElementalTypes.Earth);


            TowerGraph.CreateTwoTowers("Shard", "Gem", 400,
              null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Shard"), 450, 350, 200, 0), ElementalTypes.Light, ElementalTypes.Earth, ElementalTypes.Light, ElementalTypes.Earth);


            TowerGraph.CreateTwoTowers("Gem", "Sabre", 900,
              null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Beamberg-b"), 110, 600, 4, 0), ElementalTypes.Light, ElementalTypes.Earth, ElementalTypes.Light, ElementalTypes.Earth, ElementalTypes.Light, ElementalTypes.Earth);




            TowerGraph.CreateTwoTowers("Poison", "Venom", 325,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Dark, Content.Load<Texture2D>("Buttons/Poison"), 110, 600, 4, 0), ElementalTypes.Dark, ElementalTypes.Earth, ElementalTypes.Dark, ElementalTypes.Earth, ElementalTypes.Dark, ElementalTypes.Earth);

            TowerGraph.CreateTwoTowers("Venom", "Basilisk", 875,
    null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Dark, Content.Load<Texture2D>("Buttons/Styx"), 110, 600, 4, 0), ElementalTypes.Dark, ElementalTypes.Earth, ElementalTypes.Dark, ElementalTypes.Earth, ElementalTypes.Dark, ElementalTypes.Earth, ElementalTypes.Dark, ElementalTypes.Earth);


            TowerGraph.CreateTwoTowers("Heatwave", "Meltdown", 380,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Meltdown"), 110, 600, 4, 0), ElementalTypes.Fire, ElementalTypes.Wind, ElementalTypes.Fire, ElementalTypes.Wind);


            TowerGraph.CreateTwoTowers("Meltdown", "Torch", 850,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Fire-ele"), 110, 600, 4, 0), ElementalTypes.Fire, ElementalTypes.Wind, ElementalTypes.Fire, ElementalTypes.Wind, ElementalTypes.Fire, ElementalTypes.Wind);




            TowerGraph.CreateTwoTowers("Steam", "Geyser", 350,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Mist"), 110, 600, 4, 0), ElementalTypes.Water, ElementalTypes.Fire, ElementalTypes.Water, ElementalTypes.Fire);



            TowerGraph.CreateTwoTowers("Geyser", "Vaporizer", 999,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Charon - dark - water"), 110, 600, 4, 0), ElementalTypes.Water, ElementalTypes.Fire, ElementalTypes.Water, ElementalTypes.Fire, ElementalTypes.Water, ElementalTypes.Fire);


            TowerGraph.CreateTwoTowers("Gunpowder", "Artillery", 385,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Fire, Content.Load<Texture2D>("Buttons/Grazer"), 110, 600, 4, 0), ElementalTypes.Fire, ElementalTypes.Dark, ElementalTypes.Fire, ElementalTypes.Dark);


            TowerGraph.CreateTwoTowers("Artillery", "Hades", 900,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Fire, Content.Load<Texture2D>("Buttons/Pyro"), 110, 600, 4, 0), ElementalTypes.Fire, ElementalTypes.Dark, ElementalTypes.Fire, ElementalTypes.Dark, ElementalTypes.Fire, ElementalTypes.Dark);



            TowerGraph.CreateTwoTowers("Grazer", "Laser", 400,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Light, Content.Load<Texture2D>("Buttons/Beamberg-b"), 110, 600, 4, 0), ElementalTypes.Light, ElementalTypes.Fire, ElementalTypes.Light, ElementalTypes.Fire);


            TowerGraph.CreateTwoTowers("Laser", "Photon", 1010,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Light, Content.Load<Texture2D>("Buttons/Acid-b"), 110, 600, 4, 0), ElementalTypes.Light, ElementalTypes.Fire, ElementalTypes.Light, ElementalTypes.Fire, ElementalTypes.Light, ElementalTypes.Fire);



            TowerGraph.CreateTwoTowers("Whirlpool", "Ice", 385,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Ice"), 110, 600, 4, 0), ElementalTypes.Wind, ElementalTypes.Water, ElementalTypes.Wind, ElementalTypes.Water);


            TowerGraph.CreateTwoTowers("Ice", "Prism", 950,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Water, Content.Load<Texture2D>("Buttons/Acid-b"), 110, 600, 4, 0), ElementalTypes.Wind, ElementalTypes.Water, ElementalTypes.Wind, ElementalTypes.Water, ElementalTypes.Wind, ElementalTypes.Water);


            TowerGraph.CreateTwoTowers("Vortex", "Fujin", 350,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Fujin-b"), 110, 600, 4, 0), ElementalTypes.Wind, ElementalTypes.Dark, ElementalTypes.Wind, ElementalTypes.Dark);


            TowerGraph.CreateTwoTowers("Fujin", "Gravity", 960,
                null, new TowerDataHolder(Base[3], cannon[3], ElementalTypes.Wind, Content.Load<Texture2D>("Buttons/Gravity-b"), 110, 600, 4, 0), ElementalTypes.Wind, ElementalTypes.Dark, ElementalTypes.Wind, ElementalTypes.Dark, ElementalTypes.Wind, ElementalTypes.Dark);


            TowerGraph.FinsishedAdding();
        }

        private void InitializeBullets()
        {
            Texture2D bulletText = Content.Load<Texture2D>("Shot");
            SpriteAnimation ShotAnim = new SpriteAnimation(bulletText);
            ShotAnim.AddAnimations("Default", new FrameAnimation(5, bulletText.Width / 4, bulletText.Height, 4, 0, 0, new Point(4, 1)));
            Bullet bullet = new Bullet(ShotAnim, Vector2.Zero, 100, 1, 80);

            Texture2D baseTower1 = Content.Load<Texture2D>("Bases/turret-1-base");
            Texture2D baseTowerTurret1 = Content.Load<Texture2D>("Bases/turret-1-cannon");

            SpriteAnimation baseAnim1 = new SpriteAnimation(baseTower1);
            SpriteAnimation baseTurretAnim1 = new SpriteAnimation(baseTowerTurret1);
            baseAnim1.AddAnimations("Random", new FrameAnimation(1, baseTower1.Width, baseTower1.Height, 1, 0, 0, new Point(1, 1)));
            baseTurretAnim1.AddAnimations("Random", new FrameAnimation(1, baseTowerTurret1.Width, baseTowerTurret1.Height, 1, 0, 0, new Point(1, 1)));

            //Tower RandomTower = new Tower(baseAnim1, baseTurretAnim1, Content.Load<Texture2D>("Buttons/Dark-b"), "BaseTower", new Vector2(144, 192), 0, 0, 150, 300, 0);
            Tower.projectile = bullet;
            Tower.projectile.AoeEffect = new StraightAoeDamage(1);

            //TowersCurrentlyInGame.Add(RandomTower);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public static bool Exited = false;

        MouseState mouse;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Exited)
            {
                this.Exit();
            }

            if (InputHandler.KeyReleased(Keys.E))
            {
                this.Exit();
            }
            if (InputHandler.KeyReleased(Keys.Escape))
            {
                Menu.Update(gameTime);
                Menu.Update(gameTime);
                Menu.Update(gameTime);
                currentState = States.menu;
                sound.playSound("MainMenu");
            }
            if (currentState == States.logo)
            {
                Menu.Update(gameTime);
                Menu.Update(gameTime);
                Menu.Update(gameTime);
                startLogo(gameTime);
            }

            else if (currentState == States.arcade)
            {

                //Game.Update(gameTime);

                if (Life > 0)
                {
                    //Menu.Update(gameTime);
                    mouse = Mouse.GetState();
                    MoveCamera();
                    WaveSpawner(gameTime);
                    EnemyManager(gameTime);
                    Updatetowers(gameTime);
                    towerGui.Update(gameTime, mouse);
                }
                else {

                    newGameWaitTime += gameTime.ElapsedGameTime.Milliseconds;

                    if (newGameWaitTime > newGametime) {
                        newGameWaitTime = 0;
                        currentState = States.logo;
                        totaltime = 0;
                        g = 0;
                        timer = 0;
                        Life = 15;
                        listOfEnemies.Clear();
                        SpawnQueue.Clear();

                        EffectsManager.TextEffects.Clear();
                        TowersCurrentlyInGame.Clear();
                        towerGui.BoughtElementsList.Clear();
                        towerGui.Gold = 100;
                        towerGui.Materia = 0;
                       // towerGui = new TowerGui(Content.Load<Texture2D>(@"GUI/toolbar-latest"), Content.Load<Texture2D>(@"GUI/upper-toolbar"), Content.Load<Texture2D>(@"GUI/element"), Content.Load<Texture2D>(@"GUI/TOWERS"), Content.Load<Texture2D>("GUI/SellButton"), Content.Load<Texture2D>("GUI/onHover3"), TowersCurrentlyInGame, Map, TowerGraph, Content.Load<Effect>(@"Shader/Select"), listOfEnemies);
                        Wave();

                    }

                
                }

                EffectsManager.Update(gameTime);
            }
            else if (currentState == States.menu)
            {
                Menu.Update(gameTime);
            }
            else if (currentState == States.pause)
            {

            }
            else if (currentState == States.instructions)
            {
               
                instructionsscreen(gameTime);
            }

            base.Update(gameTime);
        }

        private void MoveCamera()
        {
            Rectangle MouseRect = new Rectangle(mouse.X, mouse.Y, 2, 2);
            Vector2 CameraPos = Camera.Position;
            if (MouseRect.Intersects(Top) || InputHandler.KeyDown(Keys.Up))
            {
                CameraPos.Y -= 5;
            }
            if (MouseRect.Intersects(Bottom) || InputHandler.KeyDown(Keys.Down))
            {
                CameraPos.Y += 5;
            }
            if (MouseRect.Intersects(Left) || InputHandler.KeyDown(Keys.Left))
            {
                CameraPos.X -= 5;
            }
            if (MouseRect.Intersects(Right) || InputHandler.KeyDown(Keys.Right))
            {
                CameraPos.X += 5;

            }

            Camera.Position = CameraPos;
        }



        private void startLogo(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            totaltime += gameTime.ElapsedGameTime.Milliseconds;

            background = logoAnim[g];
            if (g < 40 && timer >= 75)
            {
                g++;
                timer = 0;

                if (g == 21)
                {

                    sound.playSound("SilverSageIntro");
                }
            }
            if (totaltime >= 4000)
            {
                currentState = States.menu;

                sound.playSound("MainMenu");

                g = 0;
            }

        }

        private void instructionsscreen(GameTime gameTime)
        {
            Menu.Update(gameTime);
            Menu.Update(gameTime);
            Menu.Update(gameTime);
            if (InputHandler.KeyReleased(Keys.Escape) )
            {
    
                currentState = States.menu;
            }
            else if (InputHandler.KeyReleased(Keys.Space)) {


                instructionCounter++;
                if (instructionCounter < 4)
                {

                    instructionScreen = Content.Load<Texture2D>(@"instruction"+instructionCounter);
                }
                else {
                  
                   currentState = States.menu;
                    instructionCounter = 1;
                }
            }

        }

        public static float SpawnDelay;
        float DelaySpawn = 1000;
        public static int DelayBetweenSpawns = 5000;
        bool Swing = false;
        private void WaveSpawner(GameTime gameTime)
        {

            if (listOfEnemies.Count == 0 && Waves.Count != 0 && SpawnQueue.Count == 0)
            {
                TimeElaspedSinceWaveKilled += gameTime.ElapsedGameTime.Milliseconds;
                if (TimeElaspedSinceWaveKilled >= DelayBetweenSpawns)
                {
                    sound.pauseSound("WaitingForWaves");
                    // sound.pauseSound("WaitingForWaves");
                    
                    SpawnQueue = Waves.Dequeue();
                    TimeElaspedSinceWaveKilled = 0;
                    WavesElasped++;
                    Enemies Enem = SpawnQueue.Dequeue();
                   // Console.WriteLine((ElementalTypes)Enem.Type);
                    DelaySpawn = Enem.Speed * (1000 / Enem.Speed);
                    SpawnQueue.Enqueue(Enem);
                    Swing = true;
                }
                else {
                   
                    sound.playSound("WaitingForWaves");
                
                }
            }


            if (SpawnQueue.Count > 0)
            {
                SpawnDelay += gameTime.ElapsedGameTime.Milliseconds;

                if (SpawnDelay >= DelaySpawn)
                {
                    listOfEnemies.Add(SpawnQueue.Dequeue());
                    SpawnDelay = 0;
                }
            }

            if (listOfEnemies.Count == 0 && SpawnQueue.Count == 0 && Swing)
            {
                            if (WavesElasped % 3 == 0)
                {
                    towerGui.Materia++;
                }
                Swing = false;
            }
        }
    

        private void Updatetowers(GameTime gameTime)
        {
            foreach (Tower tower in TowersCurrentlyInGame)
            {
                tower.Update(gameTime, listOfEnemies);

                foreach (Bullet bullet in tower.bullets)
                {
                    bullet.Update(gameTime);
                    Rectangle BulletRect = bullet.GetCollisionRect;

                    foreach (Enemies enemy in listOfEnemies)
                    {
                        Rectangle enemyRect = enemy.GetCollisionRect;

                        if (BulletRect.Intersects(enemyRect))
                        {
                            enemy.GetHit(bullet.Damage, bullet.Type);
                            Vector2 ImpactVelocity = bullet.Direction * bullet.Speed;
                            //EffectsManager.AddSparksEffect(Content.Load<Texture2D>(@"Particles/sprite"),enemy.CenterofSprite, ImpactVelocity);
                            bullet.OnHit(listOfEnemies, enemy);
                            enemy.GetHit();
                            bullet.Active = false;
                        }
                    }
                }

                List<Bullet> bullets = tower.bullets;

                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    if (!bullets[i].Active)
                    {
                        bullets.RemoveAt(i);
                    }
                }
            }
        }

        public static int Life = 15;
        public static float TimeElaspedSinceWaveKilled;
        private void EnemyManager(GameTime gametime)
        {
            foreach (Enemies enemy in listOfEnemies)
            {
                enemy.Update(gametime);
                if (enemy.pathToFollow.Count <= 1)
                {
                    enemy.Active = false;
                    Life -= 1;
                    EffectsManager.lifeLostEffect();
                    if (Life <= 0)
                    {
                        EffectsManager.gameOverEffect();
                    }
                    enemy.gold = 0;

                }
            }

            for (int i = listOfEnemies.Count - 1; i >= 0; i--)
            {
                Enemies enemy = listOfEnemies[i];

                if (!enemy.Active)
                {
                    towerGui.Gold += enemy.gold;
                    EffectsManager.WritingEffect(enemy.CenterofSprite, enemy.gold);
                    listOfEnemies.RemoveAt(i);
                    sound.playSound("EnemiesDyingNew");
                }
            }

        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            foreach (Enemies enemy in listOfEnemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // spriteBatch.Begin();
            if (currentState == States.logo )
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.End();
            }

            else if(currentState == States.instructions){
                spriteBatch.Begin();
                spriteBatch.Draw(instructionScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.End();
            
            }
            else if (currentState == States.menu)
            {
                Menu.Draw(spriteBatch);
            }
            else
            {
                Map.Draw(spriteBatch);
                DrawEnemies(spriteBatch);
                DrawTowers(spriteBatch);
                EffectsManager.Draw(spriteBatch);
                towerGui.Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }

        public static int WavesElasped = 1;


        private void DrawTowers(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in TowersCurrentlyInGame)
            {
                tower.Draw(spriteBatch);
            }
        }


    }
}