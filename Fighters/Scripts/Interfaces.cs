namespace IJunior
{
    interface IFighter : IDamagable, IDealDamage
    {
        string Name { get; }

        int GetHealthInformation { get; }

        Weapon Weapon { get; }
    }

    interface IDamagable
    {
        void GetDamage(int damage);
    }

    interface IDealDamage
    {
        int DealDamage { get; }

        void Attack(IDamagable something);
    }
}
