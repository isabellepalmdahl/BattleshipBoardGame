using System.Drawing;

namespace BattleshipBG.ViewModels
{
    public class Submarine : Ship
    {
        public Submarine()
        {
            Size = 2;
            Point point = new Point(1, 1);
            SetCoordinates(point);
        }
    }
}
