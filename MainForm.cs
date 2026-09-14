using System;
using System.Drawing;
using System.Windows.Forms;

namespace FreeFirePanel
{
    public partial class MainForm : Form
    {
        private bool esEnabled = false;
        private bool aimEnabled = false;
        private bool flEnabled = false;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form Settings
            this.ClientSize = new Size(500, 650);
            this.BackColor = Color.FromArgb(20, 20, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);
            this.Text = "FREE FIRE - ELITE PANEL";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = null;

            // Add rounded corners
            int radius = 15;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);

            // Header Panel
            Panel headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(255, 76, 0);
            headerPanel.Size = new Size(500, 80);
            headerPanel.Location = new Point(0, 0);
            headerPanel.Paint += HeaderPanel_Paint;
            this.Controls.Add(headerPanel);

            // Title Label
            Label titleLabel = new Label();
            titleLabel.Text = "⚡ FREE FIRE ELITE ⚡";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(100, 20);
            headerPanel.Controls.Add(titleLabel);

            // Subtitle
            Label subtitleLabel = new Label();
            subtitleLabel.Text = "Professional Gaming Panel";
            subtitleLabel.Font = new Font("Segoe UI", 9);
            subtitleLabel.ForeColor = Color.FromArgb(230, 230, 230);
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(120, 50);
            headerPanel.Controls.Add(subtitleLabel);

            // Close Button
            Button closeBtn = new Button();
            closeBtn.Text = "✕";
            closeBtn.Font = new Font("Segoe UI", 12);
            closeBtn.ForeColor = Color.White;
            closeBtn.BackColor = Color.Transparent;
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Size = new Size(40, 40);
            closeBtn.Location = new Point(455, 10);
            closeBtn.Click += (s, e) => this.Close();
            headerPanel.Controls.Add(closeBtn);

            // Content Panel
            Panel contentPanel = new Panel();
            contentPanel.BackColor = Color.FromArgb(25, 25, 40);
            contentPanel.Size = new Size(480, 540);
            contentPanel.Location = new Point(10, 90);
            this.Controls.Add(contentPanel);

            // ES Switch
            CreateSwitchPanel(contentPanel, "ES (Enemy Sense)", 30, ref esEnabled, 0);

            // AIM Switch
            CreateSwitchPanel(contentPanel, "AIM (Auto Aim)", 150, ref aimEnabled, 1);

            // FL Switch
            CreateSwitchPanel(contentPanel, "FL (FlashLight)", 270, ref flEnabled, 2);

