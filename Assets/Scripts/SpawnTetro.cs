using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SpawnTetro : MonoBehaviour
{
    [SerializeField] Transform[] createPiece;
    [SerializeField] List<GameObject> showPiece;
    [SerializeField] int nextPiece;
    [SerializeField] bool NextPieceSend;
    float cout = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var piece in createPiece)
        {
            piece.gameObject.SetActive(true);
        }
        nextPiece = Random.Range(0, showPiece.Count);
        NextPiece();
    }

    // Update is called once per frame
    void Update()
    {
        if (NextPieceSend && cout <= 0)
        {
            NextPiece();
            cout = 1;
        }
        if (cout > 0)
        {
            cout -= Time.deltaTime;
        }

    }
    public void NextPiece()
    {
        Instantiate(createPiece[nextPiece], transform.position,
        Quaternion.identity);
        nextPiece = Random.Range(0, showPiece.Count);
        for (int i = 0; i < showPiece.Count; i++)
        {
            showPiece[i].SetActive(false);
        }
        showPiece[nextPiece].SetActive(true);
    }
    public void SetNextPieceStatus(bool status)
    {
        NextPieceSend = status;
    }
}
