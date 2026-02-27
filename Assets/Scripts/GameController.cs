using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField]int score = 0;
    [SerializeField]int lineDeleted;
    [SerializeField]int difficulty = 1;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] GameObject pauseGo;
    static int height = 20;
    static int width = 10;
    static Transform[,] grid = new Transform[width, height]; 
    private int[] indexLine = new int[4];
    [SerializeField] bool isPaused;
    public static GameController instance;
    public float Speed { get { return speed;} }
    public bool IsPaused { get { return isPaused;} }
    public int Difficulty{get{return difficulty;}}


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
        scoreText.text = UpdateScore();
        difficultyText.text = difficulty.ToString();
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
            grid[x,y].GetComponentInChildren<ParticleSystem>().Play();
            Destroy(grid[x,y].gameObject, 1f);
            grid[x,y]= null;
        }

    }

    public void Deleteline()
    {
        // for(int y = 0; y < height; y++)
        // {
        //     if (FullLine(y))
        //     {
        //         for(int i = 0; i < indexLine.Length ; i++)
        //         {
        //             if(indexLine[i]< 0)
        //             {
        //                 indexLine [i] = y ;
        //                 break;
        //             }
        //         }
        //         DeleteSquare(y);
        //         y--;
        //     }
        // }
        // for(int i = indexLine.Length - 1; i >= 0; i--)
        // {
        //     if (indexLine[i] >= 0)
        //     {
        //         MoveAllLineDown(indexLine[i] + 1);
        //         indexLine[i] = - 1;
        //     }
        // }
        StartCoroutine(WaitingTime());
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

    public string UpdateScore()
    {
        string scoreTempText = "";

        if (score == 0)
        {
            scoreTempText ="000000";
        }
        else if (score > 0 && score < 100)
        {
            scoreTempText =  "0000" + score.ToString();
        }
          else if (score >= 100 && score < 1000)
        {
            scoreTempText =  "000" + score.ToString();
        }
          else if (score >= 1000 && score < 10000)
        {
            scoreTempText =  "00" + score.ToString();
        }
          else if (score >= 10000 && score < 100000)
        {
            scoreTempText =  "0" + score.ToString();
        }
          else 
        {
            scoreTempText = score.ToString();
        }

        return scoreTempText;
    }

    public void AddScore(int points)
    {
        score += points;
    }
    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            pauseGo.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseGo.SetActive(true);
            isPaused =(true);
        }
    }
    
    IEnumerator WaitingTime()
    {
        for(int y = 0; y < height; y++)
        {
            if (FullLine(y))
            {
                isPaused = true;
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

        yield return new WaitForSeconds(1.1f);

        int points = 0;
        int scoreTotal = 0; 

        for(int i = indexLine.Length - 1; i >= 0; i--)
        {
            if (indexLine[i] >= 0)
            {
                points++;
                MoveAllLineDown(indexLine[i] + 1);
                indexLine[i] = - 1;
            }
        }

        switch (points)
        {
            case 1: 
            scoreTotal = 100 * difficulty;
            StartCoroutine(PointsText(scoreTotal));
            score += scoreTotal;
            break;
              case 2: 
            scoreTotal = 300 * difficulty;
            StartCoroutine(PointsText(scoreTotal));
              score += scoreTotal;
            break;
              case 3: 
            scoreTotal = 600 * difficulty;
            StartCoroutine(PointsText(scoreTotal));
              score += scoreTotal;
            break;
              case 4: 
            scoreTotal = 1000 * difficulty;
            StartCoroutine(PointsText(scoreTotal));
              score += scoreTotal;
            break;
        }
        lineDeleted += points;
        if(lineDeleted >= 10)
        {
            lineDeleted -= 10;
            difficulty++;
        }

        isPaused = false;
    }

    IEnumerator PointsText(int points)
    {
        pointsText.enabled = true;
        pointsText.text = "+" + points.ToString();
        yield return new WaitForSeconds(.5f);
        pointsText.enabled = false;
    }
 }
