public interface ILevelingSystemOR
{
    void RegisterObserver(ILevelingSystemObserver observer);
    void UnregisterObserver(ILevelingSystemObserver observer);
}
