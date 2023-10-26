using UnityEngine;

namespace Logic.Team
{
    [CreateAssetMenu(fileName = "Team", menuName = "ScriptableObjects/Team/Creat Team", order = 1)]
    public class TeamModel : ScriptableObject
    {
        [SerializeField] private string _teamName;
        [SerializeField] private Color _teamColor;

        public string TeamName => _teamName;
        public Color TeamColor => _teamColor;

        public string TeamId
        {
            get
            {
                return TeamName;
            }
        }
    }
}
