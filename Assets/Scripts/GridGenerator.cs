using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject grid;
    int gridSize;
    
    void Start()
    {
        gridSize = 10;
        GenerateGrids();
    }

    void GenerateGrids()
    {
        // creating 10*10 grid 

        for(int i=0; i<gridSize; i++)
        {
            for(int j=0; j<gridSize; j++)
            {
                GameObject curGrid = Instantiate(grid, transform);

                Vector3 spawnPos = new Vector3(i - 4.5f, 0, j - 4.5f);
               
                curGrid.GetComponent<GridScript>().SetInfo(spawnPos , i, j);
            }
        }
    }
    
}
