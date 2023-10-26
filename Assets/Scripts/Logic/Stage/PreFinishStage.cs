using Logic.Level;
using Logic.Team;

namespace Logic.Stage
{
    public class PreFinishStage : StageBase
    {
        public override void Initialize(BlockConfig teamModels, TeamsConfig teamsConfig)
        {
            _blockConfig = teamModels;
            _teamModels = teamsConfig;

            if (!_bridgeSpawner)
                return;

            _bridgeBehaviors = _bridgeSpawner.Init(_teamModels.GetTeams(), _blockConfig.GetStairsCount, true);
        }

        public override void EndStage()
        {
        }
    }
}
