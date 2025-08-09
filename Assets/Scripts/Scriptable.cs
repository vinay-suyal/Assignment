using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Scriptable : ScriptableObject
{
    public List<bool> WalkPath = new();

    public int grid_size = 10;  


    public void CreateData()
    {
        if (WalkPath.Count != 100)
        {
            for(int i=0; i<grid_size*grid_size; i++)
            {
                WalkPath.Add(true);
            }
        }

        CheckData();
    }

    void CheckData()
    {
        for(int i=0; i<grid_size*grid_size; i++)
        {
            if (WalkPath[i] == false)
            {
                WalkPath[i] = true;
            }
        }
    }

    public void UpdateData(int ind)
    {
        Debug.Log("Value updated");
        bool curval = WalkPath[ind];
        WalkPath[ind] = !curval;
    }

    public bool GetValue(int ind)
    {
        return WalkPath[ind];
    }
}
