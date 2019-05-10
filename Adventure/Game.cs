using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    public class Game
    {
        public static Random rand = new Random();
        public event UpdateHandler Update;
        protected int width, height;
        protected Timer tmr = new Timer();
        protected List<Locateable> map = new List<Locateable>();
        protected List<Mob> monsters = new List<Mob>();
        protected List<int> mobsToDelete = new List<int>();
        protected List<Projectile> projectiles = new List<Projectile>();
        protected List<int> projectilesToDelete = new List<int>();
        protected Mob link;
        protected Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        protected List<string> log = new List<string>();

        protected void CreateAnimations()
        {
            Animation aniSwordExcalibur = new Animation(Properties.Resources.SwordExcalibur,
                0, 0, 1, 1, 65, 248, true, 0.01, 0.2);
            Animation aniBowOfOrion = new Animation(Properties.Resources.BowOfOrion,
                0, 0, 6, 2, 40, 52, true, 0.5, 0.5);
            Animation aniPotionHealth = new Animation(Properties.Resources.PotionAnimated,
                0, 0, 11, 4, 80, 105, true, 0.5, 0.3);
            Animation aniPotionSpeed = new Animation(Properties.Resources.potions,
                390, 2, 1, 5, 55, 55,true, 0.1, 0.5);
            Animation aniPotionStrength = new Animation(Properties.Resources.potions,
                445, 2, 1, 5, 55, 55, true, 0.1, 0.5);
            Animation aniPotionDefense = new Animation(Properties.Resources.potions,
                500, 2, 1, 5, 55, 55, true, 0.1, 0.5);

            Animation aniTree = new Animation(Properties.Resources.TreesAnimated,
                0, 0, 11, 4, 271, 249, true, 0.2, 0.3);
            Animation aniTreePh = new Animation(Properties.Resources.TreePhantomAni,
                0, 330, 12, 1, 281, 320, true, 0.2, 0.5);
            Animation aniTreePh2 = new Animation(Properties.Resources.TreePhantomAni,
                0, 1815, 12, 1, 225, 310, true, 0.2, 0.2);
            Animation aniShrubPh = new Animation(Properties.Resources.TreePhantomAni,
                400, 690, 1, 1, 210, 90, true, 0.1, 0.3);
            Animation aniMobMoving = new Animation(Properties.Resources.MobMoving,
                460, 36, 4, 1, 110, 75, true, 0.2, 0.5);
            Animation aniMobNone = new Animation(Properties.Resources.MobMoving,
                112, 36, 1, 1, 110, 75, true, 0.1, 0.5);
            Animation aniLinkNone = new Animation(Properties.Resources.LinkMove,
                0, 0, 1, 1, 24, 37, true, 0.1, 1.5);
            Animation aniLinkMove = new Animation(Properties.Resources.LinkMove,
                0, 0, 6, 1, 24, 37, true, 0.3, 1.5);
            Animation aniLinkMelee = new Animation(Properties.Resources.LinkMelee,
                0, 0, 8, 1, 48, 45, false, 0.5, 1.5);
            Animation aniLinkRanged = new Animation(Properties.Resources.linkRanged,
                0, 0, 16, 1, 36, 39, false, 1.0, 1.5);
            Animation aniDragonNone = new Animation(Properties.Resources.Dragon,
                0, 318, 1, 1, 106, 106, true, 0.1, 1.2);
            Animation aniDragonMove = new Animation(Properties.Resources.Dragon,
                0, 318, 3, 1, 106, 106, true, 0.2, 1.2);
            Animation aniDragonRanged = new Animation(Properties.Resources.Dragon,
                0, 318, 3, 1, 106, 106, false, 0.05, 1.2);
            Animation aniGDragonNone = new Animation(Properties.Resources.greenDragon,
                0, 384, 1, 1, 128, 128, true, 0.01, 0.8);
            Animation aniGDragonMove = new Animation(Properties.Resources.greenDragon,
                0, 0, 8, 1, 128, 128, true, 0.25, 0.8);
            Animation aniGDragonRanged = new Animation(Properties.Resources.greenDragon,
                0, 128, 8, 1, 128, 128, false, 0.15, 0.8);
            Animation aniZombieNone = new Animation(Properties.Resources.zombieMove,
                0, 0, 1, 1, 128, 128, true, 0.1, 1);
            Animation aniZombieMove = new Animation(Properties.Resources.zombieMove,
                0, 0, 9, 3, 128, 128, true, 0.5, 1);
            Animation aniZombieMelee = new Animation(Properties.Resources.zombieMove,
                0, 512, 9, 3, 128, 128, false, 1.0, 1);
            Animation aniFire = new Animation(Properties.Resources.Fire2,
                0, 0, 8, 6, 256, 256, false, 1.0, 0.5);
            Animation aniIce = new Animation(Properties.Resources.iceBreath,
                0, 0, 14, 1, 64, 64, false, 0.5, 2);
            Animation aniArrow = new Animation(Properties.Resources.arrow,
                0, 0, 1, 1, 5, 17, true, 0.01, 1.0);
            Animation aniTrollNone = new Animation(Properties.Resources.Troll,
                296, 0, 1, 1, 74, 74, true, 0.01, 1.0);
            Animation aniTrollMove = new Animation(Properties.Resources.Troll,
                296, 0, 1, 5, 74, 74, true, 0.25, 1.0);
            Animation aniTrollMelee = new Animation(Properties.Resources.Troll,
                296, 370, 1, 4, 74, 74, false, 0.15, 1.0);
            Animation aniOrcNone = new Animation(Properties.Resources.Orc,
                0, 350, 1, 1, 50, 50, true, 0.01, 1.0);
            Animation aniOrcMove = new Animation(Properties.Resources.Orc,
                0, 150, 4, 2, 50, 50, true, 0.25, 1.0);
            Animation aniOrcMelee = new Animation(Properties.Resources.Orc,
                0, 300, 5, 1, 50, 50, false, 0.15, 1.0);

            animations.Add("swordExcalibur", aniSwordExcalibur);
            animations.Add("bowOfOrion", aniBowOfOrion);
            animations.Add("ice", aniIce);
            animations.Add("fire", aniFire);
            animations.Add("arrow", aniArrow);
            animations.Add("potionOfHealth", aniPotionHealth);
            animations.Add("potionOfSpeed", aniPotionSpeed);
            animations.Add("potionOfStrength", aniPotionStrength);
            animations.Add("potionOfDefense", aniPotionDefense);
            animations.Add("tree", aniTree);
            animations.Add("treePh", aniTreePh);
            animations.Add("treePh2", aniTreePh2);
            animations.Add("shrub", aniShrubPh);
            animations.Add("mobNone", aniMobNone);
            animations.Add("mobMove", aniMobMoving);
            animations.Add("linkNone", aniLinkNone);
            animations.Add("linkMove", aniLinkMove);
            animations.Add("linkMelee", aniLinkMelee);
            animations.Add("linkRanged", aniLinkRanged);
            animations.Add("dragonNone", aniDragonNone);
            animations.Add("dragonMove", aniDragonMove);
            animations.Add("dragonRanged", aniDragonRanged);
            animations.Add("gDragonNone", aniGDragonNone);
            animations.Add("gDragonMove", aniGDragonMove);
            animations.Add("gDragonRanged", aniGDragonRanged);
            animations.Add("zombieNone", aniZombieNone);
            animations.Add("zombieMove", aniZombieMove);
            animations.Add("zombieMelee", aniZombieMelee);
            animations.Add("trollNone", aniTrollNone);
            animations.Add("trollMove", aniTrollMove);
            animations.Add("trollMelee", aniTrollMelee);
            animations.Add("orcNone", aniOrcNone);
            animations.Add("orcMove", aniOrcMove);
            animations.Add("orcMelee", aniOrcMelee);
        }

        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
            CreateAnimations();
            CreateForest(3 * width / 4, height / 4, width / 4, height / 2, 50);
            CreateForest(width / 4, 3* height / 4, width / 2, height / 3, 30);
            PlaceRandomPotions(10);
            CreateMonsters(width, height);

            link = new Mob("Link", new Vector(30, 30), animations["linkNone"],
                animations["linkMove"], animations["linkMelee"], animations["linkRanged"],
                MobType.Human, new Vector(30, 30), 2000, 10, 10, 5, 250);
            link.MeleeCombatEnded += m_MeleeCombatEnded;
            link.RangedCombatEnded += mob_RangedCombatEnded;
            link.Died += link_Died;
            Sword sw = new Sword("long sword", new Vector(0, 0), null, 10);
            link.Inventory.Add(sw);
            link.EquippedWeapon = sw;
            Projectile arrow = new Projectile("arrow", new Vector(), animations["arrow"], new Vector(),
                350, false, 5, null);
            Bow bow = new Bow("longbow", new Vector(), null, 5, 30, 350, arrow);
            link.Inventory.Add(bow);
            link.EquippedWeapon = bow;

            Bow bowOfOrion = new Bow("Bow of Orion", new Vector(rand.Next(0, width), rand.Next(0, height)), animations["bowOfOrion"],
                15, 50, 600, arrow);
            map.Add(bowOfOrion);

            Sword excalibur = new Sword("Excalibur", new Vector(rand.Next(0, width), rand.Next(0, height)), animations["swordExcalibur"],
                25);
            map.Add(excalibur);

            tmr.Interval = 17;
            tmr.Tick += tmr_Tick;
        }

        private void CreateMonsters(int width, int height)
        {
            for (int i = 0; i < 6; i++)
            {   // orcs...
                Mob orc = new Mob("orc", new Vector(rand.Next(0, width), rand.Next(0, height)),
                    animations["orcNone"], animations["orcMove"], animations["orcMelee"], null,
                        MobType.Orc, new Vector(rand.Next(0, width), rand.Next(0, height)), 15, 10, 5, 3, 350);
                orc.MeleeCombatEnded += m_MeleeCombatEnded;
                orc.Died += m_Died;
                Claw claw = new Claw("troll claw", new Vector(0, 0), null, 12);
                orc.Inventory.Add(claw);
                orc.EquippedWeapon = claw;
                monsters.Add(orc);
            }
            for (int i = 0; i < 6; i++)
            {   // 2-headed trolls...
                Mob troll = new Mob("troll", new Vector(rand.Next(0, width), rand.Next(0, height)),
                    animations["trollNone"], animations["trollMove"], animations["trollMelee"], null,
                        MobType.Troll, new Vector(rand.Next(0, width), rand.Next(0, height)), 25, 12, 8, 4, 250);
                troll.MeleeCombatEnded += m_MeleeCombatEnded;
                troll.Died += m_Died;
                Claw claw = new Claw("troll claw", new Vector(0, 0), null, 12);
                troll.Inventory.Add(claw);
                troll.EquippedWeapon = claw;
                monsters.Add(troll);
            }

            for (int i = 0; i < 12; i++)
            {   // zombies...
                Mob zombie = new Mob("zombie" + i.ToString(), new Vector(rand.Next(0, width), rand.Next(0, height)),
                    animations["zombieNone"], animations["zombieMove"], animations["zombieMelee"], null, MobType.Zombie,
                    new Vector(rand.Next(0, width), rand.Next(0, height)), 10, 12, 4, 2, 150);
                zombie.MeleeCombatEnded += m_MeleeCombatEnded;
                zombie.Died += m_Died;
                Claw claw = new Claw("zombie claw", new Vector(0, 0), null, 10);
                zombie.Inventory.Add(claw);
                zombie.EquippedWeapon = claw;
                monsters.Add(zombie);
            }
            for (int i = 0; i < 4; i++)
            {   // dragons...
                Projectile proj;
                Mob dragon;
                if (i % 2 == 0)
                {
                    proj = new Projectile("fireball", new Vector(), animations["fire"],
                        new Vector(), 2000, true, 100, null);
                    dragon = new Mob("redDragon", new Vector(rand.Next(0, width), rand.Next(0, height)),
                        animations["dragonNone"], animations["dragonMove"], null, animations["dragonRanged"], MobType.Dragon,
                        new Vector(rand.Next(0, width), rand.Next(0, height)),
                        200, 25, 20, 4, 400);
                }
                else
                {
                    proj = new Projectile("iceball", new Vector(), animations["ice"],
                        new Vector(), 2000, true, 100, null);
                    dragon = new Mob("greenDragon", new Vector(rand.Next(0, width), rand.Next(0, height)),
                        animations["gDragonNone"], animations["gDragonMove"], null, animations["gDragonRanged"], MobType.Dragon,
                        new Vector(rand.Next(0, width), rand.Next(0, height)),
                        200, 25, 20, 6, 400);
                }
                BreathWeapon bw = new BreathWeapon("breath", new Vector(), null, 0, 0, 2000, 15, proj);
                dragon.Inventory.Add(bw);
                dragon.EquippedWeapon = bw;
                dragon.RangedCombatEnded += mob_RangedCombatEnded;
                dragon.Died += m_Died;
                monsters.Add(dragon);
            }
        }

        void link_Died(Mob deadGuy)
        {
            Pause();
            List<Locateable> oldMap = new List<Locateable>();
            foreach(Locateable loc in link.Map)
            {
                oldMap.Add(loc);
            }
            int r = rand.Next(0, oldMap.Count);
            Vector newPos = new Vector(oldMap[r].Position.X, oldMap[r].Position.Y);
            DialogResult dr = MessageBox.Show(deadGuy.Name + " has just died!");
            if(dr == DialogResult.OK)
            {
                link = new Mob("link", newPos, animations["linkNone"],
                    animations["linkMove"], animations["linkMelee"], animations["linkRanged"],
                    MobType.Human, newPos, 500, 10, 5, 5, 300);
                link.Sense(oldMap);
                Sword sw = new Sword("long sword", new Vector(0, 0), null, 10);
                link.Inventory.Add(sw);
                link.EquippedWeapon = sw;
                link.MeleeCombatEnded += m_MeleeCombatEnded;
                link.Died += link_Died;
                Play();
            }
        }

        public void Play()
        {
            tmr.Start();
        }

        public void Pause()
        {
            tmr.Stop();
        }
        
        void m_Died(Mob deadGuy)
        {
            int mobIdx = monsters.IndexOf(deadGuy);
            if (!mobsToDelete.Contains(mobIdx))
            {   // only add this dead mob if he was NOT already added before...
                mobsToDelete.Add(mobIdx);
            }
        }

        protected void RemoveDeadGuys()
        {
            if (mobsToDelete.Count > 0)
            {
                mobsToDelete.Sort();
                mobsToDelete.Reverse();// reverse order to prevent crashing the List
                foreach (int i in mobsToDelete)
                {
                    log.Add(monsters[i].Name + " has just perished. God rest his soul. ");
                    monsters.RemoveAt(i);
                }
                mobsToDelete.Clear();// clear out the list of dead guys
            }
        }

        protected void RemoveDeadProjectiles()
        {
            if (projectilesToDelete.Count > 0)
            {
                projectilesToDelete.Sort();
                projectilesToDelete.Reverse();
                foreach (int i in projectilesToDelete)
                {
                    projectiles.RemoveAt(i);
                }
                projectilesToDelete.Clear();// clear out list of dead Projectiles
            }
        }

        void m_MeleeCombatEnded(Mob attacker, List<Mob> possibleTargets)
        {
            foreach (Mob m in possibleTargets)
            {   // make sure monsters cannot attack other monsters...
                if ((attacker.Type != MobType.Human && m.Type == MobType.Human)
                    || (attacker.Type == MobType.Human && m.Type != MobType.Human))
                {
                    Vector pointingToMob = m.Position - attacker.Position;
                    double dist = pointingToMob.Magnitude;
                    if (dist <= attacker.EquippedWeapon.MaxRange)
                    {
                        double angleBetween = attacker.Velocity.AngleBetween(pointingToMob);
                        if (angleBetween > -90 && angleBetween < 90)
                        {   // Make sure target is IN FRONT of us when we attacked
                            double totDamage = attacker.Attack + attacker.EquippedWeapon.Value;
                            totDamage += rand.Next(-5, 6);
                            double percReduction = Math.Max(0.90, m.Defense / 100.0);
                            totDamage = totDamage - percReduction * totDamage;// target's defense reduces damage by percentage
                            if (totDamage < 0)
                            {
                                totDamage = 0;
                            }
                            m.HP -= (int)totDamage;// mob takes damage
                            log.Add(attacker.Name + " hit " + m.Name + " and did "
                                + totDamage + " damage");
                        }
                    }
                }
            }
        }

        void mob_RangedCombatEnded(Projectile proj)
        {
            if (!projectiles.Contains(proj))
            {
                proj.EndedRange += proj_EndedRange;
                projectiles.Add(proj);
            }
        }

        void proj_EndedRange(Projectile proj)
        {
            proj.EndedRange -= proj_EndedRange;
            int idx = projectiles.IndexOf(proj);
            if (idx != -1)
            {
                projectilesToDelete.Add(idx);
            }
            else
            {
            }
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            CheckProjectileCollisions();
            RemoveDeadGuys();// remove dead mobs before they sense/react to anything...
            RemoveDeadProjectiles();
            CheckForLoot();
            foreach (Mob m in monsters)
            {
                List<Locateable> thingsICanSee = FindAllLocateablesInRadius(m);
                m.Sense(thingsICanSee);
                List<Mob> mobsICanSee = FindAllMobsInRadius(m);
                m.Sense(mobsICanSee);
                MobAction act = m.React(1.0);
            }
            List<Locateable> thingsLinkCanSee = FindAllLocateablesInRadius(link);
            link.Sense(thingsLinkCanSee);
            List<Mob> mobsLinkCanSee = FindAllMobsInRadius(link);
            link.Sense(mobsLinkCanSee);
            MobAction actLink = link.React(1.0);

            foreach (Projectile proj in projectiles)
            {
                proj.Move(1.0);
            }

            GameInfo gi = new GameInfo();
            gi.Link = link;
            gi.Monsters = link.Contacts;
            gi.Map = link.Map;
            gi.Projectiles = projectiles;
            gi.Log = log;

            if (Update != null)
            {   // Notify the Form that the Game status has changed...
                Update(gi);
            }
            log.Clear();// clear out the log for the next round of deaths to come
        }

        protected bool Overlapping(Locateable loc1, Locateable loc2)
        {
            int depth = (int)(Math.Max(loc1.Height, loc2.Height) * 1 / 4.0f);
            Vector pointing = loc2.Position - loc1.Position;
            double dist = pointing.Magnitude;
            if (dist < depth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void CheckForLoot()
        {
            List<Item> itemsToAddToInv = new List<Item>();
            foreach (Locateable loc in link.Map)
            {
                if (loc is Item)
                {
                    if (Overlapping(loc, link))
                    {
                        Item i = (Item)loc;
                        itemsToAddToInv.Add(i);
                    }
                }
            }
            foreach (Item i in itemsToAddToInv)
            {
                map.Remove(i);  // remove the potion from the world map
                link.Map.Remove(i);
                link.Inventory.Add(i);// add it to our Inventory for use later
                log.Add("You got the " + i.Name + "!");
            }
        }

        protected void CheckProjectileCollisions()
        {
            foreach (Projectile proj in projectiles)
            {
                foreach (Mob m in monsters)
                {
                    if (Overlapping(m, proj))
                    {   // we have collided
                        if (proj.Originator.Type != m.Type)// prevent friendly fire from same Mob Type
                        {
                            int origProjDamage = proj.Damage;
                            proj.Damage -= m.HP;// projectile's potency reduced by the mob it hit
                            m.HP -= origProjDamage;// projectile incurs damage to mob
                        }
                    }
                }
                // now check if this projectile hits Link...
                if (Overlapping(link, proj))
                {   // link has collided with a projectile
                    if (proj.Originator != link)// prevent friendly fire from himself
                    {
                        int origProjDamage = proj.Damage;
                        proj.Damage -= link.HP;// projectile's potency reduced by link absorbing it
                        link.HP -= origProjDamage;// link takes damage from projectile
                    }
                }
                if (proj.Damage <= 0)
                {   // projectile is destroyed, so prepare to remove it...
                    int idxProj = projectiles.IndexOf(proj);
                    if (!projectilesToDelete.Contains(idxProj))
                    {
                        projectilesToDelete.Add(idxProj);// note which proj to remove later
                    }
                }
            }
        }

        public void PlayerInput(PlayerInput pi)
        {
            if (pi.Choice == PlayerChoice.MouseLeftUp)
            {   // player chooses to attack, so determine which type of attack
                Vector pointing = pi.MouseLoc - link.Position;
                Vector unit = pointing.Unitized;
                link.Velocity = 0.01 * unit;

                if (link.EquippedWeapon.Type == WeaponType.Melee)
                {
                    link.Action = MobAction.Melee;
                }
                else if (link.EquippedWeapon.Type == WeaponType.Ranged)
                {
                    link.Action = MobAction.Ranged;
                }
                else
                {
                    link.Action = MobAction.Spell;
                }
            }
            else if (pi.Choice == PlayerChoice.MouseRightUp)
            {
                link.Action = MobAction.Move;
                link.Home = pi.MouseLoc;
            }
            else if (pi.Choice == PlayerChoice.SpaceDown)
            {
                Pause();
            }
            else if (pi.Choice == PlayerChoice.SpaceUp)
            {
                Play();
            }
        }

        public void UseItem(Item item)
        {
            link.UseItem(item);
            if (!(item is Potion))
            {
                log.Add(item.Name + " has been equipped.");
            }
            else
            {
                log.Add("You drank the " + item.Name);
            }
        }

        protected List<Locateable> FindAllLocateablesInRadius(Mob m)
        {
            List<Locateable> thingsICanSee = new List<Locateable>();
            foreach (Locateable loc in map)
            {
                Vector pointing = loc.Position - m.Position;
                double dist = pointing.Magnitude;
                if (dist <= m.SightRange)
                {
                    thingsICanSee.Add(loc);
                }
            }
            return thingsICanSee;
        }

        protected List<Mob> FindAllMobsInRadius(Mob m)
        {
            List<Mob> mobsICanSee = new List<Mob>();
            foreach (Mob om in monsters)
            {
                if (om != m)// do NOT check YOURSELF!!
                {
                    Vector pointing = om.Position - m.Position;
                    double dist = pointing.Magnitude;
                    if (dist <= m.SightRange)
                    {
                        mobsICanSee.Add(om);
                    }
                }
            }

            if (m != link)
            {
                Vector pointingToLink = link.Position - m.Position;
                double distToLink = pointingToLink.Magnitude;
                if (distToLink <= m.SightRange)
                {
                    mobsICanSee.Add(link);
                }
            }
            return mobsICanSee;
        }

        protected void CreateForest(int centerX, int centerY, int radX, int radY, int density)
        {
            double radAvg = (radX + radY) / 2;
            for (int y = centerY - radY; y <= centerY + radY; y+= rand.Next(30, 50))
            {
                int origY = y;
                for (int x = centerX - radX; x <= centerX + radX; x+= rand.Next(30, 50))
                {   // calc dist to the center of the forest...
                    y += rand.Next(-30, 31);// a little more randomness in y
                    int dx = x - centerX;
                    int dy = y - centerY;
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    double prob = density * (1 - (dist / radAvg));
                    int r = rand.Next(0, 101);
                    if (r <= prob)
                    {   // decide which kind of tree...
                        int kind = rand.Next(0, 100);
                        Animation ani;
                        if(kind < 10)
                        {
                            ani = animations["treePh"];
                        }
                        else if (kind < 20)
                        {
                            ani = animations["treePh2"];
                        }
                        else if (kind < 60)
                        {
                            ani = animations["tree"];
                        }
                        else 
                        {
                            ani = animations["shrub"];
                        }
                        Landscape tree = new Landscape("ls", new Vector(x, y), ani, true, LandscapeType.Tree);
                        tree.Type = LandscapeType.Tree;
                        map.Add(tree);
                    }
                }
                y = origY;
            }
        }

        protected void PlaceRandomPotions(int numPotions)
        {
            for (int i = 0; i < numPotions; i++)
            {
                int x = rand.Next(30, width - 30);
                int y = rand.Next(30, height - 30);
                int r = rand.Next(0, 100);
                int strength;
                string name;
                PotionType potType = PotionType.Health;
                Animation ani = animations["potionOfHealth"];
                if(r < 5)
                {
                    name = "Potion of Mega Health";
                    strength = 200;
                }
                else if (r < 15)
                {
                    name = "Potion of Super Health";
                    strength = 100;
                }
                else if (r < 30)
                {
                    name = "Potion of Extra Health";
                    strength = 50;
                }
                else if (r < 50)
                {
                    name = "Potion of Health";
                    strength = 25;
                }
                else if (r < 70)
                {
                    name = "Potion of Speed";
                    strength = 3;
                    potType = PotionType.Speed;
                    ani = animations["potionOfSpeed"];
                }
                else if (r < 85)
                {
                    name = "Potion of Strength";
                    strength = 5;
                    potType = PotionType.Strength;
                }
                else
                {
                    name = "Potion of Defense";
                    strength = 5;
                    potType = PotionType.Defense;
                }
                Potion p = new Potion(name, new Vector(x, y), ani, strength, potType);
                map.Add(p);
            }
        }

    }
}
