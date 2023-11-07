using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool onTask =false;
    public GameObject[,] objectGrid = new GameObject[5, 7];
    public bool[][] checkedGrid = new bool[6][]
        {
            new bool[] { true, true, true, true, true },
            new bool[] { true, true, true, true, true },
            new bool[] { true, true, true, true, true },
            new bool[] { true, true, true, true, true },
            new bool[] { true, true, true, true, true },
            new bool[] { true, true, true, true, true },
        };
    public GameObject[] lobbyObject;
    public int width, height;
    public List<GameObject> busList = new List<GameObject>();
    [SerializeField]
    private GameObject currentBusPos;
    private GameObject currentBusNow;


    private void Start()
    {
        GameObject[] busObjects = GameObject.FindGameObjectsWithTag("Bus");
        foreach (GameObject busObject in busObjects)
        {
            busList.Add(busObject);
            if (CheckCollision(busObject, currentBusPos))
            {
                currentBusNow = busObject;
            }
        }

    }

    private void Update()
    {
        
        if (busList.Count == 0)
        {
            //win 
        }
    }

    public int getWidth()
    {
        return width;
    }
    public int getHeight()
    {
        return height;
    }

    public bool checkLobby()
    {
        int check = 0;
        foreach (var item in lobbyObject)
        {
            if (item != null)
            {
                if (item.GetComponent<Tile>().isWa)
                {
                    check++;
                }
               
            }

        }
        if (check == 0) return false;
        return true;
    }

    public GameObject getFreeSlot()
    {
        foreach (var item in lobbyObject)
        {

            if (item != null)
            {
                if (item.GetComponent<Tile>().isWa)
                {
                    item.GetComponent<Tile>().isWa = false;
                 //   Debug.Log("Free : "+item.transform.position);
                    return item;

                }
            }
        }
        Debug.Log("Lobby full");
        //game Over
        return null;
    }

    public void BusMove()
    {
        foreach (GameObject go in busList)
        {
            if (go != null)
            {
                go.GetComponent<BusHandler>().isMove = true;
            }
        }
    }
    private bool CheckCollision(GameObject obj1, GameObject obj2)
    {
        Collider collider1 = obj1.GetComponent<Collider>();
        Collider collider2 = obj2.GetComponent<Collider>();
        if (collider1 != null && collider2 != null)
        {
            return collider1.bounds.Intersects(collider2.bounds);
        }
        return false;
    }

    public GameObject getBusNow()
    {
        foreach (GameObject go in busList)
        {
            if (go != null)
            {
                if (CheckCollision(go, currentBusPos)) {  return go; }
                    
            } 
        }
        return currentBusNow;
    }

}
