namespace DrawLegends
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbTestImage = new System.Windows.Forms.PictureBox();
            this.btnInitialTest = new System.Windows.Forms.Button();
            this.dgvLegendData = new System.Windows.Forms.DataGridView();
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnDrawLegenden = new System.Windows.Forms.Button();
            this.btnHatchStyleTest = new System.Windows.Forms.Button();
            this.btnTextureTest = new System.Windows.Forms.Button();
            this.btnRecolorTexture = new System.Windows.Forms.Button();
            this.btnColors = new System.Windows.Forms.Button();
            this.btnGetColorData = new System.Windows.Forms.Button();
            this.btnChooseBrush = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRenderHTML = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLegendData)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbTestImage
            // 
            this.pbTestImage.Location = new System.Drawing.Point(3, 2);
            this.pbTestImage.Name = "pbTestImage";
            this.pbTestImage.Size = new System.Drawing.Size(1050, 600);
            this.pbTestImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbTestImage.TabIndex = 0;
            this.pbTestImage.TabStop = false;
            // 
            // btnInitialTest
            // 
            this.btnInitialTest.Location = new System.Drawing.Point(1120, 415);
            this.btnInitialTest.Name = "btnInitialTest";
            this.btnInitialTest.Size = new System.Drawing.Size(117, 23);
            this.btnInitialTest.TabIndex = 1;
            this.btnInitialTest.Text = "Init Test";
            this.btnInitialTest.UseVisualStyleBackColor = true;
            this.btnInitialTest.Click += new System.EventHandler(this.btnInitialTest_Click);
            // 
            // dgvLegendData
            // 
            this.dgvLegendData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLegendData.Location = new System.Drawing.Point(1120, 13);
            this.dgvLegendData.Name = "dgvLegendData";
            this.dgvLegendData.Size = new System.Drawing.Size(363, 396);
            this.dgvLegendData.TabIndex = 2;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(1366, 444);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(117, 23);
            this.btnGetData.TabIndex = 3;
            this.btnGetData.Text = "Get Data";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnDrawLegenden
            // 
            this.btnDrawLegenden.Location = new System.Drawing.Point(1120, 444);
            this.btnDrawLegenden.Name = "btnDrawLegenden";
            this.btnDrawLegenden.Size = new System.Drawing.Size(117, 23);
            this.btnDrawLegenden.TabIndex = 4;
            this.btnDrawLegenden.Text = "Draw Legends";
            this.btnDrawLegenden.UseVisualStyleBackColor = true;
            this.btnDrawLegenden.Click += new System.EventHandler(this.btnDrawLegenden_Click);
            // 
            // btnHatchStyleTest
            // 
            this.btnHatchStyleTest.Location = new System.Drawing.Point(1243, 415);
            this.btnHatchStyleTest.Name = "btnHatchStyleTest";
            this.btnHatchStyleTest.Size = new System.Drawing.Size(117, 23);
            this.btnHatchStyleTest.TabIndex = 5;
            this.btnHatchStyleTest.Text = "Hatch Style Test";
            this.btnHatchStyleTest.UseVisualStyleBackColor = true;
            this.btnHatchStyleTest.Click += new System.EventHandler(this.btnHatchStyleTest_Click);
            // 
            // btnTextureTest
            // 
            this.btnTextureTest.Location = new System.Drawing.Point(1243, 444);
            this.btnTextureTest.Name = "btnTextureTest";
            this.btnTextureTest.Size = new System.Drawing.Size(117, 23);
            this.btnTextureTest.TabIndex = 6;
            this.btnTextureTest.Text = "Texture Brush";
            this.btnTextureTest.UseVisualStyleBackColor = true;
            this.btnTextureTest.Click += new System.EventHandler(this.btnTextureTest_Click);
            // 
            // btnRecolorTexture
            // 
            this.btnRecolorTexture.Location = new System.Drawing.Point(1243, 473);
            this.btnRecolorTexture.Name = "btnRecolorTexture";
            this.btnRecolorTexture.Size = new System.Drawing.Size(117, 23);
            this.btnRecolorTexture.TabIndex = 7;
            this.btnRecolorTexture.Text = "Recolor Texture";
            this.btnRecolorTexture.UseVisualStyleBackColor = true;
            this.btnRecolorTexture.Click += new System.EventHandler(this.btnRecolorTexture_Click);
            // 
            // btnColors
            // 
            this.btnColors.Location = new System.Drawing.Point(1366, 473);
            this.btnColors.Name = "btnColors";
            this.btnColors.Size = new System.Drawing.Size(117, 23);
            this.btnColors.TabIndex = 8;
            this.btnColors.Text = "Colors";
            this.btnColors.UseVisualStyleBackColor = true;
            this.btnColors.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // btnGetColorData
            // 
            this.btnGetColorData.Location = new System.Drawing.Point(1366, 415);
            this.btnGetColorData.Name = "btnGetColorData";
            this.btnGetColorData.Size = new System.Drawing.Size(117, 23);
            this.btnGetColorData.TabIndex = 9;
            this.btnGetColorData.Text = "Get Color Data";
            this.btnGetColorData.UseVisualStyleBackColor = true;
            this.btnGetColorData.Click += new System.EventHandler(this.btnGetColorData_Click);
            // 
            // btnChooseBrush
            // 
            this.btnChooseBrush.Location = new System.Drawing.Point(1120, 473);
            this.btnChooseBrush.Name = "btnChooseBrush";
            this.btnChooseBrush.Size = new System.Drawing.Size(117, 23);
            this.btnChooseBrush.TabIndex = 10;
            this.btnChooseBrush.Text = "Choose Brush";
            this.btnChooseBrush.UseVisualStyleBackColor = true;
            this.btnChooseBrush.Click += new System.EventHandler(this.btnChooseBrush_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pbTestImage);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 603);
            this.panel1.TabIndex = 11;
            // 
            // btnRenderHTML
            // 
            this.btnRenderHTML.Location = new System.Drawing.Point(1120, 502);
            this.btnRenderHTML.Name = "btnRenderHTML";
            this.btnRenderHTML.Size = new System.Drawing.Size(117, 23);
            this.btnRenderHTML.TabIndex = 12;
            this.btnRenderHTML.Text = "Render HTML";
            this.btnRenderHTML.UseVisualStyleBackColor = true;
            this.btnRenderHTML.Click += new System.EventHandler(this.btnRenderHTML_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1495, 630);
            this.Controls.Add(this.btnRenderHTML);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnChooseBrush);
            this.Controls.Add(this.btnGetColorData);
            this.Controls.Add(this.btnColors);
            this.Controls.Add(this.btnRecolorTexture);
            this.Controls.Add(this.btnTextureTest);
            this.Controls.Add(this.btnHatchStyleTest);
            this.Controls.Add(this.btnDrawLegenden);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.dgvLegendData);
            this.Controls.Add(this.btnInitialTest);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbTestImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLegendData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTestImage;
        private System.Windows.Forms.Button btnInitialTest;
        private System.Windows.Forms.DataGridView dgvLegendData;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.Button btnDrawLegenden;
        private System.Windows.Forms.Button btnHatchStyleTest;
        private System.Windows.Forms.Button btnTextureTest;
        private System.Windows.Forms.Button btnRecolorTexture;
        private System.Windows.Forms.Button btnColors;
        private System.Windows.Forms.Button btnGetColorData;
        private System.Windows.Forms.Button btnChooseBrush;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRenderHTML;
    }
}

