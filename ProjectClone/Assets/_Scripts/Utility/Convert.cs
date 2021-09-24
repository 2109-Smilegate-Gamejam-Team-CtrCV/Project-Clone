using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utility
{
    public static Vector2Int World2Grid(Vector3 vector3)
    {
        Vector2Int vector2 = new Vector2Int(); 
        vector2.x = Mathf.FloorToInt(vector3.x);
        vector2.y = Mathf.FloorToInt(vector3.x);
        return vector2;
    }

    public static Vector3 Grid2World(Vector2Int grid)
    {
        Vector3 vector3 = new Vector3(grid.x, grid.y);
        return vector3;
    }
}
