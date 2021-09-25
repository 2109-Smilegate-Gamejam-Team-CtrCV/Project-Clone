using System;
using UniRx;

public class GameModel
{
    public readonly ReactiveProperty<int> health;
    public readonly ReactiveProperty<float> mental;
    public readonly ReactiveProperty<int> mineral;
    public readonly ReactiveProperty<int> organism;


    public GameModel()
    {
        health = new ReactiveProperty<int>(0);
        mental = new ReactiveProperty<float>(100);
        mineral = new ReactiveProperty<int>(0);
        organism = new ReactiveProperty<int>(0);
    }

    public void AddOrganism(int v)
    {
        organism.Value += v;
    }
    internal void AddMineral(int v)
    {
        mineral.Value += v;
    }
    public void HealthHeal(int value)
    {
        health.Value += value;
    }
    internal void AddMental(float value)
    {
        mental.Value += value;
    }
}
