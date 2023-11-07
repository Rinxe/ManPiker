using System.Collections.Generic;
using UnityEngine;
using static ShortestPathFinder;

public class BotControl : MonoBehaviour
{
    public bool isMove = false;
    bool isDone = false;
    private bool isSelected = false;
    int locationX, locationY;
    public bool[][] checkedGrid = new bool[5][];
    private GameManager gameManager;
    public List<GameObject> liTrans;
    private int targetIndex = 0;
    private float speed = 2f;
    private bool isActive = false;
    [SerializeField] private Animator animator;
    private bool isGoal = false;
    private GameObject targetLobbyPos;
    public string color;
    private bool isGotLobby = false;
    

    private Vector3[] moveDirs = new Vector3[4]
    {
        new Vector3(0f,0f,1f),//up
        new Vector3(0f,0f,-1f),//down
        new Vector3(-1f,0f,0f),//left
        new Vector3(1f,0f,0f),//right
    };


    private void Start()
    {
        (locationX, locationY) = splitName(gameObject.name);
        gameManager = GameObject.FindObjectOfType<GameManager>();
        checkedGrid = gameManager.checkedGrid;
        color = gameObject.name.Trim().Split(' ')[0];
      
    }

    private void Update()
    {
        

        if (Input.GetMouseButtonDown(0) && isSelected && !isActive && !isDone&&!gameManager.onTask)
        {

            var result = FindShortestPath(checkedGrid, locationX, locationY, 5, 2);
            if (result.steps >= 0)
            {

                if (liTrans.Count == 0) getTransList(string.Join(" ", result.path));
                isMove = true;
                animator.SetBool("isMoving", true);
                isActive = true;
                gameManager.onTask = true;

            }
            else
            {
                Debug.Log("No path found.");
            }

        }

        if (isMove && targetIndex <= liTrans.Count)
        {

           
            transform.position = Vector3.MoveTowards(transform.position, liTrans[targetIndex].transform.position, speed * Time.deltaTime);

            Vector3 direction = (liTrans[targetIndex].transform.position - transform.position).normalized;
            float targetYRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetYRotation, 0);

            if (transform.position == liTrans[targetIndex].transform.position)
            {
                targetIndex++;
            }
            if (targetIndex == liTrans.Count)
            {
                isMove = false; isActive = false;gameManager.onTask= false;
                animator.SetBool("isMoving", false);
                foreach (GameObject item in liTrans)
                {
                    Destroy(item);

                };

            };

        }

