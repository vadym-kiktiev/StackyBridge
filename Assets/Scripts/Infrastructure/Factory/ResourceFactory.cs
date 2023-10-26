using Infrastructure.AssetManagement;
using Logic.Gameplay;
using Logic.Team;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory
{
    public class ResourceFactory : IResourceFactory
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly IAssets _assets;

        public GrabObject Create(Object prefab,TeamModel model, Vector3 at)
        {
            var resource = Object.Instantiate(prefab, at, Quaternion.identity, null);
            var grabObject = resource.GetComponent<GrabObject>();
            grabObject.Init(model);

            return grabObject;
        }
    }
}
