using System.Collections.Generic;
using Logic.Team;
using UnityEngine;

namespace Logic.Bridge
{
    public class BridgeSpawner : MonoBehaviour
    {
        [SerializeField] private BridgeBehavior _bridgePartPrefab;
        [SerializeField] private GameObject _blockPrefab;

        [SerializeField] private Transform _bridgeContainer;

        [SerializeField] private Transform _floor;

        public List<BridgeBehavior> Init(List<TeamModel> models, int stairsCount, bool isEnd = false)
        {
            List<BridgeBehavior> bridges = new List<BridgeBehavior>();

            var count = isEnd ? 1 : models.Count;

            var objectHeight = GetBlockHeight();

            float width = _floor.localScale.x;

            var bridgeWidth = _bridgePartPrefab.transform.localScale.x;

            var freeWidth = width - bridgeWidth * count;

            var blockWidth = freeWidth / (count + 1);

            var startPos = (-width / 2) + (blockWidth / 2);

            var bridgeHeight = _bridgePartPrefab.transform.localScale.y;

            for (int i = 0; i < count; i++)
            {
                var block = Instantiate(_blockPrefab, _bridgeContainer).transform;
                block.localScale = new Vector3(blockWidth, bridgeHeight, 1);
                block.localPosition = new Vector3(startPos, objectHeight, 0);

                startPos += blockWidth / 2 + bridgeWidth / 2;

                var bridge = Instantiate(_bridgePartPrefab, _bridgeContainer);
                bridge.transform.localPosition = new Vector3(startPos, objectHeight, 0);

                IBridgeRules rules = isEnd ? new BridgeRulesEnd() : new BridgeRulesDefault(models[i]);

                bridge.Init(models[i], stairsCount, rules);

                bridges.Add(bridge);

                startPos += bridgeWidth / 2 + blockWidth / 2;
            }

            var blocks = Instantiate(_blockPrefab, _bridgeContainer).transform;
            blocks.localScale = new Vector3(blockWidth, bridgeHeight, 1);
            blocks.localPosition = new Vector3(startPos, objectHeight, 0);

            return bridges;
        }

        private float GetBlockHeight()
        {
            float height = _bridgePartPrefab.transform.localScale.y / 2;

            var platformHeight = _floor.localScale.y / 2;

            return height + platformHeight;
        }
    }
}
