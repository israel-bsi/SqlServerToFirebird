namespace ImportaDadosSGE
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.lblFirebird = new System.Windows.Forms.Label();
            this.lblSqlServer = new System.Windows.Forms.Label();
            this.txtFbDataBase = new System.Windows.Forms.TextBox();
            this.lblFbDatabase = new System.Windows.Forms.Label();
            this.lblDataSourceSqlServer = new System.Windows.Forms.Label();
            this.txtSqlServerDatasource = new System.Windows.Forms.TextBox();
            this.lblSqlServerDatabase = new System.Windows.Forms.Label();
            this.txtSqlServerDatabase = new System.Windows.Forms.TextBox();
            this.lblfbDataSource = new System.Windows.Forms.Label();
            this.txtFbDataSource = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(148, 299);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(84, 31);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // progressbar
            // 
            this.progressbar.Location = new System.Drawing.Point(12, 267);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(363, 21);
            this.progressbar.TabIndex = 1;
            // 
            // lblFirebird
            // 
            this.lblFirebird.AutoSize = true;
            this.lblFirebird.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFirebird.Location = new System.Drawing.Point(10, 9);
            this.lblFirebird.Name = "lblFirebird";
            this.lblFirebird.Size = new System.Drawing.Size(69, 21);
            this.lblFirebird.TabIndex = 3;
            this.lblFirebird.Text = "Firebird";
            // 
            // lblSqlServer
            // 
            this.lblSqlServer.AutoSize = true;
            this.lblSqlServer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSqlServer.Location = new System.Drawing.Point(12, 140);
            this.lblSqlServer.Name = "lblSqlServer";
            this.lblSqlServer.Size = new System.Drawing.Size(83, 21);
            this.lblSqlServer.TabIndex = 4;
            this.lblSqlServer.Text = "SqlServer";
            // 
            // txtFbDataBase
            // 
            this.txtFbDataBase.Location = new System.Drawing.Point(12, 96);
            this.txtFbDataBase.Name = "txtFbDataBase";
            this.txtFbDataBase.Size = new System.Drawing.Size(363, 23);
            this.txtFbDataBase.TabIndex = 5;
            // 
            // lblFbDatabase
            // 
            this.lblFbDatabase.AutoSize = true;
            this.lblFbDatabase.Location = new System.Drawing.Point(10, 78);
            this.lblFbDatabase.Name = "lblFbDatabase";
            this.lblFbDatabase.Size = new System.Drawing.Size(58, 15);
            this.lblFbDatabase.TabIndex = 6;
            this.lblFbDatabase.Text = "Database:";
            // 
            // lblDataSourceSqlServer
            // 
            this.lblDataSourceSqlServer.AutoSize = true;
            this.lblDataSourceSqlServer.Location = new System.Drawing.Point(12, 165);
            this.lblDataSourceSqlServer.Name = "lblDataSourceSqlServer";
            this.lblDataSourceSqlServer.Size = new System.Drawing.Size(73, 15);
            this.lblDataSourceSqlServer.TabIndex = 8;
            this.lblDataSourceSqlServer.Text = "Data Source:";
            // 
            // txtSqlServerDatasource
            // 
            this.txtSqlServerDatasource.Location = new System.Drawing.Point(12, 183);
            this.txtSqlServerDatasource.Name = "txtSqlServerDatasource";
            this.txtSqlServerDatasource.Size = new System.Drawing.Size(363, 23);
            this.txtSqlServerDatasource.TabIndex = 7;
            // 
            // lblSqlServerDatabase
            // 
            this.lblSqlServerDatabase.AutoSize = true;
            this.lblSqlServerDatabase.Location = new System.Drawing.Point(12, 213);
            this.lblSqlServerDatabase.Name = "lblSqlServerDatabase";
            this.lblSqlServerDatabase.Size = new System.Drawing.Size(58, 15);
            this.lblSqlServerDatabase.TabIndex = 12;
            this.lblSqlServerDatabase.Text = "Database:";
            // 
            // txtSqlServerDatabase
            // 
            this.txtSqlServerDatabase.Location = new System.Drawing.Point(12, 231);
            this.txtSqlServerDatabase.Name = "txtSqlServerDatabase";
            this.txtSqlServerDatabase.Size = new System.Drawing.Size(363, 23);
            this.txtSqlServerDatabase.TabIndex = 11;
            // 
            // lblfbDataSource
            // 
            this.lblfbDataSource.AutoSize = true;
            this.lblfbDataSource.Location = new System.Drawing.Point(12, 33);
            this.lblfbDataSource.Name = "lblfbDataSource";
            this.lblfbDataSource.Size = new System.Drawing.Size(73, 15);
            this.lblfbDataSource.TabIndex = 16;
            this.lblfbDataSource.Text = "Data Source:";
            // 
            // txtFbDataSource
            // 
            this.txtFbDataSource.Location = new System.Drawing.Point(12, 51);
            this.txtFbDataSource.Name = "txtFbDataSource";
            this.txtFbDataSource.Size = new System.Drawing.Size(363, 23);
            this.txtFbDataSource.TabIndex = 15;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 338);
            this.Controls.Add(this.lblfbDataSource);
            this.Controls.Add(this.txtFbDataSource);
            this.Controls.Add(this.lblSqlServerDatabase);
            this.Controls.Add(this.txtSqlServerDatabase);
            this.Controls.Add(this.lblDataSourceSqlServer);
            this.Controls.Add(this.txtSqlServerDatasource);
            this.Controls.Add(this.lblFbDatabase);
            this.Controls.Add(this.txtFbDataBase);
            this.Controls.Add(this.lblSqlServer);
            this.Controls.Add(this.lblFirebird);
            this.Controls.Add(this.progressbar);
            this.Controls.Add(this.btnStart);
            this.Name = "FrmMain";
            this.Text = "SqlServerToFirebird";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnStart;
        private ProgressBar progressbar;
        private Label lblFirebird;
        private Label lblSqlServer;
        private TextBox txtFbDataBase;
        private Label lblFbDatabase;
        private Label lblDataSourceSqlServer;
        private TextBox txtSqlServerDatasource;
        private Label lblSqlServerDatabase;
        private TextBox txtSqlServerDatabase;
        private Label lblfbDataSource;
        private TextBox txtFbDataSource;
    }
}