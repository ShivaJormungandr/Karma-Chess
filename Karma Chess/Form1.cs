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

            for (int i = 0; i < board.Squares.Length; i++)
            {
                tbTestBoard.Text += $"{board.Squares[i]} | ";
                if ((i + 1) % 8 == 0) tbTestBoard.Text += "\n-----------------------------------------------\n";
            }
        }
    }
}