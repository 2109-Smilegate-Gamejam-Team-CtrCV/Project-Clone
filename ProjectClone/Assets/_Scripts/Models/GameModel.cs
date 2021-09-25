using System;
using UniRx;

public class GameModel
{
    public readonly ReactiveProperty<int> heart;
    public readonly ReactiveProperty<float> mental;
    public readonly ReactiveProperty<int> mineral;
    public readonly ReactiveProperty<int> organism;
    public readonly ReactiveProperty<int> experience;

    public GameModel()
    {
        heart = new ReactiveProperty<int>(5);
        mental = new ReactiveProperty<float>(100);
        mineral = new ReactiveProperty<int>(0);
        organism = new ReactiveProperty<int>(0);
        experience = new ReactiveProperty<int>(0);
    }

    public void AddOrganism(int v)
    {
        organism.Value += v;
    }
    internal void AddMineral(int v)
    {
        mineral.Value += v;
    }
    public void HeartHeal(int value)
    {
        heart.Value += value;
    }
    internal void AddMental(float value)
    {
        mental.Value += value;
    }
    internal void AddExperience(int value)
    {
        experience.Value += value;
    }
}
