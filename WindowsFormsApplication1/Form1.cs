using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int[,] _board;
        int colourNumber = 5;
        Color[] colourTab = { Color.Blue, Color.Green, Color.Crimson, Color.Cyan, Color.Gray, Color.Indigo, Color.Navy, Color.HotPink, Color.Red, Color.Yellow };
        int height = 5;
        int width = 5;
        private Random _random = new Random();
        int score = 0;
        private List<int[]> indexes = new List<int[]>();
        private List<string> scoreList;
        bool[,] visited;

        public Form1()
        {
            InitializeComponent();
            _board = new int[5, 5];
            indexes = new List<int[]>();
            scoreList = new List<string>();
            listView.Visible = false;
            TLP.Width = this.Width;
            StartGame();
        }

        private void StartGame()
        {
            if (score != 0)
            {
                scoreList.Add(score + "/" + TLP.RowCount * TLP.ColumnCount);
                score = 0;
            }

            TLP.Controls.Clear();
            TLP.ColumnStyles.Clear();
            TLP.RowStyles.Clear();
            TLP.AutoSize = true;
            TLP.ColumnCount = width;
            TLP.RowCount = height;
            Random rnd = new Random();
            visited = new bool[width, height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    visited[i, j] = false;

            for (int i = 0; i < TLP.RowCount; ++i)
            {
                TLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / height));
                TLP.RowStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / width));

                for (int j = 0; j < TLP.ColumnCount; ++j)
                {
                    var pctrCard = new PictureBox
                    {
                        Tag = new Point(j, i),
                        BackColor = colourTab[rnd.Next() % colourNumber],
                        Margin = new Padding(0),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        ClientSize = new Size(TLP.Size.Width / width, TLP.Size.Height / height)
                    };
                    pctrCard.Click += pctrCard_Click;

                    TLP.Controls.Add(pctrCard);
                }
            }
        }

        private void pctrCard_Click(Object sender, EventArgs e)
        {
            Control c = sender as Control;
            Point p = (Point)c.Tag;

            if (radioButtonC.Checked && c.BackColor != SystemColors.Control)
            {
                for (int i = p.Y; i > 0; i--)
                {
                    Control con1 = TLP.GetControlFromPosition(p.X, i);
                    Control con2 = TLP.GetControlFromPosition(p.X, i - 1);
                    con1.BackColor = con2.BackColor;
                }

                Control con = TLP.GetControlFromPosition(p.X, 0);
                con.BackColor = SystemColors.Control;
                score++;
                listView.Items.Clear();

                ListViewItem lvi1 = new ListViewItem("Current: " + score);
                listView.Items.Add(lvi1);

                for (int i = scoreList.Count - 1; i >= 0; i--)
                {
                    ListViewItem lvi = new ListViewItem(scoreList[i]);
                    listView.Items.Add(lvi);
                }

                Game_Over();
            }
            else
            {
                Control con = TLP.GetControlFromPosition(p.X, p.Y);

                if (con.BackColor != SystemColors.Control)
                {
                    check(p.X, p.Y, c.BackColor);
                    if (indexes.Count > 1)
                    {
                        for (int i = 0; i < indexes.Count; i++)
                        {
                            int[] tab = indexes[i];
                            TLP.GetControlFromPosition(tab[0], tab[1]).BackColor = SystemColors.Control;
                        }
                        for (int i = TLP.RowCount - 1; i > 0; i--)
                        {
                            for (int j = 0; j < TLP.ColumnCount; j++)
                                if (TLP.GetControlFromPosition(j, i).BackColor == SystemColors.Control)
                                {
                                    bool exists = false;
                                    for (int k = i - 1; k >= 0; k--)
                                        if (TLP.GetControlFromPosition(j, k).BackColor != SystemColors.Control)
                                            exists = true;

                                    if (exists)
                                        while (TLP.GetControlFromPosition(j, i).BackColor == SystemColors.Control)
                                        {
                                            for (int k = i - 1; k >= 0; k--)
                                                TLP.GetControlFromPosition(j, k + 1).BackColor = TLP.GetControlFromPosition(j, k).BackColor;
                                            TLP.GetControlFromPosition(j, 0).BackColor = SystemColors.Control;
                                        }
                                }
                        }
                        if (indexes.Count > 1)
                        {
                            score = score + indexes.Count;

                            listView.Items.Clear();

                            ListViewItem lvi1 = new ListViewItem("Current: " + score);
                            listView.Items.Add(lvi1);

                            for (int i = scoreList.Count - 1; i >= 0; i--)
                            {
                                ListViewItem lvi = new ListViewItem(scoreList[i]);
                                listView.Items.Add(lvi);
                            }

                        }
                    }
                    indexes.Clear();
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                            visited[i, j] = false;
                }

                Game_Over();
            }
        }

        private void check(int x, int y, Color c)
        {
            Control con2 = TLP.GetControlFromPosition(x, y);
            if (c == con2.BackColor)
            {
                visited[x, y] = true;
                int[] tab = new int[2];
                tab[0] = x;
                tab[1] = y;
                indexes.Add(tab);
                if (x > 0 && visited[x - 1, y] == false)
                    check(x - 1, y, c);
                if (y > 0 && visited[x, y - 1] == false)
                    check(x, y - 1, c);
                if (x < TLP.ColumnCount - 1 && visited[x + 1, y] == false)
                    check(x + 1, y, c);
                if (y < TLP.RowCount - 1 && visited[x, y + 1] == false)
                    check(x, y + 1, c);
            }
        }

        private void Game_Over()
        {
            foreach (Control c in TLP.Controls)
            {
                if (c.BackColor != SystemColors.Control)
                    return;
            }
            MessageBox.Show("Congratulations! You won!");
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _board = new int[5, 5];
            width = 5;
            height = 5;
            StartGame();
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            colourNumber = trackBar.Value;
            StartGame();
        }

        private void x10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _board = new int[10, 10];
            width = 10;
            height = 10;
            StartGame();
        }

        private void x10ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _board = new int[5, 10];
            width = 5;
            height = 10;
            StartGame();
        }

        private void x5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _board = new int[10, 5];
            width = 10;
            height = 5;
            StartGame();
        }

        private void radioButtonH_CheckedChanged(object sender, EventArgs e)
        {
            listView.Visible = true;
            TLP.Width = this.Width - listView.Width;
        }

        private void radioButtonC_CheckedChanged(object sender, EventArgs e)
        {
            listView.Visible = false;
            TLP.Width = this.Width;
        }
    }
}
