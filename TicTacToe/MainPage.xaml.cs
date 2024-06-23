namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {

        bool isGameInProgress;
        

        public MainPage()
        {
            InitializeComponent();
            board.GameStarted += Board_GameStarted;
            board.GameFinished += Board_GameFinished;
            InitializeGame();
        }

        private void Grid_SizeChanged(object sender, EventArgs e)
        {

        }

        private void startNewGame_Clicked(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            board.StartNewGame();
            startNewGame.IsVisible = false;
            isGameInProgress = false;
            
        }
        private void Board_GameFinished(object sender, bool e)
        {
            isGameInProgress = false;
            ShowStartGameButton();
            xWinningsCount.Text = "X wins: " + board.XWinnings;
            oWinningsCount.Text = "O wins: " + board.OWinnings;
        }
        private void ShowStartGameButton()
        {
            startNewGame.IsVisible = true;
        }

        private void Board_GameStarted(object sender, EventArgs e)
        {
            isGameInProgress = true;

            
        }
    }

}
