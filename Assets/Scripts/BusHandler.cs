using UnityEngine;

public class BusHandler : MonoBehaviour
{
    [SerializeField]
    private Material[] busMaterial;
    Renderer rend;
    private int rendCount = 0;
    public bool isMove = false;
    Transform nextPos;
    private float speed = 2.0f;
    public int conPlayer = 0;
    
    public string color;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rend = GetComponent<Renderer>();
        rend.material = busMaterial[0];
        color = gameObject.name.Trim().Split(' ')[0];
        nextPos = transform;
        getNextPos(); 
    }


    void Update()
    {
        if (rendCount == 3 && gameObject==gameManager.getBusNow()) { gameManager.BusMove(); }

        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
            if (transform.position.x == nextPos.position.x)
            {
                isMove = false;
                getNextPos();
            };
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bot" && rendCount < 3)
        {
            rendCount++;
            rend.material = busMaterial[rendCount];

        }
        if (collision.gameObject.tag == "Endln")
        {
            Destroy(gameObject);
        }
    }

    public void getNextPos()
    {
        GameObject temp = new GameObject();
        temp.transform.position = transform.position;
        temp.transform.rotation = transform.rotation;
        temp.transform.localScale = transform.localScale;
        temp.transform.position = new Vector3(temp.transform.position.x - 7.5f, temp.transform.position.y, temp.transform.position.z);
        nextPos = temp.transform;

    }

}
