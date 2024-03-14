using System;

namespace paper_maze
{
    partial class MazeGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.PictureBox pbGameArea;
        private System.Windows.Forms.Label label1;

        protected void InitializeComponent()
        {
            this.pbGameArea = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbMenuHeader = new System.Windows.Forms.Label();
            this.lbMenuText = new System.Windows.Forms.Label();
            this.lbShop1 = new System.Windows.Forms.Label();
            this.lbShop2 = new System.Windows.Forms.Label();
            this.btnShop2 = new paper_maze.pmButton();
            this.btnShop1 = new paper_maze.pmButton();
            this.rbHard = new paper_maze.pmRadioButton();
            this.rbMedium = new paper_maze.pmRadioButton();
            this.rbEasy = new paper_maze.pmRadioButton();
            this.btnCancel = new paper_maze.pmButton();
            this.btnOk = new paper_maze.pmButton();
            this.btnStart = new paper_maze.pmButton();
            this.btnResume = new paper_maze.pmButton();
            this.btnExit = new paper_maze.pmButton();
            this.btnAbout = new paper_maze.pmButton();
            this.btnSettings = new paper_maze.pmButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbGameArea)).BeginInit();
            this.SuspendLayout();
            // 
            // pbGameArea
            // 
            this.pbGameArea.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbGameArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbGameArea.Location = new System.Drawing.Point(0, 0);
            this.pbGameArea.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pbGameArea.Name = "pbGameArea";
            this.pbGameArea.Size = new System.Drawing.Size(524, 601);
            this.pbGameArea.TabIndex = 0;
            this.pbGameArea.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(222)))), ((int)(((byte)(233)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Inter", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.label1.Location = new System.Drawing.Point(0, 559);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbMenuHeader
            // 
            this.lbMenuHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbMenuHeader.Font = new System.Drawing.Font("Inter", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMenuHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(222)))), ((int)(((byte)(233)))));
            this.lbMenuHeader.Location = new System.Drawing.Point(10, 155);
            this.lbMenuHeader.Name = "lbMenuHeader";
            this.lbMenuHeader.Size = new System.Drawing.Size(504, 43);
            this.lbMenuHeader.TabIndex = 12;
            this.lbMenuHeader.Text = "MenuHeader";
            this.lbMenuHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMenuText
            // 
            this.lbMenuText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbMenuText.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMenuText.ForeColor = System.Drawing.Color.White;
            this.lbMenuText.Location = new System.Drawing.Point(10, 207);
            this.lbMenuText.Name = "lbMenuText";
            this.lbMenuText.Size = new System.Drawing.Size(504, 134);
            this.lbMenuText.TabIndex = 2;
            this.lbMenuText.Text = "lbMenuText";
            this.lbMenuText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbShop1
            // 
            this.lbShop1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbShop1.BackColor = System.Drawing.Color.Transparent;
            this.lbShop1.Font = new System.Drawing.Font("Inter", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbShop1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(222)))), ((int)(((byte)(233)))));
            this.lbShop1.Location = new System.Drawing.Point(52, 237);
            this.lbShop1.Name = "lbShop1";
            this.lbShop1.Size = new System.Drawing.Size(268, 43);
            this.lbShop1.TabIndex = 25;
            this.lbShop1.Text = "Cure 1 health point";
            this.lbShop1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbShop1.Visible = false;
            // 
            // lbShop2
            // 
            this.lbShop2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbShop2.BackColor = System.Drawing.Color.Transparent;
            this.lbShop2.Font = new System.Drawing.Font("Inter", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbShop2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(222)))), ((int)(((byte)(233)))));
            this.lbShop2.Location = new System.Drawing.Point(52, 283);
            this.lbShop2.Name = "lbShop2";
            this.lbShop2.Size = new System.Drawing.Size(268, 43);
            this.lbShop2.TabIndex = 26;
            this.lbShop2.Text = "Kill random enemy";
            this.lbShop2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbShop2.Visible = false;
            // 
            // btnShop2
            // 
            this.btnShop2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnShop2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShop2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(203)))), ((int)(((byte)(139)))));
            this.btnShop2.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnShop2.BackColorGradientEnabled = false;
            this.btnShop2.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnShop2.BorderColor = System.Drawing.Color.Tomato;
            this.btnShop2.BorderColorEnabled = false;
            this.btnShop2.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnShop2.BorderColorOnHoverEnabled = false;
            this.btnShop2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShop2.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnShop2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnShop2.Location = new System.Drawing.Point(338, 286);
            this.btnShop2.Name = "btnShop2";
            this.btnShop2.RippleColor = System.Drawing.Color.Black;
            this.btnShop2.Rounding = 45;
            this.btnShop2.RoundingEnable = true;
            this.btnShop2.Size = new System.Drawing.Size(104, 40);
            this.btnShop2.TabIndex = 24;
            this.btnShop2.Text = "Buy 10¢";
            this.btnShop2.TextHover = null;
            this.btnShop2.UseDownPressEffectOnClick = false;
            this.btnShop2.UseRippleEffect = true;
            this.btnShop2.UseVisualStyleBackColor = false;
            this.btnShop2.UseZoomEffectOnHover = true;
            this.btnShop2.Visible = false;
            // 
            // btnShop1
            // 
            this.btnShop1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnShop1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(203)))), ((int)(((byte)(139)))));
            this.btnShop1.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnShop1.BackColorGradientEnabled = false;
            this.btnShop1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnShop1.BorderColor = System.Drawing.Color.Tomato;
            this.btnShop1.BorderColorEnabled = false;
            this.btnShop1.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnShop1.BorderColorOnHoverEnabled = false;
            this.btnShop1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShop1.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnShop1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnShop1.Location = new System.Drawing.Point(338, 240);
            this.btnShop1.Name = "btnShop1";
            this.btnShop1.RippleColor = System.Drawing.Color.Black;
            this.btnShop1.Rounding = 45;
            this.btnShop1.RoundingEnable = true;
            this.btnShop1.Size = new System.Drawing.Size(104, 40);
            this.btnShop1.TabIndex = 23;
            this.btnShop1.Text = "Buy  5¢";
            this.btnShop1.TextHover = null;
            this.btnShop1.UseDownPressEffectOnClick = false;
            this.btnShop1.UseRippleEffect = true;
            this.btnShop1.UseVisualStyleBackColor = false;
            this.btnShop1.UseZoomEffectOnHover = true;
            this.btnShop1.Visible = false;
            // 
            // rbHard
            // 
            this.rbHard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbHard.BackColor = System.Drawing.Color.Transparent;
            this.rbHard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbHard.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbHard.ForeColor = System.Drawing.Color.White;
            this.rbHard.Location = new System.Drawing.Point(199, 245);
            this.rbHard.Name = "rbHard";
            this.rbHard.Size = new System.Drawing.Size(150, 30);
            this.rbHard.TabIndex = 22;
            this.rbHard.Text = "Hard";
            this.rbHard.UseVisualStyleBackColor = false;
            // 
            // rbMedium
            // 
            this.rbMedium.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbMedium.BackColor = System.Drawing.Color.Transparent;
            this.rbMedium.Checked = true;
            this.rbMedium.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbMedium.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbMedium.ForeColor = System.Drawing.Color.White;
            this.rbMedium.Location = new System.Drawing.Point(199, 279);
            this.rbMedium.Name = "rbMedium";
            this.rbMedium.Size = new System.Drawing.Size(150, 30);
            this.rbMedium.TabIndex = 21;
            this.rbMedium.TabStop = true;
            this.rbMedium.Text = "Medium";
            this.rbMedium.UseVisualStyleBackColor = false;
            // 
            // rbEasy
            // 
            this.rbEasy.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbEasy.BackColor = System.Drawing.Color.Transparent;
            this.rbEasy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbEasy.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbEasy.ForeColor = System.Drawing.Color.White;
            this.rbEasy.Location = new System.Drawing.Point(199, 315);
            this.rbEasy.Name = "rbEasy";
            this.rbEasy.Size = new System.Drawing.Size(150, 30);
            this.rbEasy.TabIndex = 20;
            this.rbEasy.Text = "Easy";
            this.rbEasy.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(97)))), ((int)(((byte)(106)))));
            this.btnCancel.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnCancel.BackColorGradientEnabled = false;
            this.btnCancel.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnCancel.BorderColor = System.Drawing.Color.Tomato;
            this.btnCancel.BorderColorEnabled = false;
            this.btnCancel.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnCancel.BorderColorOnHoverEnabled = false;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnCancel.Location = new System.Drawing.Point(257, 388);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RippleColor = System.Drawing.Color.Black;
            this.btnCancel.Rounding = 45;
            this.btnCancel.RoundingEnable = true;
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextHover = null;
            this.btnCancel.UseDownPressEffectOnClick = false;
            this.btnCancel.UseRippleEffect = true;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.UseZoomEffectOnHover = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(190)))), ((int)(((byte)(140)))));
            this.btnOk.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnOk.BackColorGradientEnabled = false;
            this.btnOk.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnOk.BorderColor = System.Drawing.Color.Tomato;
            this.btnOk.BorderColorEnabled = false;
            this.btnOk.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnOk.BorderColorOnHoverEnabled = false;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnOk.Location = new System.Drawing.Point(100, 388);
            this.btnOk.Name = "btnOk";
            this.btnOk.RippleColor = System.Drawing.Color.Black;
            this.btnOk.Rounding = 45;
            this.btnOk.RoundingEnable = true;
            this.btnOk.Size = new System.Drawing.Size(150, 40);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "Ok";
            this.btnOk.TextHover = null;
            this.btnOk.UseDownPressEffectOnClick = false;
            this.btnOk.UseRippleEffect = true;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.UseZoomEffectOnHover = true;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(190)))), ((int)(((byte)(140)))));
            this.btnStart.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnStart.BackColorGradientEnabled = false;
            this.btnStart.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnStart.BorderColor = System.Drawing.Color.Tomato;
            this.btnStart.BorderColorEnabled = false;
            this.btnStart.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnStart.BorderColorOnHoverEnabled = false;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnStart.Location = new System.Drawing.Point(182, 218);
            this.btnStart.Name = "btnStart";
            this.btnStart.RippleColor = System.Drawing.Color.Black;
            this.btnStart.Rounding = 45;
            this.btnStart.RoundingEnable = true;
            this.btnStart.Size = new System.Drawing.Size(150, 40);
            this.btnStart.TabIndex = 13;
            this.btnStart.Text = "Start";
            this.btnStart.TextHover = null;
            this.btnStart.UseDownPressEffectOnClick = false;
            this.btnStart.UseRippleEffect = true;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.UseZoomEffectOnHover = true;
            // 
            // btnResume
            // 
            this.btnResume.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnResume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnResume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(190)))), ((int)(((byte)(140)))));
            this.btnResume.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnResume.BackColorGradientEnabled = false;
            this.btnResume.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnResume.BorderColor = System.Drawing.Color.Tomato;
            this.btnResume.BorderColorEnabled = false;
            this.btnResume.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnResume.BorderColorOnHoverEnabled = false;
            this.btnResume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResume.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnResume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnResume.Location = new System.Drawing.Point(182, 218);
            this.btnResume.Name = "btnResume";
            this.btnResume.RippleColor = System.Drawing.Color.Black;
            this.btnResume.Rounding = 45;
            this.btnResume.RoundingEnable = true;
            this.btnResume.Size = new System.Drawing.Size(150, 40);
            this.btnResume.TabIndex = 17;
            this.btnResume.Text = "Resume";
            this.btnResume.TextHover = null;
            this.btnResume.UseDownPressEffectOnClick = false;
            this.btnResume.UseRippleEffect = true;
            this.btnResume.UseVisualStyleBackColor = false;
            this.btnResume.UseZoomEffectOnHover = true;
            this.btnResume.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(97)))), ((int)(((byte)(106)))));
            this.btnExit.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnExit.BackColorGradientEnabled = false;
            this.btnExit.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnExit.BorderColor = System.Drawing.Color.Tomato;
            this.btnExit.BorderColorEnabled = false;
            this.btnExit.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnExit.BorderColorOnHoverEnabled = false;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnExit.Location = new System.Drawing.Point(182, 356);
            this.btnExit.Name = "btnExit";
            this.btnExit.RippleColor = System.Drawing.Color.Black;
            this.btnExit.Rounding = 45;
            this.btnExit.RoundingEnable = true;
            this.btnExit.Size = new System.Drawing.Size(150, 40);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "Exit";
            this.btnExit.TextHover = null;
            this.btnExit.UseDownPressEffectOnClick = true;
            this.btnExit.UseRippleEffect = true;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.UseZoomEffectOnHover = false;
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAbout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(135)))), ((int)(((byte)(112)))));
            this.btnAbout.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnAbout.BackColorGradientEnabled = false;
            this.btnAbout.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnAbout.BorderColor = System.Drawing.Color.Tomato;
            this.btnAbout.BorderColorEnabled = false;
            this.btnAbout.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnAbout.BorderColorOnHoverEnabled = false;
            this.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbout.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnAbout.Location = new System.Drawing.Point(182, 310);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.RippleColor = System.Drawing.Color.Black;
            this.btnAbout.Rounding = 45;
            this.btnAbout.RoundingEnable = true;
            this.btnAbout.Size = new System.Drawing.Size(150, 40);
            this.btnAbout.TabIndex = 15;
            this.btnAbout.Text = "About";
            this.btnAbout.TextHover = null;
            this.btnAbout.UseDownPressEffectOnClick = false;
            this.btnAbout.UseRippleEffect = true;
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.UseZoomEffectOnHover = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(203)))), ((int)(((byte)(139)))));
            this.btnSettings.BackColorAdditional = System.Drawing.Color.Transparent;
            this.btnSettings.BackColorGradientEnabled = false;
            this.btnSettings.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnSettings.BorderColor = System.Drawing.Color.Tomato;
            this.btnSettings.BorderColorEnabled = false;
            this.btnSettings.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnSettings.BorderColorOnHoverEnabled = false;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.btnSettings.Location = new System.Drawing.Point(182, 264);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.RippleColor = System.Drawing.Color.Black;
            this.btnSettings.Rounding = 45;
            this.btnSettings.RoundingEnable = true;
            this.btnSettings.Size = new System.Drawing.Size(150, 40);
            this.btnSettings.TabIndex = 14;
            this.btnSettings.Text = "Guide";
            this.btnSettings.TextHover = null;
            this.btnSettings.UseDownPressEffectOnClick = false;
            this.btnSettings.UseRippleEffect = true;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.UseZoomEffectOnHover = true;
            // 
            // MazeGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(524, 601);
            this.Controls.Add(this.lbShop2);
            this.Controls.Add(this.lbShop1);
            this.Controls.Add(this.btnShop2);
            this.Controls.Add(this.btnShop1);
            this.Controls.Add(this.rbHard);
            this.Controls.Add(this.rbMedium);
            this.Controls.Add(this.rbEasy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lbMenuHeader);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMenuText);
            this.Controls.Add(this.pbGameArea);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MazeGame";
            ((System.ComponentModel.ISupportInitialize)(this.pbGameArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private System.Windows.Forms.Label lbMenuHeader;
        private pmButton btnSettings;
        private pmButton btnAbout;
        private pmButton btnExit;
        private pmButton btnResume;
        private pmButton btnStart;
        private pmButton btnOk;
        private pmButton btnCancel;
        private pmRadioButton rbEasy;
        private pmRadioButton rbMedium;
        private pmRadioButton rbHard;
        private System.Windows.Forms.Label lbMenuText;
        private pmButton btnShop1;
        private pmButton btnShop2;
        private System.Windows.Forms.Label lbShop1;
        private System.Windows.Forms.Label lbShop2;
    }
}


