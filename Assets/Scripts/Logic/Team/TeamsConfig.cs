using System.Collections.Generic;
using UnityEngine;

namespace Logic.Team
{
    [CreateAssetMenu(fileName = "TeamConfig", menuName = "ScriptableObjects/Team/Team Config", order = 2)]
    public class TeamsConfig : ScriptableObject
    {
        [SerializeField] private List<TeamModel> _teams = new ();

        private Dictionary<string, TeamModel> _teamsCollection;

        public void Init() =>
            _teams.ForEach(AddTeam);

        public List<TeamModel> GetTeams() =>
            _teams;

        public TeamModel GetTeamPlayer() =>
            _teams[0];

        public TeamModel GetRandomTeam() =>
            _teams[Random.Range(0, _teams.Count)];

        private void AddTeam(TeamModel team) =>
            _teamsCollection.Add(team.TeamId, team);
    }
}
