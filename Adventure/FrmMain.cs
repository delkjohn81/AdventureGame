using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    public partial class FrmMain : Form
    {
        protected const int WM_KEYDOWN = 0x100;
        protected Game g;
        protected GameInfo gi;
        protected FrmInventory fInv;
        protected Stack<string> msgs = new Stack<string>();
        protected Direction dir = Direction.None;

        public FrmMain()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            g = new Game(Width, Height);
            g.Update += g_Update;
            g.Play();// begin the simulation
        }

        void g_Update(GameInfo gi)
        {
            this.gi = gi;
            if (gi.Log.Count >= 5)
            {
                for (int i = gi.Log.Count - 5; i < gi.Log.Count; i++)
                {
                    msgs.Push(gi.Log[i]);
                }
            }
            else
            {
                foreach (string s in gi.Log)
                {
                    msgs.Push(s);
                }
            }
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                Vector newDir = new Vector() ;
                if (keyData == Keys.Space)
                {
                    g.Pause();
                    fInv = new FrmInventory(gi.Link);
                    fInv.UseItem += fInv_UseItem;
                    fInv.FormClosed += fInv_FormClosed;
                    fInv.Show();// make Toolbar visible
                }
                else if (keyData == Keys.S)
                {// down
                    dir = Direction.Down;
                    newDir = new Vector(0, 15);
                    //Vector south = new Vector(0, 100);
                    //Vector goal = gi.Link.Position + south;
                    //gi.Link.Home = goal;
                }
                else if (keyData == Keys.W)
                {//up
                    dir = Direction.Up;
                    newDir = new Vector(0, -15);
                }
                else if (keyData == Keys.A)
                {//left
                    dir = Direction.Left;
                    newDir = new Vector(-15, 0);
                }
                else if (keyData == Keys.D)
                {// right
                    dir = Direction.Right;
                    newDir = new Vector(15, 0);
                }

                if (dir != Direction.None)
                {
                    gi.Link.Home = gi.Link.Position + newDir;
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        void fInv_FormClosed(object sender, FormClosedEventArgs e)
        {
            g.Play();
        }

        void fInv_UseItem(Item i)
        {
            g.UseItem(i);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics bob = e.Graphics;
            bob.Clear(Color.Black);
            if (true)
            {
                if (gi.Link != null)
                {
                    foreach (Locateable loc in gi.Map)
                    {   // paint the trees
                        bob.TranslateTransform((float)loc.Position.X, (float)loc.Position.Y);
                        bob.TranslateTransform(-loc.Width / 2, -loc.Height / 2);
                        bob.DrawImage(loc.Img, new Point());
                        bob.ResetTransform();
                    }

                    Image mobImg;
                    foreach (Mob m in gi.Monsters)
                    {
                        bob.TranslateTransform((float)m.Position.X, (float)m.Position.Y);
                        mobImg = m.Img;
                        if (m.Type == MobType.Orc)
                        {
                            bob.RotateTransform((float)(m.Angle - 180));
                        }
                        else
                        {
                            bob.RotateTransform((float)m.Angle);
                        }
                        bob.TranslateTransform(-m.Width / 2, -m.Height / 2);
                        bob.DrawImage(mobImg, new Point());
                        bob.ResetTransform();
                    }

                    bob.TranslateTransform((float)gi.Link.Position.X, (float)gi.Link.Position.Y);
                    bob.RotateTransform((float)(gi.Link.Angle));
                    bob.TranslateTransform(-gi.Link.Width / 2, -gi.Link.Height / 2);
                    bob.DrawImage(gi.Link.Img, new Point());
                    bob.ResetTransform();

                    foreach (Projectile proj in gi.Projectiles)
                    {
                        bob.TranslateTransform((float)proj.Position.X, (float)proj.Position.Y);
                        bob.RotateTransform((float)proj.Angle);
                        bob.TranslateTransform(-proj.Width / 2, -proj.Height / 2);
                        bob.DrawImage(proj.Img, new Point());
                        bob.ResetTransform();
                    }

                    bob.TranslateTransform((float)Screen.PrimaryScreen.WorkingArea.Width - 500,
                        (float)Screen.PrimaryScreen.WorkingArea.Height - 100);
                    System.Drawing.Font font = new Font(FontFamily.GenericSansSerif, 15);
                    Brush brush = new SolidBrush(Color.Red);
                    int yLog = 0;
                    if (msgs.Count > 0)
                    {
                        string s = msgs.Peek();
                        bob.DrawString(s, font, brush, 0, yLog);
                        yLog += 10;
                    }
                }
            }
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            PlayerInput pi = new PlayerInput();
            pi.MouseLoc = new Vector(e.X, e.Y);
            if(e.Button == MouseButtons.Right)
            {
                pi.Choice = PlayerChoice.MouseRightUp;
            }
            
            if(e.Button == MouseButtons.Left)
            {
                pi.Choice = PlayerChoice.MouseLeftUp;
            }
            g.PlayerInput(pi);
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                dir = Direction.None;
            }
        }
    }
}
