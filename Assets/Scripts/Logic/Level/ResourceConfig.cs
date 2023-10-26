using UnityEngine;

namespace Logic.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/Level/Resource Config", order = 5)]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private GameObject _resourcePrefab;

        [Range(1,10)]
        [SerializeField] private int _spawnRate;

        [SerializeField] private int _maxResources;

        public GameObject GetResourcePrefab => _resourcePrefab;
        public int GetSpawnRate => _spawnRate;
        public int GetMaxResources => _maxResources;
    }
}
