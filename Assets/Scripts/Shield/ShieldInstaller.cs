using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ShieldInstaller : IInstaller
{
    [SerializeField] private GameObject _shieldParent;

    public void Install(IContainerBuilder builder)
    {
        ShieldTileView[] tiles = _shieldParent.GetComponentsInChildren<ShieldTileView>();
        builder.RegisterInstance(tiles);
    }
}
