using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMainView : MonoBehaviour
{
    [SerializeField]
    private Transform heartTransform;

    [SerializeField]
    private GameObject heartUI;

    [SerializeField]
    private Image metalGauge;

    [SerializeField]
    private TextMeshProUGUI mineralText;

    [SerializeField]
    private TextMeshProUGUI organismText;


    public float MetalGauge
    {
        set => metalGauge.fillAmount = value;
    }

    public string MineralText
    {
        set => mineralText.text = $"±§π∞ : {value}";
    }

    public string OrganismText
    {
        set => organismText.text = $"¿Ø±‚√º : {value}";
    }

    public void SetHeart(int heart)
    {
        foreach (Transform item in heartTransform)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < heart; i++)
        {
            Instantiate(heartUI, Vector3.zero, Quaternion.identity, heartTransform);
        }
    }
}
