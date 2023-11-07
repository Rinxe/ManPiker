using UnityEngine;

public class Tile : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public bool isWa = true;
    private GameManager gameManager;
    public bool[][] checkedGrid = new bool[5][];
    int locationX, locationY;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        (locationX, locationY) = splitName(gameObject.name);
        checkedGrid = gameManager.checkedGrid;
    }

    private void Update()
    {
        //chua toi uu
        checkedGrid[locationX][locationY] = isWa;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            isWa = false;
        };
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            isWa = true;
           
        };
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
   
}
