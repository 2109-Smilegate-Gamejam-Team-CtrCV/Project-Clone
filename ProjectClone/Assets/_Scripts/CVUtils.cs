using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CVUtils
{
    public static Vector2 position2D(this Transform tr)
    {
        return new Vector2(tr.position.x, tr.position.y);
    }

    public static float GetSqrDistance(this Vector2 pos, Vector2 targetPos)
    {
        float sqrDistance = (targetPos - pos).sqrMagnitude;
        return sqrDistance;
    }

    public static bool IsInRange(this Vector2 pos, Vector2 targetPos, float radius)
    {
        float squareRadius = radius * radius;
        float sqrDistance = pos.GetSqrDistance(targetPos);

        return squareRadius >= sqrDistance; // 제곱 거리 >= 실 거리
    }

    //public static void SetNextTime(this DateTime curTime, float seconds)
    //{
    //    curTime = DateTime.Now.AddSeconds(seconds);
    //}

    public static bool IsEnoughTime(this DateTime targetTime)
    {
        return targetTime <= DateTime.Now;
    }

    public static bool IsNullOrEmpty(this string str)
    {
        return str == null || str.Length < 1;
    }
}
