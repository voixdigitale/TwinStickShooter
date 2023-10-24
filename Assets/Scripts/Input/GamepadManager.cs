using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadManager : MonoBehaviour
{
    private Gamepad _gamepad;

    private void Awake() {
        _gamepad = Gamepad.current;
    }

    public void Vibrate() {
        if (_gamepad != null) {
            _gamepad.SetMotorSpeeds(0.123f, 0.234f);
            StartCoroutine(VibrationHandler(.25f));
        }
    }

    private IEnumerator VibrationHandler(float length) {
        yield return new WaitForSeconds(length);

        _gamepad.SetMotorSpeeds(0, 0);
    }
}
