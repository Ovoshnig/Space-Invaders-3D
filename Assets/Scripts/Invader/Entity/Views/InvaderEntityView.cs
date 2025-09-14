using UnityEngine;

public class InvaderEntityView : MonoBehaviour
{
    [field: SerializeField] public InvaderMoverView MoverView { get; private set; }
    [field: SerializeField] public InvaderShooterView ShooterView { get; private set; }
    [field: SerializeField] public TriggerColliderView ColliderView { get; private set; }
    [field: SerializeField] public InvaderDestroyerView DestroyerView { get; private set; }
    [field: SerializeField] public InvaderPointsView PointsView { get; private set; }
}
