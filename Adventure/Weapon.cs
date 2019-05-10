using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public enum WeaponType
    {
        Melee, Ranged, Spell
    }
    public class Weapon : Item
    {
        public WeaponType Type;
        public int MinRange, MaxRange;

        public Weapon(string name, Vector pos, Animation ani,
            int damage, int minRange, int maxRange, WeaponType type)
            : base(name, pos, ani, damage)
        {
            MinRange = minRange;
            MaxRange = maxRange;
            Type = type;
        }
    }

    public class Claw : Weapon
    {
        public Claw(string name, Vector pos, Animation ani, int damage)
            : base(name, pos, ani, damage, 0, 40, WeaponType.Melee)
        {

        }
            
    }

    public class Sword : Weapon
    {
        public Sword(string name, Vector pos, Animation ani,
            int value) : base(name, pos, ani, value, 0, 45, WeaponType.Melee)
        {
        }
    }

    public abstract class RangedWeapon : Weapon
    {
        protected Projectile ProjectileModel;
        public int ProjectileSpeed;
        public RangedWeapon(string name, Vector pos, Animation ani,
            int damage, int minRange, int maxRange, int speed, Projectile proj)
            : base(name, pos, ani, damage, minRange, maxRange, WeaponType.Ranged)
        {
            ProjectileSpeed = speed;
            ProjectileModel = proj;
        }

        public abstract Projectile Launch(Vector pos, Vector vel, Mob originator);
    }

    public class BreathWeapon : RangedWeapon
    {
        public BreathWeapon(string name, Vector pos, Animation ani,
            int damage, int minRange, int maxRange, int speed, Projectile proj)
            : base(name, pos, ani, damage, minRange, maxRange, speed, proj)
        {
            ProjectileModel = proj;
        }

        public override Projectile Launch(Vector pos, Vector direction, Mob originator)
        {
            Projectile fireball = new Projectile(ProjectileModel);
            fireball.Position = pos;
            fireball.Velocity = ProjectileSpeed * (direction.Unitized);
            fireball.Originator = originator;
            fireball.Damage += Value;
            return fireball;
        }
    }

    public class Bow : RangedWeapon
    {
        public Bow(string name, Vector pos, Animation ani,
            int damage, int speed, int range, Projectile proj) : base(name, pos, ani, damage, 15, range, speed, proj)
        {
        }

        public override Projectile Launch(Vector pos, Vector direction, Mob originator)
        {
            Projectile arrow = new Projectile(ProjectileModel);
            arrow.Position = new Vector(pos.X, pos.Y);
            arrow.Origin = new Vector(pos.X, pos.Y);
            arrow.Velocity = ProjectileSpeed * (direction.Unitized);
            arrow.Originator = originator;
            arrow.Damage += Value;
            return arrow;
        }
    }
}
