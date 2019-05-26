using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int _colorNumber = 5;
	    readonly Color[] _colorTab = { Color.Blue, Color.Green, Color.Crimson, Color.Cyan, Color.Gray, Color.Indigo, Color.Navy, Color.HotPink, Color.Red, Color.Yellow };
        int _height = 5;
        int _width = 5;
	    int _score = 0;
        private readonly List<int[]> _indexes;
        private readonly List<string> _scoreList;
        bool[,] _visited;

        public Form1()
        {
            InitializeComponent();
            _indexes = new List<int[]>();
            _scoreList = new List<string>();
            listView.Visible = false;
            TLP.Width = this.Width;
            StartGame();
        }

        private void StartGame()
        {
            if (_score != 0)
            {
                _scoreList.Add(_score + "/" + TLP.RowCount * TLP.ColumnCount);
                _score = 0;
            }

            TLP.Controls.Clear();
            TLP.ColumnStyles.Clear();
            TLP.RowStyles.Clear();
            TLP.AutoSize = true;
            TLP.ColumnCount = _width;
            TLP.RowCount = _height;
            Random rnd = new Random();
            _visited = new bool[_width, _height];

            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                    _visited[i, j] = false;

            for (int i = 0; i < TLP.RowCount; ++i)
            {
                TLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / _height));
                TLP.RowStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / _width));

                for (int j = 0; j < TLP.ColumnCount; ++j)
                {
                    var pctrCard = new PictureBox
                    {
                        Tag = new Point(j, i),
                        BackColor = _colorTab[rnd.Next() % _colorNumber],
                        Margin = new Padding(0),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        ClientSize = new Size(TLP.Size.Width / _width, TLP.Size.Height / _height)
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
                _score++;
                listView.Items.Clear();

                ListViewItem lvi1 = new ListViewItem("Current: " + _score);
                listView.Items.Add(lvi1);

                for (int i = _scoreList.Count - 1; i >= 0; i--)
                {
                    ListViewItem lvi = new ListViewItem(_scoreList[i]);
                    listView.Items.Add(lvi);
                }

                Game_Over();
            }
            else
            {
                Control con = TLP.GetControlFromPosition(p.X, p.Y);

                if (con.BackColor != SystemColors.Control)
                {
                    Check(p.X, p.Y, c.BackColor);
                    if (_indexes.Count > 1)
                    {
                        for (int i = 0; i < _indexes.Count; i++)
                        {
                            int[] tab = _indexes[i];
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
                        if (_indexes.Count > 1)
                        {
                            _score = _score + _indexes.Count;

                            listView.Items.Clear();

                            ListViewItem lvi1 = new ListViewItem("Current: " + _score);
                            listView.Items.Add(lvi1);

                            for (int i = _scoreList.Count - 1; i >= 0; i--)
                            {
                                ListViewItem lvi = new ListViewItem(_scoreList[i]);
                                listView.Items.Add(lvi);
                            }

                        }
                    }
                    _indexes.Clear();
                    for (int i = 0; i < _width; i++)
                        for (int j = 0; j < _height; j++)
                            _visited[i, j] = false;
                }

                Game_Over();
            }
        }

        private void Check(int x, int y, Color c)
        {
            Control con2 = TLP.GetControlFromPosition(x, y);
            if (c == con2.BackColor)
            {
                _visited[x, y] = true;
                int[] tab = new int[2];
                tab[0] = x;
                tab[1] = y;
                _indexes.Add(tab);
                if (x > 0 && _visited[x - 1, y] == false)
                    Check(x - 1, y, c);
                if (y > 0 && _visited[x, y - 1] == false)
                    Check(x, y - 1, c);
                if (x < TLP.ColumnCount - 1 && _visited[x + 1, y] == false)
                    Check(x + 1, y, c);
                if (y < TLP.RowCount - 1 && _visited[x, y + 1] == false)
                    Check(x, y + 1, c);
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
            _width = 5;
            _height = 5;
            StartGame();
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            _colorNumber = trackBar.Value;
            StartGame();
        }

        private void x10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _width = 10;
            _height = 10;
            StartGame();
        }

        private void x10ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _width = 5;
            _height = 10;
            StartGame();
        }

        private void x5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _width = 10;
            _height = 5;
            StartGame();
        }

        private void radioButtonH_CheckedChanged(object sender, EventArgs e)
        {
            listView.Visible = true;
            TLP.Width = Width - listView.Width;
        }

        private void radioButtonC_CheckedChanged(object sender, EventArgs e)
        {
            listView.Visible = false;
            TLP.Width = Width;
        }
    }
}
