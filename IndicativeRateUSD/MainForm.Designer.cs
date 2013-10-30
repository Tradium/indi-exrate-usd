namespace IndicativeRateUSD
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnDeals = new System.Windows.Forms.Button();
            this.btnUSD = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDeals
            // 
            this.btnDeals.Location = new System.Drawing.Point(23, 12);
            this.btnDeals.Name = "btnDeals";
            this.btnDeals.Size = new System.Drawing.Size(116, 23);
            this.btnDeals.TabIndex = 0;
            this.btnDeals.Text = "Сделки";
            this.btnDeals.UseVisualStyleBackColor = true;
            this.btnDeals.Click += new System.EventHandler(this.btnDeals_Click);
            // 
            // btnUSD
            // 
            this.btnUSD.Enabled = false;
            this.btnUSD.Location = new System.Drawing.Point(23, 41);
            this.btnUSD.Name = "btnUSD";
            this.btnUSD.Size = new System.Drawing.Size(116, 23);
            this.btnUSD.TabIndex = 0;
            this.btnUSD.Text = "USD";
            this.btnUSD.UseVisualStyleBackColor = true;
            this.btnUSD.Click += new System.EventHandler(this.btnUSD_Click);
            // 
            // btnResult
            // 
            this.btnResult.Enabled = false;
            this.btnResult.Location = new System.Drawing.Point(23, 70);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(116, 23);
            this.btnResult.TabIndex = 0;
            this.btnResult.Text = "Подобрать курсы";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.btnUSD);
            this.Controls.Add(this.btnDeals);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Индикативные курсы USD";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDeals;
        private System.Windows.Forms.Button btnUSD;
        private System.Windows.Forms.Button btnResult;
    }
}

