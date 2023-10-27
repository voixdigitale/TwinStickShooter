using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current { get; private set; }

    public static Action<int> OnCheckPointEnter;

    private void Awake() {
        if (current != null && current != this) {
            Destroy(this);
        } else {
            current = this;
        }
    }

    public void CheckPointEnter(int checkPointNum) {
        OnCheckPointEnter?.Invoke(checkPointNum);
    }
}
