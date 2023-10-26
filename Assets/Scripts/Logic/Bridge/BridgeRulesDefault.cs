using Logic.Team;

namespace Logic.Bridge
{
    public class BridgeRulesDefault : IBridgeRules
    {
        private TeamModel _teamModel;

        public BridgeRulesDefault(TeamModel teamModel) =>
            _teamModel = teamModel;

        public bool CheckTeam(TeamModel model) =>
            _teamModel.TeamId == model.TeamId;
    }
}
