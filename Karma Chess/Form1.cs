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
            board.InitStartBoard();
            board.drawBoard();
        }

        //private void btPrintBoard_Click(object sender, EventArgs e)
        //{
        //    board.FenToBoard("rnbqkbnr/pppppppp/8/8/3P4/8/PPP1PPPP/RNBQKBNR w KQkq d3 5 1");

        //    tbTestBoard.Text += "-----------------------------------------\n";
        //    for (int i = 0; i < board.Squares.Length; i++)
        //    {
        //        if (i % 8 == 0) tbTestBoard.Text += "| ";
        //        tbTestBoard.Text += $"{board.Squares[i].ToString()[..2]} | ";
        //        if ((i + 1) % 8 == 0) tbTestBoard.Text += "\n-----------------------------------------\n";
        //    }
        //}
    }
}