        if (isGoal && !isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLobbyPos.transform.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
            gameManager.onTask = true;
            Vector3 direction = (targetLobbyPos.transform.position - transform.position).normalized;
            float targetYRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetYRotation, 0);
            isActive = true;
            if (transform.position == targetLobbyPos.transform.position)
            {
                isActive = false;
                animator.SetBool("isMoving", false);
                isGoal = false;
                isDone = true;gameManager.onTask = false;
            }


        }


        GameObject lobbyBus = gameManager.getBusNow();
        if (lobbyBus != null)
        {
            if (isDone && lobbyBus.GetComponent<BusHandler>().color == color)
            {
                transform.position = Vector3.MoveTowards(transform.position, lobbyBus.transform.position, speed * Time.deltaTime);
                animator.SetBool("isMoving", true);gameManager.onTask = true;
            }
        }

    }

    private void OnMouseEnter()
    {
        isSelected = true;

    }

    private void OnMouseExit()
    {
        isSelected = false;
    }

    public (int, int) splitName(string inputString)
    {
        int firstNumber = 0;
        int secondNumber = 0;
        string[] parts = inputString.Split(' ');

        if (parts.Length == 2 && int.TryParse(parts[0], out firstNumber) && int.TryParse(parts[1], out secondNumber))
        {
            return (firstNumber, secondNumber);
        }

        return (0, 0);
    }

    private void getTransList(string dir)
    {
        string[] directions = dir.Split(' ');

        GameObject temp = new GameObject();

        foreach (string direction in directions)

        {
            switch (direction.ToLower())
            {
                case "up":
                    {
                        if (liTrans.Count == 0)
                        {
                            temp.transform.position = this.transform.position;
                            temp.transform.rotation = this.transform.rotation;
                            temp.transform.localScale = this.transform.localScale;
                        }
                        else
                        {
                            temp.transform.position = liTrans[liTrans.Count - 1].transform.position;
                            temp.transform.rotation = liTrans[liTrans.Count - 1].transform.rotation;
                            temp.transform.localScale = liTrans[liTrans.Count - 1].transform.localScale;
                        }
                        GameObject nPos = new GameObject();
                        nPos.transform.position = temp.transform.position;
                        nPos.transform.rotation = temp.transform.rotation;
                        nPos.transform.localScale = temp.transform.localScale;
                        nPos.transform.position += moveDirs[0];

                        liTrans.Add(nPos);
                        break;
                    }
                case "down":
                    {
                        if (liTrans.Count == 0)
                        {
                            temp.transform.position = this.transform.position;
                            temp.transform.rotation = this.transform.rotation;
                            temp.transform.localScale = this.transform.localScale;
                        }
                        else
                        {
                            temp.transform.position = liTrans[liTrans.Count - 1].transform.position;
                            temp.transform.rotation = liTrans[liTrans.Count - 1].transform.rotation;
                            temp.transform.localScale = liTrans[liTrans.Count - 1].transform.localScale;
                        }
                        GameObject nPos = new GameObject();
                        nPos.transform.position = temp.transform.position;
                        nPos.transform.rotation = temp.transform.rotation;
                        nPos.transform.localScale = temp.transform.localScale;
                        nPos.transform.position += moveDirs[1];

                        liTrans.Add(nPos);
                        break;
                    }
                case "left":
                    {
                        if (liTrans.Count == 0)
                        {
                            temp.transform.position = this.transform.position;
                            temp.transform.rotation = this.transform.rotation;
                            temp.transform.localScale = this.transform.localScale;
                        }
                        else
                        {
                            temp.transform.position = liTrans[liTrans.Count - 1].transform.position;
                            temp.transform.rotation = liTrans[liTrans.Count - 1].transform.rotation;
                            temp.transform.localScale = liTrans[liTrans.Count - 1].transform.localScale;
                        }
                        GameObject nPos = new GameObject();
                        nPos.transform.position = temp.transform.position;
                        nPos.transform.rotation = temp.transform.rotation;
                        nPos.transform.localScale = temp.transform.localScale;
                        nPos.transform.position += moveDirs[2];
                        liTrans.Add(nPos);
                        break;
                    }
                case "right":
                    {
                        if (liTrans.Count == 0)
                        {
                            temp.transform.position = this.transform.position;
                            temp.transform.rotation = this.transform.rotation;
                            temp.transform.localScale = this.transform.localScale;
                        }
                        else
                        {
                            temp.transform.position = liTrans[liTrans.Count - 1].transform.position;
                            temp.transform.rotation = liTrans[liTrans.Count - 1].transform.rotation;
                            temp.transform.localScale = liTrans[liTrans.Count - 1].transform.localScale;
                        }
                        GameObject nPos = new GameObject();
                        nPos.transform.position = temp.transform.position;
                        nPos.transform.rotation = temp.transform.rotation;
                        nPos.transform.localScale = temp.transform.localScale;
                        nPos.transform.position += moveDirs[3];
                        liTrans.Add(nPos);
                        break;
                    }
            }
        }




    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Goal")
        {

            if (gameManager.checkLobby()&&!isGotLobby)
            {
                targetLobbyPos = gameManager.getFreeSlot();
                isGoal = true;
                isGotLobby = true;
            };
        }
        if (collision.gameObject.tag == "Tile")
        {
            gameObject.name = collision.gameObject.name;
            (locationX, locationY) = splitName(gameObject.name);
        }
        if (collision.gameObject.tag == "Bus")
        {
            gameManager.onTask = false;
            Destroy(gameObject);
        }
    }

}
