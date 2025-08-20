using UnityEngine;
using VContainer;

public class InvaderSpawnerView : MonoBehaviour
{
    private InvaderFactory _factory;

    [Inject]
    public void Construct(InvaderFactory factory) => _factory = factory;

    public void SpawnInvader(int index, Vector3 position) =>
        _factory.Create<InvaderMoverView>(index, position);
}
