using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Sage_Engine;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;
using System.Reflection;

namespace Sage_Editor
{
    using Image = System.Drawing.Image;

    public partial class Form1 : Form
    {

        #region Variables

        public SpriteBatch spriteBatch;
        public string WorkspacePath;
        public bool workspacesheck = false;
        public Texture2D EmptyTile;
        public Texture2D Cross;

        public Dictionary<string, Texture2D> dictTextures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Image> dictImages = new Dictionary<string, Image>();
        public Dictionary<string, TileLayer> dictLayer = new Dictionary<string, TileLayer>();

        public int mouseClickCount = 20;
        public TileMap Map = new TileMap();
        public TileLayer currentLayer;

        public Stack<Command> ExcutedCommands = new Stack<Command>();
        public Command PreviousCommand = null;

        int? mouseX = null;
        int? mouseY = null;

        public int? TileX = null;
        public int? TileY = null;
        bool Hovering;


        #endregion

        #region GraghicsDevice
        public GraphicsDevice GraphicsDevices
        {
            get
            {
                return tileDisplay1.GraphicsDevice;
            }
        }

        #endregion


        #region initialise

        public Form1()
        {
            InitializeComponent();


            //while (string.IsNullOrEmpty(texPathAddress.Text))
            //{
            //    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        texPathAddress.Text = folderBrowserDialog1.SelectedPath;
            //        this.WindowState = FormWindowState.Maximized;

            //        activateControls();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please Choose A Content Directory");
            //    }
            //}

            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"Content/editorInfo.info");
            //XmlNode nodes = doc.DocumentElement;
            //WorkspacePath = nodes.Attributes["workspace"].Value;


            XmlDocument SettingsFile = new XmlDocument();

