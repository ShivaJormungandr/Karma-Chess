namespace Karma_Chess
{
    public partial class Form1 : Form
    {
        Board board;
        public Form1()
        {
            InitializeComponent();
            ////check
            //dublu check
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board();
        }

        private void btPrintBoard_Click(object sender, EventArgs e)
        {
            board.FenToBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            tbTestBoard.Text += "-----------------------------------------\n";
            for (int i = 0; i < board.Squares.Length; i++)
            {
                if (i % 8 == 0) tbTestBoard.Text += "| ";
                tbTestBoard.Text += $"{board.Squares[i].ToString()[..2]} | ";
                if ((i + 1) % 8 == 0) tbTestBoard.Text += "\n-----------------------------------------\n";
            }
        }
    }
}