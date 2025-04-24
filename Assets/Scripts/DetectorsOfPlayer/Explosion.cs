using UnityEngine;
using TMPro;

public class Explosion : Countdowners, IResetUpdater
{
    [SerializeField] private GameObject _bomb;

    protected override void AfterAction()
    {
        Debug.Log("Over");
        _bomb.SetActive(false);
        _timerText.enabled = false;
    }

    public override void IRestarted()
    {
        base.IRestarted();
        _bomb.SetActive(true);

    }
/*
    protected override void Update()
    {
        if (GameManagement.instance.isGameRestarted && !_restartHandled)
        {
            Debug.Log("Explosion re");
            _bomb.SetActive(true);
            _countdown = _startCountdown;
            _countend = false;
            _counting = false;
            _timerText.enabled = false;

        }

        base.Update(); 
    }
*/    
}
