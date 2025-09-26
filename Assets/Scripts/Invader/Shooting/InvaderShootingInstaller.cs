using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class InvaderShootingInstaller : IInstaller
{
    [SerializeField] private InvaderBulletMoverView[] _invaderBulletPrefabs;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_invaderBulletPrefabs);
        builder.Register<InvaderBulletPool>(Lifetime.Singleton);

        builder.Register<InvaderShooter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderShooterMediator>(Lifetime.Singleton);
    }
}
