using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    public bool isUnlocked, canUnlock;
    public SkillData skillData;
    public List<SkillCell> adjCell;
    
    private Image _icon;

    private void Start()
    {
        GetComponent<Button>().OnClickAsObservable()
            .Where(_ => canUnlock && !isUnlocked)
            .Subscribe(_ =>
        {
            foreach (var cell in adjCell)
            {
                cell.gameObject.SetActive(true);
                cell.canUnlock = true;
            }
            isUnlocked = true;
            SkillManager.Instance.GetSkill(skillData);
            transform.DOScale(1, 0.75f);
            GetComponent<Image>().DOColor(Color.white,0.75f);
        }).AddTo(gameObject);

        _icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        _icon.sprite = skillData.skillIcon;
    }

    private void OnEnable()
    {
        if (!isUnlocked && isActiveAndEnabled)
        {
            GetComponent<Image>().DOColor(Color.gray,0.5f).From(new Color(0.5f, 0.5f, 0.5f, 0));
            transform.DOScale(0.75f, 0.5f).From(0);
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            transform.localScale = Vector3.one;
        }

        //GetComponent<Image>().color = isUnlocked ? Color.white : Color.gray;
    }
}
