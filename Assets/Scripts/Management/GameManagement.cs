using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] private GameObject lostScreen;
    private GeneralTransition _generalTransition;

    public static GameManagement instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        _generalTransition = gameObject.GetComponent<GeneralTransition>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            _gameRestart();
        }
        
    }

    private void _gameRestart(){
        lostScreen.SetActive(false);
        _generalTransition.SceneChangend();
        isGameRestarted = true; 
        isGameOver = false;
        StartCoroutine(_resetRestartFlag());
    }

    private IEnumerator _resetRestartFlag() {
        yield return null; 
        isGameRestarted = false;
    }


    public void LostGame(){
        _generalTransition.MakeTransition();
        isGameOver = true;
        isGameRestarted = false;
        //lostScreen.GetComponentInParent<Canvas>().sortingOrder = 5;
        lostScreen.SetActive(true);
    }

    public bool isGameOver { get; private set; } = false;
    public bool isGameRestarted { get; private set; } = false;

}
