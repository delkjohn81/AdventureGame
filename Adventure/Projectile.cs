using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Projectile : Locateable
    {
        protected bool endsWithAnimation;
        public Vector Origin;

        public Vector Velocity;
        public int MaxRange;
        private int damage;
        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                if (value <= 0)
                {
                    damage = 0;
                }
                else
                {
                    damage = value;
                }
            }
        }
        public Mob Originator;
        public event EndedRangeHandler EndedRange;

        public double Angle
        {
            get
            {
                return Velocity.Angle;
            }
        }

        public Projectile(string name, Vector pos,
            Animation ani, Vector vel, int maxRange,
            bool endsWithAnimation, int damage, Mob originator)
            : base(name, pos, ani)
        {
            Origin = new Vector(pos.X, pos.Y);
            Velocity = vel;
            MaxRange = maxRange;
            Damage = damage;
            Originator = originator;
            this.endsWithAnimation = endsWithAnimation;
            if (endsWithAnimation)
            {
                ani.Ended += ani_Ended;
            }
        }

        public Projectile(Projectile model)
            : base(model.Name, model.Position, model.aniNone)
        {
            endsWithAnimation = model.endsWithAnimation;
            Origin = model.Origin;
            Velocity = model.Velocity;
            MaxRange = model.MaxRange;
            Damage = model.Damage;
            Originator = model.Originator;
            if (endsWithAnimation)
            {
                aniNone.Ended += ani_Ended;
            }
        }

        void ani_Ended(Animation ani, Locateable loc)
        {
            Projectile maybeMe = (Projectile)loc;
            if (this == maybeMe)
            {
                if (EndedRange != null)
                {   // notify that I'm done due to animation ending
                    EndedRange(this);
                }
            }
            else
            {
            }
        }

        public void Move(double time)
        {
            Position = Position + Velocity * time;
            Vector pointing = Position - Origin;
            double dist = pointing.Magnitude;
            if (dist > MaxRange)
            {
                if (EndedRange != null)
                {   // notify that I'm done due to max distance reached
                    EndedRange(this);
                }
            }
        }

    }
}
