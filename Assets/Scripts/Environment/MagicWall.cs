using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWall : MonoBehaviour
{
    [SerializeField] private int _checkPointChannel = 0;
    [SerializeField] private int _checkPointsNeeded = 1;
    [SerializeField] private GameObject _disappearVFX;

    private int _checkPointsTriggered = 0;

    private void OnEnable() {
        GameEvents.OnCheckPointEnter += CheckDisappearing;
    }

    private void OnDisable() {
        GameEvents.OnCheckPointEnter -= CheckDisappearing;
    }

    private void CheckDisappearing(int checkPointNum) {
        if (checkPointNum == _checkPointChannel) {
            _checkPointsTriggered++;

            if (_checkPointsTriggered < _checkPointsNeeded) return;
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear() {
        GameObject disappearVFX = Instantiate(_disappearVFX, transform.position, transform.rotation, transform);
        disappearVFX.transform.localScale = transform.localScale;
        yield return new WaitForSeconds(.1f);
        StartCoroutine(TagForDestruction());
    }

    private IEnumerator TagForDestruction() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
