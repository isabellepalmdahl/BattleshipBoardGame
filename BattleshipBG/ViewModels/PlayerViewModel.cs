using BattleshipBG.Data;
using BattleshipBG.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace BattleshipBG.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private static readonly Random random = new Random();
        private readonly int battleFieldSize = 10;
        public ObservableCollection<Ship> Ships { get; set; }
        public ObservableCollection<OceanPiece> Ocean { get; set; } //= new ObservableCollection<OceanPiece>();

        public void FillFleet()
        {
            Ships = new ObservableCollection<Ship>()
            {
                new BattleShip(),
                new Submarine(),
                new Cruiser()
            };
        }
        public PlayerViewModel()
        {
            FillFleet();
            FillOcean();
            //PlaceShipsRandomly();
            AddTestShips();
            ExposeAllShips();
        }


        #region Ships and Oceans
        private void FillOcean()
        {
            Ocean = new ObservableCollection<OceanPiece>();
            for (int x = 1; x <= battleFieldSize; x++)
            {
                for (int y = 1; y <= battleFieldSize; y++)
                {
                    var piece = new OceanPiece
                    {
                        Id = Ocean.Count,
                        X = x,
                        Y = y,
                        CurrentStatus = Status.Untested
                        //OceanColor = Brushes.AliceBlue
                    };
                    Ocean.Add(piece);
                }
            }
        }

        public void ExposeAllShips()
        {
            foreach (var ship in Ships)
            {
                foreach (var point in ship.Coordinates)
                {
                    var piece = Ocean.Where(o => o.X == point.X && o.Y == point.Y).FirstOrDefault();
                    piece.OceanColor = Brushes.Gray;
                }
            }
        }

        public void AddTestShips()
        {
            for (int i = 0; i < Ships.Count; i++)
            {
                Ships[i].SetCoordinates(new Point(1, i + 1));
            }
        }

        /// <summary>
        /// Place ships randomly in battlefield ocean
        /// </summary>
        public void PlaceShipsRandomly()
        {
            FillFleet();
            FillOcean();
            bool shipIsAdded;

            foreach (var ship in Ships)
            {
                ship.Direction = GetRandomDirection();
                
                do
                {
                    var point = GetRandomOceanPoint();
                    if (IsOutOfBounds(point, ship.Size))
                    {
                        shipIsAdded = false;
                    }
                    else
                    {
                        shipIsAdded = PlaceShipInOcean(ship, point);
                    }
                } while (!shipIsAdded);
            }
        }
        private bool IsOutOfBounds(Point point, int size)
        {
            size -= 1;
            return point.X + size > battleFieldSize || point.Y + size > battleFieldSize;
        }

        /// <summary>
        /// Place one ship at given point at sea
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool PlaceShipInOcean(Ship ship, Point point)
        {
            var coordinates = CalculateShipMargin(ship, point);
            if (HasSufficentSpace(coordinates))
            {
                ship.SetCoordinates(point);
                return true;
            }
            
            return false;
        }

        private bool HasSufficentSpace(List<Point> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                foreach (var ship in Ships)
                {
                    if (IsPointOccupied(coordinate, ship))
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        private bool IsPointOccupied(Point coordinate, Ship ship)
        {
            return ship.Coordinates
                .Where(s => s.X == coordinate.X && s.Y == coordinate.Y)
                .Any();
        }

        /// <summary>
        /// Calculate the ship margins
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        private List<Point> CalculateShipMargin(Ship ship, Point startPoint)
        {
            var xCoordinates = new List<int>();
            var yCoordinates = new List<int>();
            var coordinates = new List<Point>();

            switch (ship.Direction)
            {
                case Direction.Horizontal:
                    xCoordinates = Enumerable.Range(startPoint.X - 1, ship.Size + 2).ToList();
                    yCoordinates = Enumerable.Range(startPoint.X - 1, 3).ToList();
                    break;
                case Direction.Vertical:
                    xCoordinates = Enumerable.Range(startPoint.X - 1, 3).ToList();
                    yCoordinates = Enumerable.Range(startPoint.X - 1, ship.Size + 2).ToList();
                    break;
                default:
                    break;
            }
            Point point;

            foreach (var x in xCoordinates)
            {
                foreach (var y  in yCoordinates)
                {
                    point = new Point(x, y);
                    coordinates.Add(point);
                }
            }

            return coordinates;
        }

        /// <summary>
        /// Find a random point in ocean
        /// </summary>
        /// <returns></returns>
        private Point GetRandomOceanPoint()
        {
            return new Point(random.Next(battleFieldSize)+1, random.Next(battleFieldSize)+1);
        }
        /// <summary>
        /// Get random direction
        /// </summary>
        /// <returns>horizontal/vertical</returns>
        private Direction GetRandomDirection()
        {
            int magicNumber = random.Next(2);
            return (Direction)magicNumber;
        }
        #endregion
        #region War
        public Status UnderAttack(Point point)
        {
            foreach (var ship in Ships)
            {
                var result = ship.UnderAttack(point);
                if (result == Status.Hit || result == Status.Sunk)
                {
                    return result;
                }
            }
            return Status.Miss;
        }
        #endregion
    }

}
