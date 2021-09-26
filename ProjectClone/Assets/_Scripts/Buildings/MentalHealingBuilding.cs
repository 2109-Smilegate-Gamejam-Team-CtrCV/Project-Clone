using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MentalHealingBuilding : Building
{
    [SerializeField]
    private float range;

    [SerializeField]
    private float mentalHeal;
    private float timer;
    [SerializeField]
    private bool isPlayerEnter;

    [SerializeField] private CircleCollider2D checkPlayerCollider;

    private const float MetalHealDelay = 0.5f;

    private void Awake()
    {
        checkPlayerCollider.OnTriggerStayAsObservable().Subscribe(collision =>
        {
            if (collision.CompareTag("Player"))
            {
                isPlayerEnter = true;
                collision.GetComponent<Clone>().SetMindState(EMindState.Stability);
            }
        }).AddTo(gameObject);

        checkPlayerCollider.OnTriggerExitAsObservable().Subscribe(collision =>
        {
            if (collision.CompareTag("Player"))
            {
                isPlayerEnter = false;
                collision.GetComponent<Clone>().SetMindState(EMindState.Instability);
            }
        }).AddTo(gameObject);
    }
    
    private void Update()
    {
        if (isPlayerEnter && isCreate)
        {
            timer += Time.deltaTime;
            if (timer > MetalHealDelay)
            {
                timer -= MetalHealDelay;
                Debug.Log("¸ÞÅ» È¸º¹");
                GameManager.Instance.clone.mental += mentalHeal;
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
}
