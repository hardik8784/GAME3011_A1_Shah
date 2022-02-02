using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    private static MiniGame instance;

    public static MiniGame Instance { get { return instance; } }

    private System.Random random = new System.Random();

    public GameObject TilePrefab;
    public TMP_Text MessageText;

    // 2D array for the Tiles Placement
    public GameObject[,] grid = new GameObject[32, 32];

    float Reveal_Time_For_Board = 2.0f;

    /// <summary>
    /// [0,0] [0,1] ...........[0,31]
    /// .............................
    /// .............................
    /// .............................
    /// [31,0] [31,1] ........[31,31]
    /// </summary>
    public List<Vector2> ListofTiles = new List<Vector2>();


    public Color maxResource;
    public Color halfResource;
    public Color quarterResource;
    public Color @default;

    
    public int MaxResourceAmount;
  
    public int numberOfMaxResources;


    public TOOGLE_MODE Toogle_mode;

    public int scanModeClickCount = 6;
    public int maxScanCount = 6;

    public int ExtractCount = 3;
    public int ScanNumbers = 6;

    public int[] resourcesAmount = new int[12];
    public string[] resourceNames = new string[12];

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
    
        for (int row = 0; row < 32; row++)
        {
            for (int column = 0; column < 32; column++)
            {

                grid[row, column] = Instantiate(TilePrefab, this.transform );
                grid[row, column].GetComponent<CreateTile>().coordinate = new Vector2(row, column);
            }
        }

        AllocateTiles();
    }

  
    private void AllocateTiles()
    {
        Vector2 Vector = Vector2.zero;
        int icon = 0;
        for (int Counter = 0; Counter < numberOfMaxResources; Counter++)
        {
            Vector2 tileIndex = FindAvailableTile(Vector);

            if (tileIndex.x != -1)
            {

                int row = (int)tileIndex.x;
                int column = (int)tileIndex.y;
                Debug.Log(row + "," + column);
                for (int i = row - 2; i < row + 3; i++)
                {
                    for (int j = column - 2; j < column + 3; j++)
                    {
                        if (i == row && j == column)
                        {
                            grid[i, j].GetComponent<CreateTile>().fillTile(maxResource, icon, MaxResourceAmount);
                        }
                        else if ((i <= row + 1 && i >= row - 1) && (j <= column + 1 && j >= column - 1))
                        {
                            grid[i, j].GetComponent<CreateTile>().fillTile(halfResource, icon, MaxResourceAmount / 2);
                        }
                        else
                        {
                            grid[i, j].GetComponent<CreateTile>().fillTile(quarterResource, icon, MaxResourceAmount / 4);
                        }
                        ListofTiles.Add(new Vector2(i, j));
                    }
                }
                icon++;
                if (icon >= 12)
                    icon = 0;
            }
        }
    }
   
    private Vector2 FindAvailableTile(Vector2 Vector)
    {
        int row = 0, column = 0;
        bool isfilled = true;

        while (isfilled)
        {

            row = random.Next(2, 29);
            column = random.Next(2, 29);
            isfilled = false;
            for (int i = row - 2; i < row + 3; i++)
            {
                for (int j = column - 2; j < column + 3; j++)
                {
                    if (ListofTiles.Contains(new Vector2(i, j)))
                    {
                        isfilled = true;
                        break;

                    }

                }
            }
        }
        if (isfilled)
            return new Vector2(-1, -1);
        else
            return new Vector2(row, column);

    }
    
    /// <summary>
    /// Scan Mode 
    /// </summary>
    /// <param name="Vector"></param>
    public void ShowTilesScanMode(Vector2 Vector)
    {

        if (scanModeClickCount > 0)
        {
            scanModeClickCount--;
            int row = (int)Vector.x;
            int column = (int)Vector.y;
            for (int i = row - 1; i < row + 2; i++)
            {
                for (int j = column - 1; j < column + 2; j++)
                {
                    if ((j <= 31 && j >= 0) && (i <= 31 && i >= 0))
                        grid[i, j].GetComponent<CreateTile>().ToggleTileActivation(true);
                }
            }
        }
        else
        {
            MiniGame.Instance.MessageText.text = "You reached Maximum Scan Clicks, Sorry";

        }
    }

    /// <summary>
    /// Hide Tiles 
    /// </summary>
    /// <param name="show"></param>
    public void ChangeToggle(bool show)
    {
        for (int row = 0; row < 32; row++)
        {
            for (int column = 0; column < 32; column++)
            {
                grid[row, column].GetComponent<CreateTile>().ToggleTileActivation(show);
            }
        }
    }

    /// <summary>
    /// Reveal the Board for the amount of Seconds
    /// </summary>
    /// <returns></returns>
    public IEnumerator showGameBoard()
    {
        for (int row = 0; row < 32; row++)
        {
            for (int column = 0; column < 32; column++)
            {
                grid[row, column].GetComponent<CreateTile>().ToggleTileActivation(true);
            }
        }

        yield return new WaitForSeconds(Reveal_Time_For_Board);

        for (int row = 0; row < 32; row++)
        {
            for (int column = 0; column < 32; column++)
            {
                grid[row, column].GetComponent<CreateTile>().ToggleTileActivation(false);
            }
        }
    }
   
  /// <summary>
  /// ExtractTiles
  /// </summary>
  /// <param name="Vector"></param>
    public void ExtractTiles(Vector2 Vector)
    {
        if (ExtractCount > 0)
        {
            ExtractCount--;
            int row = (int)Vector.x;
            int column = (int)Vector.y;
            for (int i = row - 2; i < row + 3; i++)
            {
                for (int j = column - 2; j < column + 3; j++)
                {
                    if ((j <= 31 && j >= 0) && (i <= 31 && i >= 0))
                    {
                        if (i == row && j == column)
                        {
                            resourcesAmount[(int)grid[i, j].GetComponent<CreateTile>().resource] += grid[i, j].GetComponent<CreateTile>().amount;
                            if (grid[i, j].GetComponent<CreateTile>().isFilled)
                            {
                                MiniGame.Instance.MessageText.text = "Extracted " + grid[i, j].GetComponent<CreateTile>().amount + " " + resourceNames[(int)grid[i, j].GetComponent<CreateTile>().resource];

                                grid[i, j].GetComponent<CreateTile>().fillTile(@default, -1, 0);

                            }
                            else
                            {
                                MiniGame.Instance.MessageText.text = "You Extracted an empty tile";

                            }

                        }
                        else if (grid[i, j].GetComponent<CreateTile>().amount == MaxResourceAmount)
                        {
                            resourcesAmount[(int)grid[i, j].GetComponent<CreateTile>().resource] += MaxResourceAmount / 2;
                            grid[i, j].GetComponent<CreateTile>().fillTile(halfResource, (int)grid[i, j].GetComponent<CreateTile>().resource, MaxResourceAmount / 2);

                        }
                        else if (grid[i, j].GetComponent<CreateTile>().amount == MaxResourceAmount / 2)
                        {
                            grid[i, j].GetComponent<CreateTile>().fillTile(quarterResource, (int)grid[i, j].GetComponent<CreateTile>().resource, MaxResourceAmount / 4);
                            resourcesAmount[(int)grid[i, j].GetComponent<CreateTile>().resource] += MaxResourceAmount / 4;

                        }
                        else if (grid[i, j].GetComponent<CreateTile>().amount == MaxResourceAmount / 4)
                        {
                            resourcesAmount[(int)grid[i, j].GetComponent<CreateTile>().resource] += MaxResourceAmount / 4;
                            grid[i, j].GetComponent<CreateTile>().fillTile(@default, -1, 0);
                        }
                        else
                        {
                            grid[i, j].GetComponent<CreateTile>().fillTile(@default, -1, 0);


                        }

                    }

                }
            }

            //This function to reveal the neighbour tiles 
            for (int i = row - 2; i < row + 3; i++)
            {
                for (int j = column - 2; j < column + 3; j++)
                {
                    if ((j <= 31 && j >= 0) && (i <= 31 && i >= 0))
                        grid[i, j].GetComponent<CreateTile>().ToggleTileActivation(true);
                }
            }
        }
        else
        {
            MiniGame.Instance.MessageText.text = "You reached Maximum extraction Clicks, Sorry";

        }
    }
}

