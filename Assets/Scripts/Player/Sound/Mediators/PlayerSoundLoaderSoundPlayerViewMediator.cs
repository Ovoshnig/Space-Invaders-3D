public class PlayerSoundLoaderSoundPlayerViewMediator : Mediator
{
    private readonly PlayerSoundLoader _playerSoundLoader;
    private readonly PlayerSoundPlayerView _playerSoundPlayerView;

    public PlayerSoundLoaderSoundPlayerViewMediator(PlayerSoundLoader playerSoundLoader, 
        PlayerSoundPlayerView playerSoundPlayerView)
    {
        _playerSoundLoader = playerSoundLoader;
        _playerSoundPlayerView = playerSoundPlayerView;
    }

    public async override void Initialize()
    {
        var (shootResource, deathResource) = await _playerSoundLoader.LoadSoundsAsync(
            _playerSoundPlayerView.ShootReference, 
            _playerSoundPlayerView.DeathReference);
        _playerSoundPlayerView.SetResources(shootResource, deathResource);
    }

    public override void Dispose()
    {
        base.Dispose();

        _playerSoundLoader.ReleaseSounds();
    }
}
