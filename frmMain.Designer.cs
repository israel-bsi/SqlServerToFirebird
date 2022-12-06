namespace MysqlToFirebird
{
    partial class Form1
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgresso = new System.Windows.Forms.Label();
            this.lblFirebird = new System.Windows.Forms.Label();
            this.lblMySql = new System.Windows.Forms.Label();
            this.txtFbDataBase = new System.Windows.Forms.TextBox();
            this.lblFbDatabase = new System.Windows.Forms.Label();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.txtMsqlDatasource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMsqlUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMsqlDatabase = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(164, 372);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(84, 31);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 339);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(363, 21);
            this.progressBar1.TabIndex = 1;
            // 
            // lblProgresso
            // 
            this.lblProgresso.AutoSize = true;
            this.lblProgresso.Location = new System.Drawing.Point(177, 311);
            this.lblProgresso.Name = "lblProgresso";
            this.lblProgresso.Size = new System.Drawing.Size(59, 15);
            this.lblProgresso.TabIndex = 2;
            this.lblProgresso.Text = "Progresso";
            // 
            // lblFirebird
            // 
            this.lblFirebird.AutoSize = true;
            this.lblFirebird.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFirebird.Location = new System.Drawing.Point(21, 9);
            this.lblFirebird.Name = "lblFirebird";
            this.lblFirebird.Size = new System.Drawing.Size(69, 21);
            this.lblFirebird.TabIndex = 3;
            this.lblFirebird.Text = "Firebird";
            // 
            // lblMySql
            // 
            this.lblMySql.AutoSize = true;
            this.lblMySql.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMySql.Location = new System.Drawing.Point(16, 109);
            this.lblMySql.Name = "lblMySql";
            this.lblMySql.Size = new System.Drawing.Size(63, 21);
            this.lblMySql.TabIndex = 4;
            this.lblMySql.Text = "MySQL";
            // 
            // txtFbDataBase
            // 
            this.txtFbDataBase.Location = new System.Drawing.Point(21, 65);
            this.txtFbDataBase.Name = "txtFbDataBase";
            this.txtFbDataBase.Size = new System.Drawing.Size(357, 23);
            this.txtFbDataBase.TabIndex = 5;
            // 
            // lblFbDatabase
            // 
            this.lblFbDatabase.AutoSize = true;
            this.lblFbDatabase.Location = new System.Drawing.Point(21, 47);
            this.lblFbDatabase.Name = "lblFbDatabase";
            this.lblFbDatabase.Size = new System.Drawing.Size(58, 15);
            this.lblFbDatabase.TabIndex = 6;
            this.lblFbDatabase.Text = "Database:";
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(21, 144);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(73, 15);
            this.lblDataSource.TabIndex = 8;
            this.lblDataSource.Text = "Data Source:";
            // 
            // txtMsqlDatasource
            // 
            this.txtMsqlDatasource.Location = new System.Drawing.Point(21, 162);
            this.txtMsqlDatasource.Name = "txtMsqlDatasource";
            this.txtMsqlDatasource.Size = new System.Drawing.Size(363, 23);
            this.txtMsqlDatasource.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "User Name:";
            // 
            // txtMsqlUsername
            // 
            this.txtMsqlUsername.Location = new System.Drawing.Point(21, 212);
            this.txtMsqlUsername.Name = "txtMsqlUsername";
            this.txtMsqlUsername.Size = new System.Drawing.Size(363, 23);
            this.txtMsqlUsername.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Database:";
            // 
            // txtMsqlDatabase
            // 
            this.txtMsqlDatabase.Location = new System.Drawing.Point(21, 270);
            this.txtMsqlDatabase.Name = "txtMsqlDatabase";
            this.txtMsqlDatabase.Size = new System.Drawing.Size(363, 23);
            this.txtMsqlDatabase.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 417);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMsqlDatabase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMsqlUsername);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.txtMsqlDatasource);
            this.Controls.Add(this.lblFbDatabase);
            this.Controls.Add(this.txtFbDataBase);
            this.Controls.Add(this.lblMySql);
            this.Controls.Add(this.lblFirebird);
            this.Controls.Add(this.lblProgresso);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "MySqlToFirebird";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnStart;
        private ProgressBar progressBar1;
        private Label lblProgresso;
        private Label lblFirebird;
        private Label lblMySql;
        private TextBox txtFbDataBase;
        private Label lblFbDatabase;
        private Label lblDataSource;
        private TextBox txtMsqlDatasource;
        private Label label2;
        private TextBox txtMsqlUsername;
        private Label label3;
        private TextBox txtMsqlDatabase;
    }
}