using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateTile : MonoBehaviour
{
    public Image icon;
    public Image CoverImage;

    public Vector2 coordinate;
    public Image tileImage;
    public Color tileColor;
    public Sprite[] IconImages;
    public int amount;
    public LIST_OF_RESOURCE resource;
    public bool isFilled = false;

    // Start is called before the first frame update
    void Start()
    {
        icon.gameObject.SetActive(false);

        CoverImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillTile(Color color, int image, int amt)
    {
        amount = amt;
        tileColor = color;
        tileImage.color = tileColor;

        isFilled = true;
        if (image >= 0)
        {
            resource = (LIST_OF_RESOURCE)image;
            icon.sprite = IconImages[image];
        }
        else
        {
            icon.sprite = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (MiniGame.Instance.Toogle_mode == TOOGLE_MODE.SCAN_MODE)
        {

            //MiniGame.Instance.ShowTilesScanMode(coordinate);
        }
        else
        {
           // MiniGame.Instance.extractTiles(coordinate);
        }
    }

    public void ToggleTileActivation(bool toggle)
    {
        icon.gameObject.SetActive(toggle);
        CoverImage.gameObject.SetActive(!toggle);

    }
}
