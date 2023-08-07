using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float timePerTurn = 0.5f;

    private PlayerMove playerMove;
    //public List<EnemyMove> enemyMoves = new List<EnemyMove>();

    // Start is called before the first frame update
    void Start()
    {
        playerMove = FindAnyObjectByType<PlayerMove>();
        StartCoroutine(WaitForTurn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForTurn() {
        while (true) {
            yield return new WaitForSeconds(timePerTurn);
            playerMove.Move();
        }
    }

    public void StopTurns() {
        StopAllCoroutines();
    }
}
