using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigIt.DesertRace.Entities
{
    public class GameEntity
    {
        public string Name { get; set; }

        public Location Position { get; protected set; }

        public GameArena<GameEntity> Container { 
            get { return Position.Container;}
            set { SetContainer(value); }
        }

        public GameEntity(GameArena<GameEntity> container, ICollection<string> parameters)
        {
            if (parameters.Count < 3)
                throw new GameException(this, "Requires Name Location.X Location.Y");
            this.Name = parameters.First();
            this.Position = new Location(parameters.Skip(1));
            SetContainer(container);
        }

        public GameEntity(GameArena<GameEntity> container, Location position)
        {
            this.Position = position;
            SetContainer(container);
        }

        private void SetContainer(GameArena<GameEntity> container)
        {
            Position.Container = container;  
            container.GameEntities.Add(this);
        }

        public override string ToString()
        {
            if (this.Position == null)
                return this.Name + " ? ?";
            return this.Name + " " + this.Position.X.ToString() + " " + this.Position.Y.ToString();
        }

    }

    public enum Direction
    {
        North = 0,
        West = 1,
        South = 2,
        East = 3
    }

    /// <summary>
    /// Define de currently available commands, In case that the number or complexity of commands increase, would be better
    /// to use a proper Command Class
    /// </summary>
    public enum VehicleCommands
    {
        Left,
        Right,
        Move
    }

    public class Vehicle : GameEntity
    {
        public Direction Direction { get; protected set; }

        public const Direction DirectionMinValue = Direction.North;
        public const Direction DirectionMaxValue = Direction.East;

        public Vehicle(ArenaRectangular<GameEntity> container, string[] parameters) 
            : base (container, parameters)
        {
            if (parameters.Length < 3)
                throw new GameException(this, "Requires Location.X Location.Y and Direcction for initializacion");
            this.Direction = Util.TranslateCharToEnum<Direction>(parameters[3]).First();
        }

        public Vehicle(ArenaRectangular<GameEntity> container, Location position, Direction direction)
            : base(container, position)
        {
            this.Direction = direction;
        }

        public void ExecCommand(string commands)
        {
            var listCommands = Util.TranslateCharToEnum<VehicleCommands>(commands);
            ExecCommand(listCommands);
        }

        public void ExecCommand(IEnumerable<VehicleCommands> commands)
        {
            foreach (var command in commands)
            {
                ExecCommand(command);
            }
        }

        public void ExecCommand(VehicleCommands command)
        {
            switch (command)
            {
                case VehicleCommands.Left:
                    Direction = Direction + 1;
                    if (Direction > DirectionMaxValue)
                        Direction = DirectionMinValue;
                    break;
                case VehicleCommands.Right:
                    Direction = Direction - 1;
                    if (Direction < DirectionMinValue)
                        Direction = DirectionMaxValue;
                    break;
                case VehicleCommands.Move:
                    this.Position = this.Position.Next(this.Direction);
                    break;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.Direction.ToString()[0];
        }
    }
}
