using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{   // this is used by the Mob when Melee combat is complete to notify the Game object
    public delegate void MeleeCombatEndedHandler(Mob attacker, List<Mob> possibleTargets);
    public delegate void RangedCombatEndedHandler(Projectile proj);

    public delegate void DiedHandler(Mob deadGuy);

    public delegate void EndedRangeHandler(Projectile pr);
    public delegate void UseItemHandler(Item i);

    public struct GameInfo
    {
        public Mob Link;
        public List<Mob> Monsters;
        public List<Locateable> Map;
        public List<Projectile> Projectiles;
        public List<string> Log;
    }

    public delegate void UpdateHandler(GameInfo gi);

    public enum PlayerChoice
    {
        MouseLeftUp, MouseRightUp, SpaceUp, SpaceDown
    }

    public struct PlayerInput
    {
        public PlayerChoice Choice;
        public Vector MouseLoc;
    }

    public enum Direction
    {
        None, Left, Right, Up, Down
    }
}
