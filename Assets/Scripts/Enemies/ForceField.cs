using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Enemy {

    [SerializeField] private int _checkPointChannel = 0;
    [SerializeField] private int _checkPointsNeeded = 1;

    private int _checkPointsTriggered = 0;

    private void OnEnable() {
        GameEvents.OnCheckPointEnter += CheckInvulnerability;
    }

    private void OnDisable() {
        GameEvents.OnCheckPointEnter -= CheckInvulnerability;
    }

    public override void Movement() {
        return;
    }

    private void CheckInvulnerability(int checkPointNum) {
        if (checkPointNum == _checkPointChannel) {
            _checkPointsTriggered++;

            if (_checkPointsTriggered < _checkPointsNeeded)
                return;
            _health.DisableInvulnerability();
        }
    }
}
