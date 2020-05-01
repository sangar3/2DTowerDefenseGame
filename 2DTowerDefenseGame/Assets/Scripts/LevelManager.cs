using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs; //fields
    [SerializeField]
    private CameraMovement cameraMovement;

    public float TileSize //property to calculate how big our tiles are, this is used to place out of tiles on the correct postions
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void CreateLevel() //code for automatically genertating a plot of a single tilemap when clicking play
    {
        string[] mapData = readLevelText();
        int mapX= mapData[0].ToCharArray().Length;
        int mapY= mapData.Length;
        Vector3 maxTile = Vector3.zero;

        //finds the world start point(top letft corner of the screen)
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for(int y=0; y< mapY;y++) //y-pos
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)//x-pos
            {

                maxTile = PlaceTile(newTiles[x].ToString(),x,y,worldStart);
            }
        }
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

    }

    private Vector3 PlaceTile(string tileType,int x, int y, Vector3 worldStart)
    {
        //turns string into number
        int tileIndex = int.Parse(tileType);

        //makes a new ile and makes a refernece to that tile in the newtile var
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        //change position of tile 
        newTile.transform.position = new Vector3(worldStart.x+ (TileSize * x), worldStart.y - (TileSize * y), 0);
        return newTile.transform.position;

    }

    private string[] readLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');


    }
}
