using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    [SerializeField] private bool _enabled;
    [SerializeField] private Collider _targetDetector;
    [SerializeField] private Transform _currentTarget;

    public bool IsEnabled() => _enabled;
    public void Enable() => _enabled = true;
    public void Disable() => _enabled = false;
    public void Trigger() => _enabled = _enabled ? false : true;
    

    public Transform CurrentTarget => _currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTarget() {

    }
}
