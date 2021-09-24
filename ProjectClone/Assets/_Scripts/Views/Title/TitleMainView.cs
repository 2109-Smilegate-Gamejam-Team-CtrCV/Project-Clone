using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TitleMainView : MonoBehaviour
{
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
}
