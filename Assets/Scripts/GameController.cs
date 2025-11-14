using System.ComponentModel;
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
    public Vector2 Round(Vector2 position)
    {
        return new Vector2(Mathf.Round(position.x),
    Mathf.Round(position.y));
    }

    public Transform TransformGridPosition(Vector2 position)
    {
        if (position.y > height - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)position.x, (int)position.y];
        }
    }

    public void UpdateGrid(PieceMove pieceTetris)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == pieceTetris.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

     foreach(Transform piece in pieceTetris.transform)
        {
            Vector2 position = Round(piece.position);
            if(position.y < height)
            {
                grid[(int)position.x, (int)position.y] = piece;
            }
        }
    }
}
