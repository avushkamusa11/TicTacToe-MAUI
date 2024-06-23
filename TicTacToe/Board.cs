using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TicTacToe
{
    public class Board : AbsoluteLayout
    {
        private const int COLS = 4;
        private const int ROWS = 4;
        private Tile[,] tiles = new Tile[COLS, ROWS];
        private bool isGameFinished;

        public int XWinnings { get; set; }
        public int OWinnings { get; set; }
        public Player currentPlayer { get; set; }

        public event EventHandler GameStarted;
        public event EventHandler<bool> GameFinished;

        public Board()
        {
            currentPlayer = Player.O;
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    Tile tile = new Tile(row, col);
                    tile.StatusChanged += Tile_StatusChanged;
                    tiles[row, col] = tile;
                    Children.Add(tile);
                }
            }

            SizeChanged += (sender, args) =>
            {
                double width = Width / COLS;
                double height = Height / ROWS;

                foreach (Tile tile in tiles)
                {
                    Rect bounds = new Rect(tile.Col * width, tile.Row * height, width, height);
                    AbsoluteLayout.SetLayoutBounds(tile, bounds);
                }
            };

            StartNewGame();
        }

        public void StartNewGame()
        {
            foreach (Tile tile in tiles)
            {
                tile.Initialize();
            }
            isGameFinished = false;
            currentPlayer = Player.O; 
        }

        private void Tile_StatusChanged(object sender, TileStatus e)
        {
            if (isGameFinished)
            {
                return;
            }

            Tile clickedTile = (Tile)sender;

            if (clickedTile.Player == Player.None)
            {
                clickedTile.Player = currentPlayer;
                clickedTile.Status = TileStatus.Open;
                bool gameHasBeenWon = IsGameWon(currentPlayer);
                
                if (gameHasBeenWon)
                {
                    isGameFinished = true;
                    if(currentPlayer == Player.O)
                    {
                        OWinnings += 1;
                    }else
                    {
                        XWinnings += 1;
                    }
                    GameFinished?.Invoke(this, true);

                }
                else if (IsGameFinishedWithoutWinner())
                {
                    GameFinished?.Invoke(this, false);
                }
                else
                {
                    currentPlayer = currentPlayer == Player.O ? Player.X : Player.O; 
                }
            }
        }

        private bool IsGameWon(Player player)
        {
            for (int i = 0; i < 4; i++)
            {
                if (CheckLine(player, i, 0, 0, 1) || CheckLine(player, 0, i, 1, 0))
                {
                    return true;
                }
            }
            return CheckLine(player, 0, 0, 1, 1) || CheckLine(player, 0, 3, 1, -1);
        }

        private bool CheckLine(Player player, int startRow, int startCol, int rowStep, int colStep)
        {
            for (int i = 0; i < 4; i++)
            {
                if (tiles[startRow + i * rowStep, startCol + i * colStep].Player != player)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsGameFinishedWithoutWinner()
        {
            bool result = true;
            foreach (Tile tile in tiles)
            {
                if(tile.Status == TileStatus.Closed)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}