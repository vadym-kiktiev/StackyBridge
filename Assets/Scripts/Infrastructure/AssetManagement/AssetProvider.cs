using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
