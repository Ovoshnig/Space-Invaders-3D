using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class InvaderInstaller : IInstaller
{
    [SerializeField] private InvaderSpawnInstaller _invaderSpawnInstaller;
    [SerializeField] private InvaderMovementInstaller _invaderMovementInstaller;
    [SerializeField] private InvaderShootingInstaller _invaderShootingInstaller;
    [SerializeField] private InvaderCollisionInstaller _invaderCollisionInstaller;
    [SerializeField] private InvaderDestructionInstaller _invaderDestructionInstaller;
    [SerializeField] private InvaderSoundInstaller _invaderSoundInstaller;

    public void Install(IContainerBuilder builder)
    {
        _invaderSpawnInstaller.Install(builder);
        _invaderMovementInstaller.Install(builder);
        _invaderShootingInstaller.Install(builder);
        _invaderCollisionInstaller.Install(builder);
        _invaderDestructionInstaller.Install(builder);
        _invaderSoundInstaller.Install(builder);
    }
}
