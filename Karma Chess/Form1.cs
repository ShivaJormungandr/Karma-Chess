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
            board = new Board(this);
            board.InitStartBoard();
            board.drawBoard();
            SetupClickEvents(Controls);
        }

        private void SetupClickEvents(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.Click += HandleClicks;
            }
        }
        private void HandleClicks(object sender, EventArgs e)
        {
            var control = (PictureBox)sender;
            board.ReceiveClickedPiece(control.Location.X, control.Location.Y);
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