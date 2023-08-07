using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    private TurnManager turnManager;
    private PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = GetComponent<TurnManager>();
        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void StartRespawnProcess() {
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn() {
        yield return null;
        turnManager.StopTurns();
        playerMove.enabled = false;
        yield return new WaitForSeconds(0.5f);
        GridManager.ResetGrid();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
