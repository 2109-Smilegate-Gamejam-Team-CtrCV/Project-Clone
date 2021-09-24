using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField]
    private GameMainView gameMainView;

    private void Awake()
    {
        gameMainView.SetHeart(10);
        gameMainView.MetalGauge = 0.1f;       
    }
}