            // Info Panel
            Panel infoPanel = new Panel();
            infoPanel.BackColor = Color.FromArgb(35, 35, 50);
            infoPanel.Size = new Size(460, 80);
            infoPanel.Location = new Point(10, 400);
            infoPanel.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 76, 0), 2), 0, 0, infoPanel.Width - 1, infoPanel.Height - 1);
            };
            contentPanel.Controls.Add(infoPanel);

            Label statusLabel = new Label();
            statusLabel.Text = "STATUS:";
            statusLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            statusLabel.ForeColor = Color.FromArgb(255, 76, 0);
            statusLabel.Location = new Point(15, 10);
            statusLabel.AutoSize = true;
            infoPanel.Controls.Add(statusLabel);

            Label statusValueLabel = new Label();
            statusValueLabel.Text = "Ready";
            statusValueLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusValueLabel.ForeColor = Color.LimeGreen;
            statusValueLabel.Location = new Point(80, 10);
            statusValueLabel.AutoSize = true;
            infoPanel.Controls.Add(statusValueLabel);

            Label infoLabel = new Label();
            infoLabel.Text = "All switches are ready to use\nEnable features as needed";
            infoLabel.Font = new Font("Segoe UI", 8);
            infoLabel.ForeColor = Color.FromArgb(200, 200, 200);
            infoLabel.Location = new Point(15, 35);
            infoPanel.Controls.Add(infoLabel);

            // Active Features Label
            Label activeFeaturesLabel = new Label();
            activeFeaturesLabel.Text = "Active Features: ";
            activeFeaturesLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            activeFeaturesLabel.ForeColor = Color.FromArgb(255, 76, 0);
            activeFeaturesLabel.Location = new Point(10, 500);
            contentPanel.Controls.Add(activeFeaturesLabel);

            Label activeFeaturesValueLabel = new Label();
            activeFeaturesValueLabel.Text = "None";
            activeFeaturesValueLabel.Font = new Font("Segoe UI", 9);
            activeFeaturesValueLabel.ForeColor = Color.LimeGreen;
            activeFeaturesValueLabel.Location = new Point(140, 500);
            activeFeaturesValueLabel.AutoSize = true;
            contentPanel.Controls.Add(activeFeaturesValueLabel);

            // Update active features label
            Timer updateTimer = new Timer();
            updateTimer.Interval = 500;
            updateTimer.Tick += (s, e) =>
            {
                string activeFeatures = "";
                if (esEnabled) activeFeatures += "ES ";
                if (aimEnabled) activeFeatures += "AIM ";
                if (flEnabled) activeFeatures += "FL";
                activeFeaturesValueLabel.Text = string.IsNullOrWhiteSpace(activeFeatures) ? "None" : activeFeatures;
            };
            updateTimer.Start();

            this.ResumeLayout(false);
        }

        private void CreateSwitchPanel(Panel parent, string featureName, int yPos, ref bool featureState, int index)
        {
            Panel switchPanel = new Panel();
            switchPanel.BackColor = Color.FromArgb(35, 35, 50);
            switchPanel.Size = new Size(460, 100);
            switchPanel.Location = new Point(0, yPos);
            switchPanel.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 76, 0), 1), 0, 0, switchPanel.Width - 1, switchPanel.Height - 1);
            };
            parent.Controls.Add(switchPanel);

            // Feature Name
            Label nameLabel = new Label();
            nameLabel.Text = featureName;
            nameLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            nameLabel.ForeColor = Color.White;
            nameLabel.Location = new Point(20, 15);
            nameLabel.AutoSize = true;
            switchPanel.Controls.Add(nameLabel);

            // Status Label
            Label statusLabel = new Label();
            statusLabel.Text = "Status: OFF";
            statusLabel.Font = new Font("Segoe UI", 9);
            statusLabel.ForeColor = Color.FromArgb(200, 0, 0);
            statusLabel.Location = new Point(20, 45);
            statusLabel.AutoSize = true;
            switchPanel.Controls.Add(statusLabel);

            // Toggle Switch
            Panel toggleSwitch = new Panel();
            toggleSwitch.BackColor = Color.FromArgb(80, 80, 100);
            toggleSwitch.Size = new Size(70, 40);
            toggleSwitch.Location = new Point(380, 30);
            toggleSwitch.Cursor = Cursors.Hand;
            toggleSwitch.Paint += (s, e) =>
            {
                bool isEnabled = (index == 0 && esEnabled) || (index == 1 && aimEnabled) || (index == 2 && flEnabled);
                e.Graphics.DrawRectangle(new Pen(isEnabled ? Color.LimeGreen : Color.Gray, 2), 0, 0, toggleSwitch.Width - 1, toggleSwitch.Height - 1);
                e.Graphics.FillRectangle(isEnabled ? Brushes.LimeGreen : Brushes.DarkGray, isEnabled ? 35 : 5, 5, 30, 30);
                e.Graphics.DrawString(isEnabled ? "ON" : "OFF", new Font("Segoe UI", 8, FontStyle.Bold), isEnabled ? Brushes.White : Brushes.WhiteSmoke, isEnabled ? 10 : 20, 10);
            };
            toggleSwitch.Click += (s, e) =>
            {
                switch (index)
                {
                    case 0: esEnabled = !esEnabled; break;
                    case 1: aimEnabled = !aimEnabled; break;
                    case 2: flEnabled = !flEnabled; break;
                }
                statusLabel.Text = featureName + ": " + ((index == 0 && esEnabled) || (index == 1 && aimEnabled) || (index == 2 && flEnabled) ? "ON" : "OFF");
                statusLabel.ForeColor = ((index == 0 && esEnabled) || (index == 1 && aimEnabled) || (index == 2 && flEnabled)) ? Color.LimeGreen : Color.FromArgb(200, 0, 0);
                toggleSwitch.Invalidate();
            };
            switchPanel.Controls.Add(toggleSwitch);
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(255, 100, 20), 2), 0, 79, 500, 79);
        }
    }
}
