using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace abolscs
{
    public partial class Form1 : Form
    {
        /* Ekrānuzņēmumam */
        private class GDI32 /* nav mans! */
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        private class User32 /* arī nav mans! */
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }

        public System.Drawing.Image Ekranuznj() /* arī nav mans! */
        {
            IntPtr handle = User32.GetDesktopWindow();
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            GDI32.SelectObject(hdcDest, hOld);
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            Image img = Image.FromHbitmap(hBitmap);
            GDI32.DeleteObject(hBitmap);
            return img;
        }

        /* Mainīgie */
        private bool Ciklisks = false;

        private StreamReader fails;

        public Image ekruz;
        public Color Balts = Color.Red;
        public Color Melns = Color.Transparent;

        public int Rindas = 16;
        public int Kolonnas = 43;

        public int Kadri = 6569;
        public int Kadrs = 0;
        public int FPS = 30;

        Panel[,] flizes;

        public Timer Ture;

        public Form1()
        {
            GatavotFailu();

            if (MessageBox.Show("Lai apstādinātu šo izrādi, klaviatūrā spied [Alt] un [F4] vienlaicīgi. Turpināt?", "Pagaidi!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                Attirit();
            }

            /* ciklisks? */
            string[] cliargz = Environment.GetCommandLineArgs();
            if (cliargz.Length > 1 && cliargz[1] == "-c")
            {
                Ciklisks = true;
            }

            /* sagatavo taimeri */
            Ture = new Timer();
            Ture.Interval = 1000 / FPS;
            Ture.Tick += Zimet;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* sāk animāciju */
            ekruz = Ekranuznj();

            /* pārvērš flīžu krāsas par pretējām */
            Bitmap invp = new Bitmap(ekruz);
            for (int y = 0; (y <= (invp.Height - 1)); y++)
            {
                for (int x = 0; (x <= (invp.Width - 1)); x++)
                {
                    Color inv = invp.GetPixel(x, y);
                    inv = Color.FromArgb(inv.A, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    invp.SetPixel(x, y, inv);
                }
            }
            ekruz = invp;
            
            /* sagatavo flīzes */
            flizes = new Panel[Rindas, Kolonnas];

            for (int i = 0; i < Rindas; i++) {
                FlowLayoutPanel rinda = new FlowLayoutPanel();
                rinda.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
                rinda.Margin = new System.Windows.Forms.Padding(0);
                rinda.Height = this.Height / Rindas;
                rinda.Width = this.Width;

                for (int j = 0; j < Kolonnas; j++)
                {
                    flizes[i, j] = new Panel();
                    flizes[i, j].Margin = new System.Windows.Forms.Padding(0);
                    flizes[i, j].BackColor = Color.Green;
                    flizes[i, j].Width = this.Width / Kolonnas;
                    flizes[i, j].Height = this.Height / Rindas;
                    flizes[i, j].BackColor = Melns;

                    rinda.Controls.Add(flizes[i, j]);
                }

                this.molberts.Controls.Add(rinda);
            }

            this.BackgroundImage = ekruz;
            Ture.Start();
        }

        private void Zimet(object ssender, EventArgs e) {
            if (Kadrs > Kadri)
            {
                if (Ciklisks)
                {
                    fails.Close();
                    GatavotFailu();
                    Kadrs = 0;
                }
                else
                {
                    Ture.Stop();
                    Ture.Enabled = false;
                    Attirit();
                }
            }
            else
            {   
                String rinda;

                for (int i = 0; i < Rindas; i++)
                {
                    rinda = fails.ReadLine();
                    if (rinda != null)
                    {
                        for (int j = 0; j < Kolonnas; j++)
                        {
                            if (rinda[j] == ' ' && flizes[i, j].BackColor != Melns)
                                flizes[i, j].BackColor = Melns;
                            else if (rinda[j] == '!' && flizes[i, j].BackColor != Balts)
                                flizes[i, j].BackColor = Balts;
                        }
                    }
                }
                Kadrs++;
            }
        }

        private void GatavotFailu()
        {
            /* atver failu */
            try
            {
                fails = new StreamReader("rāmji.txt");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Nevarēju atvērt \"rāmji\".txt!!!");
                Attirit();
            }
        }

        private void Attirit()
        {
            if (fails != null) fails.Close();
            Environment.Exit(0);
        }
    }
}
