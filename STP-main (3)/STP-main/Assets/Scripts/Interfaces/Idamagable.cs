using System;

public interface IDamagable
{
    public void TakeDamage(float damage);
    public event Action OnDeath;
}
