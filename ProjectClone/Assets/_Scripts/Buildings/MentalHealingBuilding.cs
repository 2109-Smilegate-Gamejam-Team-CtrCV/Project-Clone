using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalHealingBuilding : Building
{
    [SerializeField]
    private float range;

    [SerializeField]
    private float mentalHeal;
    private float timer;
    private bool isPlayerEnter;

    private const float MetalHealDelay = 0.5f;
    private void Update()
    {
        if (isPlayerEnter && isCreate)
        {
            timer += Time.deltaTime;
            if (timer > MetalHealDelay)
            {
                timer -= MetalHealDelay;
                GameManager.Instance.gamePresenter.gameModel.AddMental(mentalHeal * 0.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerEnter = false;
        }
    }
}
