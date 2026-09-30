using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class SolidAfterLeavePlatform : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    private float initialAlpha = 0.35f;

    [SerializeField]
    private string playerTag = "AutoMove";

    private Renderer _platformRenderer;
    private Material _platformMaterial;
    private Collider _platformCollider;
    
    private bool _playerEntered;
    private bool _hasBecomeSolid;

    private void Awake()
    {
        _platformRenderer = GetComponent<Renderer>();
        _platformMaterial = _platformRenderer.material;
        _platformCollider = GetComponent<Collider>();

        _platformCollider.isTrigger = true;
        SetAlpha(initialAlpha);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            _playerEntered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_hasBecomeSolid || !_playerEntered)
            return;

        if (other.CompareTag(playerTag))
        {
            _hasBecomeSolid = true;
            _platformCollider.isTrigger = false;
            SetAlpha(1f);
        }
    }
    
    private void SetAlpha(float alpha)
    {
        Color color = _platformMaterial.color;
        color.a = alpha;
        _platformMaterial.color = color;
    }
}