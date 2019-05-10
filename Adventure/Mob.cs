using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public enum MobType
    {
        Human, Kobold, Dragon, Goblin, Skeleton, Ghost, Orc, Troll, Zombie
    }

    public enum MobAction
    {
        None, Move, Melee, Ranged, Spell, CallForHelp
    }

    public class Mob : Locateable
    {
        public MobType Type;
        protected MobAction action;
        public MobAction Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
                if (action == MobAction.Melee)
                {
                    aniMelee.Ended += aniMelee_Ended;
                }
                else if (action == MobAction.Ranged)
                {
                    aniRanged.Ended += aniRanged_Ended;
                }
                else if (action == MobAction.Spell)
                {
                    // aniSpell.Ended += 
                }
            }
        }

        public Vector Velocity;
        public Vector Goal, Home;
        public int Attack, Defense;
        public double Speed;
        public double SightRange;
        public List<Item> Inventory = new List<Item>();
        public event MeleeCombatEndedHandler MeleeCombatEnded;
        public event RangedCombatEndedHandler RangedCombatEnded;
        public event DiedHandler Died;
        protected Animation aniMelee, aniMove, aniRanged;

        protected int hp;
        public int HP
        {
            get
            {
                return hp;
            }
            set
            {
                if (value <= 0)
                {
                    hp = 0;
                    if (Died != null)
                    {   // notify that we have died!
                        Died(this);
                    }
                }
                else
                {
                    hp = value;
                }
            }
        }

        public override System.Drawing.Image Img
        {
            get
            {
                if(action == MobAction.Melee)
                {
                    return aniMelee.GetFrame(this);
                }
                else if(action == MobAction.Move)
                {
                    return aniMove.GetFrame(this);
                }
                else if (action == MobAction.Ranged)
                {
                    if (Type == MobType.Dragon)
                    {
                    }
                    return aniRanged.GetFrame(this);
                }
                else
                {
                    return aniNone.GetFrame(this);
                }
            }
        }
        public double Angle
        {
            get
            {
                return Velocity.Angle;
            }
        }
        public Weapon EquippedWeapon;

        public List<Locateable> Map = new List<Locateable>();
        public List<Mob> Contacts = new List<Mob>();

        public Mob(string name, Vector pos, Animation aniNone,
            Animation aniMove, Animation aniMelee, Animation aniRanged,
            MobType type, Vector goal, int hp,
            int atk, int def, double spd, double sight)
            : base(name, pos, aniNone)
        {
            Type = type;
            Goal = goal;
            Home = goal;
            this.hp = hp;
            Attack = atk;
            Defense = def;
            Speed = spd;
            SightRange = sight;
            Velocity = new Vector(0, 0);
            this.aniMove = aniMove;
            if (aniMove != null)
            {
                aniMove.AddLocateable(this);
            }
            this.aniMelee = aniMelee;
            if (aniMelee != null)
            {
                aniMelee.AddLocateable(this);
            }
            this.aniRanged = aniRanged;
            if (aniRanged != null)
            {
                aniRanged.AddLocateable(this);
            }
        }

        public void Sense(List<Locateable> visuals)
        {
            foreach (Locateable loc in visuals)
            {
                if(!Map.Contains(loc))
                {
                    Map.Add(loc);
                }
            }
        }

        public void Sense(List<Mob> mobs)
        {
            Contacts.Clear();
            foreach(Mob m in mobs)
            {
                Contacts.Add(m);
            }
        }

        public MobAction React(double time)
        {   
            // only allow an Action if NOT currently committed to another action...
            if (action != MobAction.Melee && action != MobAction.Ranged
                && action != MobAction.Spell )
            {
                Mob target = CheckForTarget();
                if (target != null && Type != MobType.Human)
                {
                    // we found an enemy, so see if we can attack from current position...
                    Vector pointing = target.Position - Position;
                    double dist = pointing.Magnitude;
                    Weapon bestWpn = FindBestWeapon(dist);
                    if (bestWpn != null)
                    {// found a weapon that can attack  immediately...
                        Vector unit = pointing.Unitized;
                        Velocity = 0.01 * unit;// make sure to face the enemy before attacking
                        if (bestWpn.Type == WeaponType.Melee)
                        {
                            Action = MobAction.Melee;
                        }
                        else if (bestWpn.Type == WeaponType.Ranged)
                        {
                            Action = MobAction.Ranged;
                        }
                        else
                        {
                            Action = MobAction.Spell;
                        }
                    }
                    else
                    {   // no current weapon, so move TOWARD enemy instead...
                        if (dist < 10)
                        {   // we are next to enemy already, so stay put
                            Action = MobAction.None;
                        }
                        else
                        {   // move toward the enemy...
                            Goal = target.Position;
                            Vector unit = pointing.Unitized;
                            Velocity = Speed * unit;
                            Action = MobAction.Move;
                        }
                    }
                }
                else
                {// NO target around us, so now what to do?
                    Vector pointing = Home - Position;
                    double dist = pointing.Magnitude;
                    if (dist < 10)// 10
                    {   // already at my Goal, so nothing to do...
                        Action = MobAction.None;
                    }
                    else
                    {   // NOT near my Goal, so head toward it now...
                        Vector unit = pointing.Unitized;
                        Velocity = Speed * unit;
                        Action = MobAction.Move;
                    }
                }
            }

            if (action == MobAction.Move)
            {   // if we decide to Move, then call our Move() function now...
                Move(time);
            }
            return action;
        }

        public void aniMelee_Ended(Animation ani, Locateable loc)
        {
            if (loc is Mob)
            {
                Mob maybeMe = (Mob)loc;
                if (this == maybeMe)
                {
                    aniMelee.Ended -= aniMelee_Ended;
                    Action = MobAction.None;
                    if (MeleeCombatEnded != null)
                    {
                        MeleeCombatEnded(this, Contacts);
                    }
                }
            }
        }

        void aniRanged_Ended(Animation ani, Locateable loc)
        {
            if (loc is Mob)
            {
                Mob maybeMe = (Mob)loc;
                if (this == maybeMe)
                {// 1st make sure we're still aiming at target...
                    Vector unit;
                    Mob target = CheckForTarget();
                    if (target != null)
                    {   // we still see enemy, so aim at him...
                        Vector pointing = target.Position - Position;
                        unit = pointing.Unitized;
                        Velocity = 0.01 * unit;
                    }
                    else
                    {// target is missing, so prepare to launch at last known dir anyway
                        unit = Velocity.Unitized;
                    }

                    RangedWeapon rw = (RangedWeapon)EquippedWeapon;
                    Vector offset;
                    if (Type == MobType.Dragon)
                    {
                        offset = 50 * unit;
                    }
                    else
                    {
                        offset = 15 * unit;
                    }
                    Vector projPos = Position + offset;
                    Projectile proj = rw.Launch(projPos, unit, this);
                    aniRanged.Ended -= aniRanged_Ended;
                    Action = MobAction.None;
                    if (RangedCombatEnded != null)
                    {
                        RangedCombatEnded(proj);
                    }
                }
            }
        }

        protected void Move(double time)
        {
            Position = Position + Velocity * time;
        }

        public void UseItem(Item item)
        {
            if (item is Weapon)
            {
                EquippedWeapon = (Weapon)item;
            }
            //            else if (i is Armor)
            else if (item is Potion)
            {
                Potion pot = (Potion)item;
                if (pot.Type == PotionType.Health)
                {
                    HP += item.Value;
                }
                else if (pot.Type == PotionType.Speed)
                {
                    Speed += pot.Value;
                }
                else if (pot.Type == PotionType.Strength)
                {
                    Attack += pot.Value;
                }
                else if (pot.Type == PotionType.Defense)
                {
                    Defense += pot.Value;
                }
                Inventory.Remove(item);
            }
        }

        protected Mob CheckForTarget()
        {
            Mob target = null;
            foreach (Mob m in Contacts)
            {
                if (m.Type == MobType.Human)
                {
                    target = m;
                    break;
                }
            }
            return target;
        }

        protected Weapon FindBestWeapon(double dist)
        {
            int bestDamage = 0;
            Weapon bestWpn = null;
            foreach (Item usable in Inventory)
            {
                if (usable is Weapon)
                {
                    Weapon w = (Weapon)usable;
                    if (w.MinRange < dist && w.MaxRange > dist)
                    {
                        if (w.Value >= bestDamage)
                        {
                            bestDamage = w.Value;
                            bestWpn = w;
                        }
                    }
                }
            }
            return bestWpn;
        }

    }
}
