using System.Collections.Generic;
using Logic.Level;
using Logic.Stage;
using Logic.Team;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<IStage> CreateLevel(LevelConfig config);
    }
}
