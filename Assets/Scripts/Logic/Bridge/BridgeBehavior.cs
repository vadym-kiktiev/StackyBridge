using System.Collections.Generic;
using DG.Tweening;
using Logic.Team;
using UnityEngine;

namespace Logic.Bridge
{
    public class BridgeBehavior : MonoBehaviour
    {
        [SerializeField] private VerticalObjectLayout _verticalObjectLayout;

        [SerializeField] private SpriteRenderer _bridgePartPrefab;

        [SerializeField] private GameObject _bridgeBlockCollider;
        [SerializeField] private GameObject _bridgeBotBlockCollider;

        [SerializeField] private GameObject _bridgeBlockFullCollider;

        [SerializeField] private BridgeTrigger _bridgeTrigger;

        [SerializeField] private GameObject _nextStageTrigger;
        [SerializeField] private Transform _nextStagePoint;

        private List<GameObject> _bridgePartsContainer = new ();

        public int PartsLeft => _bridgePartsContainer.Count;

        private TeamModel _teamModel;
        private IBridgeRules _rules;
        private bool _isFill;

        public string TeamId => _teamModel.TeamId;
        public TeamModel TeamModel => _teamModel;
        public Transform BridgeCollectPoint => _bridgeTrigger.transform;
        public Transform NextStagePoint => _nextStagePoint;
        public bool IsFilled => _isFill;

        private void OnDestroy()
        {
            foreach (var part in _bridgePartsContainer)
                part.transform.DOKill();
        }

        public void Init(TeamModel teamModel, int count, IBridgeRules rules)
        {
            _isFill = false;

            _rules = rules;
            _teamModel = teamModel;

            for (int i = 0; i <= count; i++)
                Instantiate(_bridgePartPrefab, _verticalObjectLayout.transform).color = teamModel.TeamColor;

            _verticalObjectLayout.LayoutChildren();

            foreach (Transform child in _verticalObjectLayout.transform)
            {
                _bridgePartsContainer.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }

            _bridgePartsContainer.Reverse();

            RestoreBridgePart();
        }

        public bool CheckTeam(TeamModel model) =>
            _rules.CheckTeam(model);

        public void RestoreBridgePart(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (_bridgePartsContainer.Count == 0)
                    return;

                var bridgePart = _bridgePartsContainer[0];
                _bridgePartsContainer.RemoveAt(0);

                ShowBridgePart(bridgePart, i * 0.1f);
            }

            if (_bridgePartsContainer.Count > 0)
                _bridgeBotBlockCollider.transform.position = _bridgePartsContainer[0].transform.position;
            else
                AllowMove();

            _bridgeTrigger.gameObject.SetActive(false);
            _bridgeTrigger.gameObject.SetActive(true);
        }

        private void AllowMove()
        {
            _bridgeBotBlockCollider.SetActive(false);
            _bridgeBlockCollider.SetActive(false);

            _bridgeTrigger.gameObject.SetActive(false);

            _nextStageTrigger.SetActive(true);

            _isFill = true;
        }

        private void ShowBridgePart(GameObject part, float delay = 0f)
        {
            part.gameObject.SetActive(true);

            var scale = part.transform.localScale.y;

            part.transform.DOScaleY(0, 0f);
            part.transform.DOScaleY(scale, 0.2f).SetDelay(delay);
        }

        public void HideNextTrigger()
        {
            _nextStageTrigger.SetActive(false);

            _bridgeBlockFullCollider.SetActive(true);

            _bridgeBlockFullCollider.transform.DOScaleY(0.1f, 0);
            _bridgeBlockFullCollider.transform.DOLocalMoveY(-0.5f, 0);

            _bridgeBlockFullCollider.transform.DOScaleY(1, 0.4f);
            _bridgeBlockFullCollider.transform.DOLocalMoveY(0, 0.2f);
        }
    }
}
