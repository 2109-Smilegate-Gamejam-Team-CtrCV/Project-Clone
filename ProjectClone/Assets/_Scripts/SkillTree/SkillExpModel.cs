using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class SkillExpModel : MonoBehaviour
{
    private ReactiveProperty<float> needExp = new FloatReactiveProperty();
    private ReactiveProperty<float> currExp = new FloatReactiveProperty();
    private ReactiveProperty<float> getExpPerSec = new FloatReactiveProperty();
    private ReactiveProperty<float> expGrowthFigure = new FloatReactiveProperty();

    public IObservable<float> NeedExp => needExp.ObserveOnMainThread();
    public IObservable<float> CurrExp => currExp.ObserveOnMainThread();
    public IObservable<float> GetExpPerSec => getExpPerSec.ObserveOnMainThread();
    public IObservable<float> ExpGrowthFigure => expGrowthFigure.ObserveOnMainThread();
    public bool CanGetSkill => currExp.Value >= needExp.Value;
    public IDisposable canGetSkillShowEvent;

    private static readonly WaitForSeconds ExpCooldown = new WaitForSeconds(1f);
    private Coroutine addExpCoroutine;

    public void Initialize()
    {
        needExp.Value = 12;
        currExp.Value = 0;
        getExpPerSec.Value = .3f;
        expGrowthFigure.Value = 2f;

        if (addExpCoroutine != null)
        {
            StopCoroutine(addExpCoroutine);
            addExpCoroutine = null;
        }
        addExpCoroutine = StartCoroutine(CoAddExp());
            
        SkillGridInitializer.Instance.Initialize();
    }

    public bool GetSkill()
    {
        if (needExp.Value <= currExp.Value)
        {
            currExp.Value -= needExp.Value;
            needExp.Value += expGrowthFigure.Value;
            return true;
        }

        return false;
    }

    private IEnumerator CoAddExp()
    {
        while (true)
        {
            yield return ExpCooldown;
            currExp.Value += getExpPerSec.Value;
        }
    }

    private void Start()
    {
        Initialize();
        canGetSkillShowEvent = currExp.Where(value => value >= needExp.Value)
            .Subscribe(_ => GameManager.Instance.gamePresenter.gameSkillView.Show())
            .AddTo(gameObject);
    }
}