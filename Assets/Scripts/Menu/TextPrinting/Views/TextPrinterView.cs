using Cysharp.Threading.Tasks;
using R3;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextPrinterView : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _speed = 5f;
    [SerializeField] private bool _playOnAwake = false;

    private readonly Subject<Unit> _completed = new();

    private TMP_Text _tmpText;
    private CancellationTokenSource _cts;

    public Observable<Unit> Completed => _completed;

    protected TMP_Text TmpText => _tmpText;

    protected virtual void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();

        if (_playOnAwake)
        {
            string initialText = _tmpText.text;
            PrintAsync(initialText).Forget();
        }
    }

    protected virtual void OnDestroy() => CancelPrinting();

    public async UniTask PrintAsync(string fullText)
    {
        CancelPrinting();
        _cts = new CancellationTokenSource();

        _tmpText.text = string.Empty;

        StringBuilder stringBuilder = new();
        float delay = 1 / _speed;

        foreach (var symbol in fullText)
        {
            stringBuilder.Append(symbol);
            _tmpText.text = stringBuilder.ToString();

            await UniTask.WaitForSeconds(delay, cancellationToken: _cts.Token);
        }

        _completed.OnNext(Unit.Default);
    }

    protected void CancelPrinting()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
