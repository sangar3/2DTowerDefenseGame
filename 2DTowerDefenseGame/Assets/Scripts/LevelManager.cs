using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// this script places each tile in the game
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs; //fields
    [SerializeField]
    private CameraMovement cameraMovement;
    private Point blueSpawn;
    private Point redSpawn;
    [SerializeField]
    private GameObject bluePortalPrefab;
    [SerializeField]
    private GameObject redPortalPrefab;
    public Dictionary<Point,Tile> Tiles { get; set; }


    public float TileSize //property to calculate how big our tiles are, this is used to place out of tiles on the correct postions
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    void Start()
    {
        CreateLevel();
    }

    
    void Update()
    {
        
    }




    private void CreateLevel() //code for automatically genertating a plot of a single tilemap when clicking play
    {
        Tiles = new Dictionary<Point, Tile>();


        // reading level doc 
        string[] mapData = readLevelText();
       
        //calculates the x map size 
        int mapX= mapData[0].ToCharArray().Length;

        //calculates the y map size
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        //finds the world start point(top letft corner of the screen)
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for(int y=0; y< mapY;y++) //y-pos
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)//x-pos
            {
                //places tiles
                PlaceTile(newTiles[x].ToString(),x,y,worldStart);
            }
        }
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;
        //sets the boundries for the camera
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();


    }

    private void PlaceTile(string tileType,int x, int y, Vector3 worldStart)
    {
        //turns string into number
        int tileIndex = int.Parse(tileType);

        //makes a new tile and makes a refernece to that tile in the newtile var
        Tile newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<Tile>();
        //change position of tile 


        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));

        Tiles.Add(new Point(x, y), newTile);

        

    }

    private string[] readLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');


    }

    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 0);
        Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<Tile>().WorldPosition, Quaternion.identity);

        redSpawn = new Point(17, 0);
        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<Tile>().WorldPosition, Quaternion.identity);

    }
}
