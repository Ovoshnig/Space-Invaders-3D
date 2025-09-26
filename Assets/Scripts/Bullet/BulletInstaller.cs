using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BulletInstaller : IInstaller
{
    [SerializeField] private PlayerBulletMoverView _playerBulletMoverView;

    public void Install(IContainerBuilder builder) =>
        builder.RegisterComponent(_playerBulletMoverView);
}
