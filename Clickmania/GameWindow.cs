using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Clickmania
{
    public partial class GameWindow : Form
    {
	    private int _colorNumber = 5;
	    private readonly Color[] _colorTab = { Color.Blue, Color.Green, Color.Crimson, Color.Cyan, Color.Gray, Color.Indigo, Color.Navy, Color.HotPink, Color.Red, Color.Yellow };
	    private int _height = 5;
	    private int _width = 5;
	    private int _score;
        private readonly List<int[]> _indexes;
        private readonly List<string> _scoreList;
        private bool[,] _visited;

        public GameWindow()
        {
            InitializeComponent();
            _indexes = new List<int[]>();
            _scoreList = new List<string>();
            HighScoreList.Visible = false;
            GameBoard.Width = Width;
            StartGame();
        }

        private void StartGame()
        {
            if (_score != 0)
            {
                _scoreList.Add(_score + "/" + GameBoard.RowCount * GameBoard.ColumnCount);
                _score = 0;
            }

            GameBoard.Controls.Clear();
            GameBoard.ColumnStyles.Clear();
            GameBoard.RowStyles.Clear();
            GameBoard.AutoSize = true;
            GameBoard.ColumnCount = _width;
            GameBoard.RowCount = _height;
            Random rnd = new Random();
            _visited = new bool[_width, _height];

            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                    _visited[i, j] = false;

            for (int i = 0; i < GameBoard.RowCount; ++i)
            {
                GameBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / _height));
                GameBoard.RowStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / _width));

                for (int j = 0; j < GameBoard.ColumnCount; ++j)
                {
                    var pctrCard = new PictureBox
                    {
                        Tag = new Point(j, i),
                        BackColor = _colorTab[rnd.Next() % _colorNumber],
                        Margin = new Padding(0),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        ClientSize = new Size(GameBoard.Size.Width / _width, GameBoard.Size.Height / _height)
                    };
                    pctrCard.Click += pctrCard_Click;

                    GameBoard.Controls.Add(pctrCard);
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
                    Control con1 = GameBoard.GetControlFromPosition(p.X, i);
                    Control con2 = GameBoard.GetControlFromPosition(p.X, i - 1);
                    con1.BackColor = con2.BackColor;
                }

                Control con = GameBoard.GetControlFromPosition(p.X, 0);
                con.BackColor = SystemColors.Control;
                _score++;
                HighScoreList.Items.Clear();

                ListViewItem lvi1 = new ListViewItem("Current: " + _score);
                HighScoreList.Items.Add(lvi1);

                for (int i = _scoreList.Count - 1; i >= 0; i--)
                {
                    ListViewItem lvi = new ListViewItem(_scoreList[i]);
                    HighScoreList.Items.Add(lvi);
                }

                Game_Over();
            }
            else
            {
                Control con = GameBoard.GetControlFromPosition(p.X, p.Y);

                if (con.BackColor != SystemColors.Control)
                {
                    Check(p.X, p.Y, c.BackColor);
                    if (_indexes.Count > 1)
                    {
                        for (int i = 0; i < _indexes.Count; i++)
                        {
                            int[] tab = _indexes[i];
                            GameBoard.GetControlFromPosition(tab[0], tab[1]).BackColor = SystemColors.Control;
                        }
                        for (int i = GameBoard.RowCount - 1; i > 0; i--)
                        {
                            for (int j = 0; j < GameBoard.ColumnCount; j++)
                                if (GameBoard.GetControlFromPosition(j, i).BackColor == SystemColors.Control)
                                {
                                    bool exists = false;
                                    for (int k = i - 1; k >= 0; k--)
                                        if (GameBoard.GetControlFromPosition(j, k).BackColor != SystemColors.Control)
                                            exists = true;

                                    if (exists)
                                        while (GameBoard.GetControlFromPosition(j, i).BackColor == SystemColors.Control)
                                        {
                                            for (int k = i - 1; k >= 0; k--)
                                                GameBoard.GetControlFromPosition(j, k + 1).BackColor = GameBoard.GetControlFromPosition(j, k).BackColor;
                                            GameBoard.GetControlFromPosition(j, 0).BackColor = SystemColors.Control;
                                        }
                                }
                        }
                        if (_indexes.Count > 1)
                        {
                            _score = _score + _indexes.Count;

                            HighScoreList.Items.Clear();

                            ListViewItem lvi1 = new ListViewItem("Current: " + _score);
                            HighScoreList.Items.Add(lvi1);

                            for (int i = _scoreList.Count - 1; i >= 0; i--)
                            {
                                ListViewItem lvi = new ListViewItem(_scoreList[i]);
                                HighScoreList.Items.Add(lvi);
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
            Control con2 = GameBoard.GetControlFromPosition(x, y);
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
                if (x < GameBoard.ColumnCount - 1 && _visited[x + 1, y] == false)
                    Check(x + 1, y, c);
                if (y < GameBoard.RowCount - 1 && _visited[x, y + 1] == false)
                    Check(x, y + 1, c);
            }
        }

        private void Game_Over()
        {
            foreach (Control c in GameBoard.Controls)
            {
                if (c.BackColor != SystemColors.Control)
                    return;
            }
            MessageBox.Show("Congratulations! You won!");
        }

		private void ChooseClassVersion(object sender, EventArgs e)
		{
            HighScoreList.Visible = false;
            GameBoard.Width = Width;
		}

		private void ChooseHomeVersion(object sender, EventArgs e)
		{
			HighScoreList.Visible = true;
			GameBoard.Width = Width - HighScoreList.Width;
		}

		private void ChangeColorNumber(object sender, EventArgs e)
		{
			_colorNumber = trackBar.Value;
			StartGame();
		}

		private void Start5X5Game(object sender, EventArgs e)
		{
            _width = 5;
            _height = 5;
            StartGame();
		}

		private void Start10X10Game(object sender, EventArgs e)
		{
            _width = 10;
            _height = 10;
            StartGame();
		}

		private void Start5X10Game(object sender, EventArgs e)
		{
            _width = 5;
            _height = 10;
            StartGame();
		}

		private void Start10X5Game(object sender, EventArgs e)
		{
			_width = 10;
			_height = 5;
			StartGame();
		}
	}
}
