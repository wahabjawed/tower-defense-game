using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Engine
{
    public static class Camera
    {

        #region Variables
        public static int MapWidth;
        public static int MapHeight;
        public static int ScreenWidth;
        public static int ScreenHeight;
        static bool Set = false;
        private static bool cameraFixed = false;
        private static Vector2 position = Vector2.Zero;
        #endregion


        public static bool Fixed
        {
            get
            {
                return cameraFixed;
            }
            set
            {
                cameraFixed = value;
            }
        }

        #region Constructor
        /// <summary>
        /// Call this method before trying to use the camera class or wont clamp properly.
        /// Needs ViewPorts width and height.
        /// </summary>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// 
        public static void Initialise(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Set = true;
        }
        /// <summary>
        /// Call this method before trying to use the camera class or wont clamp properly.
        /// Needs the graphics device so it can access viewport width and height.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            ScreenWidth = graphicsDevice.Viewport.Width;
            ScreenHeight = graphicsDevice.Viewport.Height;
            Set = true;
        }

        #endregion

        #region Property
        /// <summary>
        ///Call this Propety In every spritebatch to draw Srites Relative to Screen Co-Oridnates form world co-ordinates. 
        /// </summary>

        public static Matrix TransFormMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-position, 0f));
            }
        }

        /// <summary>
        /// The position of the camera, this method Validates and clapms itself to the map automatically.
        /// </summary>
        public static Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                if (!Fixed)
                {
                    position = value;
                    if (Set)
                        ClampToMap(MapWidth, MapHeight);
                }
            }
        }


        #endregion

        #region Logic
        /// <summary>
        /// Call this Method In the main Update to clamp Camera to the Map, Pass in Map Width And Height for the method to work.
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>

        public static void ClampToMap(int Width, int Height)
        {
            if (position.X <= 0)
            {
                position.X = 0;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
            if( (Width > ScreenWidth) && (position.X >= Width - ScreenWidth))
            {
                position.X = Width - ScreenWidth;
            }
            if( (Height > ScreenHeight) && (position.Y >= Height - ScreenHeight))
            {
                position.Y = Height - ScreenHeight;
            }
        }

        #endregion
    }
}
