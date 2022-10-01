using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper.Core
{
    public enum CellType
    {
        Regular,
        Mine,
        Flagged,
        FlaggedMine
    }

    public enum CellState
    {
        Opened,
        Closed
    }

    public class Cell : Button
    {
        public int XLoc { get; set; }
        public int YLoc { get; set; }
        public int CellSize { get; set; }
        public CellState CellState { get; set; }
        public CellType CellType { get; set; }
        public int NumMines { get; set; }
        public Board Board { get; set; }
        private int _adjacentMines;

        public void SetupDesign()
        {
            BackColor = SystemColors.ButtonFace;
            Location = new Point(XLoc * CellSize, YLoc * CellSize);
            Size = new Size(CellSize, CellSize);
            UseVisualStyleBackColor = true;
            Font = new Font("Verdana", 15.75F, FontStyle.Bold);
        }

        public void OnFlag()
        {
            if ((CellType == CellType.Mine || CellType == CellType.Regular) && Board.FlagsLeft > 0)
            {
                Image = new Bitmap(
                    Image.FromFile(
                        @"resources/flag.png"),
                    new Size(50, 50));

                Board.FlagsLeft--;

                CellType = CellType == CellType.Mine ? CellType.FlaggedMine : CellType.Flagged;
            }
            else if (CellType == CellType.FlaggedMine || CellType == CellType.Flagged)
            {
                Image = null;
                Board.FlagsLeft++;

                CellType = CellType == CellType.FlaggedMine ? CellType.Mine : CellType.Regular;
            }
        }

        public void OnClick()
        {
            if (IsAllCellsClosed())
            {
                MineInit(XLoc, YLoc);
            }

            OpenCell();

            if (IsAllMinesCleared())
            {
                GameOver(true);
            }
        }

        private void OpenCell()
        {
            switch (CellType)
            {
                case CellType.Flagged:
                case CellType.FlaggedMine:
                    return;
                case CellType.Regular when CellState != CellState.Opened:
                {
                    Text = _adjacentMines == 0 ? "" : _adjacentMines.ToString();
                    ForeColor = _adjacentMines == 0 ? Color.WhiteSmoke : GetCellColour();
                    BackColor = Color.WhiteSmoke;
                    CellState = CellState.Opened;
                    if (_adjacentMines == 0)
                    {
                        OpenAdjacentCells();
                    }

                    break;
                }
            }

            if (CellType != CellType.Mine) return;

            Image = new Bitmap(Image.FromFile(@"resources/mine.png"), new Size(50, 50));

            GameOver();
        }

        private void GameOver(bool win = false)
        {
            for (int row = 0; row < Board.Cells.GetLength(0); row++)
            {
                for (int col = 0; col < Board.Cells.GetLength(1); col++)
                {
                    var cell = Board.Cells[row, col];

                    if (cell.CellType == CellType.Mine && win)
                    {
                        cell.Image = new Bitmap(Image.FromFile(@"resources/mine.png"), new Size(50, 50));
                    }

                    else if (cell.CellType == CellType.Mine && !win)
                    {
                        cell.Image = new Bitmap(Image.FromFile(@"resources/explode.png"), new Size(50, 50));
                    }

                    else if (cell.CellType == CellType.FlaggedMine)
                    {
                        cell.Image = new Bitmap(Image.FromFile(@"resources/mine.png"), new Size(50, 50));
                    }
                    else
                    {
                        cell.Text = cell._adjacentMines == 0 ? "" : cell._adjacentMines.ToString();
                        cell.ForeColor = cell.GetCellColour();
                        cell.BackColor = Color.WhiteSmoke;
                    }
                }
            }
        }

        private void OpenAdjacentCells()
        {
            if (XLoc > 0)
            {
                Board.Cells[XLoc - 1, YLoc].OpenCell();

                if (YLoc > 0)
                {
                    Board.Cells[XLoc - 1, YLoc - 1].OpenCell();
                }

                if (YLoc < Board.Cells.GetLength(1) - 1)
                {
                    Board.Cells[XLoc - 1, YLoc + 1].OpenCell();
                }
            }

            if (XLoc < Board.Cells.GetLength(0) - 1)
            {
                Board.Cells[XLoc + 1, YLoc].OpenCell();
                if (YLoc > 0)
                {
                    Board.Cells[XLoc + 1, YLoc - 1].OpenCell();
                }

                if (YLoc < Board.Cells.GetLength(1) - 1)
                {
                    Board.Cells[XLoc + 1, YLoc + 1].OpenCell();
                }
            }

            if (YLoc > 0)
            {
                Board.Cells[XLoc, YLoc - 1].OpenCell();
            }

            if (YLoc < Board.Cells.GetLength(1) - 1)
            {
                Board.Cells[XLoc, YLoc + 1].OpenCell();
            }
        }

        private bool IsAllCellsClosed()
        {
            for (int i = 0; i < Board.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Cells.GetLength(1); j++)
                {
                    if (Board.Cells[i, j].CellState == CellState.Opened)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsAllMinesCleared()
        {
            for (int i = 0; i < Board.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Cells.GetLength(1); j++)
                {
                    if (Board.Cells[i, j].CellState == CellState.Closed &&
                        Board.Cells[i, j].CellType == CellType.Regular)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void MineInit(int xLoc, int yLoc)
        {
            Random rng = new Random();

            int mines = 0;
            while (mines < Board.NumMines)
            {
                int x = rng.Next(0, Board.Width);
                int y = rng.Next(0, Board.Height);

                if (Board.Cells[x, y].CellType != CellType.Mine && x != xLoc && y != yLoc)
                {
                    mines++;
                    Board.Cells[x, y].CellType = CellType.Mine;
                }
            }

            for (int i = 0; i < Board.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Cells.GetLength(1); j++)
                {
                    int minesAdj = 0;
                    if (i > 0)
                    {
                        minesAdj += CheckCellForMine(i - 1, j);
                        if (j > 0)
                        {
                            minesAdj += CheckCellForMine(i - 1, j - 1);
                        }

                        if (j < Board.Cells.GetLength(1) - 1)
                        {
                            minesAdj += CheckCellForMine(i - 1, j + 1);
                        }
                    }

                    if (i < Board.Cells.GetLength(0) - 1)
                    {
                        minesAdj += CheckCellForMine(i + 1, j);
                        if (j > 0)
                        {
                            minesAdj += CheckCellForMine(i + 1, j - 1);
                        }

                        if (j < Board.Cells.GetLength(1) - 1)
                        {
                            minesAdj += CheckCellForMine(i + 1, j + 1);
                        }
                    }

                    if (j > 0)
                    {
                        minesAdj += CheckCellForMine(i, j - 1);
                    }

                    if (j < Board.Cells.GetLength(1) - 1)
                    {
                        minesAdj += CheckCellForMine(i, j + 1);
                    }

                    Board.Cells[i, j]._adjacentMines = minesAdj;
                }
            }
        }

        private int CheckCellForMine(int xLoc, int yLoc)
        {
            return Board.Cells[xLoc, yLoc].CellType == CellType.Mine ? 1 : 0;
        }

        /// <summary>
        /// Return the colour code associated with the number of surrounding mines
        /// </summary>
        /// <returns></returns>
        private Color GetCellColour()
        {
            switch (this._adjacentMines)
            {
                case 1:
                    return ColorTranslator.FromHtml("0x0000FE"); // 1
                case 2:
                    return ColorTranslator.FromHtml("0x186900"); // 2
                case 3:
                    return ColorTranslator.FromHtml("0xAE0107"); // 3
                case 4:
                    return ColorTranslator.FromHtml("0x000177"); // 4
                case 5:
                    return ColorTranslator.FromHtml("0x8D0107"); // 5
                case 6:
                    return ColorTranslator.FromHtml("0x007A7C"); // 6
                case 7:
                    return ColorTranslator.FromHtml("0x902E90"); // 7
                case 8:
                    return ColorTranslator.FromHtml("0x000000"); // 8
                default:
                    return ColorTranslator.FromHtml("0xffffff");
            }
        }
    }
}