            while (!(Directory.Exists(@"C:\SilverSages")))
            {

                MessageBox.Show("ContentPath Not Found Please Browse To your contentFolder", "Path Not Found");

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    Directory.CreateDirectory(@"C:\SilverSages");

                    XmlWriterSettings writerSettings = new XmlWriterSettings();
                    writerSettings.Indent = true;
                    writerSettings.NewLineChars = "<";

                    XmlWriter writer = XmlWriter.Create(@"C:\SilverSages\Settings.info");
                    writer.WriteElementString("ContentFolderPath", folderBrowserDialog1.SelectedPath);

                    writer.Flush();
                    writer.Close();
                }
            }
            if (!File.Exists(@"C:\SilverSages\Settings.info"))
            {
                MessageBox.Show("ContentPath Not Found Plese Browse To your contentFolder", "Path Not Found");

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    XmlWriterSettings writerSettings = new XmlWriterSettings();
                    writerSettings.Indent = true;

                    XmlWriter writer = XmlWriter.Create(@"C:\SilverSages\Settings.info");
                    writer.WriteElementString("ContentFolderPath", folderBrowserDialog1.SelectedPath);

                    writer.Flush();
                    writer.Close();
                }
            }

            SettingsFile.Load(@"C:\SilverSages\Settings.info");
            texPathAddress.Text = SettingsFile.DocumentElement.InnerText.Trim();
            this.WindowState = FormWindowState.Maximized;
            activateControls();

            tileDisplay1.OnInitialise += new EventHandler(tileDisplay1_OnInitialise);
            tileDisplay1.OnDraw += new EventHandler(tileDisplay1_OnDraw);

            Application.Idle += delegate { tileDisplay1.Invalidate(); };
            Application.Idle += delegate { vScrollBar1.Invalidate(); };
        }


        private void activateControls()
        {
            radDraw.Enabled = true;
            radErase.Enabled = true;
            chekFill.Enabled = true;
            barAlpha.Enabled = true;
            LayerList.Enabled = true;
            btnAddLayer.Enabled = true;
            btnRemoveTexture.Enabled = true;
            btnAddFiles.Enabled = false;
            btnRemoveLayer.Enabled = true;
            TextureList.Enabled = true;
            btnAddTexture.Enabled = true;
            btnRemoveLayer.Enabled = true;
            pictureBox1.Enabled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == (System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z))
            {
                if (ExcutedCommands.Count > 0)
                {
                    Command cmd = ExcutedCommands.Pop();
                    cmd.Undo();
                }
            }

            if (keyData == (System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up))
            {
                TileLayer.SetTileHeight = TileLayer.GetTileHeight + 16;
                TileLayer.SetTileWidth = TileLayer.GetTileWidth + 16;
            }


            if (keyData == (System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down))
            {
                TileLayer.SetTileHeight = TileLayer.GetTileHeight - 16;
                TileLayer.SetTileWidth = TileLayer.GetTileWidth - 16;
            }

            if (keyData == System.Windows.Forms.Keys.F)
            {
                InvertFillCheckBox();
            }

            if (keyData == System.Windows.Forms.Keys.E)
            {
                if (radDraw.Checked)
                {
                    radDraw.Checked = false;
                    radErase.Checked = true;
                }
                else
                {
                    radDraw.Checked = true;
                    radErase.Checked = false;
                }
            }

            return false;
        }

        private void InvertFillCheckBox()
        {
            if (!chekFill.Checked)
                chekFill.Checked = true;
            else
            {
                chekFill.Checked = false;
            }
        }


        void tileDisplay1_OnInitialise(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevices);
            using (FileStream stream = new FileStream(@"Content/tile.png", FileMode.Open))
            {
                EmptyTile = Texture2D.FromStream(GraphicsDevices, stream);
            }

            using (FileStream stream = new FileStream(@"Content/Cross.png", FileMode.Open))
            {
                Cross = Texture2D.FromStream(GraphicsDevices, stream);
            }

            radDraw.Select();
            DrawEmptyTiles.Initialise(this);

            Mouse.WindowHandle = tileDisplay1.Handle;


            tileDisplay1.Cursor = Cursors.Cross;
            MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            tileDisplay1.MouseHover += new EventHandler(tileDisplay1_MouseHover);
            tileDisplay1.MouseLeave += new EventHandler(tileDisplay1_MouseLeave);
            this.Focus();
            CommandFactory.Initilise(this);
            Camera.Initialise(GraphicsDevices);
        }


        #endregion


        #region DrawingCode

        public void tileDisplay1_OnDraw(object sender, EventArgs e)
        {
            vScrollBar1.Location = new System.Drawing.Point(tileDisplay1.Location.X + tileDisplay1.Size.Width, vScrollBar1.Location.Y);
            Logic();
            Render();
        }

        private void Render()
        {
            GraphicsDevices.Clear(Color.Black);
            DrawLayers();
        }


        private void DrawLayers()
        {
            Map.DrawBackGround(spriteBatch);

            foreach (TileLayer layer in Map.Layers)
            {
                if (layer == null)
                    return;
                layer.Draw(spriteBatch);

                DrawEmptyTiles.DrawTiles();

                if (layer == currentLayer)
                    break;
            }

            DrawEmptyTiles.DrawSelectedTile();

        }

        #endregion

        #region Logic

        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (vScrollBar1.Visible)
            {
                if (e.Delta > 0)
                {
                    int x = e.Delta % 119;
                    vScrollBar1.Value -= x;
                }
                else
                {
                    int x = e.Delta % 119;
                    vScrollBar1.Value -= x;
                }
            }
        }

        private void Logic()
        {
            Camera.Position = new Vector2(hScrollBar1.Value * 4, vScrollBar1.Value * 4);

            if (currentLayer != null)
            {
                if (currentLayer.LayerWidthInPixels >= tileDisplay1.Width)
                {
                    hScrollBar1.Visible = true;
                    hScrollBar1.Minimum = 0;
                    hScrollBar1.Maximum = (int)Math.Ceiling(((currentLayer.LayerWidthinTiles * TileLayer.GetTileWidth) - GraphicsDevices.Viewport.Width + 250) * 0.25);
                }

                if (currentLayer.LayerHeightInPixels >= tileDisplay1.Height)
                {
                    vScrollBar1.Visible = true;
                    vScrollBar1.Minimum = 0;
                    vScrollBar1.Maximum = (int)Math.Ceiling(((currentLayer.LayerHeightinTiles * TileLayer.GetTileHeight) - GraphicsDevices.Viewport.Height + 250) * 0.25);
                }
            }

            if (Hovering)
            {
                SelectSelectedTile();
            }
            MouseButtonClicked();
        }

        private void ExcuteCommands(Command command)
        {
            if (PreviousCommand == null)
            {
                PreviousCommand = command;
            }
            else
            {
                command.Excute();
                if (!command.CompareTo(PreviousCommand))
                {

                    ExcutedCommands.Push(command);
                }
            }
            PreviousCommand = command;
        }

        private void MouseButtonClicked()
        {
            if (mouseClickCount > 2)
            {
                MouseState mouse = Mouse.GetState();

                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    switch (tabControl1.SelectedIndex)
                    {
                        case 0:
                            if (TileX != null || TileY != null)
                            {

                                if (chekFill.Checked)
                                {
                                    if (radDraw.Checked)
                                    {
                                        Command command = CommandFactory.Execute(Commands.FillCellIndex);
                                        ExcuteCommands(command);
                                    }
                                    else
                                    {
                                        Command command = CommandFactory.Execute(Commands.FillCellErase);
                                        ExcuteCommands(command);
                                    }
                                }
                                else
                                {
                                    if (radDraw.Checked)
                                    {
                                        Command command = CommandFactory.Execute(Commands.SetTileCommand);
                                        ExcuteCommands(command);
                                    }
                                    else
                                    {
                                        Command command = CommandFactory.Execute(Commands.EraseCellCommand);
                                        ExcuteCommands(command);
                                    }
                                }
                            }

                            mouseClickCount = 0;
                            break;

                        case 1:
                            if (radioSetColl.Checked == true)
                            {
                                if ((TileX != null) && (TileY != null))
                                    Map.SetCollisionTile((int)TileX, (int)TileY, 1);
                            }
                            else
                            {
                                if ((TileX != null) && (TileY != null))
                                    Map.SetCollisionTile((int)TileX, (int)TileY, 0);
                            }

                            break;
                    }
                }
            }
            else
            {
                mouseClickCount++;
            }
        }

        private void SelectSelectedTile()
        {
            MouseState mouse = Mouse.GetState();

            Vector2 CameraPos = Camera.Position;
            mouseX = mouse.X;
            mouseY = mouse.Y;
            mouseX += (int)CameraPos.X;
            mouseY += (int)CameraPos.Y;
            TileX = (int)mouseX / TileLayer.GetTileWidth;
            TileY = (int)mouseY / TileLayer.GetTileHeight;
            if (currentLayer == null)
            {
                TileX = null;
                TileY = null;
                return;
            }
            if ((TileX > currentLayer.LayerWidthinTiles - 1) || (TileY > currentLayer.LayerHeightinTiles - 1))
            {
                TileX = null;
                TileY = null;
            }
        }

        void tileDisplay1_MouseLeave(object sender, EventArgs e)
        {
            TileX = null;
            TileY = null;
            Hovering = false;
        }

        void tileDisplay1_MouseHover(object sender, EventArgs e)
        {
            Hovering = true;
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = WorkspacePath;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                texPathAddress.Text = folderBrowserDialog1.SelectedPath;
                activateControls();
            }

        }

        private void exitBitchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            string layerName;
            int layerWidth;
            int layerHeight;
            TileLayer layer;

            LayerForm Form = new LayerForm(LayerList.Items.Count);
            Form.ShowDialog();


            if (Form.OkPressed)
            {
                layerName = Form.txtLayerName.Text;
                layerWidth = int.Parse(Form.txtLayrWidth.Text);
                layerHeight = int.Parse(Form.txtLayHeight.Text);
                layer = new TileLayer(layerWidth, layerHeight);
                dictLayer.Add(layerName, layer);
                LayerList.Items.Add(layerName);
                Map.Addlayer(layer);
                currentLayer = layer;
                foreach (string s in TextureList.Items)
                {
                    currentLayer.AddTexture(dictTextures[s]);
                }
                LayerList.SetSelected(LayerList.Items.Count - 1, true);
            }

        }

        private void LayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LayerList.SelectedItem != null)
            {
                string CurrentList = LayerList.SelectedItem.ToString();
                currentLayer = dictLayer[CurrentList];
                barAlpha.Value = (int)currentLayer.Alpha * 100;
            }
        }

        private void btnAddTexture_Click(object sender, EventArgs e)
        {
            if (currentLayer != null)
            {
                string Path;
                openFileDialog1.InitialDirectory = WorkspacePath;
                openFileDialog1.Filter = "Png|*.png|Jpeg|*.jpeg|Jpg|*.jpg|All Image Formats|*.png;*.jpeg;*.jpg";
                openFileDialog1.Multiselect = true;
                openFileDialog1.InitialDirectory = texPathAddress.Text;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    foreach (string path in openFileDialog1.FileNames)
                    {
                        Path = path;
                        Texture2D text;
                        Image img;
                        try
                        {
                            FileStream stream = new FileStream(Path, FileMode.Open);

                            text = Texture2D.FromStream(GraphicsDevices, stream);
                            img = Image.FromStream(stream);
                            stream.Close();
                            stream.Dispose();

                            string TextureNameToBeSaved = "";
                            try
                            {
                                TextureNameToBeSaved = path.Substring(texPathAddress.Text.Length);

                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                MessageBox.Show("Texture Was Not Found In Content Directory, Cannot Load Texture");
                                return;
                            }

                            currentLayer.AddTexture(text);
                            foreach (TileLayer layer in Map.Layers)
                            {
                                if (layer.HasTexture(text) == -1)
                                {
                                    layer.AddTexture(text);
                                }
                            }

                            string[] TextureNameSplit = TextureNameToBeSaved.Split('.');
                            string textureNameToWriteOut = TextureNameSplit[0];
                            textureNameToWriteOut = textureNameToWriteOut.Substring(1);

                            dictTextures[textureNameToWriteOut] = text;
                            dictImages[textureNameToWriteOut] = img;

                            TextureList.Items.Add(textureNameToWriteOut);
                        }

                        catch (IOException ex)
                        {
                            MessageBox.Show("The File: " + Path + "Is already Added", "Error");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Select A Layer First Please");
            }
        }

        private void TextureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TextureList.SelectedItem != null)
                pictureBox1.Image = dictImages[TextureList.SelectedItem as string];
        }

        private void btnRemoveTexture_Click(object sender, EventArgs e)
        {

            if (TextureList.SelectedItem != null)
            {
                if (MessageBox.Show("This operation cannot be reversed and will remove all tiles With the name of: " + TextureList.SelectedItem as string +
                     " in all layers of this map", "Cautiuon: Removing Layer", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {

                    Texture2D textureToBeRemoved = dictTextures[TextureList.SelectedItem as string];
                    dictTextures.Remove(TextureList.SelectedItem as string);
                    dictImages.Remove(TextureList.SelectedItem as string);
                    pictureBox1.Image = null;
                    TextureList.Items.Remove(TextureList.SelectedItem);

                    foreach (TileLayer layer in Map.Layers)
                    {
                        int TextureIndex = layer.HasTexture(textureToBeRemoved);
                        layer.RemoveTexture(TextureIndex);
                    }
                    ExcutedCommands.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Select a texture First");
            }
        }

        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedItem != null)
            {

                //currentLayer = null;


                Map.RemoveLayer(dictLayer[LayerList.SelectedItem as string]);
                dictLayer.Remove(LayerList.SelectedItem as string);
                LayerList.Items.Remove(LayerList.SelectedItem);

                if (LayerList.Items.Count > 0)
                {
                    currentLayer = dictLayer[LayerList.Items[0].ToString()];
                    LayerList.SetSelected(0, true);
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        #endregion


        string SaveLayerPath = "";


        string[] Extensions = new string[]
        {
            ".jpg", ".png"
        };

        #region ButtonEventHandler

        private void loadLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void texPathAddress_TextChanged(object sender, EventArgs e)
        {

        }
        private void quickLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = WorkspacePath;
            openFileDialog2.Filter = "Layer File|*.layer|Map File|*.map|Xml File|*.xml|All Supported Files|*.layer;*.map;*.xml";
            Dictionary<int, string> texturesToLoad;
            string NameOfLayer;

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                TileLayer layer = TileLayer.ReadInLayer(openFileDialog2.FileName, out texturesToLoad);
                NameOfLayer = layer.LayerName;
                string extensFound = "";

                try
                {
                    foreach (KeyValuePair<int, string> texturespaths in texturesToLoad)
                    {
                        foreach (string ext in Extensions)
                        {
                            if (File.Exists(texPathAddress.Text + "\\" + texturespaths.Value + ext))
                            {
                                extensFound = ext;
                                break;
                            }
                        }

                        FileStream stream = new FileStream(texPathAddress.Text + "\\" + texturespaths.Value + extensFound, FileMode.Open);
                        Texture2D texture = Texture2D.FromStream(GraphicsDevices, stream);
                        layer.AddTexture(texture);
                        stream.Dispose();
                        if (!dictTextures.ContainsKey(texturespaths.Value))
                        {
                            TextureList.Items.Add(texturespaths.Value);
                            dictTextures.Add(texturespaths.Value, texture);
                            Image img = Image.FromFile(texPathAddress.Text + "\\" + texturespaths.Value + extensFound);
                            dictImages.Add(texturespaths.Value, img);
                        }
                    }
                    dictLayer[NameOfLayer] = layer;
                    Map.Addlayer(layer);
                    LayerList.Items.Add(NameOfLayer);
                    currentLayer = layer;
                    LayerList.SelectedIndex = LayerList.Items.Count - 1;
                    activateControls();
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("Cannot Load Layer File. the texure: " +
                        ex.FileName +
                        " Could not be found in the content folder you have chosen. Content Folder Selector button has been enabled, you have can choose another Directory that has the resource you are trying to load.", "RESOURCE FILE NOT FOUND");
                    btnAddFiles.Enabled = true;
                }
            }
        }


        private void saveLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = WorkspacePath;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.FileName = LayerList.SelectedItem as string;
            saveFileDialog1.DefaultExt = "layer";
            saveFileDialog1.Filter = "Layer|*.layer";
            if ((currentLayer != null) && (LayerList.SelectedItem as string != null))
            {
                if ((SaveLayerPath == "") && (saveFileDialog1.ShowDialog() == DialogResult.OK))
                {
                    SaveLayerPath = saveFileDialog1.FileName;
                }

                if (SaveLayerPath != "")
                {
                    string[] textureList = new string[TextureList.Items.Count];

                    for (int i = 0; i < TextureList.Items.Count; i++)
                    {
                        textureList[i] = (string)TextureList.Items[i];
                    }

                    currentLayer.ReadOutLayer(SaveLayerPath, texPathAddress.Text, textureList, dictTextures, LayerList.SelectedItem as string);
                }
            }
        }

        #endregion




        #region HelperEventHandlers

        private void barAlpha_Scroll(object sender, EventArgs e)
        {
            if (currentLayer != null)
            {
                float alph = ((float)barAlpha.Value) / 100.0f;
                currentLayer.Alpha = alph;
            }
        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
        }

        #endregion

        private void radioSetColl_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void checkShowCollision_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileLayer.SetTileHeight = TileLayer.GetTileHeight + 16;
            TileLayer.SetTileWidth = TileLayer.GetTileWidth + 16;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileLayer.SetTileHeight = TileLayer.GetTileHeight - 16;
            TileLayer.SetTileWidth = TileLayer.GetTileWidth - 16;
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = WorkspacePath;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.FileName = LayerList.SelectedItem as string;
            saveFileDialog1.DefaultExt = "map";
            saveFileDialog1.Filter = "Map|*.map";
            if ((currentLayer != null) && (LayerList.SelectedItem as string != null))
            {
                if ((SaveLayerPath == "") && (saveFileDialog1.ShowDialog() == DialogResult.OK))
                {
                    SaveLayerPath = saveFileDialog1.FileName;
                }

                if (SaveLayerPath != "")
                {
                    string[] textureList = new string[TextureList.Items.Count];

                    for (int i = 0; i < TextureList.Items.Count; i++)
                    {
                        textureList[i] = (string)TextureList.Items[i];
                    }

                    string[] Layerlists = new string[LayerList.Items.Count];

                    for (int i = 0; i < LayerList.Items.Count; i++)
                    {
                        Layerlists[i] = (string)LayerList.Items[i];
                    }

                    Map.ReadOutMap(SaveLayerPath, texPathAddress.Text, textureList, dictTextures, Layerlists);
                }
            }
        }

        private void newLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog2.InitialDirectory = WorkspacePath;
            openFileDialog2.Filter = "Map File|*.map|Xml File|*.xml|All Supported Files|*.map;*.xml";

            Dictionary<int, string> texturesToLoad = new Dictionary<int, string>();
            Dictionary<string,Texture2D> masterTexturesToLoad = new Dictionary<string,Texture2D>();
            
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Map = TileMap.ReadInMap(openFileDialog2.FileName, out texturesToLoad);
               string extensFound = "";

            try
            {
                foreach (KeyValuePair<int, string> texturespaths in texturesToLoad)
                {
                    foreach (string ext in Extensions)
                    {
                        if (File.Exists(texPathAddress.Text + "\\" + texturespaths.Value + ext))
                        {
                            extensFound = ext;
                            break;
                        }
                    }
                    
                    FileStream stream = new FileStream(texPathAddress.Text + "\\" + texturespaths.Value + extensFound, FileMode.Open);
                    Texture2D texture = Texture2D.FromStream(GraphicsDevices, stream);
                    
                    stream.Close();
                    stream.Dispose();
                    if (!masterTexturesToLoad.ContainsKey(texturespaths.Value))
                    {
                        masterTexturesToLoad.Add(texturespaths.Value,texture);
                        //masterPathToLoad.Add(texturespaths.Value);
                        
                    }

                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Cannot Load Layer File. the texure: " +
                    ex.FileName +
                    " Could not be found in the content folder you have chosen. Content Folder Selector button has been enabled, you have can choose another Directory that has the resource you are trying to load.", "RESOURCE FILE NOT FOUND");
                btnAddFiles.Enabled = true;
            }

                foreach (TileLayer layer in Map.Layers)
                {
                    dictLayer.Add(layer.LayerName, layer);
                    LayerList.Items.Add(layer.LayerName);

                    foreach (KeyValuePair<string,Texture2D> tex in masterTexturesToLoad)
                    {
                    layer.AddTexture(tex.Value);
                    }

                    if (Map.Layers.IndexOf(layer) == Map.Layers.Count - 1)
                    {
                        foreach (KeyValuePair<string, Texture2D> tex in masterTexturesToLoad)
                        {
                            TextureList.Items.Add(tex.Key);
                            dictTextures.Add(tex.Key, tex.Value);
                            Image img = Image.FromFile(texPathAddress.Text + "\\" + tex.Key + extensFound);
                            dictImages.Add(tex.Key, img);
                        }


                        
                        currentLayer = layer;
                        LayerList.SelectedIndex = LayerList.Items.Count - 1;
                        activateControls();

                    }
                   
                }
                if (!String.IsNullOrWhiteSpace(Map.bgName))
                {
                    string extForBG = "";
                    foreach (string ext in Extensions)
                    {
                        if (File.Exists(texPathAddress.Text + "\\" + Map.bgName + ext))
                        {
                            extForBG = ext;
                            break;
                        }
                    }
                    string BgFilePath = texPathAddress.Text + "\\" + Map.bgName + extForBG;

                  //  try
                    //{
                        FileStream stream = new FileStream(BgFilePath, FileMode.Open);

                        Texture2D texture = Texture2D.FromStream(GraphicsDevices, stream);
                        Map.BackGroundPicture = texture;
                        stream.Close();
                        stream.Dispose();
                 //   }
                  //  catch(Exception ex)
                 //   {
                 //       MessageBox.Show("Problem Loading BackGround");
                 //   }
                }
                
            }

        }

        private void btnBG_Click(object sender, EventArgs e)
        {
            if (Map != null)
            {
                string Path;
                openFileDialog1.InitialDirectory = WorkspacePath;
                openFileDialog1.Filter = "Png|*.png|Jpeg|*.jpeg|Jpg|*.jpg|All Image Formats|*.png;*.jpeg;*.jpg";
                openFileDialog1.Multiselect = false;
                openFileDialog1.InitialDirectory = texPathAddress.Text;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Path = openFileDialog1.FileName;
                    string PathtoSave = Path.Substring(texPathAddress.Text.Length+1);
                    PathtoSave = PathtoSave.Substring(0, PathtoSave.Length - 4);
                    Map.bgName = PathtoSave;
                    try
                    {
                        Stream stream = openFileDialog1.OpenFile();
                        Map.BackGroundPicture = Texture2D.FromStream(GraphicsDevices, stream);
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show( "There Was a Problem Loading the resource you wanted:  " + ex.StackTrace, "Error Loading File" );
                    }
                }
            }
        }

        private void loadLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       
       
    }

}
                          