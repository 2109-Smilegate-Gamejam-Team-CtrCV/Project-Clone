using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Cell
{
    private void OnDestroy()
    {
        GameManager.Instance.RemoveCell(this);
    }
}
