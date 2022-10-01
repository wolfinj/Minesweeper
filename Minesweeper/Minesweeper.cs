using Minesweeper.Core;
using System;
using System.Diagnostics.Tracing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Minesweeper : Form
    {
        private Board _board;
        private EventHandler _restartGame;

        public Minesweeper(EventHandler restartGame = null)
        {
            InitializeComponent();
            if(restartGame != null)
                _restartGame += restartGame;
        }

        private void Start_button(object sender, EventArgs e)
        {
            _board = new Board(this, 9, 9, 10);
            
            _board.SetupBoard();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _board = new Board(this, 24, 9, 60);

            _board.SetupBoard();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            _restartGame?.Invoke(this, EventArgs.Empty);
        }
    }
}
