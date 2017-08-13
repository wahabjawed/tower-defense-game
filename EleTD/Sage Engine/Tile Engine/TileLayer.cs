using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Xml;

namespace Sage_Engine
{

    public class TileLayer
    {

        
        #region Variables

        int[,] layer;
        List<Texture2D> textureList = new List<Texture2D>();

        float alpha = 1f;
        String Name = null;

        /// <summary>
        /// Change These According to the tile Height and width Must Be Same for all classes. Setters and getters included Below.
        /// </summary>

        private static int TileWidth = 64;
        private static int TileHeight = 64;

        #endregion

        #region Properties/GetterSetter

        //Setter and getter for Alpha Transparecnt value of the layer.


        public string LayerName {
            get {
                return Name;
            }
            set {
                Name = value;
            }

        }

        public List<Texture2D> TexturesList
        {
            get
            {
                return this.textureList;
            }
        }

        public int[,] TileMapArray
        {
            get
            {
                return this.layer;
            }
            set
            {
                layer = (int[,])value.Clone();
            }
        }
        /// <summary>
        /// Alpha can only be between 0.0 and 1.0f , Where 0.0f is Full Transparency/ Blending and 1.0 is no blending.
        /// </summary>
        public float Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
               alpha =  MathHelper.Clamp(value, 0.0f, 1.0f);
            }
        }

        public static int SetTileWidth
        {
            set
            {
                TileWidth = Math.Max(value, 16);
            }
            get
            {
                return TileWidth;
            }

        }


        /// <summary>
        /// Set the Tile Height of the Map, validation is done in property.
        /// </summary>
        public static int SetTileHeight
        {
            set
            {
                TileHeight = Math.Max(value, 16);
            }
            get
            {
                return TileHeight;
            }

        }

        public static int GetTileWidth
        {
            get
            {
                return TileWidth;
            }

        }

        public static int GetTileHeight
        {
            get
            {
                return TileHeight;
            }

        }

        //How many Tiles are place Horizontally.
        public int LayerWidthinTiles
        {
            get
            {
                return layer.GetLength(1);
            }
        }

        //How many tiles are place vertically.
        public int LayerHeightinTiles
        {
            get
            {
                return layer.GetLength(0);
            }
        }

        //How many pixels wide is the layer.
        public int LayerWidthInPixels
        {
            get
            {
                return layer.GetLength(1) * TileWidth;
            }
        }

        //How many pixels Tall is the TileMap.
        public int LayerHeightInPixels
        {
            get
            {
                return layer.GetLength(0) * TileHeight;
            }
        }
        #endregion

        #region Constructer region

        /// <summary>
        ///Default Constructer to initilise a small Map by default. 
        /// </summary>

        public TileLayer()
        {
            layer = new int[25, 25];
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    layer[y, x] = -1;
                }
            }
        }

        /// <summary>
        ///Get a blank map of the specified width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TileLayer(int width, int height)
        {
            layer = new int[height, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    layer[y, x] = -1;
                }
            }
        }
        /// <summary>
        /// Pass in a map to have it cloned into the Layer.
        /// </summary>
        /// <param name="existingMap"></param>
        public TileLayer(int[,] existingMap)
        {
            layer = (int[,])existingMap.Clone();
        }

        #endregion

        #region DrawingCode
        /// <summary>
        /// Render The Layer.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);

            for (int x = 0; x < LayerWidthinTiles; x++)
            {
                for (int y = 0; y < LayerHeightinTiles; y++)
                {
                    int Index = layer[y, x];
                    if (Index != -1)
                    {
                        Texture2D texture = textureList[Index];
                        spriteBatch.Draw(texture,
                            new Rectangle(
                            TileWidth * x, TileHeight * y, TileWidth, TileHeight
                            ),
                            new Color(new Vector4(1f, 1f, 1f, alpha)));

                    }


                }
            }

            spriteBatch.End();
        }
        #endregion

        #region HelperMethods


        /// <summary>
        /// Use this Method to Dynamically Change The tiles in the layer anytime you want.
        /// Specifyng X and Y Values(X is Horizontal, and Y is vertical) and the index you want to
        /// swap with. Validation / Bounds Checking is done in method itself.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Index"></param>
        public void SetCellIndex(int x, int y, int Index)
        {
            if ((y >= 0) && (y < LayerHeightinTiles) &&
                (x >= 0) && (x <= LayerWidthinTiles))
                layer[y, x] = Index;
        }

        /// <summary>
        ///  Use this Method to get a tile in the layer anytime you want.
        /// Specifyng X and Y Values(X is Horizontal, and Y is vertical) a Validation / Bounds Checking is done in method itself.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetCellIndex(int x, int y)
        {
            if ((y >= 0) && (y < LayerHeightinTiles) &&
                (x >= 0) && (x <= LayerWidthinTiles))
                return layer[y, x];

            else
                return -2;
        }

        /// <summary>
        /// Pass in content manager and textures to directly have them be loaded into the layer, Orde's The Texutres are meintioned
        /// in is Important.
        /// </summary>
        /// <param name="contentManager"></param>
        /// <param name="textureNames"></param>
        public void LoadTextures(ContentManager contentManager, params string[] textureNames)
        {
            foreach (string name in textureNames)
            {
                textureList.Add(contentManager.Load<Texture2D>("name"));
            }
        }

        /// <summary>
        /// Checks to see if the list has a specified Texture if it does will return its index.
        /// -1 Otherwise.
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public int HasTexture(Texture2D texture)
        {
            if (textureList.Contains(texture))
            {
                return textureList.IndexOf(texture);
            }

            return -1;
        }

        /// <summarey>
        /// Checks to see if the list does not already have a texture if it doesnt Then Addes it to textures. 
        /// </summary>
        /// <param name="texture"></param>
        public void AddTexture(Texture2D texture)
        {
            if (HasTexture(texture) == -1)
            {
                textureList.Add(texture);
            }
        }


        /// <summary>
        /// Get The Tile at the current Screen Pixels Co-Ordinates. 
        /// null if the pixels are out of the layers width and height.
        /// </summary>
        /// <param name="PixelX"></param>
        /// <param name="PixelY"></param>
        /// <returns></returns>
        public Vector2? GetTileAtPixels(int PixelX, int PixelY)
        {

            Vector2 pos = Camera.Position;
            Vector2 ans;
            ans.X = (PixelX + pos.X) / TileLayer.TileWidth;
            ans.Y = (PixelY + pos.Y) / TileLayer.TileHeight;
            if (ans.X > this.LayerWidthinTiles || ans.Y > this.LayerHeightinTiles)
                return null;
            else
                return ans;
        }

        /// <summary>
        /// Get The Tile at the current Screen Pixels Co-Ordinates. 
        /// null if the pixels are out of the layers width and height.
        /// </summary>
        /// <param name="Pixels"></param>
        /// <returns></returns>
        public Vector2? GetTileAtPixels(Vector2 Pixels)
        {
            return GetTileAtPixels((int)Pixels.X, (int)Pixels.Y);
        }


        public void RemoveTexture(int IndexOfTexture)
        {
            for (int x = 0; x < this.LayerWidthinTiles; x++)
            {
                for (int y = 0; y < this.LayerHeightinTiles; y++)
                {
                    int index = this.GetCellIndex(x, y);

                    if (index == IndexOfTexture)
                    {
                        this.SetCellIndex(x, y, -1);
                    }
                    else if (index > IndexOfTexture)
                    {
                        this.SetCellIndex(x, y, index - 1);
                    }
                }
            }

            textureList.RemoveAt(IndexOfTexture);

        }


        public static TileLayer ReadInLayer(string fileName, out Dictionary<int, string> textureNames)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return ReadInLayer(doc.DocumentElement, out textureNames);
           }
        
        public static TileLayer ReadInLayer(XmlNode doc, out Dictionary<int, string> textureNames)
        {
            TileLayer layerToReturn = null;
            Dictionary<int, string> dicToReturn = new Dictionary<int,string>();
            float layerAlpha=1.0f;


            XmlNode BaseNode = doc;
           
            foreach (XmlNode node in BaseNode.ChildNodes)
            {
                if (node.Name == "Textures")
                {
                    foreach (XmlNode textureNode in node.ChildNodes)
                    {
                        dicToReturn.Add(int.Parse(textureNode.Attributes["ID"].Value) , textureNode.Attributes["Name"].Value);
                    }

                }
                else if (node.Name == "Properties")
                {
                    foreach (XmlNode propertyNode in node.ChildNodes)
                    {
                        if (propertyNode.Name == "Alpha")
                        {
                            layerAlpha = float.Parse(propertyNode.Attributes["Value"].Value);
                        }
                    }
                }
                else if (node.Name == "Layer")
                {
                    int width, height;
                    width = int.Parse(node.Attributes["Width"].Value);
                    height = int.Parse(node.Attributes["Height"].Value);

                    layerToReturn = new TileLayer(width, height);
                    int  y = 0;

                    foreach (XmlNode RowNode in node.ChildNodes)
                    {
                        string row = RowNode.InnerText;
                        row.Trim();
                        row.TrimStart(' ');
                        string[] CellsInRow = row.Split(' ');

                        for (int x = 1; x < CellsInRow.Length; x++)
                        {
                            layerToReturn.SetCellIndex(x-1, y, int.Parse(CellsInRow[x]));
                        }
                        y++;
                    }
                }
            } //ForEach Loop Ends Having Read in Everything.
            layerToReturn.Alpha = layerAlpha;
            layerToReturn.LayerName = BaseNode.Attributes["Name"].Value;
            textureNames = dicToReturn;
            return layerToReturn;
        }

        public void ReadOutLayer(String fileName,string Contentpath ,string[] TextureNames, Dictionary<string, Texture2D> dictTextures, string LayerName)
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(fileName, settings);

            writer.WriteStartElement("TileLayer");
            writer.WriteAttributeString("Name", LayerName);
            writer.WriteAttributeString("ContentPath", Contentpath);

            writer.WriteStartElement("Textures");
            foreach (string Text in TextureNames)
            {
                writer.WriteStartElement("Texture");
                writer.WriteAttributeString("Name", Text);
                writer.WriteAttributeString("ID", "" + this.HasTexture(dictTextures[Text]));
                writer.WriteEndElement(); //Closing Tag of Texture
            }
            writer.WriteEndElement(); //Closing Tag of Textures

            writer.WriteStartElement("Properties");
            writer.WriteStartElement("Alpha");
            writer.WriteAttributeString("Value", ""+ Alpha);
            writer.WriteEndElement(); //Closing tag of Alpha Attribute.

            writer.WriteEndElement();//Closing tag of Properties

            writer.WriteStartElement("Layer");
            writer.WriteAttributeString("Width", "" + this.LayerWidthinTiles);
            writer.WriteAttributeString("Height", "" + this.LayerHeightinTiles);

            for(int y=0; y < this.LayerHeightinTiles; y++)
            {
                writer.WriteStartElement("TileRow");
                for (int x = 0; x < this.LayerWidthinTiles; x++)
                {
                    writer.WriteString(" " + GetCellIndex(x, y));
                }
                writer.WriteEndElement();// Closing Tag of TileRow
            }
            writer.WriteEndElement();// Closing Tag of Layer

            writer.WriteEndElement(); //Closing Tag of TileLayer

            writer.Flush();
            writer.Close();
        }
        
        #endregion

        public void AddTextures(ContentManager manager, string[] TextureNames)
        {
            foreach (string Texture in TextureNames)
            {
                Texture2D tex = manager.Load<Texture2D>(Texture);
                textureList.Add(tex);
            }
        }
    }


    
}
