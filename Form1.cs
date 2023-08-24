using System.Diagnostics;
using System.Globalization;

namespace BrawlSubspaceCharEditor;

public partial class Form1 : Form
{
    public SelectCharacterFile selectCharacterFile;
    public Image image;
    public bool temp = false;
    public Form1()
    {
        InitializeComponent();
        tabControl2.TabPages.Clear();
        versionToolStripMenuItem.Text = "Version v" + Application.ProductVersion;
    }
    // Open a .dat file and use its file stream to create SelectCharacterFile()
    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "mu_adv_selchrb_tbl (*.dat)|*.dat",
            RestoreDirectory = true
        };
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            // Close existing file stream
            if (selectCharacterFile.fileStream != null)
                selectCharacterFile.fileStream.Close();
            fileNameToolStripMenuItem.Text = "File: " + openFileDialog.FileName;
            FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open);
            selectCharacterFile = new SelectCharacterFile(fileStream);
            closeToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            addCharacterToolStripMenuItem.Enabled = true;
            removeCharacterToolStripMenuItem.Enabled = true;
            // Now for each indice, create a tab page
            for (int i = 0; i < selectCharacterFile.CharacterTables.Length; i++)
            {
                TabPage tabPage = new TabPage
                {
                    Text = "Table " + (i + 1)
                };
                tabControl2.TabPages.Add(tabPage);
                // Tab page to hold each character
                TabControl tabControl = new TabControl
                {
                    Location = new Point(3, 3),
                    Size = new Size(406, 198),
                    Dock = DockStyle.Top
                };
                tabPage.Controls.Add(tabControl);
                // Now for each character in the table, create a new tab
                for (int j = 0; j < selectCharacterFile.CharacterTables[i].CharacterSelection.Length; j++)
                {
                    TabPage tabPage2 = new TabPage
                    {
                        Text = selectCharacterFile.CharacterTables[i].CharacterSelection[j].GetCharacterName()
                    };
                    tabControl.TabPages.Add(tabPage2);
                    Label label = new Label
                    {
                        Location = new Point(6, 3),
                        Size = new Size(72, 15),
                        Text = "Character ID"
                    };
                    tabPage2.Controls.Add(label);
                    ComboBox comboBox = new ComboBox
                    {
                        Name = "comboBox" + i + "_" + j,
                        Location = new Point(6, 21),
                        Size = new Size(129, 23)
                    };
                    comboBox.Items.AddRange(CharacterSelection.CharacterNames);
                    comboBox.SelectedIndex = selectCharacterFile.CharacterTables[i].CharacterSelection[j].CharacterID;
                    // Add action listener to update character ID
                    comboBox.SelectedIndexChanged += (object sender2, EventArgs e2) =>
                    {
                        // Get indices from name (i, j)
                        try
                        {
                            string[] indices = comboBox.Name.Replace("comboBox", "").Split(new char[] { '_' });
                            int.TryParse(indices[0], out int i2);
                            int.TryParse(indices[1], out int j2);
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CharacterID = (byte)comboBox.SelectedIndex;
                            // Set tab name
                            tabControl.TabPages[j2].Text = selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].GetCharacterName();
                        }
                        catch (Exception)
                        {
                            // Stale reference, show an error
                            MessageBox.Show("Error: This character's index is a stale reference. Please save and reopen the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    tabPage2.Controls.Add(comboBox);
                    PictureBox pictureBox = new PictureBox
                    {
                        Location = new Point(141, 21),
                        Size = new Size(249, 140)
                    };
                    tabPage2.Controls.Add(pictureBox);
                    Label label2 = new Label
                    {
                        Location = new Point(6, 47),
                        Size = new Size(88, 15),
                        Text = "Cursor Position"
                    };
                    tabPage2.Controls.Add(label2);
                    Label label3 = new Label
                    {
                        Location = new Point(6, 68),
                        Size = new Size(14, 15),
                        Text = "X"
                    };
                    tabPage2.Controls.Add(label3);
                    TextBox textBox = new TextBox
                    {
                        Name = "cursorPositionXtextBox" + i + "_" + j,
                        Location = new Point(26, 65),
                        Size = new Size(42, 23),
                        Text = selectCharacterFile.CharacterTables[i].CharacterSelection[j].CursorPositionX.ToString()
                    };
                    // Add action listener to update cursor position
                    textBox.TextChanged += (object sender2, EventArgs e2) =>
                    {
                        float.TryParse(textBox.Text, out float result);
                        // Get indices from name (i, j)
                        string[] indices = textBox.Name.Replace("cursorPositionXtextBox", "").Split(new char[] { '_' });
                        int.TryParse(indices[0], out int i2);
                        int.TryParse(indices[1], out int j2);
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX = result;
                        drawMarkers(
                            pictureBox,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
                        );
                    };
                    tabPage2.Controls.Add(textBox);
                    Label label4 = new Label
                    {
                        Location = new Point(74, 68),
                        Size = new Size(14, 15),
                        Text = "Y"
                    };
                    tabPage2.Controls.Add(label4);
                    TextBox textBox2 = new TextBox
                    {
                        Name = "cursorPositionYtextBox" + i + "_" + j,
                        Location = new Point(94, 65),
                        Size = new Size(41, 23),
                        Text = selectCharacterFile.CharacterTables[i].CharacterSelection[j].CursorPositionY.ToString()
                    };
                    // Add action listener to update cursor position
                    textBox2.TextChanged += (object sender2, EventArgs e2) =>
                    {
                        float.TryParse(textBox2.Text, out float result);
                        // Get indices from name (i, j)
                        string[] indices = textBox2.Name.Replace("cursorPositionYtextBox", "").Split(new char[] { '_' });
                        int.TryParse(indices[0], out int i2);
                        int.TryParse(indices[1], out int j2);
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY = result;
                        drawMarkers(
                            pictureBox,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
                        );
                    };
                    tabPage2.Controls.Add(textBox2);
                    Label label5 = new Label
                    {
                        Location = new Point(6, 91),
                        Size = new Size(85, 15),
                        Text = "Name Position"
                    };
                    tabPage2.Controls.Add(label5);
                    Label label6 = new Label
                    {
                        Location = new Point(6, 112),
                        Size = new Size(14, 15),
                        Text = "X"
                    };
                    tabPage2.Controls.Add(label6);
                    TextBox textBox3 = new TextBox
                    {
                        Name = "namePositionXtextBox" + i + "_" + j,
                        Location = new Point(26, 109),
                        Size = new Size(42, 23),
                        Text = selectCharacterFile.CharacterTables[i].CharacterSelection[j].NamePositionX.ToString()
                    };
                    // Add action listener to update name position
                    textBox3.TextChanged += (object sender2, EventArgs e2) =>
                    {
                        float.TryParse(textBox3.Text, out float result);
                        // Get indices from name (i, j)
                        string[] indices = textBox3.Name.Replace("namePositionXtextBox", "").Split(new char[] { '_' });
                        int.TryParse(indices[0], out int i2);
                        int.TryParse(indices[1], out int j2);
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX = result;
                        drawMarkers(
                            pictureBox,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
                        );
                    };
                    tabPage2.Controls.Add(textBox3);
                    Label label7 = new Label
                    {
                        Location = new Point(74, 112),
                        Size = new Size(14, 15),
                        Text = "Y"
                    };
                    tabPage2.Controls.Add(label7);
                    TextBox textBox4 = new TextBox
                    {
                        Name = "namePositionYtextBox" + i + "_" + j,
                        Location = new Point(94, 109),
                        Size = new Size(41, 23),
                        Text = selectCharacterFile.CharacterTables[i].CharacterSelection[j].NamePositionY.ToString()
                    };
                    // Add action listener to update name position
                    textBox4.TextChanged += (object sender2, EventArgs e2) =>
                    {
                        float.TryParse(textBox4.Text, out float result);
                        // Get indices from name (i, j)
                        string[] indices = textBox4.Name.Replace("namePositionYtextBox", "").Split(new char[] { '_' });
                        int.TryParse(indices[0], out int i2);
                        int.TryParse(indices[1], out int j2);
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY = result;
                        drawMarkers(
                            pictureBox,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
                        );
                    };
                    tabPage2.Controls.Add(textBox4);
                    Label label9 = new Label
                    {
                        Location = new Point(141, 3),
                        Size = new Size(76, 15),
                        Text = "Movie Frame"
                    };
                    tabPage2.Controls.Add(label9);
                    Button button = new Button
                    {
                        Name = "button" + i + "_" + j,
                        Location = new Point(6, 138),
                        Size = new Size(129, 23),
                        Text = "Set Movie Frame"
                    };
                    tabPage2.Controls.Add(button);
                    // Add action listener to set movie frame
                    button.Click += (object sender2, EventArgs e2) =>
                    {
                        OpenFileDialog openFileDialog2 = new OpenFileDialog
                        {
                            Filter = "PNG (*.png)|*.png",
                            RestoreDirectory = true
                        };
                        if (openFileDialog2.ShowDialog() == DialogResult.OK)
                        {
                            image = Image.FromFile(openFileDialog2.FileName);
                            // Get indices from name (i, j)
                            string[] indices = button.Name.Replace("button", "").Split(new char[] { '_' });
                            int.TryParse(indices[0], out int i2);
                            // Get tab control from parent tab page
                            TabControl tabControlP = (TabControl)button.Parent.Parent;
                            for (int k2 = 0; k2 < tabControlP.TabPages.Count; k2++)
                            {
                                // Set image for each character
                                drawMarkers(
                                    (PictureBox)tabControlP.TabPages[k2].Controls[2],
                                    selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].CursorPositionX,
                                    selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].CursorPositionY,
                                    selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].NamePositionX,
                                    selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].NamePositionY
                                );
                            }
                        }
                    };
                }
                Label label8 = new Label
                {
                    Location = new Point(3, 201),
                    Size = new Size(60, 15),
                    Text = "Extra Data",
                    Dock = DockStyle.Bottom
                };
                tabPage.Controls.Add(label8);
                RichTextBox richTextBox = new RichTextBox
                {
                    Location = new Point(3, 216),
                    Size = new Size(406, 52),
                    Text = BitConverter.ToString(selectCharacterFile.CharacterTables[i].Unknown).Replace("-", " "),
                    Dock = DockStyle.Bottom
                };
                // Add action listener to update unknown bytes
                richTextBox.TextChanged += (object sender2, EventArgs e2) =>
                {
                    string[] hexValuesSplit = richTextBox.Text.Split(' ');
                    byte[] newUnknown = new byte[20];
                    for (int j = 0; j < hexValuesSplit.Length; j++)
                    {
                        byte.TryParse(hexValuesSplit[j], NumberStyles.HexNumber, null, out byte result);
                        newUnknown[j] = result;
                    }
                    selectCharacterFile.CharacterTables[i].Unknown = newUnknown;
                };
                tabPage.Controls.Add(richTextBox);
            }
        }
    }
    private void drawMarkers(PictureBox pictureBox, float x1, float y1, float x2, float y2)
    {
        // Draw markers for cursor and name positions
        Bitmap modifiedImage = new Bitmap(image, new Size(pictureBox.Width, pictureBox.Height));
        Graphics graphics = Graphics.FromImage(modifiedImage);
        int xweighted1, yweighted1, xweighted2, yweighted2;
        int baseResolutionX = 64, baseResolutionY = 64;
        // These coordinates start from the center, but Y is inverted
        y1 = -y1;
        y2 = -y2;
        // Convert these to local coordinates for the picture box
        xweighted1 = (int)(x1 * pictureBox.Width / baseResolutionX) + pictureBox.Width / 2;
        yweighted1 = (int)(y1 * pictureBox.Height / baseResolutionY) + pictureBox.Height / 2;
        xweighted2 = (int)(x2 * pictureBox.Width / baseResolutionX) + pictureBox.Width / 2;
        yweighted2 = (int)(y2 * pictureBox.Height / baseResolutionY) + pictureBox.Height / 2;
        // Draw arrow
        graphics.DrawLine(new Pen(Color.Red, 2), xweighted1, yweighted1, xweighted1, yweighted1 - 10);
        graphics.DrawLine(new Pen(Color.Red, 2), xweighted1, yweighted1, xweighted1 - 5, yweighted1 - 5);
        graphics.DrawLine(new Pen(Color.Red, 2), xweighted1, yweighted1, xweighted1 + 5, yweighted1 - 5);
        // Draw box
        graphics.DrawRectangle(new Pen(Color.Blue, 2), xweighted2 - 10, yweighted2 - 5, 20, 10);
        pictureBox.Image = modifiedImage;
    }
    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        closeToolStripMenuItem.Enabled = false;
        saveToolStripMenuItem.Enabled = false;
        saveAsToolStripMenuItem.Enabled = false;
        addCharacterToolStripMenuItem.Enabled = false;
        removeCharacterToolStripMenuItem.Enabled = false;
        tabControl2.TabPages.Clear();
        // Close file stream
        selectCharacterFile.fileStream.Close();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (temp)
        {
            saveAsToolStripMenuItem_Click(sender, e);
        }
        else
        {
            fileNameToolStripMenuItem.Text = "File: " + selectCharacterFile.fileStream.Name;
            selectCharacterFile.Save(selectCharacterFile.fileStream.Name);
        }
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "mu_adv_selchrb_tbl (*.dat)|*.dat",
            RestoreDirectory = true
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            fileNameToolStripMenuItem.Text = "File: " + saveFileDialog.FileName;
            selectCharacterFile.Save(saveFileDialog.FileName);
            temp = false;
        }
    }

    private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "https://github.com/KiwifruitDev/BrawlSubspaceCharEditor",
            UseShellExecute = true
        };
        Process.Start(psi);
    }

    private void addCharacterToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Add character after selected character
        // Get current tab (table index)
        int currentTableIndex = tabControl2.SelectedIndex;
        // Get current tab control (character index)
        TabControl currentTabControl = (TabControl)tabControl2.SelectedTab.Controls[0];
        int currentCharacterIndex = currentTabControl.SelectedIndex;
        // Get current character table
        CharacterTable currentCharacterTable = selectCharacterFile.CharacterTables[currentTableIndex];
        // Get current character
        CharacterSelection currentCharacter = currentCharacterTable.CharacterSelection[currentCharacterIndex];
        // Create new character
        CharacterSelection newCharacter = new CharacterSelection
        {
            CharacterID = currentCharacter.CharacterID,
            CursorPositionX = currentCharacter.CursorPositionX,
            CursorPositionY = currentCharacter.CursorPositionY,
            NamePositionX = currentCharacter.NamePositionX,
            NamePositionY = currentCharacter.NamePositionY
        };
        // Insert new character
        List<CharacterSelection> characterSelectionList = currentCharacterTable.CharacterSelection.ToList();
        characterSelectionList.Insert(currentCharacterIndex + 1, newCharacter);
        currentCharacterTable.CharacterSelection = characterSelectionList.ToArray();
        selectCharacterFile.CharacterTables[currentTableIndex] = currentCharacterTable;
        // Create new tab
        TabPage tabPage = new TabPage
        {
            Text = newCharacter.GetCharacterName()
        };
        currentTabControl.TabPages.Insert(currentCharacterIndex + 1, tabPage);
        Label label = new Label
        {
            Location = new Point(6, 3),
            Size = new Size(72, 15),
            Text = "Character ID"
        };
        tabPage.Controls.Add(label);
        ComboBox comboBox = new ComboBox
        {
            Name = "comboBox" + currentTableIndex + "_" + (currentCharacterIndex + 1),
            Location = new Point(6, 21),
            Size = new Size(129, 23)
        };
        comboBox.Items.AddRange(CharacterSelection.CharacterNames);
        comboBox.SelectedIndex = newCharacter.CharacterID;
        // Add action listener to update character ID
        comboBox.SelectedIndexChanged += (object sender2, EventArgs e2) =>
        {
            try
            {
                // Get indices from name (i, j)
                string[] indices = comboBox.Name.Replace("comboBox", "").Split(new char[] { '_' });
                int.TryParse(indices[0], out int i2);
                int.TryParse(indices[1], out int j2);
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CharacterID = (byte)comboBox.SelectedIndex;
                // Set tab name
                currentTabControl.TabPages[j2].Text = selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].GetCharacterName();
            }
            catch
            {
                // stale reference, show an error
                MessageBox.Show("Error: This character's index is a stale reference. Please save and reopen the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        tabPage.Controls.Add(comboBox);
        PictureBox pictureBox = new PictureBox
        {
            Location = new Point(141, 21),
            Size = new Size(249, 140)
        };
        tabPage.Controls.Add(pictureBox);
        Label label2 = new Label
        {
            Location = new Point(6, 47),
            Size = new Size(88, 15),
            Text = "Cursor Position"
        };
        tabPage.Controls.Add(label2);
        Label label3 = new Label
        {
            Location = new Point(6, 68),
            Size = new Size(14, 15),
            Text = "X"
        };
        tabPage.Controls.Add(label3);
        TextBox textBox = new TextBox
        {
            Name = "cursorPositionXtextBox" + currentTableIndex + "_" + (currentCharacterIndex + 1),
            Location = new Point(26, 65),
            Size = new Size(42, 23),
            Text = newCharacter.CursorPositionX.ToString()
        };
        // Add action listener to update cursor position
        textBox.TextChanged += (object sender2, EventArgs e2) =>
        {
            float.TryParse(textBox.Text, out float result);
            // Get indices from name (i, j)
            string[] indices = textBox.Name.Replace("cursorPositionXtextBox", "").Split(new char[] { '_' });
            int.TryParse(indices[0], out int i2);
            int.TryParse(indices[1], out int j2);
            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX = result;
            drawMarkers(
                pictureBox,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
            );
        };
        tabPage.Controls.Add(textBox);
        Label label4 = new Label
        {
            Location = new Point(74, 68),
            Size = new Size(14, 15),
            Text = "Y"
        };
        tabPage.Controls.Add(label4);
        TextBox textBox2 = new TextBox
        {
            Name = "cursorPositionYtextBox" + currentTableIndex + "_" + (currentCharacterIndex + 1),
            Location = new Point(94, 65),
            Size = new Size(41, 23),
            Text = newCharacter.CursorPositionY.ToString()
        };
        // Add action listener to update cursor position
        textBox2.TextChanged += (object sender2, EventArgs e2) =>
        {
            float.TryParse(textBox2.Text, out float result);
            // Get indices from name (i, j)
            string[] indices = textBox2.Name.Replace("cursorPositionYtextBox", "").Split(new char[] { '_' });
            int.TryParse(indices[0], out int i2);
            int.TryParse(indices[1], out int j2);
            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY = result;
            drawMarkers(
                pictureBox,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
            );
        };
        tabPage.Controls.Add(textBox2);
        Label label5 = new Label
        {
            Location = new Point(6, 91),
            Size = new Size(85, 15),
            Text = "Name Position"
        };
        tabPage.Controls.Add(label5);
        Label label6 = new Label
        {
            Location = new Point(6, 112),
            Size = new Size(14, 15),
            Text = "X"
        };
        tabPage.Controls.Add(label6);
        TextBox textBox3 = new TextBox
        {
            Name = "namePositionXtextBox" + currentTableIndex + "_" + (currentCharacterIndex + 1),
            Location = new Point(26, 109),
            Size = new Size(42, 23),
            Text = newCharacter.NamePositionX.ToString()
        };
        // Add action listener to update name position
        textBox3.TextChanged += (object sender2, EventArgs e2) =>
        {
            float.TryParse(textBox3.Text, out float result);
            // Get indices from name (i, j)
            string[] indices = textBox3.Name.Replace("namePositionXtextBox", "").Split(new char[] { '_' });
            int.TryParse(indices[0], out int i2);
            int.TryParse(indices[1], out int j2);
            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX = result;
            drawMarkers(
                pictureBox,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
            );
        };
        tabPage.Controls.Add(textBox3);
        Label label7 = new Label
        {
            Location = new Point(74, 112),
            Size = new Size(14, 15),
            Text = "Y"
        };
        tabPage.Controls.Add(label7);
        TextBox textBox4 = new TextBox
        {
            Name = "namePositionYtextBox" + currentTableIndex + "_" + (currentCharacterIndex + 1),
            Location = new Point(94, 109),
            Size = new Size(41, 23),
            Text = newCharacter.NamePositionY.ToString()
        };
        // Add action listener to update name position
        textBox4.TextChanged += (object sender2, EventArgs e2) =>
        {
            float.TryParse(textBox4.Text, out float result);
            // Get indices from name (i, j)
            string[] indices = textBox4.Name.Replace("namePositionYtextBox", "").Split(new char[] { '_' });
            int.TryParse(indices[0], out int i2);
            int.TryParse(indices[1], out int j2);
            selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY = result;
            drawMarkers(
                pictureBox,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].CursorPositionY,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionX,
                selectCharacterFile.CharacterTables[i2].CharacterSelection[j2].NamePositionY
            );
        };
        tabPage.Controls.Add(textBox4);
        Label label9 = new Label
        {
            Location = new Point(141, 3),
            Size = new Size(76, 15),
            Text = "Movie Frame"
        };
        tabPage.Controls.Add(label9);
        Button button = new Button
        {
            Name = "button" + currentTableIndex + "_" + (currentCharacterIndex + 1),
            Location = new Point(6, 138),
            Size = new Size(129, 23),
            Text = "Set Movie Frame"
        };
        tabPage.Controls.Add(button);
        // Add action listener to set movie frame
        button.Click += (object sender2, EventArgs e2) =>
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                Filter = "PNG (*.png)|*.png",
                RestoreDirectory = true
            };
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog2.FileName);
                // Get indices from name (i, j)
                string[] indices = button.Name.Replace("button", "").Split(new char[] { '_' });
                int.TryParse(indices[0], out int i2);
                // Get tab control from parent tab page
                TabControl tabControlP = (TabControl)button.Parent.Parent;
                for (int k2 = 0; k2 < tabControlP.TabPages.Count; k2++)
                {
                    // Set image for each character
                    drawMarkers(
                        (PictureBox)tabControlP.TabPages[k2].Controls[2],
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].CursorPositionX,
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].CursorPositionY,
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].NamePositionX,
                        selectCharacterFile.CharacterTables[i2].CharacterSelection[k2].NamePositionY
                    );
                }
            }
        };
        // Update all table offsets after this one by 20
        for (int i = currentTableIndex + 1; i < selectCharacterFile.CharacterTables.Length; i++)
        {
            selectCharacterFile.CharacterTables[i].Offset += 20;
        }
        // Update file size by 20
        selectCharacterFile.Header.FileSize += 20;
        // File stream: Offset all bytes by 20
        // This ensures the footer is updated correctly
        byte[] bytes = new byte[selectCharacterFile.fileStream.Length];
        selectCharacterFile.fileStream.Position = 0;
        selectCharacterFile.fileStream.Read(bytes, 0, bytes.Length);
        selectCharacterFile.fileStream.Close();
        // Delete temporary file
        File.Delete("temp.dat");
        // Make temporary file stream
        FileStream fileStreamTemp = new FileStream("temp.dat", FileMode.Create)
        {
            Position = 20
        };
        fileStreamTemp.Write(bytes, 0, bytes.Length);
        selectCharacterFile.fileStream = fileStreamTemp;
        // Set file name
        fileNameToolStripMenuItem.Text = "Temporary File";
        temp = true;
    }

    private void removeCharacterToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Don't allow removing the last character
        if (selectCharacterFile.CharacterTables[tabControl2.SelectedIndex].CharacterSelection.Length == 1)
            return;
        // Get current tab (table index)
        int currentTableIndex = tabControl2.SelectedIndex;
        // Get current tab control (character index)
        TabControl currentTabControl = (TabControl)tabControl2.SelectedTab.Controls[0];
        int currentCharacterIndex = currentTabControl.SelectedIndex;
        // Get current character table
        CharacterTable currentCharacterTable = selectCharacterFile.CharacterTables[currentTableIndex];
        // Remove character
        List<CharacterSelection> characterSelectionList = currentCharacterTable.CharacterSelection.ToList();
        characterSelectionList.RemoveAt(currentCharacterIndex);
        currentCharacterTable.CharacterSelection = characterSelectionList.ToArray();
        selectCharacterFile.CharacterTables[currentTableIndex] = currentCharacterTable;
        // Remove tab
        currentTabControl.TabPages.RemoveAt(currentCharacterIndex);
        // Update all table offsets after this one by -20
        for (int i = currentTableIndex + 1; i < selectCharacterFile.CharacterTables.Length; i++)
        {
            selectCharacterFile.CharacterTables[i].Offset -= 20;
        }
        // Update file size by -20
        selectCharacterFile.Header.FileSize -= 20;
        // File stream: Offset all bytes by -20
        // This ensures the footer is updated correctly
        byte[] bytes = new byte[selectCharacterFile.fileStream.Length - 20];
        selectCharacterFile.fileStream.Position = 20;
        selectCharacterFile.fileStream.Read(bytes, 0, bytes.Length);
        selectCharacterFile.fileStream.Close();
        // Delete temporary file
        File.Delete("temp.dat");
        // Make temporary file stream
        FileStream fileStreamTemp = new FileStream("temp.dat", FileMode.Create)
        {
            Position = 0
        };
        fileStreamTemp.Write(bytes, 0, bytes.Length);
        selectCharacterFile.fileStream = fileStreamTemp;
        // Set file name
        fileNameToolStripMenuItem.Text = "Temporary File";
        temp = true;
    }
}
