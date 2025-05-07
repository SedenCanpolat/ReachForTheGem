using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameManagement : MonoBehaviour
{
    [SerializeField] private GameObject lostScreen;
    [SerializeField] private GameObject winScreen;
    private GeneralTransition _generalTransition;

    private List<IResetUpdater> _resetUpdaters;

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
        _resetUpdaters = transform.root.GetComponentsInChildren<MonoBehaviour>().OfType<IResetUpdater>().ToList();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            _gameRestart();
        }
        
    }


    private void _gameRestart(){
        _generalTransition.EnterTransition(
            () => {
                lostScreen.SetActive(false);
                winScreen.SetActive(false);
                isGameRestarted = true; 
                isGameOver = false;
                Inventory.instance.EmptyInventory();

                // Interface function called 
                foreach (var item in _resetUpdaters){
                    item.IRestarted();
                }
            }
        );
        
        //StartCoroutine(_resetRestartFlag());
    }


    private IEnumerator _resetRestartFlag() {
        yield return null; 
        isGameRestarted = false;
    }


    public void LostGame(int situation = 0){
        _generalTransition.EnterTransition(
            () => {
                isGameOver = true;
                isGameRestarted = false;
                if (situation == 0)
                    lostScreen.SetActive(true);
                else
                    winScreen.SetActive(true);  
                
            }
        );
        
    }


    public bool isGameOver { get; private set; } = false;
    public bool isGameRestarted { get; private set; } = false;

}
