using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigIt.DesertRace.Entities
{
    [Serializable()]
    public class GameException : ApplicationException
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GameException(object source, string message) : base(message)
        {
            if (source != null)
                this.Source = source.ToString();
        }
    }

    public class BaseRaceGame<TArena, TEntity> 
        where TArena : GameArena<TEntity> , new ()
        where TEntity : GameEntity
    {
        public TArena Arena { get; set; }

        public BaseRaceGame()
        {
            this.Arena = new TArena();
        }
        
        public IEnumerable<TEntity> GenerateRank()
        {
            var vehicleScores = from v in Arena.GameEntities
                                select new
                                {
                                    distance = CalculateScore(v),
                                    vehicle = v
                                };
            vehicleScores = vehicleScores.OrderBy(v => v.distance);
            var result = from r in vehicleScores
                         select r.vehicle;

            return result;
        }

        public virtual double CalculateScore(TEntity entity)
        {
            return Arena.Goal.DistanceEuclidean(entity.Position);
        }
    }

    public class DesertRaceGame
    {
        public static void Play(System.IO.TextReader reader, System.IO.TextWriter results)
        {
            var game = new BaseRaceGame<ArenaRectangular<GameEntity>, GameEntity>();

            string inputLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine))
                throw new GameException(game.Arena, "An arena configuration is required");
            game.Arena.MaxLocation = new Location(Util.SeparedParams(inputLine));

            inputLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine))
                throw new GameException(game.Arena, "An arena Goal is required");
            game.Arena.Goal = new Location(Util.SeparedParams(inputLine));
            do
            {
                inputLine = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(inputLine))
                    break;
                var Vehicle = new Vehicle(game.Arena, Util.SeparedParams(inputLine));
                inputLine = reader.ReadLine();
                Vehicle.ExecCommand(inputLine);
            }
            while (inputLine != null);

            var rank = game.GenerateRank();
            foreach (var vehicle in rank)
            {
                results.WriteLine(vehicle);
            }

        }
    }

}
