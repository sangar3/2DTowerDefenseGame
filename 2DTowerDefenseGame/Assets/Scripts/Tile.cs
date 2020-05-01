using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Point GridPosititon { get; private set; }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPos)
    {
        this.GridPosititon = gridPos;
        transform.position = worldPos;


    }
}
