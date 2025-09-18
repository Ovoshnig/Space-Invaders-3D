using System.Linq;
using UnityEngine;
using VContainer;

public class InvaderCenterView : MonoBehaviour
{
    private InvaderRegistry _registry;

    [Inject]
    public void Construct(InvaderRegistry registry) => _registry = registry;

    private void Update()
    {
        int count = _registry.Invaders.Count;

        if (count == 0)
            return;

        Vector3 sumPosition = _registry.Invaders
            .Select(i => i.transform.position)
            .Aggregate(Vector3.zero, (sum, pos) => sum + pos) / count;

        transform.position = sumPosition;
    }
}
