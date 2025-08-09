using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstacle;

    [SerializeField]
    private Scriptable scriptable;

    #region Instance creation
    public static ObstacleManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    int cols;
    private void Start()
    {
        cols = 10;
        CheckScriptableObjectData();
    }

    void CheckScriptableObjectData()
    {
        // we want to have obstacles where the scriptable object value is false

        for(int i=0; i<100; i++)
        {
            if(!scriptable.GetValue(i))
            {
                GenerateObstacle(i);
            }
        }
    }

    void GenerateObstacle(int ind)
    {
        // changing 1d ind to 2d values below
        int row = ind / cols;
        int col = ind % cols;

        GameObject curObstacle = Instantiate(obstacle, new Vector3(row-4.5f,0,col-4.5f), Quaternion.identity);
    }

    int GetInd(int r, int c)
    {
        // converting 2d matrix values into 1d array ind  because scriptable object is having 1d array, not 2d
        return r * cols + c;
    }

    public bool CanWalkOn(int r , int c)
    {
        int ind = GetInd(r,c);

        return scriptable.WalkPath[ind] == true;
    }
}
