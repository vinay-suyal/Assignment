using UnityEngine;

public class GridScript : MonoBehaviour
{

    public void SetInfo(Vector3 pos , int r , int c)
    {
        transform.position = pos;
    }

    public string GetInfo()
    {
        return transform.position+"";
    }

    private void OnMouseEnter()
    {
        // playing sound if mouse hover.
        SoundManager.instance.PlaySelectSound();
    }

}
