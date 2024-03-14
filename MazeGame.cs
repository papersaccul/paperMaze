using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

// Welcum to the "Le govnocod"

namespace paper_maze
{
    public partial class MazeGame : Form
    {
        public string version = "0.1.7";

        // Background color change timer
        private Color targetColor;
        private Timer colorChangeTimer;
        private const double colorChangeSpeed = 0.2f;

        // Wall color change timer
        private Timer wallColorChangeTimer;


        private char[,] maze;
        private int playerX;
        private int playerY;
        public int coinsCollected;
        public int totalCoins;
        public int playerHealth;
        private bool levelComplete;
        private bool[,] coinsGenerated;
        private Random random = new Random();
        public Timer gameTimer = new Timer();   
        private double coinProbability = 0.03f;
        private double enemyProbability = 0.005f;
        private double enemyMovementProbability = 0.02f;

        private int cellSize;
        public int difficulty;

        public bool isPaused = false;
        public bool isShopMenuOpen = false;
        public bool isAbout = false;
        public bool isStarted = false;
        private bool isTakingDamage = false;

        // interp movement
        private double playerVisualX;
        private double playerVisualY;
        private double playerInterpolationFactor = 0.5;

        // Colors
        private Color colorWall = Color.FromArgb(255, 46, 52, 64);
        private Color _initialWallColor;
        private int _damageColorTargetRed = 130;
        private int _damageColorIntensity = 80;
        private const double damageColorStrenght = 0.5f;
        
        private MazeGenerator mazeGenerator = new MazeGenerator();
        private UIManager uiManager;

        public MazeGame()
        {
            InitializeComponent();

            uiManager = new UIManager(this);

            gameTimer = new Timer();
            gameTimer.Interval = 32; // 31.25fps lock
            gameTimer.Tick += GameTick;

            colorChangeTimer = new Timer();
            colorChangeTimer.Interval = 16; // ~62.5 fps
            colorChangeTimer.Tick += BackgroundColorChangeTimer_Tick;

            wallColorChangeTimer = new Timer();
            wallColorChangeTimer.Interval = 32;
            wallColorChangeTimer.Tick += WallColorChangeTimer_Tick;
            _initialWallColor = colorWall;

            btnStart.Click += uiManager.BtnStart_Click;
            btnSettings.Click += uiManager.BtnSettings_Click;
            btnAbout.Click += uiManager.BtnAbout_Click;
            btnResume.Click += uiManager.BtnResume_Click;
            btnShop1.Click += uiManager.BtnShop1_Click;
            btnShop2.Click += uiManager.BtnShop2_Click;
            btnExit.Click += uiManager.BtnExit_Click;


            btnOk.Click += uiManager.BtnOk_Click;
            btnCancel.Click += uiManager.BtnCancel_Click;

            uiManager.HideGameContent();
            uiManager.HideDifficultyForm();
            uiManager.ShowMainMenu();

            while (isStarted)
            {
                if (isPaused)
                    gameTimer.Stop();
                else
                    gameTimer.Start();
            }
        }

        


        //============================= Init ===============================
        public void InitializeGame(int difficulty)
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

        public void LoadMaze(string[] mazeData)
        {
            try
            {
                int height = mazeData.Length;
                int width = mazeData[0].Length;
                maze = new char[height, width];
                coinsGenerated = new bool[height, width];

                totalCoins = 0;
                coinsCollected = 15;
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
                            uiManager.ShowPauseMenu();
                        else uiManager.HidePauseMenu();
                        break;
                    case Keys.Tab:
                        if (!isPaused)
                            uiManager.ShowShopMenu();
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

        public void KillRandomEnemy()
            {
                List<(int, int)> enemies = new List<(int, int)>();

                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        if (maze[i, j] == 'E')
                        {
                            enemies.Add((i, j));
                        }
                    }
                }

                if (enemies.Count > 0)
                {
                    int randomEnemyIndex = random.Next(enemies.Count);
                    (int enemyY, int enemyX) = enemies[randomEnemyIndex];

                    maze[enemyY, enemyX] = '0';
                }
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
                if (playerHealth > 0)
                {
                    playerHealth--;
                    isTakingDamage = true;
                    if (!wallColorChangeTimer.Enabled)
                        wallColorChangeTimer.Start();
                    else
                    {
                        wallColorChangeTimer.Stop();
                        colorWall = _initialWallColor;
                    }
                    
                }
                    
                if (playerHealth <= 0)
                {
                    DrawMaze();
                    HudUpdate();
                    maze[playerY, playerX] = '0'; // Remove enemy
                    gameTimer.Stop();
                    MessageBox.Show("Game over! Player health depleted.");
                    gameTimer.Stop();
                    uiManager.HideGameContent();
                    uiManager.ShowMainMenu();
                }
                else
                {
                    maze[playerY, playerX] = '0'; // Remove enemy
                    DrawMaze();
                    HudUpdate();
                }
            }
        }

        public void HudUpdate()
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

        public void BackgroundColorChangeTimer_Tick(object sender, EventArgs e)
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

        private void WallColorChangeTimer_Tick(object sender, EventArgs e)
        {
            if (isTakingDamage) // go to red
            {
                colorWall = Color.FromArgb(
                    Math.Min(colorWall.R + (int)(_damageColorIntensity * damageColorStrenght), _damageColorTargetRed),
                    colorWall.G,
                    colorWall.B
                );

                if (colorWall.R == _damageColorTargetRed)
                    isTakingDamage = false;
            }
            else // go to initialColor
            {
                colorWall = Color.FromArgb(
                    Math.Max(colorWall.R - (int)(_damageColorIntensity * damageColorStrenght), _initialWallColor.R),
                    colorWall.G,
                    colorWall.B
                );

                if (colorWall.R == _initialWallColor.R)
                    wallColorChangeTimer.Stop();
                }
                }
        


        //============================= Render ===============================
        public void DrawMaze()
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
                            brush = new SolidBrush(colorWall);
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

        public void SmoothBackgroundColorTransition(Color targetColor)
        {
            this.targetColor = targetColor;
            colorChangeTimer.Start();
        }
    }
}

