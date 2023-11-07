using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private int width, height;
    [SerializeField] private GameObject tilePrefab;
    private GameManager gameManager;
    public GameObject[,] objectGrid;


    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        width = gameManager.getWidth();
        height = gameManager.getHeight();
        objectGrid = gameManager.objectGrid;
}

    void Start()
    {
        GenGrid(); 
    }



    void GenGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
                spawnedTile.name = j+" "+i;
                objectGrid[j, i] = spawnedTile;

            }
        }
    }
   
}
