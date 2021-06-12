
namespace OrderMaker
{
    partial class Main
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
            this.tpTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnEditConfig = new System.Windows.Forms.Button();
            this.btnLogs = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tpStrategy = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddStrategy = new System.Windows.Forms.Button();
            this.fpanelStrategies = new System.Windows.Forms.FlowLayoutPanel();
            this.tpTop.SuspendLayout();
            this.tpStrategy.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpTop
            // 
            this.tpTop.ColumnCount = 4;
            this.tpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tpTop.Controls.Add(this.btnEditConfig, 1, 0);
            this.tpTop.Controls.Add(this.btnLogs, 3, 0);
            this.tpTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tpTop.Location = new System.Drawing.Point(0, 0);
            this.tpTop.Name = "tpTop";
            this.tpTop.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.tpTop.RowCount = 1;
            this.tpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpTop.Size = new System.Drawing.Size(800, 45);
            this.tpTop.TabIndex = 1;
            // 
            // btnEditConfig
            // 
            this.btnEditConfig.Location = new System.Drawing.Point(210, 13);
            this.btnEditConfig.Name = "btnEditConfig";
            this.btnEditConfig.Size = new System.Drawing.Size(75, 23);
            this.btnEditConfig.TabIndex = 2;
            this.btnEditConfig.Text = "設定";
            this.btnEditConfig.Click += new System.EventHandler(this.OnEditConfig);
            // 
            // btnLogs
            // 
            this.btnLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogs.Location = new System.Drawing.Point(722, 13);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(75, 23);
            this.btnLogs.TabIndex = 0;
            this.btnLogs.Text = "查看Log";
            this.btnLogs.UseVisualStyleBackColor = true;
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 100);
            this.panel1.TabIndex = 0;
            // 
            // tpStrategy
            // 
            this.tpStrategy.ColumnCount = 2;
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tpStrategy.Controls.Add(this.btnAddStrategy, 1, 0);
            this.tpStrategy.Location = new System.Drawing.Point(0, 154);
            this.tpStrategy.Name = "tpStrategy";
            this.tpStrategy.Padding = new System.Windows.Forms.Padding(10);
            this.tpStrategy.RowCount = 1;
            this.tpStrategy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpStrategy.Size = new System.Drawing.Size(800, 45);
            this.tpStrategy.TabIndex = 3;
            // 
            // btnAddStrategy
            // 
            this.btnAddStrategy.Location = new System.Drawing.Point(286, 13);
            this.btnAddStrategy.Name = "btnAddStrategy";
            this.btnAddStrategy.Size = new System.Drawing.Size(75, 19);
            this.btnAddStrategy.TabIndex = 0;
            this.btnAddStrategy.Text = "新增策略";
            this.btnAddStrategy.Click += new System.EventHandler(this.OnAddStrategy);
            // 
            // fpanelStrategies
            // 
            this.fpanelStrategies.Location = new System.Drawing.Point(0, 207);
            this.fpanelStrategies.Name = "fpanelStrategies";
            this.fpanelStrategies.Size = new System.Drawing.Size(800, 12);
            this.fpanelStrategies.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tpTop);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tpStrategy);
            this.Controls.Add(this.fpanelStrategies);
            this.Name = "Main";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.tpTop.ResumeLayout(false);
            this.tpStrategy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        #region Basic
        private System.Windows.Forms.TableLayoutPanel tpTop;
        private System.Windows.Forms.Panel panel1;

        private OrderMaker.UI.EditConfig editConfig;
        private System.Windows.Forms.Button btnEditConfig;
        private System.Windows.Forms.Button btnLogs;
        #endregion

        #region Strategy
        private System.Windows.Forms.TableLayoutPanel tpStrategy;
        private System.Windows.Forms.FlowLayoutPanel fpanelStrategies;

        private OrderMaker.UI.EditStrategy editStrategy;
        private System.Windows.Forms.Button btnAddStrategy;
        #endregion
    }
}