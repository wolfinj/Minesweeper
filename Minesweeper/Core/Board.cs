using System.Windows.Forms;

namespace Minesweeper.Core
{
    public class Board
    {
        private Minesweeper _minesweeper;
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumMines { get; set; }
        public int FlagsLeft { get; set; }
        public Cell[,] Cells { get; set; }

        public Board(Minesweeper minesweeper, int width, int height, int mines)
        {
            _minesweeper = minesweeper;
            Width = width;
            Height = height;
            NumMines = mines;
            Cells = new Cell[width, height];
            FlagsLeft = mines;
        }

        public void SetupBoard()
        {
            for (int row = 0; row < Width; row++)
            {
                for (int col = 0; col < Height; col++)
                {
                    var c = new Cell
                    {
                        XLoc = row,
                        YLoc = col,
                        CellState = CellState.Closed,
                        CellType = CellType.Regular,
                        CellSize = 50,
                        Board = this
                    };

                    c.SetupDesign();
                    c.MouseDown += Cell_MouseClick;
                    Cells[row, col] = c;

                    _minesweeper.Controls.Add(c);
                }
            }
        }

        private void Cell_MouseClick(object sender, MouseEventArgs e)
        {
            var cell = (Cell)sender;

            if (cell.CellState == CellState.Opened)
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    cell.OnClick();
                    break;

                case MouseButtons.Right:
                    cell.OnFlag();

                    break;

                default:
                    return;
            }
        }
    }
}
