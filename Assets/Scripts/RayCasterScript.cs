using TMPro;
using UnityEngine;

public class RayCasterScript : MonoBehaviour
{
    public TextMeshProUGUI Pos;

    void Update()
    {
        DoRaycast();
    }

    void DoRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitObj;

        if(Physics.Raycast(ray , out hitObj))
        {
            if (hitObj.collider.CompareTag("Tile"))
            {
                // if our ray hit tile, then show info else ignore.
                string gridPos = hitObj.transform.GetComponent<GridScript>().GetInfo();
                Display(gridPos);
            }
        }
        else
        {
            Display("-");
        }
    }

    void Display(string gridPos)
    {
        Pos.text = gridPos;
    }
}
