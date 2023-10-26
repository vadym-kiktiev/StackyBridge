using Logic.Gameplay;
using Logic.Team;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IResourceFactory
    {
        GrabObject Create(Object prefab,TeamModel model, Vector3 at);
    }
}
