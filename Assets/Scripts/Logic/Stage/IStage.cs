using System;
using Logic.Level;
using Logic.Team;
using UnityEngine;

namespace Logic.Stage
{
    public interface IStage
    {
        void Initialize(BlockConfig teamModels, TeamsConfig teamsConfig);
        void StartStage();
        void EndStage();
        void Tick();
        float GetSize();
        void SetPosition(Vector3 position);
        Transform GetTarget(TeamModel teamModel, Vector2 position);
        bool CanIMoveToNextStage(TeamModel teamModel);
        void GetNextStage(TeamModel teamModel, out Transform transform);
        void GetBridgeTarget(TeamModel teamModel, out Transform transform);
        event Action OnResourceSpawned;
        void SetIndex(int i);
        int Index { get; set; }
    }
}
