public interface IEarthHealthManagerOR
{
    void RegisterObserver(IHealthObserver observer);
    void UnregisterObserver(IHealthObserver observer);
}
