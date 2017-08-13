namespace Sage_Editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWorksapceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitBitchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnBG = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chekFill = new System.Windows.Forms.CheckBox();
            this.radErase = new System.Windows.Forms.RadioButton();
            this.radDraw = new System.Windows.Forms.RadioButton();
            this.barAlpha = new System.Windows.Forms.TrackBar();
            this.btnRemoveLayer = new System.Windows.Forms.Button();
            this.btnRemoveTexture = new System.Windows.Forms.Button();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.btnAddTexture = new System.Windows.Forms.Button();
            this.LayerList = new System.Windows.Forms.ListBox();
            this.TextureList = new System.Windows.Forms.ListBox();
            this.texPathAddress = new System.Windows.Forms.TextBox();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.radioEarseColl = new System.Windows.Forms.RadioButton();
            this.radioSetColl = new System.Windows.Forms.RadioButton();
            this.checkShowCollision = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.tileDisplay1 = new Sage_Editor.TileDisplay();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAlpha)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1043, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWorksapceToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.exitBitchesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openWorksapceToolStripMenuItem
            // 
            this.openWorksapceToolStripMenuItem.Name = "openWorksapceToolStripMenuItem";
            this.openWorksapceToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openWorksapceToolStripMenuItem.Text = "Open Worksapce";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLayerToolStripMenuItem,
            this.saveAsLayerToolStripMenuItem,
            this.saveAllLayerToolStripMenuItem,
            this.loadLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // saveLayerToolStripMenuItem
            // 
            this.saveLayerToolStripMenuItem.Name = "saveLayerToolStripMenuItem";
            this.saveLayerToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveLayerToolStripMenuItem.Text = "Save Layer";
            this.saveLayerToolStripMenuItem.Click += new System.EventHandler(this.saveLayerToolStripMenuItem_Click);
            // 
            // saveAsLayerToolStripMenuItem
            // 
            this.saveAsLayerToolStripMenuItem.Name = "saveAsLayerToolStripMenuItem";
            this.saveAsLayerToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsLayerToolStripMenuItem.Text = "SaveAs Layer";
            // 
            // saveAllLayerToolStripMenuItem
            // 
            this.saveAllLayerToolStripMenuItem.Name = "saveAllLayerToolStripMenuItem";
            this.saveAllLayerToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAllLayerToolStripMenuItem.Text = "Save All Layer";
            // 
            // loadLayerToolStripMenuItem
            // 
            this.loadLayerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickLoadToolStripMenuItem});
            this.loadLayerToolStripMenuItem.Name = "loadLayerToolStripMenuItem";
            this.loadLayerToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.loadLayerToolStripMenuItem.Text = "Load Layer";
            this.loadLayerToolStripMenuItem.Click += new System.EventHandler(this.loadLayerToolStripMenuItem_Click);
            // 
            // quickLoadToolStripMenuItem
            // 
            this.quickLoadToolStripMenuItem.Name = "quickLoadToolStripMenuItem";
            this.quickLoadToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.quickLoadToolStripMenuItem.Text = "QuickLoad";
            this.quickLoadToolStripMenuItem.Click += new System.EventHandler(this.quickLoadToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMapToolStripMenuItem,
            this.saveAsMapToolStripMenuItem,
            this.loadMapToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveMapToolStripMenuItem.Text = "Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // saveAsMapToolStripMenuItem
            // 
            this.saveAsMapToolStripMenuItem.Name = "saveAsMapToolStripMenuItem";
            this.saveAsMapToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveAsMapToolStripMenuItem.Text = "SaveAs Map";
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.loadMapToolStripMenuItem.Text = "Load Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLevelToolStripMenuItem,
            this.saveAsLevelToolStripMenuItem,
            this.loadLevelToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveLevelToolStripMenuItem.Text = "Save Level";
            // 
            // saveAsLevelToolStripMenuItem
            // 
            this.saveAsLevelToolStripMenuItem.Name = "saveAsLevelToolStripMenuItem";
            this.saveAsLevelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveAsLevelToolStripMenuItem.Text = "SaveAs Level";
            // 
            // loadLevelToolStripMenuItem
            // 
            this.loadLevelToolStripMenuItem.Name = "loadLevelToolStripMenuItem";
            this.loadLevelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.loadLevelToolStripMenuItem.Text = "Load Level";
            this.loadLevelToolStripMenuItem.Click += new System.EventHandler(this.loadLevelToolStripMenuItem_Click);
            // 
            // exitBitchesToolStripMenuItem
            // 
            this.exitBitchesToolStripMenuItem.Name = "exitBitchesToolStripMenuItem";
            this.exitBitchesToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitBitchesToolStripMenuItem.Text = "Exit (Bitches)";
            this.exitBitchesToolStripMenuItem.Click += new System.EventHandler(this.exitBitchesToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(703, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(328, 654);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnBG);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.chekFill);
            this.tabPage1.Controls.Add(this.radErase);
            this.tabPage1.Controls.Add(this.radDraw);
            this.tabPage1.Controls.Add(this.barAlpha);
            this.tabPage1.Controls.Add(this.btnRemoveLayer);
            this.tabPage1.Controls.Add(this.btnRemoveTexture);
            this.tabPage1.Controls.Add(this.btnAddLayer);
            this.tabPage1.Controls.Add(this.btnAddTexture);
            this.tabPage1.Controls.Add(this.LayerList);
            this.tabPage1.Controls.Add(this.TextureList);
            this.tabPage1.Controls.Add(this.texPathAddress);
            this.tabPage1.Controls.Add(this.btnAddFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(320, 628);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TileMap";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnBG
            // 
            this.btnBG.Location = new System.Drawing.Point(75, 79);
            this.btnBG.Name = "btnBG";
            this.btnBG.Size = new System.Drawing.Size(147, 27);
            this.btnBG.TabIndex = 10;
            this.btnBG.Text = "SetBackGround Image";
            this.btnBG.UseVisualStyleBackColor = true;
            this.btnBG.Click += new System.EventHandler(this.btnBG_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Location = new System.Drawing.Point(45, 418);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(211, 202);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // chekFill
            // 
            this.chekFill.AutoSize = true;
            this.chekFill.Enabled = false;
            this.chekFill.Location = new System.Drawing.Point(205, 44);
            this.chekFill.Name = "chekFill";
            this.chekFill.Size = new System.Drawing.Size(38, 17);
            this.chekFill.TabIndex = 8;
            this.chekFill.Text = "Fill";
            this.chekFill.UseVisualStyleBackColor = true;
            // 
            // radErase
            // 
            this.radErase.AutoSize = true;
            this.radErase.Enabled = false;
            this.radErase.Location = new System.Drawing.Point(123, 43);
            this.radErase.Name = "radErase";
            this.radErase.Size = new System.Drawing.Size(52, 17);
            this.radErase.TabIndex = 7;
            this.radErase.TabStop = true;
            this.radErase.Text = "Erase";
            this.radErase.UseVisualStyleBackColor = true;
            // 
            // radDraw
            // 
            this.radDraw.AutoSize = true;
            this.radDraw.Enabled = false;
            this.radDraw.Location = new System.Drawing.Point(36, 43);
            this.radDraw.Name = "radDraw";
            this.radDraw.Size = new System.Drawing.Size(50, 17);
            this.radDraw.TabIndex = 6;
            this.radDraw.TabStop = true;
            this.radDraw.Text = "Draw";
            this.radDraw.UseVisualStyleBackColor = true;
            // 
            // barAlpha
            // 
            this.barAlpha.Enabled = false;
            this.barAlpha.Location = new System.Drawing.Point(23, 112);
            this.barAlpha.Maximum = 100;
            this.barAlpha.Name = "barAlpha";
            this.barAlpha.Size = new System.Drawing.Size(278, 45);
            this.barAlpha.TabIndex = 5;
            this.barAlpha.Scroll += new System.EventHandler(this.barAlpha_Scroll);
            // 
            // btnRemoveLayer
            // 
            this.btnRemoveLayer.Enabled = false;
            this.btnRemoveLayer.Location = new System.Drawing.Point(181, 272);
            this.btnRemoveLayer.Name = "btnRemoveLayer";
            this.btnRemoveLayer.Size = new System.Drawing.Size(94, 23);
            this.btnRemoveLayer.TabIndex = 4;
            this.btnRemoveLayer.Text = "Remove Layer";
            this.btnRemoveLayer.UseVisualStyleBackColor = true;
            this.btnRemoveLayer.Click += new System.EventHandler(this.btnRemoveLayer_Click);
            // 
            // btnRemoveTexture
            // 
            this.btnRemoveTexture.Enabled = false;
            this.btnRemoveTexture.Location = new System.Drawing.Point(181, 389);
            this.btnRemoveTexture.Name = "btnRemoveTexture";
            this.btnRemoveTexture.Size = new System.Drawing.Size(94, 23);
            this.btnRemoveTexture.TabIndex = 4;
            this.btnRemoveTexture.Text = "Remove Texture";
            this.btnRemoveTexture.UseVisualStyleBackColor = true;
            this.btnRemoveTexture.Click += new System.EventHandler(this.btnRemoveTexture_Click);
            // 
            // btnAddLayer
            // 
            this.btnAddLayer.Enabled = false;
            this.btnAddLayer.Location = new System.Drawing.Point(54, 272);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(91, 23);
            this.btnAddLayer.TabIndex = 3;
            this.btnAddLayer.Text = "Add Layer";
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);
            // 
            // btnAddTexture
            // 
            this.btnAddTexture.Enabled = false;
            this.btnAddTexture.Location = new System.Drawing.Point(54, 389);
            this.btnAddTexture.Name = "btnAddTexture";
            this.btnAddTexture.Size = new System.Drawing.Size(91, 23);
            this.btnAddTexture.TabIndex = 3;
            this.btnAddTexture.Text = "Add Texture";
            this.btnAddTexture.UseVisualStyleBackColor = true;
            this.btnAddTexture.Click += new System.EventHandler(this.btnAddTexture_Click);
            // 
            // LayerList
            // 
            this.LayerList.Enabled = false;
            this.LayerList.FormattingEnabled = true;
            this.LayerList.Location = new System.Drawing.Point(23, 184);
            this.LayerList.Name = "LayerList";
            this.LayerList.Size = new System.Drawing.Size(278, 82);
            this.LayerList.TabIndex = 2;
            this.LayerList.SelectedIndexChanged += new System.EventHandler(this.LayerList_SelectedIndexChanged);
            // 
            // TextureList
            // 
            this.TextureList.Enabled = false;
            this.TextureList.FormattingEnabled = true;
            this.TextureList.Location = new System.Drawing.Point(23, 301);
            this.TextureList.Name = "TextureList";
            this.TextureList.Size = new System.Drawing.Size(278, 82);
            this.TextureList.TabIndex = 2;
            this.TextureList.SelectedIndexChanged += new System.EventHandler(this.TextureList_SelectedIndexChanged);
            // 
            // texPathAddress
            // 
            this.texPathAddress.Location = new System.Drawing.Point(22, 18);
            this.texPathAddress.Name = "texPathAddress";
            this.texPathAddress.ReadOnly = true;
            this.texPathAddress.Size = new System.Drawing.Size(231, 20);
            this.texPathAddress.TabIndex = 1;
            this.texPathAddress.TextChanged += new System.EventHandler(this.texPathAddress_TextChanged);
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(259, 17);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(41, 23);
            this.btnAddFiles.TabIndex = 0;
            this.btnAddFiles.Text = "...";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.radioEarseColl);
            this.tabPage2.Controls.Add(this.radioSetColl);
            this.tabPage2.Controls.Add(this.checkShowCollision);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(320, 628);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Collision Map";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // radioEarseColl
            // 
            this.radioEarseColl.AutoSize = true;
            this.radioEarseColl.Location = new System.Drawing.Point(29, 100);
            this.radioEarseColl.Name = "radioEarseColl";
            this.radioEarseColl.Size = new System.Drawing.Size(93, 17);
            this.radioEarseColl.TabIndex = 1;
            this.radioEarseColl.Text = "Erase Collision";
            this.radioEarseColl.UseVisualStyleBackColor = true;
            // 
            // radioSetColl
            // 
            this.radioSetColl.AutoSize = true;
            this.radioSetColl.Checked = true;
            this.radioSetColl.Location = new System.Drawing.Point(29, 77);
            this.radioSetColl.Name = "radioSetColl";
            this.radioSetColl.Size = new System.Drawing.Size(82, 17);
            this.radioSetColl.TabIndex = 1;
            this.radioSetColl.TabStop = true;
            this.radioSetColl.Text = "Set Collision";
            this.radioSetColl.UseVisualStyleBackColor = true;
            this.radioSetColl.CheckedChanged += new System.EventHandler(this.radioSetColl_CheckedChanged);
            // 
            // checkShowCollision
            // 
            this.checkShowCollision.AutoSize = true;
            this.checkShowCollision.Checked = true;
            this.checkShowCollision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkShowCollision.Location = new System.Drawing.Point(29, 37);
            this.checkShowCollision.Name = "checkShowCollision";
            this.checkShowCollision.Size = new System.Drawing.Size(123, 17);
            this.checkShowCollision.TabIndex = 0;
            this.checkShowCollision.Text = "Show CollidableTiles";
            this.checkShowCollision.UseVisualStyleBackColor = true;
            this.checkShowCollision.CheckedChanged += new System.EventHandler(this.checkShowCollision_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(320, 628);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Enemy";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(320, 628);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Objects";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(320, 628);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Animations";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(687, 25);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 633);
            this.vScrollBar1.TabIndex = 3;
            this.vScrollBar1.Visible = false;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(15, 658);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(667, 19);
            this.hScrollBar1.TabIndex = 4;
            this.hScrollBar1.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FilterIndex = 4;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.FilterIndex = 4;
            // 
            // tileDisplay1
            // 
            this.tileDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tileDisplay1.Location = new System.Drawing.Point(12, 27);
            this.tileDisplay1.Name = "tileDisplay1";
            this.tileDisplay1.Size = new System.Drawing.Size(668, 631);
            this.tileDisplay1.TabIndex = 0;
            this.tileDisplay1.Text = "tileDisplay1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 693);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tileDisplay1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sages Engine";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAlpha)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TileDisplay tileDisplay1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitBitchesToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TrackBar barAlpha;
        private System.Windows.Forms.Button btnRemoveLayer;
        private System.Windows.Forms.Button btnRemoveTexture;
        private System.Windows.Forms.Button btnAddLayer;
        private System.Windows.Forms.Button btnAddTexture;
        public  System.Windows.Forms.ListBox LayerList;
        public System.Windows.Forms.ListBox TextureList;
        private System.Windows.Forms.TextBox texPathAddress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chekFill;
        private System.Windows.Forms.RadioButton radErase;
        private System.Windows.Forms.RadioButton radDraw;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ToolStripMenuItem quickLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWorksapceToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioEarseColl;
        private System.Windows.Forms.RadioButton radioSetColl;
        public System.Windows.Forms.CheckBox checkShowCollision;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.Button btnBG;
    }
}

