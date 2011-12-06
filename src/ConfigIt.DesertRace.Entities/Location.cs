using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigIt.DesertRace.Entities
{
    
    /// <summary>
    /// </summary>
    /// <remarks>
    /// How to extend from 2D to 3D
    /// </remarks>
    public class Location : IEquatable<Location>, ICloneable
    {
        private GameArena<GameEntity> _Container;
        public GameArena<GameEntity> Container { get { return _Container; } set { SetContainer(value); } }
        
        public int X { get; set; }
        public int Y { get; set; }

        public Location()
            : this(0, 0)
        {
        }

        public Location(IEnumerable<string> parameters)
        {
            int x, y;
            var enumerator = parameters.GetEnumerator();

            if (!enumerator.MoveNext())
                throw new GameException(this, "Location requires X Y for initializacion");
            else
                if (!int.TryParse(enumerator.Current, out x))
                    throw new GameException("Value {0} is not a number valid for a X location", enumerator.Current);

            if (!enumerator.MoveNext())
                throw new GameException(this, "Location requires X Y for initializacion");
            else
                if (!int.TryParse(enumerator.Current, out y))
                    throw new GameException("Value {0} is not a number valid for a Y location", enumerator.Current);
            this.X = x;
            this.Y = y;
        }

        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        protected void SetContainer(GameArena<GameEntity> container)
        {
            if (container == null)
            {
                this._Container = null;
                return;
            }
            if (!container.IsValidLocation(this))
            {
                Util.RaiseError("Location {0} is Invalid in the Arena", this);
            }
            this._Container = container;
        }

        public static string ToString(int x, int y)
        {
            return "[X:" + x.ToString() + "," + "Y:" + y.ToString() + "]";
        }

        public override string ToString()
        {
            return ToString(X, Y);
        }

        public bool IsLessThan(int x, int y)
        {
            return (this.X < x) || (this.Y < y);
        }

        public bool IsLessThan(Location other)
        {
            if (other == null)
                return false;
            return IsLessThan(other.X, other.Y);
        }

        public bool IsGraterThan(int x, int y)
        {
            return (this.X > x) || (this.Y > y);
        }

        public bool IsGraterThan(Location other)
        {
            if (other == null)
                return false;
            return IsGraterThan(other.X, other.Y);
        }

        public Location Next(Direction direction)
        {
            int nextX = this.X;
            int nextY = this.Y;

            switch (direction)
            {
                case Direction.North:
                    nextY++;
                    break;
                case Direction.South:
                    nextY--;
                    break;
                case Direction.East:
                    nextX++;
                    break;
                case Direction.West:
                    nextX--;
                    break;
            }

            Location result = new Location(nextX, nextY);
            if (this.Container != null)
            {
                if (!this.Container.IsValidLocation(result))
                    Util.RaiseError("Movement with Direction:{0} from {1} will result in {2} which is not allowed",
                        direction, this, ToString(nextX, nextY));
            }

            return result;
        }

        public bool Equals(Location other)
        {
            return (this.X == other.X) && (this.Y == other.Y);
        }

        public double DistanceEuclidean(Location other)
        {
            if (other == null)
                return double.NaN;
            double xDelta = this.X - other.X;
            double yDelta = this.Y - other.Y;
            double result = Math.Abs(Math.Sqrt(xDelta * xDelta + yDelta * yDelta));
            return result;
        }

        public double DistanceMovement(Location other)
        {
            if (other == null)
                return double.NaN;
            double xDelta = this.X - other.X;
            double yDelta = this.Y - other.Y;
            double result = xDelta + xDelta;
            return result;
        }

        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
