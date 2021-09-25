using DG.Tweening;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TitleMainView : MonoBehaviour
{
    public Image line1;
    public Image line2;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button creditButton;

    [SerializeField]
    private Button optionButton;

    [SerializeField]
    private Button exitButton;


    public IObservable<Unit> StartClick
    {
        get => startButton.OnClickAsObservable();
    }
    public IObservable<Unit> CreditClick
    {
        get => creditButton.OnClickAsObservable();
    }
    public IObservable<Unit> OptionClick
    {
        get => optionButton.OnClickAsObservable();
    }
    public IObservable<Unit> ExitClick
    {
        get => exitButton.OnClickAsObservable();
    }

    private void Awake()
    {

        line1.material.mainTextureOffset = Vector2.zero;
        line2.material.mainTextureOffset = Vector2.zero;
        line1.material.DOOffset(new Vector2(0, 1),0.5f).SetEase(Ease.Linear).SetLoops(-1);
        line2.material.DOOffset(new Vector2(0, -1), 0.5f).SetEase(Ease.Linear).SetLoops(-1);
    }
}
