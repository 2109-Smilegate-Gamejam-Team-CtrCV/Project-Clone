using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class Building : MonoBehaviour, ICell
{
    [SerializeField]
    private Vector2Int position;

    [SerializeField]
    private List<Vector2Int> patterns;

    public bool IsExist(Vector2Int pos)
    {
        return !patterns.Any(p => p + position == pos);
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        foreach (var pattern in patterns)
        {
            Gizmos.color = Color.green;
            Vector3 vector3 = new Vector3(pattern.x, pattern.y);
            Gizmos.DrawCube(transform.position + vector3, Vector2.one);
        }
    }
#endif
}
