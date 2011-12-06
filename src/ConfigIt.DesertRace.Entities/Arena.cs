using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigIt.DesertRace.Entities
{

    public abstract class GameArena<TEntity> where TEntity : GameEntity
    {
        public List<TEntity> GameEntities { get; protected set; }

        private Location _Goal;
        public Location Goal { 
            get{return _Goal;} 
            set{ 
                if (!IsValidLocation(value))
                    Util.RaiseError("The location {0} is no valid and cannot be used as a Goal",value);
                this._Goal = value;
            }
        } 

        public GameArena()
            : base()
        {
            this.GameEntities = new List<TEntity>();
        }

        public abstract bool IsValidLocation(Location location);
    }

    public class ArenaRectangular<TEntity> : GameArena<TEntity>
        where TEntity : GameEntity
    {
        #region Properties

        private Location _MinLocation = new Location();
        public virtual Location MinLocation
        {
            get { return _MinLocation; }
            set
            {
                if (value == null)
                    return;
                if (value.IsGraterThan(MaxLocation))
                    Util.RaiseError("MinLocation {0} cannot be grater thatn MaxLocation {1}", value, MaxLocation);
                _MinLocation = value;
            }
        }

        private Location _MaxLocation = new Location();
        public virtual Location MaxLocation
        {
            get { return _MaxLocation; }
            set
            {
                if (value == null)
                    return;
                if (value.IsLessThan(MinLocation))
                    Util.RaiseError("MaxLocation {0} cannot be less thatn MinLocation {1}", value, MinLocation);
                _MaxLocation = value;
            }
        }

        #endregion

        public override bool IsValidLocation(Location location)
        {
            return IsValidLocation(location.X, location.Y);
        }

        private bool IsValidLocation(int nextX, int nextY)
        {
            if ((MaxLocation != null) && MaxLocation.IsLessThan(nextX, nextY))
                return false;
            if ((MinLocation != null) && MinLocation.IsGraterThan(nextX, nextY))
                return false;
            return true;
        }
    }

    public class ArenaCompetition2012 : ArenaRectangular<Vehicle>
    {
    }
}
