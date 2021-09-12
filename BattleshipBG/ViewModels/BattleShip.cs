using System.Drawing;

namespace BattleshipBG.ViewModels
{
    public class BattleShip : Ship
    {
        public BattleShip()
        {
            Size = 4;
            Point point = new Point(1, 1);
            Direction = Data.Direction.Vertical;
            SetCoordinates(point);
        }
    }
}
