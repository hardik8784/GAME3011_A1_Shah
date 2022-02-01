using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGame : MonoBehaviour
{
    private static MiniGame instance;

    public static MiniGame Instance { get { return instance; } }

   
    public GameObject TilePrefab;
    public TMP_Text MessageText;

    // 2D array that is filled with tiles
    public GameObject[,] grid = new GameObject[32, 32];

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
    
        for (int r = 0; r < 32; r++)
        {
            for (int c = 0; c < 32; c++)
            {

                grid[r, c] = Instantiate(TilePrefab, this.transform );
                grid[r, c].GetComponent<CreateTile>().coordinate = new Vector2(r, c);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
