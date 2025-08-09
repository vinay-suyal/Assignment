using System.Collections;
using UnityEngine;

public interface AiScript
{
    IEnumerator Move(Vector3 dest);
}
