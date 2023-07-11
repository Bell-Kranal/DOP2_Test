using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        public GameObject LoadUI();
    }
}