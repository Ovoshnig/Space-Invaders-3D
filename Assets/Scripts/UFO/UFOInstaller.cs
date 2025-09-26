using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class UFOInstaller : IInstaller
{
    [SerializeField] private UFOMovementInstaller _ufoMovementInstaller;
    [SerializeField] private UFOCollisionInstaller _ufoCollisionInstaller;
    [SerializeField] private UFODestructionInstaller _ufoDestructionInstaller;
    [SerializeField] private UFOSoundInstaller _ufoSoundInstaller;

    public void Install(IContainerBuilder builder)
    {
        _ufoMovementInstaller.Install(builder);
        _ufoCollisionInstaller.Install(builder);
        _ufoDestructionInstaller.Install(builder);
        _ufoSoundInstaller.Install(builder);
    }
}
