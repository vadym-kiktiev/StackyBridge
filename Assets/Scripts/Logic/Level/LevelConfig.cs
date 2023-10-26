using System.Collections.Generic;
using Logic.Team;
using UnityEngine;

namespace Logic.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/Level/Level Config", order = 3)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private GameObject _startPrefab;
        [SerializeField] private List<BlockConfig> _blockConfigs;
        [SerializeField] private GameObject _endPrefab;

        [SerializeField] private TeamsConfig _teamsConfig;

        public GameObject StartPrefab => _startPrefab;
        public List<BlockConfig> BlockConfigs => _blockConfigs;
        public GameObject EndPrefab => _endPrefab;
        public TeamsConfig TeamsConfig => _teamsConfig;
    }
}
