using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SecurityCamera : Countdowners
{
    protected override void AfterAction()
    {
        Debug.Log("Override");
        _countdown = _startCountdown;
        _countend = false;
        _counting = false;
        _timerText.enabled = false;
    }


}
