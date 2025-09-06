using UnityEngine;

public abstract class InvaderPointsView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    protected InvaderPointsSettings InvaderPointsSettings =>
        _gameSettings.InvaderSettings.InvaderPointsSettings;

    public abstract int GetPoints();
}
