namespace Karma_Chess
{
    public partial class Form1 : Form
    {
        Board board;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board();
        }

        private void btEmptyBoard_Click(object sender, EventArgs e)
        {
            this.EmptyBoard();
        }

        private void btStartGame_Click(object sender, EventArgs e)
        {
            var fen = tbFen.Text;
            if (fen.Equals(""))
            {
                board.InitStartBoard();
            }
            else
            {
                //"1nbqkbnr/pppppppp/8/8/3P4/8/PPP1PPPP/RNBQKBNR w KQkq d3 5 1"
                //"r5k1/5ppp/1p5q/p1p5/8/1PP2PP1/1P1r4/R1Q2R1K w - - 1 27"
                //"r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - "
                board.FenToBoard(fen);
            }
            board.CalculateLegalMoves();
            this.DrawBoard(board);
        }

        private void btBestMove_Click(object sender, EventArgs e)
        {
            try
            {
                btBestMove.Enabled = false;
                board.CalculateLegalMoves();
                if (board.CheckMate || board.LegalMoves.Count == 0)
                {
                    // Initializes the variables to pass to the MessageBox.Show method.
                    string message = "CkeckMate!";
                    string caption = "CkeckMate!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    return;
                }
                MakeBestMove();
            }
            finally
            {
                btBestMove.Enabled = true;
            }

        }

        private void btMove_Click(object sender, EventArgs e)
        {
            try
            {
                btMove.Enabled = false;
                if (board.CheckMate || board.LegalMoves.Count == 0)
                {
                    // Initializes the variables to pass to the MessageBox.Show method.
                    string message = "CkeckMate!";
                    string caption = "CkeckMate!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    return;
                }

                if (tbMove.Text == "")
                {
                    return;
                }

                var moveString = tbMove.Text.ToLower();

                (int file, int rank) from = board.AlgebircToBoardIndex(moveString[0..2]);
                (int file, int rank) to = board.AlgebircToBoardIndex(moveString[2..4]);
                var special = 0;
                switch (moveString[^1])
                {
                    case 'k':
                        special = 1;
                        break;
                    case 'b':
                        special = 2;
                        break;
                    case 'r':
                        special = 3;
                        break;
                    case 'q':
                        special = 4;
                        break;
                }

                var move = (from, to, special);
                board.CalculateLegalMoves();
                if (!board.LegalMoves.Contains(move))
                {
                    return;
                }

                board.Move(from, to, special);

                var pbfrom = GetPictureBox(from.file, from.rank);
                var pbto = GetPictureBox(to.file, to.rank);

                this.UpdateBoard(board, from, pbfrom, to, pbto);
                Refresh();

                if (cbai.Checked)
                {
                    MakeBestMove();
                }
            }
            finally
            {
                btMove.Enabled = true;
            }
        }
        private void MakeBestMove()
        {
            var mm = new MinMax();

            board.CalculateLegalMoves();

            var difficulty = tbDifficulty.Value;
            mm.MinMaxFunc(board, difficulty, int.MinValue, int.MaxValue, true, board.Turn);

            var bestMove = mm.bestMoveMinMix;
            var turnWas = board.Turn;
            var itMoved = board.Move(bestMove.from, bestMove.to, bestMove.Special);
            if (itMoved)
            {
                tbLog.Text = $"{turnWas} moved ({(char)(bestMove.from.file + 65)}, {bestMove.from.rank + 1}) to ({(char)(bestMove.to.file + 65)}, {bestMove.to.rank + 1})";
            }

            if (board.CheckMate)
            {
                //
                var testWin = 1;
            }


            var pbfrom = GetPictureBox(bestMove.from.file, bestMove.from.rank);
            var pbto = GetPictureBox(bestMove.to.file, bestMove.to.rank);

            this.UpdateBoard(board, bestMove.from, pbfrom, bestMove.to, pbto);
        }

        public PictureBox GetPictureBox(int file, int rank)
        {
            int[] positionsFile = { 6, 75, 144, 213, 282, 352, 420, 489 };
            int[] positionsRank = { 489, 420, 351, 282, 213, 144, 75, 6 };

            foreach (Control control in Controls)
            {
                if (control is PictureBox pictureBox && pictureBox.Location == new Point(positionsFile[file], positionsRank[rank]))
                {
                    return pictureBox;
                }
            }
            return null;
        }
    }
}