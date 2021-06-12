
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
            this.fpanelStrategies = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Strategy";

            // 
            // fpanelStrategies
            // 
            this.fpanelStrategies.Location = new System.Drawing.Point(0, 207);
            this.fpanelStrategies.Name = "fpanelStrategies";
            this.fpanelStrategies.Size = new System.Drawing.Size(800, 12);
            this.fpanelStrategies.TabIndex = 5;

            // 
            // tpStrategy
            // 
            this.tpStrategy.ColumnCount = 2;
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tpStrategy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
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
            this.btnAddStrategy = new System.Windows.Forms.Button();
            this.btnAddStrategy.Text = "新增策略";
            this.btnAddStrategy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tpStrategy.Controls.Add(this.btnAddStrategy, 1, 0);
            this.btnAddStrategy.Click += new System.EventHandler(this.OnAddStrategy);

            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);


            this.Controls.Add(this.tpStrategy);
            this.Controls.Add(this.fpanelStrategies);
            this.Name = "Main";
            this.Text = "Trader 2.0";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            //this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);


        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tpStrategy;
        private System.Windows.Forms.FlowLayoutPanel fpanelStrategies;

        private OrderMaker.UI.EditStrategy editStrategy;
        private System.Windows.Forms.Button btnAddStrategy;
    }
}