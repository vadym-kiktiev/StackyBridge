using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure.Services.Level
{
    public class ResourceSpawnerService : IResourceSpawnerService
    {
        public event Action ShouldSpawn;

        [Inject] private ICoroutineRunner _coroutineRunner;

        public void Start()
        {
            SpawnInitialResource();

            //_coroutineRunner.Run(ShouldSpawnResource());
        }

        private void SpawnInitialResource()
        {

        }

        /*private IEnumerator ShouldSpawnResource()
        {

        }*/
    }

    public interface IResourceSpawnerService
    {
    }
}
