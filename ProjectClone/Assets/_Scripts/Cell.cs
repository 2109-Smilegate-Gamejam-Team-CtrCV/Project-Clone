using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int position;

    [SerializeField]
    public List<Vector2Int> patterns;

    public bool IsExist(Cell cell, Vector2Int pos)
    {
        bool flag = true;

        foreach (var item in patterns)
        {
            foreach (var item1 in cell.patterns)
            {
                if (item + position == item1 + pos)
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
            Gizmos.color = new Color(0,1,0,0.5f);
            Vector3 vector3 = new Vector3(pattern.x, pattern.y);
            Gizmos.DrawCube(transform.position + vector3 + Vector3.one / 2, Vector2.one);
        }
    }
#endif
}
