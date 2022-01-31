using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public PlayerBehaviour Player;
    public bool startGame =false;
    public GameObject MiniGame;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerBehaviour>();
        MiniGame.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Player !=null)
        {
            startGame = true;
            MiniGame.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(Player != null)
        {
            startGame = false;
            MiniGame.SetActive(false);
        }
    }
}
