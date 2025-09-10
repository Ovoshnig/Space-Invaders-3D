using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

public class UFOExplosionView : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _explodedDurationSeconds = 1f;
    [SerializeField, Min(0f)] private float _pointsDurationSeconds = 0.5f;
    [SerializeField] private float _beforeDilateValue = 0f;
    [SerializeField] private float _afterDilateValue = -1f;
    [SerializeField, Min(0f)] private float _dilateDurationSeconds = 1f;

    private MeshRenderer _meshRenderer;
    private TMP_Text _pointsText;
    private CancellationTokenSource _cts = null;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _pointsText = GetComponentInChildren<TMP_Text>(includeInactive: true);
    }

    private void Start()
    {
        _meshRenderer.enabled = false;
        _pointsText.enabled = false;
    }

    public async UniTask ExplodeAsync(int points)
    {
        CancelCurrent();
        _cts = new CancellationTokenSource();

        _meshRenderer.enabled = true;

        try
        {
            await UniTask.WaitForSeconds(_explodedDurationSeconds, cancellationToken: _cts.Token);

            _meshRenderer.enabled = false;
            _pointsText.text = points.ToString();
            _pointsText.enabled = true;
            _pointsText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, _beforeDilateValue);

            await UniTask.WaitForSeconds(_pointsDurationSeconds, cancellationToken: _cts.Token);

            await _pointsText.fontMaterial
                .DOFloat(_afterDilateValue, ShaderUtilities.ID_FaceDilate, _dilateDurationSeconds)
                .OnUpdate(() => _pointsText.UpdateMeshPadding())
                .ToUniTask(cancellationToken: _cts.Token);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            if (this != null)
                _pointsText.enabled = false;
        }
    }

    private void OnDestroy() => CancelCurrent();

    private void CancelCurrent()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}
