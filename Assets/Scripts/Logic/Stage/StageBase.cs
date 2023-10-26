using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Factory;
using Logic.Bridge;
using Logic.Gameplay;
using Logic.Level;
using Logic.Team;
using ModestTree;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Logic.Stage
{
    public abstract class StageBase : MonoBehaviour, IStage
    {
        [SerializeField] protected BridgeSpawner _bridgeSpawner;
        [SerializeField] protected SpawnArea _spawnArea;

        [Inject] IResourceFactory _resourceFactory;

        protected TeamsConfig _teamModels;
        protected BlockConfig _blockConfig;

        protected List<GrabObject> _spawnedResources = new();
        protected List<BridgeBehavior> _bridgeBehaviors = new();

        protected float _timer = 0f;

        [SerializeField] private float _size = 0f;

        public event Action OnResourceSpawned;

        public void SetIndex(int i) =>
            Index = i;

        public int Index { get; set; }

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public virtual void Initialize(BlockConfig teamModels, TeamsConfig teamsConfig)
        {
            _blockConfig = teamModels;
            _teamModels = teamsConfig;

            if (!_bridgeSpawner)
                return;

            _bridgeBehaviors = _bridgeSpawner.Init(_teamModels.GetTeams(), _blockConfig.GetStairsCount);
        }

        public virtual void StartStage()
        {
            if (!_bridgeSpawner)
                return;

            for (int i = 0; i < 4; i++)
                SpawnResource();

            OnResourceSpawned?.Invoke();
        }

        public virtual void EndStage()
        {
            foreach (var bridge in _bridgeBehaviors)
            {
                var howNeed = bridge.PartsLeft - _spawnedResources.Count(x => x.TeamId == bridge.TeamId);

                for (int i = 0; i < howNeed; i++)
                    SpawnResource(bridge.TeamModel);

                OnResourceSpawned?.Invoke();
            }
        }

        public virtual void Tick()
        {
            if (_timer >= 0)
            {
                _timer -= Time.deltaTime;
                return;
            }

            _timer = _blockConfig.GetSpawnRate;

            if (_blockConfig.GetMaxResources > _spawnedResources.Count)
            {
                SpawnResource();
                OnResourceSpawned?.Invoke();
            }
            else
            {
                Debug.Log("Max resources " + _spawnedResources.Count);
            }
        }

        public float GetSize()
        {
            if (_size != 0)
                return _size;

            Bounds bounds = new Bounds(Vector3.zero,Vector3.zero);

            foreach (var rend in transform.GetComponentsInChildren<SpriteRenderer>())
                bounds.Encapsulate(rend.bounds);

            _size = bounds.size.y;

            return _size;
        }

        private void SpawnResource()
        {
            GrabObject resource =
                _resourceFactory
                    .Create(
                        _blockConfig.GetResourceConfig.GetResourcePrefab,
                        GetLessPopularTeam(),
                        _spawnArea.TakeRandomPosition());

            _spawnedResources.Add(resource);

            resource.OnGrab += OnGrab;
        }

        private TeamModel GetLessPopularTeam()
        {
            var teams = _teamModels.GetTeams();

            var groups = _spawnedResources.GroupBy(x => x.TeamId);

            if (groups.Any())
            {
                foreach (var team in teams)
                {
                    if (!groups.Any(x => x.Key == team.TeamId))
                        return team;
                }

                var smallestGroup = groups.OrderBy(group => group.Count()).First();
                var smallestTeamId = smallestGroup.Key;

                return teams.FirstOrDefault(team => team.TeamId == smallestTeamId) ?? _teamModels.GetRandomTeam();
            }

            return _teamModels.GetRandomTeam();
        }


        private void SpawnResource(TeamModel model)
        {
            GrabObject resource =
                _resourceFactory
                    .Create(
                        _blockConfig.GetResourceConfig.GetResourcePrefab,
                        model,
                        _spawnArea.TakeRandomPosition());

            _spawnedResources.Add(resource);

            resource.OnGrab += OnGrab;
        }

        private void OnGrab(GrabObject resource)
        {
            resource.OnGrab -= OnGrab;

            _spawnedResources.Remove(resource);

            Destroy(resource.gameObject);
        }

        public Transform GetTarget(TeamModel teamModel, Vector2 position)
        {
            var resoures = _spawnedResources.Where(x => x.TeamId == teamModel.TeamId);

            if (!resoures.Any())
                return null;

            return resoures.OrderBy(x => Vector2.Distance(x.transform.position, position)).First().transform;
        }

        public bool CanIMoveToNextStage(TeamModel teamModel)
        {
            var bridge = FindBridge(teamModel);

            if (!bridge)
                return false;
            else
                return FindBridge(teamModel).IsFilled;
        }

        public void GetNextStage(TeamModel teamModel, out Transform transform)
        {
            var bridge = FindBridge(teamModel);

            if (!bridge)
                transform = null;
            else
                transform = FindBridge(teamModel).NextStagePoint;
        }

        public void GetBridgeTarget(TeamModel teamModel, out Transform transform)
        {
            var bridge = FindBridge(teamModel);

            if (!bridge)
                transform = null;
            else
                transform = FindBridge(teamModel).BridgeCollectPoint;
        }

        private BridgeBehavior FindBridge(TeamModel teamModel)
        {
            if (_bridgeBehaviors.Count==0)
                return null;

            return _bridgeBehaviors.First(x => x.CheckTeam(teamModel));
        }
    }
}
