<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mandelbrot
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Mandelbrot))
        Me.PicMandel = New System.Windows.Forms.PictureBox()
        Me.BtnCiz = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Hesap = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CoorX = New System.Windows.Forms.Label()
        Me.CoorY = New System.Windows.Forms.Label()
        Me.Btnreset = New System.Windows.Forms.Button()
        Me.Btncapture = New System.Windows.Forms.Button()
        Me.PicHarita = New System.Windows.Forms.PictureBox()
        Me.BtnVideo = New System.Windows.Forms.Button()
        Me.Not1 = New System.Windows.Forms.Label()
        Me.Not2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GrbZoomIn = New System.Windows.Forms.GroupBox()
        Me.RollZoomInKatsayısı = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolstrpCikis = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.YardımıGörüntüleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HakkındaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PicRenk = New System.Windows.Forms.PictureBox()
        Me.GrpBtns = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        Me.PrgrsVideo = New System.Windows.Forms.ProgressBar()
        Me.lblvideobar = New System.Windows.Forms.Label()
        Me.Lblcizdirbar = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.PicMandel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicHarita, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrbZoomIn.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.PicRenk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBtns.SuspendLayout()
        Me.SuspendLayout()
        '
        'PicMandel
        '
        Me.PicMandel.Location = New System.Drawing.Point(245, 33)
        Me.PicMandel.Name = "PicMandel"
        Me.PicMandel.Size = New System.Drawing.Size(800, 600)
        Me.PicMandel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicMandel.TabIndex = 1
        Me.PicMandel.TabStop = False
        '
        'BtnCiz
        '
        Me.BtnCiz.Font = New System.Drawing.Font("Segoe Print", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.BtnCiz.Location = New System.Drawing.Point(6, 19)
        Me.BtnCiz.Name = "BtnCiz"
        Me.BtnCiz.Size = New System.Drawing.Size(140, 39)
        Me.BtnCiz.TabIndex = 2
        Me.BtnCiz.Text = "Yenile"
        Me.BtnCiz.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 638)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Zoom Katsayısı: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(97, 638)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "   "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(72, 162)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "         "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 184)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Hesaplama Süresi: "
        '
        'Hesap
        '
        Me.Hesap.AutoSize = True
        Me.Hesap.Location = New System.Drawing.Point(105, 184)
        Me.Hesap.Name = "Hesap"
        Me.Hesap.Size = New System.Drawing.Size(43, 13)
        Me.Hesap.TabIndex = 7
        Me.Hesap.Text = "            "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 162)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Veri Tipi: "
        '
        'CoorX
        '
        Me.CoorX.AutoSize = True
        Me.CoorX.Location = New System.Drawing.Point(10, 206)
        Me.CoorX.Name = "CoorX"
        Me.CoorX.Size = New System.Drawing.Size(20, 13)
        Me.CoorX.TabIndex = 9
        Me.CoorX.Text = "X: "
        '
        'CoorY
        '
        Me.CoorY.AutoSize = True
        Me.CoorY.Location = New System.Drawing.Point(10, 230)
        Me.CoorY.Name = "CoorY"
        Me.CoorY.Size = New System.Drawing.Size(20, 13)
        Me.CoorY.TabIndex = 10
        Me.CoorY.Text = "Y: "
        '
        'Btnreset
        '
        Me.Btnreset.Font = New System.Drawing.Font("Segoe Print", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Btnreset.Location = New System.Drawing.Point(6, 64)
        Me.Btnreset.Name = "Btnreset"
        Me.Btnreset.Size = New System.Drawing.Size(140, 28)
        Me.Btnreset.TabIndex = 11
        Me.Btnreset.Text = "Reset"
        Me.Btnreset.UseVisualStyleBackColor = True
        '
        'Btncapture
        '
        Me.Btncapture.Font = New System.Drawing.Font("Segoe Print", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Btncapture.Location = New System.Drawing.Point(6, 98)
        Me.Btncapture.Name = "Btncapture"
        Me.Btncapture.Size = New System.Drawing.Size(140, 23)
        Me.Btncapture.TabIndex = 12
        Me.Btncapture.Text = "Resim Çek"
        Me.Btncapture.UseVisualStyleBackColor = True
        '
        'PicHarita
        '
        Me.PicHarita.Image = CType(resources.GetObject("PicHarita.Image"), System.Drawing.Image)
        Me.PicHarita.Location = New System.Drawing.Point(1, 460)
        Me.PicHarita.Name = "PicHarita"
        Me.PicHarita.Size = New System.Drawing.Size(239, 172)
        Me.PicHarita.TabIndex = 13
        Me.PicHarita.TabStop = False
        '
        'BtnVideo
        '
        Me.BtnVideo.Font = New System.Drawing.Font("Segoe Print", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.BtnVideo.Location = New System.Drawing.Point(6, 127)
        Me.BtnVideo.Name = "BtnVideo"
        Me.BtnVideo.Size = New System.Drawing.Size(140, 23)
        Me.BtnVideo.TabIndex = 15
        Me.BtnVideo.Text = "Video Çek"
        Me.BtnVideo.UseVisualStyleBackColor = True
        '
        'Not1
        '
        Me.Not1.AutoSize = True
        Me.Not1.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Not1.Location = New System.Drawing.Point(1059, 412)
        Me.Not1.Name = "Not1"
        Me.Not1.Size = New System.Drawing.Size(203, 68)
        Me.Not1.TabIndex = 16
        Me.Not1.Text = "Yakınlaştırma yapmak için ctrl" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "basılı iken; ya bir alan tarayın " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ya da farenin " & _
    "topunu hareket" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ettirin."
        '
        'Not2
        '
        Me.Not2.AutoSize = True
        Me.Not2.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Not2.Location = New System.Drawing.Point(1059, 359)
        Me.Not2.Name = "Not2"
        Me.Not2.Size = New System.Drawing.Size(170, 34)
        Me.Not2.TabIndex = 17
        Me.Not2.Text = "Shift + Fare ile tutup " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sürükleme yapabilirsiniz."
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'GrbZoomIn
        '
        Me.GrbZoomIn.Controls.Add(Me.RollZoomInKatsayısı)
        Me.GrbZoomIn.Location = New System.Drawing.Point(57, 359)
        Me.GrbZoomIn.Name = "GrbZoomIn"
        Me.GrbZoomIn.Size = New System.Drawing.Size(140, 55)
        Me.GrbZoomIn.TabIndex = 21
        Me.GrbZoomIn.TabStop = False
        Me.GrbZoomIn.Text = "Roll ile Yakınlaştırma"
        '
        'RollZoomInKatsayısı
        '
        Me.RollZoomInKatsayısı.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RollZoomInKatsayısı.FormattingEnabled = True
        Me.RollZoomInKatsayısı.Items.AddRange(New Object() {"Düşük", "Orta", "Yüksek", "Çok Yüksek"})
        Me.RollZoomInKatsayısı.Location = New System.Drawing.Point(6, 19)
        Me.RollZoomInKatsayısı.Name = "RollZoomInKatsayısı"
        Me.RollZoomInKatsayısı.Size = New System.Drawing.Size(128, 21)
        Me.RollZoomInKatsayısı.TabIndex = 21
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripDropDownButton2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1289, 25)
        Me.ToolStrip1.TabIndex = 25
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolstrpCikis})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(52, 22)
        Me.ToolStripDropDownButton1.Text = "Dosya"
        '
        'ToolstrpCikis
        '
        Me.ToolstrpCikis.Name = "ToolstrpCikis"
        Me.ToolstrpCikis.Size = New System.Drawing.Size(99, 22)
        Me.ToolstrpCikis.Text = "Çıkış"
        '
        'ToolStripDropDownButton2
        '
        Me.ToolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.YardımıGörüntüleToolStripMenuItem, Me.HakkındaToolStripMenuItem})
        Me.ToolStripDropDownButton2.Image = CType(resources.GetObject("ToolStripDropDownButton2.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
        Me.ToolStripDropDownButton2.Size = New System.Drawing.Size(58, 22)
        Me.ToolStripDropDownButton2.Text = "Yardım"
        '
        'YardımıGörüntüleToolStripMenuItem
        '
        Me.YardımıGörüntüleToolStripMenuItem.Name = "YardımıGörüntüleToolStripMenuItem"
        Me.YardımıGörüntüleToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.YardımıGörüntüleToolStripMenuItem.Text = "Yardımı Görüntüle"
        '
        'HakkındaToolStripMenuItem
        '
        Me.HakkındaToolStripMenuItem.Name = "HakkındaToolStripMenuItem"
        Me.HakkındaToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.HakkındaToolStripMenuItem.Text = "Hakkında..."
        '
        'PicRenk
        '
        Me.PicRenk.Location = New System.Drawing.Point(6, 258)
        Me.PicRenk.Name = "PicRenk"
        Me.PicRenk.Size = New System.Drawing.Size(215, 50)
        Me.PicRenk.TabIndex = 26
        Me.PicRenk.TabStop = False
        '
        'GrpBtns
        '
        Me.GrpBtns.Controls.Add(Me.BtnCiz)
        Me.GrpBtns.Controls.Add(Me.PicRenk)
        Me.GrpBtns.Controls.Add(Me.Label3)
        Me.GrpBtns.Controls.Add(Me.Label4)
        Me.GrpBtns.Controls.Add(Me.Hesap)
        Me.GrpBtns.Controls.Add(Me.Label5)
        Me.GrpBtns.Controls.Add(Me.CoorX)
        Me.GrpBtns.Controls.Add(Me.CoorY)
        Me.GrpBtns.Controls.Add(Me.BtnVideo)
        Me.GrpBtns.Controls.Add(Me.Btnreset)
        Me.GrpBtns.Controls.Add(Me.Btncapture)
        Me.GrpBtns.Location = New System.Drawing.Point(12, 34)
        Me.GrpBtns.Name = "GrpBtns"
        Me.GrpBtns.Size = New System.Drawing.Size(227, 319)
        Me.GrpBtns.TabIndex = 27
        Me.GrpBtns.TabStop = False
        Me.GrpBtns.Text = "Butonlar"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(1062, 34)
        Me.ProgressBar1.Maximum = 1024
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(215, 23)
        Me.ProgressBar1.TabIndex = 28
        Me.ProgressBar1.Visible = False
        '
        'PrgrsVideo
        '
        Me.PrgrsVideo.Location = New System.Drawing.Point(1062, 208)
        Me.PrgrsVideo.Name = "PrgrsVideo"
        Me.PrgrsVideo.Size = New System.Drawing.Size(215, 23)
        Me.PrgrsVideo.TabIndex = 31
        Me.PrgrsVideo.Visible = False
        '
        'lblvideobar
        '
        Me.lblvideobar.AutoSize = True
        Me.lblvideobar.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblvideobar.Location = New System.Drawing.Point(1059, 240)
        Me.lblvideobar.Name = "lblvideobar"
        Me.lblvideobar.Size = New System.Drawing.Size(34, 13)
        Me.lblvideobar.TabIndex = 32
        Me.lblvideobar.Text = "video"
        Me.lblvideobar.Visible = False
        '
        'Lblcizdirbar
        '
        Me.Lblcizdirbar.AutoSize = True
        Me.Lblcizdirbar.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Lblcizdirbar.Location = New System.Drawing.Point(1059, 67)
        Me.Lblcizdirbar.Name = "Lblcizdirbar"
        Me.Lblcizdirbar.Size = New System.Drawing.Size(209, 13)
        Me.Lblcizdirbar.TabIndex = 33
        Me.Lblcizdirbar.Text = "Resim çizdiriliyor. Lütfen bekleyiniz..."
        Me.Lblcizdirbar.Visible = False
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Mandelbrot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1289, 657)
        Me.Controls.Add(Me.Lblcizdirbar)
        Me.Controls.Add(Me.lblvideobar)
        Me.Controls.Add(Me.PrgrsVideo)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.GrpBtns)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GrbZoomIn)
        Me.Controls.Add(Me.Not2)
        Me.Controls.Add(Me.Not1)
        Me.Controls.Add(Me.PicHarita)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PicMandel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1309, 700)
        Me.MinimumSize = New System.Drawing.Size(1309, 700)
        Me.Name = "Mandelbrot"
        Me.Text = "Mandelbrot Fractal Drawer v7.1"
        CType(Me.PicMandel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicHarita, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrbZoomIn.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.PicRenk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBtns.ResumeLayout(False)
        Me.GrpBtns.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PicMandel As System.Windows.Forms.PictureBox
    Friend WithEvents BtnCiz As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Hesap As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CoorX As System.Windows.Forms.Label
    Friend WithEvents CoorY As System.Windows.Forms.Label
    Friend WithEvents Btnreset As System.Windows.Forms.Button
    Friend WithEvents Btncapture As System.Windows.Forms.Button
    Friend WithEvents PicHarita As System.Windows.Forms.PictureBox
    Friend WithEvents BtnVideo As System.Windows.Forms.Button
    Friend WithEvents Not1 As System.Windows.Forms.Label
    Friend WithEvents Not2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GrbZoomIn As System.Windows.Forms.GroupBox
    Friend WithEvents RollZoomInKatsayısı As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolstrpCikis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton2 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents YardımıGörüntüleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HakkındaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PicRenk As System.Windows.Forms.PictureBox
    Friend WithEvents GrpBtns As System.Windows.Forms.GroupBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SaveFileDialog2 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents PrgrsVideo As System.Windows.Forms.ProgressBar
    Friend WithEvents lblvideobar As System.Windows.Forms.Label
    Friend WithEvents Lblcizdirbar As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker

End Class
