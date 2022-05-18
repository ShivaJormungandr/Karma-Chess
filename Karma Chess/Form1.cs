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
            this.DrawBoard(board);
        }
    }
}