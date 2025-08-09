using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour, AiScript
{
    public static EnemyScript instance;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        transform.position = new Vector3(-4.5f, 0, -4.5f);
    }

    float timeBtwCells = 0.35f;

    public IEnumerator Move(Vector3 destPos)
    {
        Vector3 startPos = transform.position;
        float st = 0, tt = timeBtwCells;
        
        while (st < tt)
        {
            st += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, destPos, st / tt);
            yield return null;
        }

        transform.position = destPos;
    }

}
