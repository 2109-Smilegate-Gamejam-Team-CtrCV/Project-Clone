using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Vector2Int position;

    [SerializeField]
    private List<Vector2Int> patterns;

    public bool IsExist(Building building, Vector2Int pos)
    {
        bool flag = true;

        foreach (var item in patterns)
        {
            foreach (var item1 in building.patterns)
            {
                if(item + position == item1 + pos)
                    flag = false;
            }
        }
        return flag;
    }


#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        foreach (var pattern in patterns)
        {
            Gizmos.color = Color.green;
            Vector3 vector3 = new Vector3(pattern.x, pattern.y);
            Gizmos.DrawCube(transform.position + vector3 + Vector3.one/ 2, Vector2.one);
        }
    }
#endif
}
