using System;
using System.Windows.Forms;

namespace paper_maze
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var gameForm = new MazeGame();
            Application.Run(gameForm);
        }
    }
}