using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    bool playerWalking;
    public Vector2Int destinationCell;
    int p_row;
    int p_col;

    private void Start()
    {
        p_row = 0;
        p_col = 0;
        playerWalking = false;
        transform.position = new(-4.5f, 0, -4.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // player move when space is pressed
            FindPath();
        }
    }

    Dictionary<Vector2Int, Vector2Int> cameFrom = new();

    
    void FindPath()
    {
        Vector2Int startCell = new(p_row, p_col); 
        bool[,] visited = new bool[10, 10];
        Dictionary<Vector2Int, Vector2Int> cameFrom = new();

        // using queue for B.F.S. as BFS algo is great to search in 2d matrix

        Queue<Vector2Int> q = new();
        
        // intializing queue with start values
        q.Enqueue(startCell);
        visited[startCell.x, startCell.y] = true;

        //  adjacent cells
        int[] rows = { -1, 0, 1, 0 }; // up, right, down, left
        int[] cols = { 0, 1, 0, -1 };

        while (q.Count > 0)
        {
            // grabbing an item from queue.
            Vector2Int curCell = q.Dequeue();

            // checking is it is the destination
            if (curCell == destinationCell)
            {
                List<Vector2Int> path = CorrectPath(cameFrom, startCell, destinationCell);
                StartCoroutine(MovePlayer(path));
                return;
            }

            // if not destination, then check for adjacent cells 
            for (int i = 0; i < 4; i++)
            {
                int r = curCell.x + rows[i];
                int c = curCell.y + cols[i];
                Vector2Int nextCell = new(r, c);

                if (r >= 0 && r < 10 && c >= 0 && c < 10 && ObstacleManager.instance.CanWalkOn(r, c) 
                    && !visited[r, c])
                {
                    // checking conditions to make sure we step on the valid cell
                    visited[r, c] = true;
                    cameFrom[nextCell] = curCell;
                    q.Enqueue(nextCell);
                }
            }
        }
    }

    List<Vector2Int> CorrectPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int start, Vector2Int end)
    {
        // just arranging path properly.
        print("reconstructing path");
        List<Vector2Int> path = new();
        Vector2Int current = end;

        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        path.Add(start);
        path.Reverse();
        return path;
    }

    float timeBtwCells = 0.25f;

    IEnumerator MovePlayer(List<Vector2Int> path)
    {
        Vector2Int playerFinalPos = path[path.Count - 1];

        for (int i=0; i<path.Count; i++)
        {
            // moving player through each cell of the path.
            print("moving player to " + path[i].x  + " , " + path[i].y);
            p_row = path[i].x;
            p_col = path[i].y;
            Vector3 destPos = new(path[i].x - 4.5f, 0, path[i].y - 4.5f);
            yield return Move(destPos);

            if(i != path.Count-1)
            {
                // giving my last value to enemy to follow player

                GameObject enemy = EnemyScript.instance.gameObject;

                AiScript curInterface = enemy.GetComponent<AiScript>();

                if(curInterface != null)
                    StartCoroutine(curInterface.Move(destPos));
            }
        }
    }

    IEnumerator Move(Vector3 destPos)
    {
        // lerping player here.

        SoundManager.instance.PlayStepSound();

        Vector3 startPos = transform.position;
        float st = 0 , tt = timeBtwCells;
        transform.GetChild(0).GetComponent<Animation>().Play("Jump");
        
        while (st < tt)
        {
            st += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, destPos, st / tt);
            yield return null;
        }

        transform.position = destPos;
    }
}
