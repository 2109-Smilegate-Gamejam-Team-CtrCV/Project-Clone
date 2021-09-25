using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Cell
{
    private void OnDestroy()
    {
        GameManager.Instance.RemoveCell(this);
    }
}
