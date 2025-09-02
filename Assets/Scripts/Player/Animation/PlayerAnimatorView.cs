using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorView : MonoBehaviour
{
    private Animator _animator;

    private Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            return _animator;
        }
    }

    public void SetExploding(bool value) => Animator.SetBool(PlayerAnimatorConstants.IsExplodingId, value);
}
