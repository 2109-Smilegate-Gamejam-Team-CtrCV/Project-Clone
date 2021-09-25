using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillGridInitializer : MonoBehaviour
{
    private int[,] _arr = {
        {0, 0, 1, 0, 0},
        {0, 2, 3, 4, 0},
        {5, 6, 7, 8, 9},
        {0, 10, 11, 12, 0},
        {0, 0, 13, 0, 0}
    };

    private static readonly (int, int)[] ArrayDirection =
    {
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0)
    };

    [SerializeField] private SkillCell[] cells;
    
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _arr.GetLength(0); i++)
        {
            for (int j = 0; j < _arr.GetLength(1); j++)
            {
                if (_arr[i, j] > 0)
                {
                    foreach (var valueTuple in ArrayDirection.Where(pair =>
                    {
                        var newI = i + pair.Item1;
                        var newJ = j + pair.Item2;

                        return newI >= 0 && newI < _arr.GetLength(0) && newJ >= 0 && newJ < _arr.GetLength(1);
                    }))
                    {
                        var value = _arr[i + valueTuple.Item1, j + valueTuple.Item2];
                        if (value > 0)
                        {
                            cells[_arr[i, j] - 1].adjCell.Add(cells[value - 1]);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
