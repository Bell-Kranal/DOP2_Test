using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject LoadUI()
        {
            GameObject gameObject = Resources.Load<GameObject>(AssetsPath.UIPath);

            return gameObject;
        }
    }
}