using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillGridInitializer : MonoBehaviour
{
    private readonly int[,] _arr =
    {
        {0, 0, 0, 0, 1, 0, 0, 0, 0},
        {0, 0, 0, 0, 2, 3, 4, 5, 0},
        {0, 6, 0, 7, 8, 0, 0, 9, 0},
        {0, 10, 0, 11, 12, 0, 0, 13, 0},
        {14, 15, 16, 17, 18, 19, 20, 21, 22},
        {0, 0, 23, 0, 24, 25, 26, 0, 0},
        {0, 27, 28, 29, 30, 0, 0, 0, 0},
        {0, 31, 32, 0, 33, 34, 35, 0, 0},
        {0, 0, 0, 0, 36, 0, 0, 0, 0}
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

                    cells[_arr[i, j] - 1].gameObject.SetActive(cells[_arr[i, j] - 1].canUnlock);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
