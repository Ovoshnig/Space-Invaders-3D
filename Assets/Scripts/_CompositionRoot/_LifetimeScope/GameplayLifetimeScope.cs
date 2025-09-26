using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private CameraInstaller _cameraInstaller;
    [SerializeField] private GamePauseInstaller _gamePauseInstaller;
    [SerializeField] private GameStateChangeInstaller _gameStateChangeInstaller;
    [SerializeField] private ScoreInstaller _scoreInstaller;
    [SerializeField] private PlayerInstaller _playerInstaller;
    [SerializeField] private BulletInstaller _bulletInstaller;
    [SerializeField] private InvaderInstaller _invaderInstaller;
    [SerializeField] private UFOInstaller _ufoInstaller;
    [SerializeField] private ShieldInstaller _shieldInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        new CompositionRootInstaller().Install(builder);

        _cameraInstaller.Install(builder);
        _gamePauseInstaller.Install(builder);
        _gameStateChangeInstaller.Install(builder);
        _scoreInstaller.Install(builder);
        _playerInstaller.Install(builder);
        _bulletInstaller.Install(builder);
        _invaderInstaller.Install(builder);
        _ufoInstaller.Install(builder);
        _shieldInstaller.Install(builder);
    }
}
