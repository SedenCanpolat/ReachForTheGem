using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] private GameObject lostScreen;
    public static GameManagement instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }



    public void LostGame(){
        lostScreen.SetActive(true);
        isGameOver = true;
    }

    public bool isGameOver { get; private set; } = false;

}
