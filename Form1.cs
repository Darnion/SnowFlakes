using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnowFlakes
{
    public partial class Form1 : Form
    {
        private readonly List<SnowFlake> snowflakes;
        private readonly Timer timer;
        private Bitmap background, snowflake, bgScreen, sfScreen;
        private Graphics draw;
        public Form1()
        {
            InitializeComponent();

            snowflakes = new List<SnowFlake>();
            background = new Bitmap(Properties.Resources.background);
            snowflake = new Bitmap(Properties.Resources.snowflake);

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;

            bgScreen = new Bitmap(background,
                                  Screen.PrimaryScreen.WorkingArea.Width,
                                  Screen.PrimaryScreen.WorkingArea.Height + 40);

            sfScreen = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width,
                                  Screen.PrimaryScreen.WorkingArea.Height + 40);

            draw = Graphics.FromImage(sfScreen);

            CreateSnowFlakes();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }

        private void CreateSnowFlakes()
        {
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                snowflakes.Add(new SnowFlake(random.Next(Screen.PrimaryScreen.WorkingArea.Width),
                                             -random.Next(20, Screen.PrimaryScreen.WorkingArea.Height + 40),
                                             random.Next(10, 20)));
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Draw();
            timer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            draw.DrawImage(bgScreen, 0, 0);

            foreach (var sf in snowflakes)
            {
                if (sf.Top > Screen.PrimaryScreen.WorkingArea.Height + 40)
                {
                    sf.Top = -sf.Size;
                }

                draw.DrawImage(snowflake,
                    new Rectangle(sf.Left,
                                  sf.Top,
                                  sf.Size,
                                  sf.Size));

                sf.Top += sf.Size;
            }

            var graph = CreateGraphics();
            graph.DrawImage(sfScreen, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
