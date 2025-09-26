using R3;
using System;

public class UFODestroyer : CollidedDestroyer<UFOMoverView, PlayerBulletMoverView>
{
    private readonly InvaderPointsSettings _invaderPointsSettings;
    private readonly Random _random = new();
    private readonly ReactiveProperty<int> _lastPoints = new();

    public UFODestroyer(UFOCollisionReporter ufoCollisionReporter,
        InvaderPointsSettings invaderPointsSettings) : base(ufoCollisionReporter) =>
        _invaderPointsSettings = invaderPointsSettings;

    public ReadOnlyReactiveProperty<int> LastPoints => _lastPoints;

    protected override void OnCollided(CollidedEvent<UFOMoverView> collidedEvent)
    {
        if (collidedEvent.Other.TryGetComponent(out PlayerBulletMoverView playerBulletView))
        {
            int randomIndex = _random.Next(_invaderPointsSettings.UFOPointsVariants.Length);
            _lastPoints.Value = _invaderPointsSettings.UFOPointsVariants[randomIndex];
        }

        base.OnCollided(collidedEvent);
    }
}
