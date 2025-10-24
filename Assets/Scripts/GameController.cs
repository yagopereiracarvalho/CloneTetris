using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float speed;
    static int height = 20;
    static int width = 10;
    static Transform[,] grid = new Transform[width, height]; 
    public static GameController instance;
    public float Speed { get { return speed;} }


    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool InsideGrid(Vector2 position)
    {
        return ((int)position.x >= 0 && (int)position.x < width && (int)position.y >= 0);
    }
    public Vector2  Round(Vector2 position)
    {
        return new Vector2(Mathf.Round(position.x),
    Mathf.Round(position.y));
    }

}
