using diff;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace paper_maze
{
    public partial class MazeGame : Form
    {
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


        public MazeGame(int difficulty)
        {
            InitializeComponent();
            LoadMaze("level.txt");
            GenerateCoinsAndEnemies();
            InitializeGame(difficulty);
            DrawMaze();
        }

        private void InitializeGame(int difficulty)
        {
            playerHealth = 3;
            label1.Text = $"Coins: {coinsCollected} | Health: {playerHealth}";
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            if (difficulty < 30)
                cellSize = 16;
            else cellSize = 10;

            int pictureBoxWidth = cellSize * maze.GetLength(1);
            int pictureBoxHeight = cellSize * maze.GetLength(0);
            this.AutoSize = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pictureBox1.Size = new Size(pictureBoxWidth, pictureBoxHeight + 44);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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

            return base.ProcessCmdKey(ref msg, keyData);
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

        private void RestartGame()
        {
            levelComplete = false;
            coinsCollected = 0;
            playerHealth = 3;
            Application.Restart();
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

        private List<(int, int)> GetPossibleMoves(int y, int x)
        {
            List<(int, int)> possibleMoves = new List<(int, int)>();

            if (IsValidMove(y - 1, x)) possibleMoves.Add((y - 1, x)); // Up
            if (IsValidMove(y + 1, x)) possibleMoves.Add((y + 1, x)); // Down
            if (IsValidMove(y, x - 1)) possibleMoves.Add((y, x - 1)); // Left
            if (IsValidMove(y, x + 1)) possibleMoves.Add((y, x + 1)); // Right

            return possibleMoves;
        }

        private bool IsValidMove(int y, int x)
        {
            return y >= 0 && y < maze.GetLength(0) && x >= 0 && x < maze.GetLength(1) && maze[y, x] == '0';
        }

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

            pictureBox1.Image = bitmap;
        }


        // Temporarily here
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(528, 601);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Impact", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 558);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MazeGame
            // 
            this.ClientSize = new System.Drawing.Size(528, 601);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MazeGame";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}