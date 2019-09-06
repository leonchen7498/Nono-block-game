using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class blocks : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        //Use Vector3Int to place a tile via the script
        /*Tile tile = new Tile();
        tile.sprite = sprite;
        tilemap.SetTile(new Vector3Int(1,0,0), tile);
        tilemap.RefreshAllTiles();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
