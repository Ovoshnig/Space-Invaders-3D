using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    private Image[] _images = null;

    public Image[] Images => _images ??= GetComponentsInChildren<Image>();

    public void Set(int value)
    {
        if (value > Images.Length)
        {
            Debug.LogError($"There should be at least {value} health images");
            return;
        }

        for (int i = 0; i < value; i++)
            Images[i].gameObject.SetActive(true);

        for (int i = value; i < _images.Length; i++)
            Images[i].gameObject.SetActive(false);
    }
}
