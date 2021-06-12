
namespace OrderMaker.UI
{
    partial class EditStrategy
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tpStrategy = new System.Windows.Forms.TableLayoutPanel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.numericInterval = new System.Windows.Forms.NumericUpDown();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDaytrade = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericOffset = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fpanelAccounts = new System.Windows.Forms.FlowLayoutPanel();
            this.tpBtns = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tpStrategy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffset)).BeginInit();
            this.tpBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tpStrategy);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 90);
            this.panel1.TabIndex = 0;
            // 
            // tpStrategy
            // 
            this.tpStrategy.ColumnCount = 5;
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 283F));
            this.tpStrategy.Controls.Add(this.txtName, 1, 0);
            this.tpStrategy.Controls.Add(this.numericInterval, 1, 1);
            this.tpStrategy.Controls.Add(this.txtFilePath, 4, 1);
            this.tpStrategy.Controls.Add(this.label1, 0, 0);
            this.tpStrategy.Controls.Add(this.label2, 0, 1);
            this.tpStrategy.Controls.Add(this.label3, 3, 1);
            this.tpStrategy.Controls.Add(this.chkDaytrade, 2, 0);
            this.tpStrategy.Controls.Add(this.label5, 3, 0);
            this.tpStrategy.Controls.Add(this.numericOffset, 4, 0);
            this.tpStrategy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpStrategy.Location = new System.Drawing.Point(0, 0);
            this.tpStrategy.Name = "tpStrategy";
            this.tpStrategy.RowCount = 2;
            this.tpStrategy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tpStrategy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tpStrategy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpStrategy.Size = new System.Drawing.Size(763, 90);
            this.tpStrategy.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtName.Location = new System.Drawing.Point(123, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(114, 22);
            this.txtName.TabIndex = 2;
            // 
            // numericInterval
            // 
            this.numericInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericInterval.Cursor = System.Windows.Forms.Cursors.Default;
            this.numericInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericInterval.Location = new System.Drawing.Point(123, 56);
            this.numericInterval.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numericInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericInterval.Name = "numericInterval";
            this.numericInterval.ReadOnly = true;
            this.numericInterval.Size = new System.Drawing.Size(114, 22);
            this.numericInterval.TabIndex = 0;
            this.numericInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtFilePath.Location = new System.Drawing.Point(483, 56);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(265, 22);
            this.txtFilePath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "策略名稱";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "讀取頻率";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "檔案路徑";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDaytrade
            // 
            this.chkDaytrade.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDaytrade.AutoSize = true;
            this.chkDaytrade.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDaytrade.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkDaytrade.Location = new System.Drawing.Point(243, 13);
            this.chkDaytrade.Name = "chkDaytrade";
            this.chkDaytrade.Size = new System.Drawing.Size(71, 19);
            this.chkDaytrade.TabIndex = 1;
            this.chkDaytrade.Text = "當沖單";
            this.chkDaytrade.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(424, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "下單加點";
            // 
            // numericOffset
            // 
            this.numericOffset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericOffset.Location = new System.Drawing.Point(483, 11);
            this.numericOffset.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericOffset.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            -2147483648});
            this.numericOffset.Name = "numericOffset";
            this.numericOffset.Size = new System.Drawing.Size(120, 22);
            this.numericOffset.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.InitialDirectory = "C:\\";
            // 
            // fpanelAccounts
            // 
            this.fpanelAccounts.Location = new System.Drawing.Point(13, 151);
            this.fpanelAccounts.Name = "fpanelAccounts";
            this.fpanelAccounts.Size = new System.Drawing.Size(763, 12);
            this.fpanelAccounts.TabIndex = 6;
            // 
            // tpBtns
            // 
            this.tpBtns.ColumnCount = 8;
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tpBtns.Controls.Add(this.label4, 0, 0);
            this.tpBtns.Controls.Add(this.btnRemove, 7, 0);
            this.tpBtns.Controls.Add(this.btnSave, 5, 0);
            this.tpBtns.Controls.Add(this.btnAddAccount, 1, 0);
            this.tpBtns.Location = new System.Drawing.Point(12, 110);
            this.tpBtns.Name = "tpBtns";
            this.tpBtns.RowCount = 1;
            this.tpBtns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpBtns.Size = new System.Drawing.Size(764, 35);
            this.tpBtns.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "下單帳號";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.BackColor = System.Drawing.Color.Red;
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(686, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "刪除";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.PaleGreen;
            this.btnSave.Location = new System.Drawing.Point(478, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "存檔";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAddAccount.Location = new System.Drawing.Point(98, 6);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(75, 23);
            this.btnAddAccount.TabIndex = 2;
            this.btnAddAccount.Text = "新增帳號";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // EditStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tpBtns);
            this.Controls.Add(this.fpanelAccounts);
            this.Controls.Add(this.panel1);
            this.Name = "EditStrategy";
            this.Text = "EditStrategy";
            this.panel1.ResumeLayout(false);
            this.tpStrategy.ResumeLayout(false);
            this.tpStrategy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffset)).EndInit();
            this.tpBtns.ResumeLayout(false);
            this.tpBtns.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tpStrategy;
        private System.Windows.Forms.NumericUpDown numericInterval;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel fpanelAccounts;
        private System.Windows.Forms.TableLayoutPanel tpBtns;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkDaytrade;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericOffset;
    }
}