using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Up, Down, Left, Right
}

public class PlayerMove : MonoBehaviour
{
    public float moveSmoothing = 10;
    public Animator eyesAnim;
    public Animator bodyAnim;

    private Direction facingDirection = Direction.Up;
    private RestartManager restartManager;

    // Start is called before the first frame update
    void Start()
    {
        GridManager.AddToGrid(gameObject);
        restartManager = FindObjectOfType<RestartManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            facingDirection = Direction.Up;
            eyesAnim.SetTrigger("LookUp");
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            facingDirection = Direction.Down;
            eyesAnim.SetTrigger("LookDown");
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            facingDirection = Direction.Left;
            eyesAnim.SetTrigger("LookLeft");
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            facingDirection = Direction.Right;
            eyesAnim.SetTrigger("LookRight");
        }
    }

    public void Move() {
        Vector2Int gridPosition = GridManager.GetGridPosition(gameObject);
        Vector2Int gridMovePosition = gridPosition;
        switch (facingDirection) {
            case Direction.Up:
                gridMovePosition.y++;
                break;
            case Direction.Down:
                gridMovePosition.y--;
                break;
            case Direction.Left:
                gridMovePosition.x--;
                break;
            case Direction.Right:
                gridMovePosition.x++;
                break;
        }
        List<GameObject> thingsAtPoint = GridManager.LookInGrid(gridMovePosition);
        if (thingsAtPoint == null) {
            GridManager.MoveObjectInGrid(gameObject, gridMovePosition, gridPosition);
            //bodyAnim.SetTrigger("Move");
            StopAllCoroutines();
            StartCoroutine(SlideMove(gridPosition, gridMovePosition));
        } else {
            if (thingsAtPoint[0].CompareTag("Wall")) {
                //StopAllCoroutines();
                //StartCoroutine(SlideMove(gridPosition, gridMovePosition));
            }
            if (thingsAtPoint[0].CompareTag("Fire")) {
                GridManager.MoveObjectInGrid(gameObject, gridMovePosition, gridPosition);
                StopAllCoroutines();
                StartCoroutine(SlideMove(gridPosition, gridMovePosition));
                restartManager.StartRespawnProcess();
                StartShrink();
            }
        }
    }

    IEnumerator SlideMove(Vector2Int oldPosition, Vector2Int newPosition) {
        Vector2 curPositon = oldPosition;
        while (Vector2.Distance(curPositon, newPosition) > 0.01f) {
            curPositon = Vector2.MoveTowards(curPositon, newPosition, moveSmoothing * Time.deltaTime);
            transform.position = new Vector3(curPositon.x, 0.5f, curPositon.y);
            yield return null;
        }
        transform.position = new Vector3(newPosition.x, 0.5f, newPosition.y);
    }

    public void StartShrink() {
        StartCoroutine(ShrinkToNothing());
    }

    IEnumerator ShrinkToNothing() {
        yield return new WaitForSeconds(0.1f);
        while(true) {
            yield return null;
            transform.Rotate(Vector3.up * 3);
            transform.localScale *= 0.98f;
        }
    }
}
