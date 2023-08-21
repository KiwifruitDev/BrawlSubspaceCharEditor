namespace BrawlSubspaceCharEditor;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
        closeToolStripMenuItem = new ToolStripMenuItem();
        helpToolStripMenuItem = new ToolStripMenuItem();
        aboutToolStripMenuItem = new ToolStripMenuItem();
        richTextBox1 = new RichTextBox();
        label2 = new Label();
        tabPage1 = new TabPage();
        button1 = new Button();
        label4 = new Label();
        label1 = new Label();
        pictureBox2 = new PictureBox();
        comboBox1 = new ComboBox();
        label9 = new Label();
        label10 = new Label();
        textBox3 = new TextBox();
        label11 = new Label();
        textBox4 = new TextBox();
        label8 = new Label();
        label7 = new Label();
        textBox2 = new TextBox();
        label6 = new Label();
        textBox1 = new TextBox();
        tabControl1 = new TabControl();
        tabControl2 = new TabControl();
        tabPage2 = new TabPage();
        menuStrip1.SuspendLayout();
        tabPage1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        tabControl1.SuspendLayout();
        tabControl2.SuspendLayout();
        tabPage2.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(420, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, closeToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Size = new Size(180, 22);
        openToolStripMenuItem.Text = "Open";
        openToolStripMenuItem.Click += openToolStripMenuItem_Click;
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Enabled = false;
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Size = new Size(180, 22);
        saveToolStripMenuItem.Text = "Save";
        saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Enabled = false;
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.Size = new Size(180, 22);
        saveAsToolStripMenuItem.Text = "Save As...";
        saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
        // 
        // closeToolStripMenuItem
        // 
        closeToolStripMenuItem.Enabled = false;
        closeToolStripMenuItem.Name = "closeToolStripMenuItem";
        closeToolStripMenuItem.Size = new Size(180, 22);
        closeToolStripMenuItem.Text = "Close";
        closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
        // 
        // helpToolStripMenuItem
        // 
        helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
        helpToolStripMenuItem.Enabled = false;
        helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        helpToolStripMenuItem.Size = new Size(44, 20);
        helpToolStripMenuItem.Text = "Help";
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Enabled = false;
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        aboutToolStripMenuItem.Size = new Size(107, 22);
        aboutToolStripMenuItem.Text = "About";
        // 
        // richTextBox1
        // 
        richTextBox1.Dock = DockStyle.Bottom;
        richTextBox1.Enabled = false;
        richTextBox1.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
        richTextBox1.Location = new Point(3, 216);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.ReadOnly = true;
        richTextBox1.Size = new Size(406, 52);
        richTextBox1.TabIndex = 4;
        richTextBox1.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 C1 BC 00 00 41 2E 66 66 C1 C5 D7 0A C1 0C 00 00";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Bottom;
        label2.Location = new Point(3, 201);
        label2.Name = "label2";
        label2.Size = new Size(60, 15);
        label2.TabIndex = 7;
        label2.Text = "Extra Data";
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(button1);
        tabPage1.Controls.Add(label4);
        tabPage1.Controls.Add(label1);
        tabPage1.Controls.Add(pictureBox2);
        tabPage1.Controls.Add(comboBox1);
        tabPage1.Controls.Add(label9);
        tabPage1.Controls.Add(label10);
        tabPage1.Controls.Add(textBox3);
        tabPage1.Controls.Add(label11);
        tabPage1.Controls.Add(textBox4);
        tabPage1.Controls.Add(label8);
        tabPage1.Controls.Add(label7);
        tabPage1.Controls.Add(textBox2);
        tabPage1.Controls.Add(label6);
        tabPage1.Controls.Add(textBox1);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(398, 170);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Mario";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // button1
        // 
        button1.Location = new Point(6, 138);
        button1.Name = "button1";
        button1.Size = new Size(129, 23);
        button1.TabIndex = 13;
        button1.Text = "Set Movie Frame";
        button1.UseVisualStyleBackColor = true;
        button1.Visible = false;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(141, 3);
        label4.Name = "label4";
        label4.Size = new Size(76, 15);
        label4.TabIndex = 21;
        label4.Text = "Movie Frame";
        label4.Visible = false;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(6, 3);
        label1.Name = "label1";
        label1.Size = new Size(72, 15);
        label1.TabIndex = 18;
        label1.Text = "Character ID";
        // 
        // pictureBox2
        // 
        pictureBox2.Location = new Point(141, 21);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(249, 140);
        pictureBox2.TabIndex = 20;
        pictureBox2.TabStop = false;
        pictureBox2.Visible = false;
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[] { "Mario", "Donkey Kong", "Link", "Samus", "Zero Suit Samus", "Yoshi", "Kirby", "Fox", "Pikachu", "Luigi", "Captain Falcon", "Ness", "Bowser/Giga Bowser", "Peach", "Zelda", "Sheik", "Ice Climbers", "Marth", "Mr. Game & Watch", "Falco", "Ganondorf", "Wario/Wario-Man", "Meta Knight", "Pit", "Olimar & Pikmin", "Lucas", "Diddy Kong", "Pokémon Trainer", "Charizard", "Squirtle", "Ivysaur", "King Dedede", "Lucario", "Ike", "R.O.B.", "Jigglypuff", "Toon Link", "Wolf", "Snake", "Sonic", "None", "Random", "Knuckles (Project+)", "Roy (Project M)", "Mewtwo (Project M)", "Red Alloy", "Blue Alloy", "Yellow Alloy", "Green Alloy" });
        comboBox1.Location = new Point(6, 21);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(129, 23);
        comboBox1.TabIndex = 17;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(6, 91);
        label9.Name = "label9";
        label9.Size = new Size(85, 15);
        label9.TabIndex = 16;
        label9.Text = "Name Position";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(74, 112);
        label10.Name = "label10";
        label10.Size = new Size(14, 15);
        label10.TabIndex = 15;
        label10.Text = "Y";
        // 
        // textBox3
        // 
        textBox3.Enabled = false;
        textBox3.Location = new Point(94, 109);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(41, 23);
        textBox3.TabIndex = 14;
        textBox3.Text = "-15.75";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(6, 112);
        label11.Name = "label11";
        label11.Size = new Size(14, 15);
        label11.TabIndex = 13;
        label11.Text = "X";
        // 
        // textBox4
        // 
        textBox4.Enabled = false;
        textBox4.Location = new Point(26, 109);
        textBox4.Name = "textBox4";
        textBox4.Size = new Size(42, 23);
        textBox4.TabIndex = 12;
        textBox4.Text = "-15.75";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(6, 47);
        label8.Name = "label8";
        label8.Size = new Size(88, 15);
        label8.TabIndex = 11;
        label8.Text = "Cursor Position";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(74, 68);
        label7.Name = "label7";
        label7.Size = new Size(14, 15);
        label7.TabIndex = 10;
        label7.Text = "Y";
        // 
        // textBox2
        // 
        textBox2.Enabled = false;
        textBox2.Location = new Point(94, 65);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(41, 23);
        textBox2.TabIndex = 9;
        textBox2.Text = "-15.75";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(6, 68);
        label6.Name = "label6";
        label6.Size = new Size(14, 15);
        label6.TabIndex = 8;
        label6.Text = "X";
        // 
        // textBox1
        // 
        textBox1.Enabled = false;
        textBox1.Location = new Point(26, 65);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(42, 23);
        textBox1.TabIndex = 7;
        textBox1.Text = "-15.75";
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Dock = DockStyle.Top;
        tabControl1.Enabled = false;
        tabControl1.Location = new Point(3, 3);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(406, 198);
        tabControl1.TabIndex = 8;
        // 
        // tabControl2
        // 
        tabControl2.Controls.Add(tabPage2);
        tabControl2.Dock = DockStyle.Fill;
        tabControl2.Location = new Point(0, 24);
        tabControl2.Name = "tabControl2";
        tabControl2.SelectedIndex = 0;
        tabControl2.Size = new Size(420, 299);
        tabControl2.TabIndex = 13;
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(tabControl1);
        tabPage2.Controls.Add(label2);
        tabPage2.Controls.Add(richTextBox1);
        tabPage2.Location = new Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(412, 271);
        tabPage2.TabIndex = 0;
        tabPage2.Text = "Table 1";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(420, 323);
        Controls.Add(tabControl2);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Brawl Subspace Character Editor";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        tabPage1.ResumeLayout(false);
        tabPage1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        tabControl1.ResumeLayout(false);
        tabControl2.ResumeLayout(false);
        tabPage2.ResumeLayout(false);
        tabPage2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private RichTextBox richTextBox1;
    private Label label2;
    private TabPage tabPage1;
    private TextBox textBox1;
    private TabControl tabControl1;
    private Button button1;
    private Label label6;
    private Label label7;
    private TextBox textBox2;
    private Label label8;
    private Label label9;
    private Label label10;
    private TextBox textBox3;
    private Label label11;
    private TextBox textBox4;
    private ToolStripMenuItem closeToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private Label label1;
    private ComboBox comboBox1;
    private TabControl tabControl2;
    private TabPage tabPage2;
    private Label label4;
    private PictureBox pictureBox2;
}
