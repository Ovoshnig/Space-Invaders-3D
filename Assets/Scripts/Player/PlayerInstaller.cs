using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerInstaller : IInstaller
{
    [SerializeField] private PlayerSpawnInstaller _playerSpawnInstaller;
    [SerializeField] private PlayerMovementInstaller _playerMovementInstaller;
    [SerializeField] private PlayerShootingInstaller _playerShootingInstaller;
    [SerializeField] private PlayerCollisionInstaller _playerCollisionInstaller;
    [SerializeField] private PlayerDestructionInstaller _playerDestructionInstaller;
    [SerializeField] private PlayerHealthInstaller _playerHealthInstaller;
    [SerializeField] private PlayerAnimationInstaller _playerAnimationInstaller;
    [SerializeField] private PlayerSoundInstaller _playerSoundInstaller;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerInputHandler>(Lifetime.Singleton).AsSelf();

        _playerSpawnInstaller.Install(builder);
        _playerMovementInstaller.Install(builder);
        _playerShootingInstaller.Install(builder);
        _playerCollisionInstaller.Install(builder);
        _playerDestructionInstaller.Install(builder);
        _playerHealthInstaller.Install(builder);
        _playerAnimationInstaller.Install(builder);
        _playerSoundInstaller.Install(builder);
    }
}
