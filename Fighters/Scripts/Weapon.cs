using System;

namespace IJunior
{
    class WeaponBilder
    {     
        public static Weapon CreateWeapon(string name)
        {
            const string nameOfFirstWeapon = "Меч";
            const int damageOfFirstWeapon = 20;

            const string nameOfSecondWeapon = "Магия";
            const int damageOfSecondWeapon = 30;

            switch (name)
            {
                case nameOfFirstWeapon:
                    return new Weapon(damageOfFirstWeapon,name);

                case nameOfSecondWeapon:
                    return new Weapon(damageOfSecondWeapon, name);

                default:
                    throw new Exception("Передано несуществующее имя бойца!");
            }
        }
    }

    class Weapon
    {
        public Weapon(int damage, string name)
        {
            Damage = damage;
            Name = name;
        }

        public int Damage { get; }
        public string Name { get; }
    }    
}
