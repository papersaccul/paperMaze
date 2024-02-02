using diff;
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
            DifficultyForm difficultyForm = new DifficultyForm();

            if (difficultyForm.ShowDialog() == DialogResult.OK)
            {
                int difficulty = difficultyForm.DifficultyLevel;
                var maze = new Maze(difficulty, difficulty);
                maze.DisplayAndSaveToFile("level.txt");

                Application.Run(new MazeGame(difficulty));
            }
            else
            {
                return;
            }
        }
    }
}