using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float speed;
    static int height = 20;
    static int width = 10;
    static Transform[,] grid = new Transform[width, height]; 
    private int[] indexLine = new int[4];
    public static GameController instance;
    public float Speed { get { return speed;} }


    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 4 ; i++) 
        {
            indexLine[i] = -1;
        } 
            
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

    public bool FullLine(int y)
    {
        for(int x = 0; x <width; x++) 
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteSquare(int y)
    {
        for(int x = 0; x < width; x++)
        {
            grid[x,y].GetComponent<SpriteRenderer>().enabled = false;
            Destroy(grid[x,y].gameObject);
            grid[x,y]= null;
        }

    }

    public void Deleteline()
    {
        for(int y = 0; y < height; y++)
        {
            if (FullLine(y))
            {
                for(int i = 0; i < indexLine.Length ; i++)
                {
                    if(indexLine[i]< 0)
                    {
                        indexLine [i] = y ;
                        break;
                    }
                }
                DeleteSquare(y);
                y--;
            }
        }
        for(int i = indexLine.Length - 1; i >= 0; i--)
        {
            if (indexLine[i] >= 0)
            {
                MoveAllLineDown(indexLine[i] + 1);
                indexLine[i] = - 1;
            }
        }
    }

    public void MoveLineDown(int y)
    {
        for(int x = 0; x < width ; x++)
        {
            if(grid[x,y] != null)
            {
                grid[x,y -1 ] = grid[x,y];
                grid[x,y] = null;
                grid[x,y - 1 ].position += new Vector3(0, -1, 0);
            }
        } 
    }
    public void MoveAllLineDown(int y)
    {
        for(int i = y; i <height ; i++) 
        {
            MoveLineDown(i);    
        }
    }
    
 }
