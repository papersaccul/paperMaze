using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace paper_maze
{
    public partial class MazeGame : Form
    {
        private string version = "0.1.4";
        private char[,] maze;
        private int playerX;
        private int playerY;
        private int coinsCollected;
        private int totalCoins;
        private int playerHealth;
        private bool levelComplete;
        private bool[,] coinsGenerated;
        private Random random = new Random();
        private double coinProbability = 0.03;
        private double enemyProbability = 0.003;
        private double enemyMovementProbability = 0.4;
        private int cellSize;
        private int difficulty;
        private bool isPaused = false;
        private bool isAbout = false;

        public MazeGame()
        {
            InitializeComponent();


            btnStart.Click += BtnStart_Click;
            btnSettings.Click += BtnSettings_Click;
            btnAbout.Click += BtnAbout_Click;
            btnResume.Click += BtnResume_Click;
            btnExit.Click += BtnExit_Click;

            HideGameContent();
            HideDifficultyForm();
            ShowMainMenu();
        }

        //============================= Button Click ==========================
        private void BtnStart_Click(object sender, EventArgs e)
        {
            HideMainMenu();
            ShowDifficultyForm();
            this.BackColor = Color.FromArgb(255, 59, 66, 82); // #3b4252

            difficulty = 2;

            btnOk.Click += (s, a) =>
            {

                if (rbEasy.Checked)
                    difficulty = 16;
                else if (rbMedium.Checked)
                    difficulty = 24;
                else // rbHard.Checked
                    difficulty = 32;



                var maze = new Maze(difficulty, difficulty);
                maze.DisplayAndSaveToFile("level.txt");

                LoadMaze("level.txt");
                InitializeGame(difficulty);
                HideDifficultyForm();
                ShowGameContent();

            };

            btnCancel.Click += (s, a) =>
            {
                HideDifficultyForm();
                ShowMainMenu();
            };
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 59, 66, 82); // #3b4252

            isAbout = true;

            lbMenuHeader.Visible = true;
            btnResume.Visible = false;
            btnStart.Visible = false;
            btnSettings.Visible = false;
            btnAbout.Visible = false;
            lbMenuText.Visible = true;

            lbMenuHeader.Text = "Guide";
            lbMenuText.Text = $"Arrows or WASD for movement,\nEsc for pause,\nYellow cells - coins,\nRed cells - enemies.\nGreen cell - finish";
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 59, 66, 82); // #3b4252

            isAbout = true;

            lbMenuHeader.Visible = true;
            btnResume.Visible = false;
            btnStart.Visible = false;
            btnSettings.Visible = false;
            btnAbout.Visible = false;
            lbMenuText.Visible = true;

            lbMenuHeader.Text = "About";
            lbMenuText.Text = $"Version {version}\n\n https://github.com/papersaccul";
        }

        private void BtnResume_Click(object sender, EventArgs e)
        {
            HidePauseMenu();
            ShowGameContent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (isAbout && isPaused)
            {
                ShowPauseMenu();
                lbMenuText.Visible = false;

                isAbout = false;
            }
            else if (isPaused && !isAbout)
            {
                HidePauseMenu();
                HideDifficultyForm();
                ShowMainMenu();
                this.Size = new Size(540, 640);
            }
            else if (!isPaused && isAbout)
            {
                isAbout = false;
                ShowMainMenu();
                lbMenuText.Visible = false;
            }
            else Close();

        }

        //============================= UI Switch =============================
        private void HideGameContent()
        {
            label1.Visible = false;
            pbGameArea.Visible = false;
        }

        private void ShowGameContent()
        {
            label1.Visible = true;
            pbGameArea.Visible = true;
            DrawMaze();
        }

        private void ShowDifficultyForm()
        {
            lbMenuHeader.Text = "Choose a difficulty";
            lbMenuHeader.Visible = true;

            btnStart.Visible = false;

            rbEasy.Visible = true;
            rbMedium.Visible = true;
            rbHard.Visible = true;

            btnOk.Visible = true;
            btnCancel.Visible = true;

        }

        private void HideDifficultyForm()
        {
            lbMenuHeader.Visible = false;
            rbEasy.Visible = false;
            rbMedium.Visible = false;
            rbHard.Visible = false;

            btnOk.Visible = false;
            btnCancel.Visible = false;
        }

        private void HideMainMenu()
        {
            lbMenuHeader.Visible = false;
            btnStart.Visible = false;
            btnSettings.Visible = false;
            btnAbout.Visible = false;
            btnExit.Visible = false;
        }

        private void ShowMainMenu()
        {
            this.BackColor = Color.FromArgb(255, 46, 52, 64); // #2e3440

            lbMenuText.Visible = false;
            lbMenuHeader.Text = "Main Menu";
            lbMenuHeader.Visible = true;
            btnStart.Visible = true;
            btnSettings.Visible = true;
            btnAbout.Visible = true;
            btnExit.Visible = true;
        }

        private void ShowPauseMenu()
        {
            isPaused = true;

            HideGameContent();

            lbMenuHeader.Text = "Pause";
            lbMenuHeader.Visible = true;
            btnResume.Visible = true;
            btnSettings.Visible = true;
            btnAbout.Visible = true;
            btnExit.Visible = true;
        }

        private void HidePauseMenu()
        {
            isPaused = false;

            lbMenuHeader.Visible = false;
            btnResume.Visible = false;
            btnSettings.Visible = false;
            btnAbout.Visible = false;
            btnExit.Visible = false;
        }

        //============================= Init ===============================
        private void InitializeGame(int difficulty)
        {
            playerHealth = 3;
            label1.Text = $"Coins: {coinsCollected} | Health: {playerHealth}";
            pbGameArea.SizeMode = PictureBoxSizeMode.AutoSize;

            GenerateCoinsAndEnemies();

            if (difficulty < 30)
                cellSize = 16;
            else cellSize = 14;

            int pictureBoxWidth = cellSize * maze.GetLength(1);
            int pictureBoxHeight = cellSize * maze.GetLength(0);
            this.AutoSize = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pbGameArea.Size = new Size(pictureBoxWidth, pictureBoxHeight + 44);
        }

        private void LoadMaze(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int height = lines.Length;
                int width = lines[0].Length;
                maze = new char[height, width];
                coinsGenerated = new bool[height, width];

                totalCoins = 0;
                coinsCollected = 0;
                levelComplete = false;

                bool firstZeroFound = false;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        maze[i, j] = lines[i][j];

                        // Player Spawnpoint
                        if (maze[i, j] == 'P')
                        {
                            playerX = j;
                            playerY = i;
                        }
                        // Coins
                        else if (maze[i, j] == 'C')
                        {
                            totalCoins++;
                        }
                        // Replace the first '0' with 'P'
                        else if (maze[i, j] == '0' && !firstZeroFound)
                        {
                            maze[i, j] = 'P';
                            playerX = j;
                            playerY = i;
                            firstZeroFound = true;
                        }
                        // Replace the last '0' with 'F'
                        else if (maze[i, j] == '0' && j == width - 2 && i == height - 2)
                        {
                            maze[i, j] = 'F';
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading maze: {ex.Message}");
                RestartGame();
            }
        }

        private void GenerateCoinsAndEnemies()
        {
            totalCoins = 0;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    double randomValue = random.NextDouble();

                    if (maze[i, j] == '0')
                    {
                        totalCoins++;

                        // Place a coin
                        if (randomValue < coinProbability)
                        {
                            maze[i, j] = 'C';
                        }
                        // Place an enemy
                        else if (randomValue < coinProbability + enemyProbability)
                        {
                            maze[i, j] = 'E';
                        }
                    }
                }
            }
        }

        private void RestartGame()
        {
            levelComplete = false;
            coinsCollected = 0;
            playerHealth = 3;
            Application.Restart();
        }

        //============================= Update =============================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (pbGameArea.Visible == true)
            {


                if (levelComplete)
                {
                    MessageBox.Show($"Level complete! Coins collected: {coinsCollected}");
                    RestartGame();
                    return base.ProcessCmdKey(ref msg, keyData);
                }

                // Movement
                int newPlayerX = playerX;
                int newPlayerY = playerY;

                switch (keyData)
                {
                    case Keys.Up:
                        newPlayerY--;
                        break;
                    case Keys.Down:
                        newPlayerY++;
                        break;
                    case Keys.Left:
                        newPlayerX--;
                        break;
                    case Keys.Right:
                        newPlayerX++;
                        break;
                    case Keys.Escape:
                        if (!isPaused)
                            ShowPauseMenu();
                        else HidePauseMenu();
                        break;
                }

                ProcessEnemiesMovement();

                // Check new pos
                if (newPlayerX >= 0 && newPlayerX < maze.GetLength(1) && newPlayerY >= 0 && newPlayerY < maze.GetLength(0) && maze[newPlayerY, newPlayerX] != '1')
                {
                    playerX = newPlayerX;
                    playerY = newPlayerY;

                    // Coin Check
                    if (maze[playerY, playerX] == 'C')
                    {
                        coinsCollected++;
                        maze[playerY, playerX] = '0'; // Remove the coin
                        coinsGenerated[playerY, playerX] = false;
                        DrawMaze();
                        label1.Text = $"Coins: {coinsCollected} | Health: {playerHealth}";
                    }

                    // Enemy Check
                    else if (maze[playerY, playerX] == 'E')
                    {
                        playerHealth--;
                        if (playerHealth <= 0)
                        {
                            DrawMaze();
                            label1.Text = $"Coins: {coinsCollected} | Health: {playerHealth}";
                            MessageBox.Show("Game over! Player health depleted.");
                            RestartGame();
                        }
                        else
                        {
                            maze[playerY, playerX] = '0'; // Remove enemy
                            DrawMaze();
                            label1.Text = $"Coins: {coinsCollected} | Health: {playerHealth}";
                        }
                    }
                    else
                    {
                        DrawMaze();
                    }

                    // Win Check
                    if (maze[playerY, playerX] == 'F')
                    {
                        levelComplete = true;
                        MessageBox.Show($"Level complete! Coins collected: {coinsCollected}");
                        RestartGame();
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private List<(int, int)> GetPossibleMoves(int y, int x)
        {
            List<(int, int)> possibleMoves = new List<(int, int)>();

            if (IsValidMove(y - 1, x)) possibleMoves.Add((y - 1, x)); // Up
            if (IsValidMove(y + 1, x)) possibleMoves.Add((y + 1, x)); // Down
            if (IsValidMove(y, x - 1)) possibleMoves.Add((y, x - 1)); // Left
            if (IsValidMove(y, x + 1)) possibleMoves.Add((y, x + 1)); // Right

            return possibleMoves;
        }

        private void ProcessEnemiesMovement()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == 'E')
                    {
                        double randomValue = random.NextDouble();

                        if (randomValue < enemyMovementProbability)
                        {
                            MoveEnemy(i, j);
                        }
                    }
                }
            }
        }

        private void MoveEnemy(int enemyY, int enemyX)
        {
            List<(int, int)> possibleMoves = GetPossibleMoves(enemyY, enemyX);

            if (possibleMoves.Count > 0)
            {
                // Random
                int randomMoveIndex = random.Next(possibleMoves.Count);
                (int newY, int newX) = possibleMoves[randomMoveIndex];

                // Pos update
                maze[enemyY, enemyX] = '0';
                maze[newY, newX] = 'E';

                DrawMaze();
            }
        }

        private bool IsValidMove(int y, int x)
        {
            return y >= 0 && y < maze.GetLength(0) && x >= 0 && x < maze.GetLength(1) && maze[y, x] == '0';
        }

        //============================= Render ===============================
        private void DrawMaze()
        {

            Bitmap bitmap = new Bitmap(maze.GetLength(1) * cellSize, maze.GetLength(0) * cellSize);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        Brush brush;
                        if (maze[i, j] == '1')
                        {
                            brush = Brushes.Black; // Wall
                        }
                        else if (maze[i, j] == 'P')
                        {
                            brush = Brushes.White; // Player
                        }
                        else if (maze[i, j] == 'C')
                        {
                            brush = Brushes.Yellow; // Coin 
                        }
                        else if (maze[i, j] == 'E')
                        {
                            brush = Brushes.Red; // Enemy
                        }
                        else if (maze[i, j] == 'F')
                        {
                            brush = Brushes.Green; // Finish
                        }
                        else
                        {
                            brush = Brushes.White; // Space
                        }

                        g.FillRectangle(brush, j * cellSize, i * cellSize, cellSize, cellSize);

                        if (i == playerY && j == playerX)
                        {
                            g.FillEllipse(Brushes.Red, j * cellSize, i * cellSize, cellSize, cellSize);
                        }
                    }
                }
            }

            pbGameArea.Image = bitmap;
        }

    }
}