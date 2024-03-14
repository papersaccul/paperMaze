using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace paper_maze
{
    public class UIManager
    {
        private Form _form;
        private MazeGenerator mazeGenerator = new MazeGenerator();
        private MazeGame _mazeGame;
        public UIManager(Form form) 
        { 
            _form = form;
            _mazeGame = (MazeGame)form;

        }

//============================= Button Click ==========================
        public void BtnStart_Click(object sender, EventArgs e)
        {
            HideMainMenu();
            ShowDifficultyForm();
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252
        }

        public void BtnOk_Click(object sender, EventArgs e)
        {
            RadioButton rbEasy = (RadioButton)_form.Controls["rbEasy"];
            RadioButton rbMedium = (RadioButton)_form.Controls["rbEasy"];

            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 216, 222, 223)); // #2e3440 
            if (rbEasy.Checked)
                _mazeGame.difficulty = 16;
            else if (rbMedium.Checked)
                _mazeGame.difficulty = 24;
            else // rbHard.Checked
                _mazeGame.difficulty = 32;



            _mazeGame.LoadMaze(mazeGenerator.GenerateMaze(_mazeGame.difficulty, _mazeGame.difficulty));
            _mazeGame.InitializeGame(_mazeGame.difficulty);
            HideDifficultyForm();
            ShowGameContent();
            _mazeGame.gameTimer.Start();
        }

        public void BtnCancel_Click(object sender, EventArgs e)
        {
            HideDifficultyForm();
            ShowMainMenu();
        }

        public void BtnSettings_Click(object sender, EventArgs e)
        {
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252

            _mazeGame.isAbout = true;

            _form.Controls["lbMenuHeader"].Visible = true;
            _form.Controls["btnResume"].Visible = false;
            _form.Controls["btnStart"].Visible = false;
            _form.Controls["btnSettings"].Visible = false;
            _form.Controls["btnAbout"].Visible = false;
            _form.Controls["lbMenuText"].Visible = true;

            _form.Controls["btnExit"].Text = "Back";
            _form.Controls["lbMenuHeader"].Text = "Guide";
            _form.Controls["lbMenuText"].Text = $"Arrows or WASD for movement,\nEsc for pause,\nYellow cells - coins,\nRed cells - enemies.\nGreen cell - finish";
        }

        public void BtnAbout_Click(object sender, EventArgs e)
        {
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252

            _mazeGame.isAbout = true;

            _form.Controls["lbMenuHeader"].Visible = true;
            _form.Controls["btnResume"].Visible = false;
            _form.Controls["btnStart"].Visible = false;
            _form.Controls["btnSettings"].Visible = false;
            _form.Controls["btnAbout"].Visible = false;
            _form.Controls["lbMenuText"].Visible = true;

            _form.Controls["btnExit"].Text = "Back";
            _form.Controls["lbMenuHeader"].Text = "About";
            _form.Controls["lbMenuText"].Text = $"Version {_mazeGame.version}\n\n https://github.com/papersaccul";
        }

        public void BtnResume_Click(object sender, EventArgs e)
        {
            HidePauseMenu();
            ShowGameContent();
        }
        public void BtnShop1_Click(object sender, EventArgs e)
        {
            if (_mazeGame.coinsCollected >= 5)
            {
                _mazeGame.coinsCollected -= 5;
                _mazeGame.playerHealth += 1;
                _mazeGame.HudUpdate();
                _form.Controls["btnShop1"].Enabled = _mazeGame.coinsCollected >= 5;
                _form.Controls["btnShop2"].Enabled = _mazeGame.coinsCollected >= 10;
                _form.Controls["btnShop1"].BackColor = _form.Controls["btnShop1"].Enabled ? Color.FromArgb(235, 203, 139) : Color.FromArgb(208, 135, 112); 
                _form.Controls["btnShop2"].BackColor = _form.Controls["btnShop2"].Enabled ? Color.FromArgb(235, 203, 139) : Color.FromArgb(208, 135, 112); 
            }
        }

        public void BtnShop2_Click(object sender, EventArgs e)
        {
            if (_mazeGame.coinsCollected >= 10)
            {
                _mazeGame.coinsCollected -= 10;
                _mazeGame.KillRandomEnemy();
                _mazeGame.HudUpdate();
                _form.Controls["btnShop1"].Enabled = _mazeGame.coinsCollected >= 5;
                _form.Controls["btnShop2"].Enabled = _mazeGame.coinsCollected >= 10;
                _form.Controls["btnShop1"].BackColor = _form.Controls["btnShop1"].Enabled ? Color.FromArgb(235, 203, 139) : Color.FromArgb(208, 135, 112); 
                _form.Controls["btnShop2"].BackColor = _form.Controls["btnShop2"].Enabled ? Color.FromArgb(235, 203, 139) : Color.FromArgb(208, 135, 112); 
            }
        }

        public void BtnExit_Click(object sender, EventArgs e)
        {
            if (_mazeGame.isAbout && _mazeGame.isPaused)
            {
                ShowPauseMenu();
                _form.Controls["lbMenuText"].Visible = false;
                _mazeGame.isAbout = false;
            }
            else if (_mazeGame.isPaused && !_mazeGame.isAbout)
            {
                HidePauseMenu();
                if (_mazeGame.isShopMenuOpen)
                {
                    BtnResume_Click(sender, e);
                    HideShopMenu();
                }
                else
                {
                    HideShopMenu();
                    HideDifficultyForm();
                    ShowMainMenu(); 
                    _form.Size = new Size(540, 640);
                }
            }
            else if (!_mazeGame.isPaused && _mazeGame.isAbout)
            {
                _mazeGame.isAbout = false;
                ShowMainMenu();
                _form.Controls["lbMenuText"].Visible = false;
            }
            else _form.Close();

        }

        //============================= UI Switch =============================
        public void HideGameContent()
        {
            _form.Controls["label1"].Visible = false;
            _form.Controls["pbGameArea"].Visible = false;
        }

        public void ShowGameContent()
        {
            _form.Controls["label1"].Visible = true;
            _form.Controls["pbGameArea"].Visible = true;
            _mazeGame.DrawMaze();
        }

        
        public void ShowDifficultyForm()
        {
            _form.Controls["lbMenuHeader"].Text = "Choose a difficulty";
            _form.Controls["lbMenuHeader"].Visible = true;

            _form.Controls["btnStart"].Visible = false;

            _form.Controls["rbEasy"].Visible = true;
            _form.Controls["rbMedium"].Visible = true;
            _form.Controls["rbHard"].Visible = true;

            _form.Controls["btnOk"].Visible = true;
            _form.Controls["btnCancel"].Visible = true;

        }

        public void HideDifficultyForm()
        {
            _form.Controls["lbMenuHeader"].Visible = false;
            _form.Controls["rbEasy"].Visible = false;
            _form.Controls["rbMedium"].Visible = false;
            _form.Controls["rbHard"].Visible = false;

            _form.Controls["btnOk"].Visible = false;
            _form.Controls["btnCancel"].Visible = false;
        }

        public void HideMainMenu()
        {
            _form.Controls["lbMenuHeader"].Visible = false;
            _form.Controls["btnStart"].Visible = false;
            _form.Controls["btnSettings"].Visible = false;
            _form.Controls["btnAbout"].Visible = false;
            _form.Controls["btnExit"].Visible = false;
        }

        public void ShowMainMenu()
        {
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 46, 52, 64)); // #2e3440

            _form.Controls["lbMenuText"].Visible = false;
            _form.Controls["lbMenuHeader"].Text = "Main Menu";
            _form.Controls["lbMenuHeader"].Visible = true;
            _form.Controls["btnStart"].Visible = true;
            _form.Controls["btnSettings"].Visible = true;
            _form.Controls["btnAbout"].Visible = true;
            _form.Controls["btnExit"].Text = "Exit";
            _form.Controls["btnExit"].Visible = true;
        }

        public void ShowPauseMenu()
        {
            _mazeGame.isPaused = true;
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252
            HideGameContent();

            _form.Controls["lbMenuHeader"].Text = "Pause";
            _form.Controls["lbMenuHeader"].Visible = true;
            _form.Controls["btnResume"].Visible = true;
            _form.Controls["btnSettings"].Visible = true;
            _form.Controls["btnAbout"].Visible = true;
            _form.Controls["btnExit"].Text = "Back";
            _form.Controls["btnExit"].Visible = true;
        }

        public void HidePauseMenu()
        {
            _mazeGame.isPaused = false;
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 216, 222, 223));

            _form.Controls["lbMenuHeader"].Visible = false;
            _form.Controls["btnResume"].Visible = false;
            _form.Controls["btnSettings"].Visible = false;
            _form.Controls["btnAbout"].Visible = false;
            _form.Controls["btnExit"].Text = "Exit";
            _form.Controls["btnExit"].Visible = false;
        }

        public void ShowShopMenu()
        {
            _mazeGame.isShopMenuOpen = true;
            _mazeGame.isPaused = true;
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 59, 66, 82)); // #3b4252
            HideGameContent();

            _form.Controls["lbMenuHeader"].Text = "Shop";
            _form.Controls["lbMenuHeader"].Visible = true;
            _form.Controls["btnShop1"].Visible = true;
            _form.Controls["btnShop2"].Visible = true;
            _form.Controls["btnShop1"].Enabled = _mazeGame.coinsCollected >= 5;
            _form.Controls["btnShop2"].Enabled = _mazeGame.coinsCollected >= 10;
            _form.Controls["btnShop1"].BackColor = _form.Controls["btnShop1"].Enabled ? Color.FromArgb(235, 203, 139) : Color.FromArgb(208, 135, 112); 
            _form.Controls["btnShop2"].BackColor = _form.Controls["btnShop2"].Enabled ? Color.FromArgb(235, 203, 139) : Color.FromArgb(208, 135, 112); 
            _form.Controls["lbShop1"].Visible = true;
            _form.Controls["lbShop2"].Visible = true;

            _form.Controls["btnOk"].Visible = false;
            _form.Controls["btnCancel"].Visible = false;
            _form.Controls["btnExit"].Text = "Back";
            _form.Controls["btnExit"].Visible = true;
        }
        public void HideShopMenu()
        {
            _mazeGame.isShopMenuOpen = false;
            _mazeGame.isPaused = false;
            _mazeGame.SmoothBackgroundColorTransition(Color.FromArgb(255, 216, 222, 223));

            _form.Controls["btnShop1"].Visible = false;
            _form.Controls["btnShop2"].Visible = false;
            _form.Controls["lbShop1"].Visible = false;
            _form.Controls["lbShop2"].Visible = false;
        }

    }
}
