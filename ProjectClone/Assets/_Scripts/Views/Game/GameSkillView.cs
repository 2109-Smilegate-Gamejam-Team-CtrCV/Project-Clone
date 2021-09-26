using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameSkillView : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        GetComponent<Button>().OnClickAsObservable().Subscribe(_ => HIde()).AddTo(gameObject);
    }

    public void Show()
    {
        if(!isActiveAndEnabled)
        {
            canvasGroup.DOFade(1, 1).From(0).OnStart(() => gameObject.SetActive(true));
        }
    }

    public void HIde()
    {
        var expModel = SkillManager.Instance.expModel;
        if (!expModel.CanGetSkill)
        {
            canvasGroup.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
