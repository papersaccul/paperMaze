using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace paper_maze
{
    public partial class MazeGame : Form
    {
        private string version = "0.1.5";

        private Color targetColor;
        private Timer colorChangeTimer;
        private const double colorChangeSpeed = 0.2f;

        private char[,] maze;
        private int playerX;
        private int playerY;

        private int coinsCollected;
        private int totalCoins;
        private int playerHealth;
        private bool levelComplete;
        private bool[,] coinsGenerated;
        private Random random = new Random();
        Timer gameTimer = new Timer();
        private double coinProbability = 0.03;
        private double enemyProbability = 0.005;
        private double enemyMovementProbability = 0.025;

        private int cellSize;
        private int difficulty;
        private bool isPaused = false;
        private bool isAbout = false;
        private bool isStarted = false;

        // interp movement
        private double playerVisualX;
        private double playerVisualY;
        private double playerInterpolationFactor = 0.5;

        private MazeGenerator mazeGenerator;

        public MazeGame()
        {
            InitializeComponent();

            mazeGenerator = new MazeGenerator();

            gameTimer = new Timer();
            gameTimer.Interval = 16; // 62.5fps lock
            gameTimer.Tick += GameTick;

            colorChangeTimer = new Timer();
            colorChangeTimer.Interval = 16; // ~62.5fps
            colorChangeTimer.Tick += ColorChangeTimer_Tick;

            btnStart.Click += BtnStart_Click;
            btnSettings.Click += BtnSettings_Click;
            btnAbout.Click += BtnAbout_Click;
            btnResume.Click += BtnResume_Click;
            btnExit.Click += BtnExit_Click;

            HideGameContent();
            HideDifficultyForm();
            ShowMainMenu();

            while (isStarted)
            {
                if (isPaused)
                    gameTimer.Stop();
                else
                    gameTimer.Start();
            }
        }

        //============================= Button Click ==========================
        private void BtnStart_Click(object sender, EventArgs e)
        {
            HideMainMenu();
            ShowDifficultyForm();
            SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252


            btnOk.Click += (s, a) =>
            {
                SmoothBackgroundColorTransition(Color.FromArgb(255, 216, 222, 223)); // #2e3440 
                    if (rbEasy.Checked)
                        difficulty = 16;
                    else if (rbMedium.Checked)
                        difficulty = 24;
                    else // rbHard.Checked
                        difficulty = 32;



                LoadMaze(mazeGenerator.GenerateMaze(difficulty, difficulty));
                InitializeGame(difficulty);
                HideDifficultyForm();
                ShowGameContent();
                gameTimer.Start();

            };

            btnCancel.Click += (s, a) =>
            {
                HideDifficultyForm();
                ShowMainMenu();
            };
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252

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
            SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252

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
            SmoothBackgroundColorTransition(Color.FromArgb(255, 46, 52, 64)); // #2e3440

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
            SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252
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

            SmoothBackgroundColorTransition(Color.FromArgb(255, 216, 222, 223));

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
            HudUpdate();
            pbGameArea.SizeMode = PictureBoxSizeMode.AutoSize;
            pbGameArea.BackColor = Color.FromArgb(0, 0, 0, 0);

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

        private void LoadMaze(string[] mazeData)
        {
            try
            {
                int height = mazeData.Length;
                int width = mazeData[0].Length;
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
                        maze[i, j] = mazeData[i][j];

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
                        HudUpdate();
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

        private void PosChecker()
        {
            if (maze[playerY, playerX] == 'E')
            {
                playerHealth--;
                if (playerHealth <= 0)
                {
                    DrawMaze();
                    HudUpdate();
                    MessageBox.Show("Game over! Player health depleted.");
                    RestartGame();
                }
                else
                {
                    maze[playerY, playerX] = '0'; // Remove enemy
                    DrawMaze();
                    HudUpdate();
                }
            }
        }

        private void HudUpdate()
        {
            label1.Text = $"Coins: {coinsCollected} | Health: {playerHealth}";
        }

        private bool IsValidMove(int y, int x)
        {
            return y >= 0 && y < maze.GetLength(0) && x >= 0 && x < maze.GetLength(1) && maze[y, x] == '0';
        }

        private void GameTick(object sender, EventArgs e)
        {
            ProcessEnemiesMovement();
            PosChecker();
            DrawMaze();
        }

        private void ColorChangeTimer_Tick(object sender, EventArgs e)
        {

            int currentR = this.BackColor.R;
            int currentG = this.BackColor.G;
            int currentB = this.BackColor.B;

            int targetR = targetColor.R;
            int targetG = targetColor.G;
            int targetB = targetColor.B;

            int newR = (int)(currentR + (targetR - currentR) * colorChangeSpeed);
            int newG = (int)(currentG + (targetG - currentG) * colorChangeSpeed);
            int newB = (int)(currentB + (targetB - currentB) * colorChangeSpeed);

            this.BackColor = Color.FromArgb(newR, newG, newB);

            if (Math.Abs(newR - targetR) < 2 && Math.Abs(newG - targetG) < 2 && Math.Abs(newB - targetB) < 2)
            {
                colorChangeTimer.Stop();
            }
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
                            brush = new SolidBrush(Color.FromArgb(255, 46, 52, 64)); // Wall
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
                            brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0)); // Space
                        }

                        int currentX = j * cellSize;
                        int currentY = i * cellSize;


                        if (i != playerY || j != playerX)
                        {
                            g.FillRectangle(brush, currentX, currentY, cellSize, cellSize);
                        }
                        else
                        {
                            int visualX = (int)(playerVisualX * cellSize);
                            int visualY = (int)(playerVisualY * cellSize);

                            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0, 0)), currentX, currentY, cellSize, cellSize);
                            g.FillEllipse(Brushes.Red, visualX, visualY, cellSize, cellSize);
                        }

                        Application.DoEvents();
                    }
                }
            }

            pbGameArea.Image = bitmap;

            double factor = HermiteInterpolate(playerInterpolationFactor);
            playerVisualX += (playerX - playerVisualX) * factor;
            playerVisualY += (playerY - playerVisualY) * factor;
        }

        private double HermiteInterpolate(double t)
        {
            return t * t * (3 - 2 * t);
        }

        private void SmoothBackgroundColorTransition(Color targetColor)
        {
            this.targetColor = targetColor;
            colorChangeTimer.Start();
        }

    }
}