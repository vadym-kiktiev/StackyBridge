using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Logic.Level;
using Logic.Stage;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly IAssets _assets;

        private Object _heroPrefab;

        private List<GameObject> _stageSizes = new List<GameObject>();

        public void Load()
        {
            _heroPrefab = _assets.Load<Object>(AssetPath.HeroPath);
        }

        public void Create(Vector3 at)
        {
            _diContainer.InstantiatePrefab(_heroPrefab, at, Quaternion.identity, null);
        }

        private Transform _parent;

        public List<IStage> CreateLevel(LevelConfig config)
        {
            var parent = Object.Instantiate(new GameObject("Environment")).transform;
            parent.position = new Vector3(0, -2.5f, 0);

            _parent = parent;

            List<IStage> stages = new();

            for (int i = 0; i < config.BlockConfigs.Count; i++)
            {
                var block = config.BlockConfigs[i];

                var blockObject =
                    _diContainer.InstantiatePrefab(block.GetBlockPrefab, Vector3.zero, Quaternion.identity, parent);

                _stageSizes.Add(blockObject);

                var stage = blockObject.GetComponent<IStage>();
                stage.Initialize(block, config.TeamsConfig);

                stages.Add(stage);
            }

            SetPosition(stages);

            return stages;
        }

        private void SetPosition(List<IStage> stages)
        {
            var totalHeight = 0f;

            totalHeight += stages[0].GetSize();

            stages[0].SetPosition(new Vector3(0,stages[0].GetSize() / 2f,0));

            for (int i = 1; i < stages.Count; i++)
            {
                var blockObject = stages[i];

                var size = blockObject.GetSize();

                var height = totalHeight + (size / 2f);

                blockObject.SetPosition(new Vector3(0, height, 0));

                totalHeight += size;
            }

            _parent.localPosition = new Vector3(0, -(stages[0].GetSize()), 0);
        }
    }
}
