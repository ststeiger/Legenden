<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnRenderHTML = New System.Windows.Forms.Button()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.pbTestImage = New System.Windows.Forms.PictureBox()
        Me.btnChooseBrush = New System.Windows.Forms.Button()
        Me.btnGetColorData = New System.Windows.Forms.Button()
        Me.btnColors = New System.Windows.Forms.Button()
        Me.btnRecolorTexture = New System.Windows.Forms.Button()
        Me.btnTextureTest = New System.Windows.Forms.Button()
        Me.btnHatchStyleTest = New System.Windows.Forms.Button()
        Me.btnDrawLegenden = New System.Windows.Forms.Button()
        Me.btnGetData = New System.Windows.Forms.Button()
        Me.dgvLegendData = New System.Windows.Forms.DataGridView()
        Me.btnInitialTest = New System.Windows.Forms.Button()
        Me.btnToPdf = New System.Windows.Forms.Button()
        Me.panel1.SuspendLayout()
        CType(Me.pbTestImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLegendData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnRenderHTML
        '
        Me.btnRenderHTML.Location = New System.Drawing.Point(634, 503)
        Me.btnRenderHTML.Name = "btnRenderHTML"
        Me.btnRenderHTML.Size = New System.Drawing.Size(117, 23)
        Me.btnRenderHTML.TabIndex = 24
        Me.btnRenderHTML.Text = "Render HTML"
        Me.btnRenderHTML.UseVisualStyleBackColor = True
        '
        'panel1
        '
        Me.panel1.AutoScroll = True
        Me.panel1.Controls.Add(Me.pbTestImage)
        Me.panel1.Location = New System.Drawing.Point(12, 14)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(601, 603)
        Me.panel1.TabIndex = 23
        '
        'pbTestImage
        '
        Me.pbTestImage.Location = New System.Drawing.Point(0, 0)
        Me.pbTestImage.Name = "pbTestImage"
        Me.pbTestImage.Size = New System.Drawing.Size(600, 600)
        Me.pbTestImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTestImage.TabIndex = 0
        Me.pbTestImage.TabStop = False
        '
        'btnChooseBrush
        '
        Me.btnChooseBrush.Location = New System.Drawing.Point(634, 474)
        Me.btnChooseBrush.Name = "btnChooseBrush"
        Me.btnChooseBrush.Size = New System.Drawing.Size(117, 23)
        Me.btnChooseBrush.TabIndex = 22
        Me.btnChooseBrush.Text = "Choose Brush"
        Me.btnChooseBrush.UseVisualStyleBackColor = True
        '
        'btnGetColorData
        '
        Me.btnGetColorData.Location = New System.Drawing.Point(880, 416)
        Me.btnGetColorData.Name = "btnGetColorData"
        Me.btnGetColorData.Size = New System.Drawing.Size(117, 23)
        Me.btnGetColorData.TabIndex = 21
        Me.btnGetColorData.Text = "Get Color Data"
        Me.btnGetColorData.UseVisualStyleBackColor = True
        '
        'btnColors
        '
        Me.btnColors.Location = New System.Drawing.Point(880, 474)
        Me.btnColors.Name = "btnColors"
        Me.btnColors.Size = New System.Drawing.Size(117, 23)
        Me.btnColors.TabIndex = 20
        Me.btnColors.Text = "Colors"
        Me.btnColors.UseVisualStyleBackColor = True
        '
        'btnRecolorTexture
        '
        Me.btnRecolorTexture.Location = New System.Drawing.Point(757, 474)
        Me.btnRecolorTexture.Name = "btnRecolorTexture"
        Me.btnRecolorTexture.Size = New System.Drawing.Size(117, 23)
        Me.btnRecolorTexture.TabIndex = 19
        Me.btnRecolorTexture.Text = "Recolor Texture"
        Me.btnRecolorTexture.UseVisualStyleBackColor = True
        '
        'btnTextureTest
        '
        Me.btnTextureTest.Location = New System.Drawing.Point(757, 445)
        Me.btnTextureTest.Name = "btnTextureTest"
        Me.btnTextureTest.Size = New System.Drawing.Size(117, 23)
        Me.btnTextureTest.TabIndex = 18
        Me.btnTextureTest.Text = "Texture Brush"
        Me.btnTextureTest.UseVisualStyleBackColor = True
        '
        'btnHatchStyleTest
        '
        Me.btnHatchStyleTest.Location = New System.Drawing.Point(757, 416)
        Me.btnHatchStyleTest.Name = "btnHatchStyleTest"
        Me.btnHatchStyleTest.Size = New System.Drawing.Size(117, 23)
        Me.btnHatchStyleTest.TabIndex = 17
        Me.btnHatchStyleTest.Text = "Hatch Style Test"
        Me.btnHatchStyleTest.UseVisualStyleBackColor = True
        '
        'btnDrawLegenden
        '
        Me.btnDrawLegenden.Location = New System.Drawing.Point(634, 445)
        Me.btnDrawLegenden.Name = "btnDrawLegenden"
        Me.btnDrawLegenden.Size = New System.Drawing.Size(117, 23)
        Me.btnDrawLegenden.TabIndex = 16
        Me.btnDrawLegenden.Text = "Draw Legends"
        Me.btnDrawLegenden.UseVisualStyleBackColor = True
        '
        'btnGetData
        '
        Me.btnGetData.Location = New System.Drawing.Point(880, 445)
        Me.btnGetData.Name = "btnGetData"
        Me.btnGetData.Size = New System.Drawing.Size(117, 23)
        Me.btnGetData.TabIndex = 15
        Me.btnGetData.Text = "Get Data"
        Me.btnGetData.UseVisualStyleBackColor = True
        '
        'dgvLegendData
        '
        Me.dgvLegendData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLegendData.Location = New System.Drawing.Point(634, 14)
        Me.dgvLegendData.Name = "dgvLegendData"
        Me.dgvLegendData.Size = New System.Drawing.Size(363, 396)
        Me.dgvLegendData.TabIndex = 14
        '
        'btnInitialTest
        '
        Me.btnInitialTest.Location = New System.Drawing.Point(634, 416)
        Me.btnInitialTest.Name = "btnInitialTest"
        Me.btnInitialTest.Size = New System.Drawing.Size(117, 23)
        Me.btnInitialTest.TabIndex = 13
        Me.btnInitialTest.Text = "Init Test"
        Me.btnInitialTest.UseVisualStyleBackColor = True
        '
        'btnToPdf
        '
        Me.btnToPdf.Location = New System.Drawing.Point(880, 595)
        Me.btnToPdf.Name = "btnToPdf"
        Me.btnToPdf.Size = New System.Drawing.Size(117, 23)
        Me.btnToPdf.TabIndex = 25
        Me.btnToPdf.Text = "ToPDF"
        Me.btnToPdf.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 630)
        Me.Controls.Add(Me.btnToPdf)
        Me.Controls.Add(Me.btnRenderHTML)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.btnChooseBrush)
        Me.Controls.Add(Me.btnGetColorData)
        Me.Controls.Add(Me.btnColors)
        Me.Controls.Add(Me.btnRecolorTexture)
        Me.Controls.Add(Me.btnTextureTest)
        Me.Controls.Add(Me.btnHatchStyleTest)
        Me.Controls.Add(Me.btnDrawLegenden)
        Me.Controls.Add(Me.btnGetData)
        Me.Controls.Add(Me.dgvLegendData)
        Me.Controls.Add(Me.btnInitialTest)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        CType(Me.pbTestImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLegendData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents btnRenderHTML As System.Windows.Forms.Button
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents pbTestImage As System.Windows.Forms.PictureBox
    Private WithEvents btnChooseBrush As System.Windows.Forms.Button
    Private WithEvents btnGetColorData As System.Windows.Forms.Button
    Private WithEvents btnColors As System.Windows.Forms.Button
    Private WithEvents btnRecolorTexture As System.Windows.Forms.Button
    Private WithEvents btnTextureTest As System.Windows.Forms.Button
    Private WithEvents btnHatchStyleTest As System.Windows.Forms.Button
    Private WithEvents btnDrawLegenden As System.Windows.Forms.Button
    Private WithEvents btnGetData As System.Windows.Forms.Button
    Private WithEvents dgvLegendData As System.Windows.Forms.DataGridView
    Private WithEvents btnInitialTest As System.Windows.Forms.Button
    Private WithEvents btnToPdf As System.Windows.Forms.Button

End Class
