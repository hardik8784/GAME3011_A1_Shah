using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGame : MonoBehaviour
{
    private static MiniGame instance;

    public static MiniGame Instance { get { return instance; } }

    private System.Random random = new System.Random();

    public GameObject TilePrefab;
    public TMP_Text MessageText;

    public TOOGLE_MODE Toogle_mode;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Toogle_mode = TOOGLE_MODE.EXTRACT_MODE;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
