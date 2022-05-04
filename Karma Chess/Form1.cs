namespace Karma_Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Board board = new Board(this);
            //board.InitStartBoard();
            //board.FenToBoard("rnbqkbnr/pppppppp/8/8/3P4/8/PPP1PPPP/RNBQKBNR w KQkq d3 5 1");
            board.FenToBoard("r5k1/5ppp/1p5q/p1p5/8/1PP2PP1/1P1r4/R1Q2R1K w - - 1 27");
            this.drawBoard(board);
        }
    }
}