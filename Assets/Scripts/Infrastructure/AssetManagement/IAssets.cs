using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssets
    {
        T Load<T>(string path) where T : Object;
    }
}
