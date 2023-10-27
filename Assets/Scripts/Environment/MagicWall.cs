using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWall : MonoBehaviour
{
    [SerializeField] private int _checkPointChannel = 0;
    [SerializeField] private GameObject _disappearVFX;

    private void OnEnable() {
        GameEvents.OnCheckPointEnter += CheckDisappearing;
    }

    private void OnDisable() {
        GameEvents.OnCheckPointEnter -= CheckDisappearing;
    }

    private void CheckDisappearing(int checkPointNum) {
        if (checkPointNum == _checkPointChannel) {
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear() {
        Instantiate(_disappearVFX, transform.position, transform.rotation, transform);
        yield return new WaitForSeconds(.1f);
        StartCoroutine(TagForDestruction());
    }

    private IEnumerator TagForDestruction() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
