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

    // Interface fonksiyonunu çağırma
    

    private void _gameRestart(){
        _generalTransition.EnterTransition(
            () => {
                lostScreen.SetActive(false);
                isGameRestarted = true; 
                isGameOver = false;
            }
        );
        StartCoroutine(_resetRestartFlag());
        
    }


    private IEnumerator _resetRestartFlag() {
        yield return null; 
        isGameRestarted = false;
    }


    public void LostGame(){
        _generalTransition.EnterTransition(
            () => {
                isGameOver = true;
                isGameRestarted = false;
                lostScreen.SetActive(true);
            }
        );
    }

    public bool isGameOver { get; private set; } = false;
    public bool isGameRestarted { get; private set; } = false;

}
