
namespace OrderMaker.Test
{
    partial class StrategyTestForm
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
            this.tpStrategy = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddStrategy = new System.Windows.Forms.Button();
            this.fpanelStrategies = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.tpStrategy.SuspendLayout();
            this.SuspendLayout();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(538, 396);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StrategyTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tpStrategy);
            this.Controls.Add(this.fpanelStrategies);
            this.Name = "StrategyTestForm";
            this.Text = "Trader 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StrategyTestForm_FormClosing);
            this.Load += new System.EventHandler(this.StrategyTestForm_Load);
            this.tpStrategy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tpStrategy;
        private System.Windows.Forms.FlowLayoutPanel fpanelStrategies;

        private OrderMaker.UI.EditStrategy editStrategy;
        private System.Windows.Forms.Button btnAddStrategy;
        private System.Windows.Forms.Button button1;
    }
}