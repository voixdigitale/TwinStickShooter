using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float _flashTime = .2f;
    [SerializeField] private float _offSpeed = .02f;
    [SerializeField] private Renderer _objectRenderer;

    private bool _isFlashing = false;

    private void Update() {
        float shininess = _objectRenderer.material.GetFloat("_GlowPower");
        if (shininess > 0 && _isFlashing) {
            _objectRenderer.material.SetFloat("_GlowPower", shininess - _offSpeed);
        } else {
            _objectRenderer.material.SetFloat("_GlowPower", 0);
            _objectRenderer.material.SetInt("_Enable", 0);
            _isFlashing = false;
        }
    }

    public void StartFlash() {
        if (!_isFlashing) {
            _objectRenderer.material.SetFloat("_GlowPower", 2f);
            _objectRenderer.material.SetInt("_Enable", 1);
            _isFlashing = true;
        }
    }
}
