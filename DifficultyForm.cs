using System;
using System.Windows.Forms;

namespace diff
{
    public partial class DifficultyForm : Form
    {
            public int DifficultyLevel { get; private set; }

            private RadioButton rbEasy;
            private RadioButton rbMedium;
            private RadioButton rbHard;
            private Button btnOk;
            private Button btnCancel;

            public DifficultyForm()
            {
                InitializeComponent();
            }

            // Temp init
            private void InitializeComponent()
            {
                // Init
                this.rbEasy = new System.Windows.Forms.RadioButton();
                this.rbMedium = new System.Windows.Forms.RadioButton();
                this.rbHard = new System.Windows.Forms.RadioButton();
                this.btnOk = new System.Windows.Forms.Button();
                this.btnCancel = new System.Windows.Forms.Button();
                this.SuspendLayout();

                // rbEasy
                this.rbEasy.AutoSize = true;
                this.rbEasy.Location = new System.Drawing.Point(50, 30);
                this.rbEasy.Name = "rbEasy";
                this.rbEasy.Size = new System.Drawing.Size(55, 17);
                this.rbEasy.Text = "Easy";
                this.rbEasy.TabIndex = 0;
                this.rbEasy.TabStop = true;

                // rbMedium
                this.rbMedium.AutoSize = true;
                this.rbMedium.Location = new System.Drawing.Point(50, 60);
                this.rbMedium.Name = "rbMedium";
                this.rbMedium.Size = new System.Drawing.Size(75, 17);
                this.rbMedium.Text = "Medium";
                this.rbMedium.TabIndex = 1;

                // rbHard
                this.rbHard.AutoSize = true;
                this.rbHard.Location = new System.Drawing.Point(50, 90);
                this.rbHard.Name = "rbHard";
                this.rbHard.Size = new System.Drawing.Size(55, 17);
                this.rbHard.Text = "Hard";
                this.rbHard.TabIndex = 2;

                // btnOk
                this.btnOk.Location = new System.Drawing.Point(50, 120);
                this.btnOk.Name = "btnOk";
                this.btnOk.Size = new System.Drawing.Size(75, 23);
                this.btnOk.Text = "OK";
                this.btnOk.UseVisualStyleBackColor = true;
                this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

                // btnCancel
                this.btnCancel.Location = new System.Drawing.Point(150, 120);
                this.btnCancel.Name = "btnCancel";
                this.btnCancel.Size = new System.Drawing.Size(75, 23);
                this.btnCancel.Text = "Cancel";
                this.btnCancel.UseVisualStyleBackColor = true;
                this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

                // Add
                this.Controls.Add(this.rbEasy);
                this.Controls.Add(this.rbMedium);
                this.Controls.Add(this.rbHard);
                this.Controls.Add(this.btnOk);
                this.Controls.Add(this.btnCancel);

                // Settings
                this.ClientSize = new System.Drawing.Size(250, 180);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Choose Difficulty";
                this.ResumeLayout(false);
            }

            private void btnOk_Click(object sender, EventArgs e)
            {
                if (rbEasy.Checked)
                    DifficultyLevel = 16;
                else if (rbMedium.Checked)
                    DifficultyLevel = 24;
                else if (rbHard.Checked)
                    DifficultyLevel = 36;

                DialogResult = DialogResult.OK;
                Close();
            }

            private void btnCancel_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }

