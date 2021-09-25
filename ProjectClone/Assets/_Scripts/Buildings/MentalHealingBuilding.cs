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

#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = new Color(0, 0.56f, 1);
        Gizmos.DrawWireSphere(transform.position, range);
    }

#endif

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerEnter = true;
            collision.GetComponent<Clone>().eMindState = EMindState.Stability;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerEnter = false;
            collision.GetComponent<Clone>().eMindState = EMindState.Instability;
        }
    }
}
