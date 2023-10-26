using UnityEngine;

namespace Logic.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/Level/Block Config", order = 4)]
    public class BlockConfig : ScriptableObject
    {
        [SerializeField] private ResourceConfig _resources;

        [SerializeField] private int _height;

        [Range(0.1f, 5f)]
        [SerializeField] private int _spawnRate;

        [SerializeField] private GameObject _blockPrefab;
        [SerializeField] private int _stairsCount = 5;

        [SerializeField] private int _maxResources;

        public ResourceConfig GetResourceConfig => _resources;
        public int GetHeight => _height;
        public int GetSpawnRate => _spawnRate;
        public GameObject GetBlockPrefab => _blockPrefab;
        public int GetStairsCount => _stairsCount;
        public int GetMaxResources => _maxResources;
    }
}
