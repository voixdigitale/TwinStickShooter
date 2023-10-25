using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadManager : MonoBehaviour {
    private Gamepad _gamepad;

    private void Awake() {
        _gamepad = Gamepad.current;
    }

    public void Vibrate(float duration = .25f) {
        if (_gamepad != null) {
            _gamepad.SetMotorSpeeds(0.123f, 0.234f);
            StartCoroutine(VibrationHandler(duration));
        }
    }

    private IEnumerator VibrationHandler(float length) {
        yield return new WaitForSeconds(length);

        _gamepad.SetMotorSpeeds(0, 0);
    }

    void OnApplicationQuit() {
        _gamepad.SetMotorSpeeds(0, 0);
    }

    private void OnApplicationPause(bool pause) {
        if (pause)
            _gamepad.SetMotorSpeeds(0, 0);
    }
}
