using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TicTacToe
{
    public class Tile : Border
    {
        private TileStatus status = TileStatus.Closed;
        private readonly Image xImage;
        private readonly Image oImage;

        public int Row { get; set; }
        public int Col { get; set; }
        private Player player = Player.None;

        public Tile(int row, int col)
        {
            Row = row;
            Col = col;

            BackgroundColor = Color.FromArgb("90EE90"); // light green
            Stroke = Colors.White;
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 5
            };

            oImage = new Image { Source = ImageSource.FromFile("o.png") };
            xImage = new Image { Source = ImageSource.FromFile("x.png") };

            TapGestureRecognizer singleTap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };

            singleTap.Tapped += SingleTap_Tapped;
            GestureRecognizers.Add(singleTap);
        }

        private void SingleTap_Tapped(object sender, TappedEventArgs e)
        {
            if (Status == TileStatus.Closed)
            {
                Status = TileStatus.Open;
                StatusChanged?.Invoke(this, status);
            }
        }

        public void Initialize()
        {
            Status = TileStatus.Closed;
            Player = Player.None;
            Content = null;
        }

        public event EventHandler<TileStatus> StatusChanged;

        public TileStatus Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    if (status == TileStatus.Open)
                    {
                        Content = Player == Player.O ? oImage : xImage;
                    }
                }
            }
        }

        public Player Player
        {
            get => player;
            set
            {
                player = value;
                if (status == TileStatus.Open)
                {
                    Content = player == Player.O ? oImage : xImage;
                }
            }
        }
    }
}