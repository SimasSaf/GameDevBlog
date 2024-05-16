public interface IHealthObserver
{
    void OnDamageTaken(int currentHealth, int maxHealth);
    void OnFatalDamageTaken();
}
