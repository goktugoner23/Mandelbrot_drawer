Imports System.Math
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class Mandelbrot

#Region "Update Notes"
    'Mandelbrot Fractal Drawer v1.0 - Açıklama(05.03.2013) : Mandelbrot fraktalının çizilmesine yarayan bir programdır.
    'Mandelbrot Fractal Drawer v1.1 - Güncelleme(07.03.2013) : Labellarla arayüz değiştirildi. (Interface Update)
    'Mandelbrot Fractal Drawer v2.0 - Açıklama(13.03.2013) : Mandelbrot fraktalını çizen ve yakınlaştırma yapan bir program haline geldi. (Zoom Update)
    'Mandelbrot Fractal Drawer v2.1 - Güncelleme(20.03.2013) : Yüksek miktarda yakınlaştırma için gerekli olan veri tipleri eklendi. (Bug Fixed)
    'Mandelbrot Fractal Drawer v3.0 - Açıklama(25.03.2013) : Resim çekme komutu eklendi. (Capture Update)
    'Mandelbrot Fractal Drawer v3.1 - Güncelleme(28.03.2013) : Nerede olduğunuzu gösteren harita eklendi. (Map Update)
    'Mandelbrot Fractal Drawer v3.2 - Güncelleme(05.04.2013) : Zoom yöntemi değiştirildi.(CTRL + MouseWheel) (Zoom Update)
    'Mandelbrot Fractal Drawer v4.0 - Açıklama(08.04.2013) : Sürükle-Bırak işlemi eklendi. (Drag And Drop Update)
    'Mandelbrot Fractal Drawer v4.1 - Güncelleme(08.04.2013) : Harita arayüzü güncellendi. (Map Update)
    'Mandelbrot Fractal Drawer v5.0 - Açıklama(11.04.2013) : Video çekme komutu eklendi. (Video Record Update)
    'Mandelbrot Fractal Drawer v5.1 - Güncelleme(21.04.2013) : Arayüz güncellendi. (Interface Update)
    'Mandelbrot Fractal Drawer v6.0 - Açıklama(22.04.2013) : Renk paleti eklendi. (Color Palette Update)
    'Mandelbrot Fractal Drawer v6.1 - Güncelleme(22.04.2013) : Yardım kısmı ve harita kısmı yenilendi. (Help & Map Update, Thanks to SONY Xperia U :-) )
    'Mandelbrot Fractal Drawer v6.2 - Güncelleme(19.05.2013) : İterasyon fonksiyonları eklendi. (Calculation Update)
    'Mandelbrot Fractal Drawer v6.3 - Güncelleme(30.05.2013) : Resim - Video kaydetme seçenekleri, renk paleti, yakınlaştırma fonksiyonları güncellendi (File save, Rainbow, Zoom Update)
    'Mandelbrot Fractal Drawer v7.0 - Açıklama(02.06.2013) : Video prosedürü tamamen yenilendi. (Video Update)
    'Mandelbrot Fractal Drawer v7.1 - Güncelleme(18.06.2013) : Ekran öteleme prosedürü, arayüz güncellendi.  (Drag&Drop and Interface update)
#End Region

#Region "Local Variables"

    Dim rsmsayi As Integer

    Dim SbtMaxX, SbtMinX, SbtMaxY, SbtMinY As Decimal

    Dim MandelMaxX As Decimal = 1
    Dim MandelMinX As Decimal = -2.39
    Dim MandelMaxY As Decimal = 1.25
    Dim MandelMinY As Decimal = -1.2

    Dim BitmapPixelHeight As Integer
    Dim BitmapPixelWidth As Integer

    Dim BitmapPixelHeightCapture As Integer
    Dim BitmapPixelWidthCapture As Integer

    Dim FractalBtm As Bitmap
    Dim Rainbow As Bitmap

    Dim HueFrom As Decimal = 0.672 'tonlama için gerekli rakamlar
    Dim HueTo As Decimal = 0.481

    Dim SelectFrom As Point  'zoom icin gerekenler
    Dim SelectingRec As Rectangle

    Dim ZoomFactor As Double = 1 'zoom katsayısı(yazdırmak icin)

    Public i As Integer

    Public ctrl As Boolean = False

    Dim a As Integer = 1

    Dim dragx, dragy As Decimal

    Dim ZoomKatsayisi As Decimal = 5

    Dim load As Boolean = True

#End Region

#Region "Draw"

    Public Sub mandelcizsingle()
        Application.DoEvents()

        FractalBtm = New Bitmap(BitmapPixelWidth, BitmapPixelHeight, PixelFormat.Format32bppRgb)
        Dim Mat As BitmapData = FractalBtm.LockBits(New Rectangle(0, 0, BitmapPixelWidth, BitmapPixelHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, FractalBtm.PixelFormat)
        'system memory e sabitleme

        Dim Esc = Math.Ceiling(5 * Math.Log(IIf(ZoomFactor < 1, 1, ZoomFactor)) + 3) 'iterasyon fonksiyonları
        Dim EscapeValue = If(Esc > 2000, 2000, Esc)
        Dim Int = Math.Ceiling(200 * Math.Log(IIf(ZoomFactor < 1, 1, ZoomFactor)) + 150)
        Dim Maksiterasyon = If(Int > 10000, 10000, Int)

        Dim EscapeValue2 As Single = EscapeValue ^ 2 'sapma payları 'Me.nudEscapeValue.Value ^ 2
        Dim InvLogEscapeValue2 As Single = 1 / Math.Log(EscapeValue2)
        Dim MaxIterasyon As Integer = Maksiterasyon 'Me.nudMaxIteractions.Value
        Dim CurIterasyon As Integer

        'Dim MandelCurX As Single = 0
        'Dim MandelCurY As Single = 0

        Dim CurColor As Color '- smoothcoloring ve smart brightness

        Dim xTmp As Single
        Dim InvLog2 As Single = 1 / Math.Log(2) 'sapma payı

        Dim Dens As Single

        Dim x0 As Single
        Dim y0 As Single
        Dim y02 As Single

        Dim MandelCurX As Single = 0
        Dim MandelCurY As Single = 0

        'Dim Dens As Single - smoothcoloring ve smart brightness

        Dim Pixsel2MandelX As Single = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1) 'pikselden birime çevirme
        Dim Pixsel2MandelY As Single = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)

        Dim BtmPixselPos As Integer
        Dim q As Single

        Dim MandelCurX2 As Single
        Dim MandelCurY2 As Single


        Dim FirstPixPointer As IntPtr = Mat.Scan0
        For x = 0 To BitmapPixelWidth - 1 Step a
            'ProgressBar1.Visible = True
            ProgressBar1.Value = x
            For y = 0 To BitmapPixelHeight - 1 Step a

                CurIterasyon = 0
                MandelCurX = 0
                MandelCurX2 = 0
                MandelCurY = 0
                MandelCurY2 = 0


                x0 = x * Pixsel2MandelX + MandelMinX
                y0 = y * Pixsel2MandelY + MandelMinY

                y02 = y0 * y0
                q = (x0 - 1 / 4) ^ 2 + y02

                BtmPixselPos = Mat.Stride * y + 4 * x

                If 4 * q * (q + x0 - 1 / 4) < y02 Or (x0 + 1) ^ 2 + y02 < 1 / 16 Then 'kaçmayan noktaları boyama
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos, Color.Black.B)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, Color.Black.G)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, Color.Black.R)
                Else
                    Do While CurIterasyon < MaxIterasyon And MandelCurX2 + MandelCurY2 < EscapeValue2
                        xTmp = MandelCurX2 - MandelCurY2 + x0
                        MandelCurY = 2 * MandelCurX * MandelCurY + y0

                        MandelCurX = xTmp

                        MandelCurX2 = MandelCurX * MandelCurX
                        MandelCurY2 = MandelCurY * MandelCurY

                        CurIterasyon += 1

                    Loop
                    If CurIterasyon < MaxIterasyon Then

                        Dens = (CurIterasyon - 1 - Math.Log(Math.Log(MandelCurX2 + MandelCurY2) * InvLogEscapeValue2) * InvLog2) / MaxIterasyon
                        Dens = 10.897 * Dens ^ 4 - 28.78 * Dens ^ 3 + 25.884 * Dens ^ 2 - 8.1052 * Dens + 1.0717
                        CurColor = RGBxHSL.SetHue(Color.Orange, HueTo - (HueTo - HueFrom) * Dens)


                        CurColor = RGBxHSL.SetBrightness(CurColor, 1 - Dens)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos, CurColor.B)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, CurColor.G)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, CurColor.R)

                    End If

                End If
            Next
        Next

        FractalBtm.UnlockBits(Mat)

        Me.PicMandel.CreateGraphics.DrawImage(FractalBtm, 0, 0) 'bitmapi picturebox ın uzerine alıyor.

        GC.Collect() 'garbage collector

        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
        'ProgressBar1.Visible = False

    End Sub

    Public Sub mandelcizdouble()

        Application.DoEvents()

        FractalBtm = New Bitmap(BitmapPixelWidth, BitmapPixelHeight, PixelFormat.Format32bppRgb)
        Dim Mat As BitmapData = FractalBtm.LockBits(New Rectangle(0, 0, BitmapPixelWidth, BitmapPixelHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, FractalBtm.PixelFormat)
        'system memory e sabitleme

        Dim Esc = Math.Ceiling(5 * Math.Log(IIf(ZoomFactor < 1, 1, ZoomFactor)) + 3) 'iterasyon fonksiyonları
        Dim EscapeValue = If(Esc > 2000, 2000, Esc)
        Dim Int = Math.Ceiling(200 * Math.Log(IIf(ZoomFactor < 1, 1, ZoomFactor)) + 150)
        Dim Maksiterasyon = If(Int > 10000, 10000, Int)

        Dim EscapeValue2 As Single = EscapeValue ^ 2 'sapma payı
        Dim InvLogEscapeValue2 As Single = 1 / Math.Log(EscapeValue2)
        Dim MaxIterasyon As Integer = Maksiterasyon 'Me.nudMaxIteractions.Value
        Dim CurIterasyon As Integer

        'Dim MandelCurX As Single = 0
        'Dim MandelCurY As Single = 0

        Dim CurColor As Color '- smoothcoloring ve smart brightness

        Dim xTmp As Double
        Dim InvLog2 As Single = 1 / Math.Log(2) 'sapma payı

        Dim Dens As Single

        Dim x0 As Double
        Dim y0 As Double
        Dim y02 As Double

        Dim MandelCurX As Double = 0
        Dim MandelCurY As Double = 0

        'Dim Dens As Single - smoothcoloring ve smart brightness

        Dim Pixsel2MandelX As Double = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1) 'pikselden birime çevirme
        Dim Pixsel2MandelY As Double = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)

        Dim BtmPixselPos As Integer
        Dim q As Double

        Dim MandelCurX2 As Double
        Dim MandelCurY2 As Double

        Dim FirstPixPointer As IntPtr = Mat.Scan0

        For x = 0 To BitmapPixelWidth - 1 Step a
            'ProgressBar1.Visible = True
            ProgressBar1.Value = x
            For y = 0 To BitmapPixelHeight - 1 Step a

                CurIterasyon = 0
                MandelCurX = 0
                MandelCurX2 = 0
                MandelCurY = 0
                MandelCurY2 = 0


                x0 = x * Pixsel2MandelX + MandelMinX
                y0 = y * Pixsel2MandelY + MandelMinY

                y02 = y0 * y0
                q = (x0 - 1 / 4) ^ 2 + y02

                BtmPixselPos = Mat.Stride * y + 4 * x

                If 4 * q * (q + x0 - 1 / 4) < y02 Or (x0 + 1) ^ 2 + y02 < 1 / 16 Then 'kaçmayan noktaları boyama
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos, Color.Black.B)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, Color.Black.G)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, Color.Black.R)
                Else
                    Do While CurIterasyon < MaxIterasyon And MandelCurX2 + MandelCurY2 < EscapeValue2
                        xTmp = MandelCurX2 - MandelCurY2 + x0
                        MandelCurY = 2 * MandelCurX * MandelCurY + y0

                        MandelCurX = xTmp

                        MandelCurX2 = MandelCurX * MandelCurX
                        MandelCurY2 = MandelCurY * MandelCurY

                        CurIterasyon += 1

                    Loop
                    If CurIterasyon < MaxIterasyon Then

                        Dens = (CurIterasyon - 1 - Math.Log(Math.Log(MandelCurX2 + MandelCurY2) * InvLogEscapeValue2) * InvLog2) / MaxIterasyon
                        Dens = 10.897 * Dens ^ 4 - 28.78 * Dens ^ 3 + 25.884 * Dens ^ 2 - 8.1052 * Dens + 1.0717
                        CurColor = RGBxHSL.SetHue(Color.Orange, HueTo - (HueTo - HueFrom) * Dens)
                        CurColor = RGBxHSL.SetBrightness(CurColor, 1 - Dens)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos, CurColor.B)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, CurColor.G)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, CurColor.R)

                    End If

                End If
            Next
        Next

        FractalBtm.UnlockBits(Mat)

        Me.PicMandel.CreateGraphics.DrawImage(FractalBtm, 0, 0) 'bitmapi picturebox ın uzerine alıyor.

        GC.Collect() 'garbage collector

        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
        'ProgressBar1.Visible = False
    End Sub

    Public Sub mandelcizdecimal()
        FractalBtm = New Bitmap(BitmapPixelWidth, BitmapPixelHeight, PixelFormat.Format32bppRgb)

        Dim Mat As BitmapData = FractalBtm.LockBits(New Rectangle(0, 0, BitmapPixelWidth, BitmapPixelHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, FractalBtm.PixelFormat)

        Dim Esc = Math.Ceiling(5 * Math.Log(IIf(ZoomFactor < 1, 1, ZoomFactor)) + 3) 'iterasyon fonksiyonları
        Dim EscapeValue = If(Esc > 2000, 2000, Esc)
        Dim Int = Math.Ceiling(200 * Math.Log(IIf(ZoomFactor < 1, 1, ZoomFactor)) + 150)
        Dim Maksiterasyon = If(Int > 10000, 10000, Int)

        Dim EscapeValue2 As Single = EscapeValue ^ 2 'sapma payları 'Me.nudEscapeValue.Value ^ 2
        Dim InvLogEscapeValue2 As Single = 1 / Math.Log(EscapeValue2)
        Dim MaxIterasyon As Integer = Maksiterasyon 'Me.nudMaxIteractions.Value

        Dim CurIteration As Integer

        Dim MandelCurX As Decimal = 0
        Dim MandelCurY As Decimal = 0

        Dim CurColor As Color

        Dim xTmp As Decimal
        Dim InvLog2 As Single = 1 / Math.Log(2)

        Dim TransX As Decimal
        Dim TransY As Decimal
        Dim TransY2 As Decimal

        Dim Dens As Single

        Dim PixselToMandelXCoef As Decimal = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1)
        Dim PixselToMandelYCoef As Decimal = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)

        Dim BtmPixselPos As Integer
        Dim q As Decimal

        Dim MandelCurX2 As Decimal
        Dim MandelCurY2 As Decimal

        Dim FirstPixPtr As IntPtr = Mat.Scan0

        For x = 0 To BitmapPixelWidth - 1 Step a
            'ProgressBar1.Visible = True
            ProgressBar1.Value = x
            For y = 0 To BitmapPixelHeight - 1 Step a

                CurIteration = 0
                MandelCurX = 0
                MandelCurX2 = 0
                MandelCurY = 0
                MandelCurY2 = 0


                TransX = x * PixselToMandelXCoef + MandelMinX
                TransY = y * PixselToMandelYCoef + MandelMinY

                TransY2 = TransY * TransY
                q = (TransX - 1 / 4) ^ 2 + TransY2

                BtmPixselPos = Mat.Stride * y + 4 * x

                If 4 * q * (q + TransX - 1 / 4) < TransY2 Or (TransX + 1) ^ 2 + TransY2 < 1 / 16 Then
                    Marshal.WriteByte(FirstPixPtr, BtmPixselPos, Color.Black.B)
                    Marshal.WriteByte(FirstPixPtr, BtmPixselPos + 1, Color.Black.G)
                    Marshal.WriteByte(FirstPixPtr, BtmPixselPos + 2, Color.Black.R)
                Else
                    Do While CurIteration < MaxIterasyon And MandelCurX2 + MandelCurY2 < EscapeValue2
                        xTmp = MandelCurX2 - MandelCurY2 + TransX
                        MandelCurY = 2 * MandelCurX * MandelCurY + TransY

                        MandelCurX = xTmp

                        MandelCurX2 = MandelCurX * MandelCurX
                        MandelCurY2 = MandelCurY * MandelCurY

                        CurIteration += 1

                    Loop

                    If CurIteration < MaxIterasyon Then

                        Dens = (CurIteration - 1 - Math.Log(Math.Log(MandelCurX2 + MandelCurY2) * InvLogEscapeValue2) * InvLog2) / MaxIterasyon
                        Dens = 10.897 * Dens ^ 4 - 28.78 * Dens ^ 3 + 25.884 * Dens ^ 2 - 8.1052 * Dens + 1.0717
                        CurColor = RGBxHSL.SetHue(Color.Orange, HueTo - (HueTo - HueFrom) * Dens)

                        CurColor = RGBxHSL.SetBrightness(CurColor, 1 - Dens)

                        Marshal.WriteByte(FirstPixPtr, BtmPixselPos, CurColor.B)
                        Marshal.WriteByte(FirstPixPtr, BtmPixselPos + 1, CurColor.G)
                        Marshal.WriteByte(FirstPixPtr, BtmPixselPos + 2, CurColor.R)
                    End If
                End If
            Next
        Next

        FractalBtm.UnlockBits(Mat)

        Me.PicMandel.CreateGraphics.DrawImage(FractalBtm, 0, 0) 'bitmapi picturebox ın uzerine alıyor.

        GC.Collect() 'garbage collector

        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
        'ProgressBar1.Visible = False

    End Sub

    Private Sub BtnCiz_Click(sender As Object, e As EventArgs) Handles BtnCiz.Click
        Dim MandelMaxX As Decimal = 1
        Dim MandelMinX As Decimal = -2.39
        Dim MandelMaxY As Decimal = 1.25
        Dim MandelMinY As Decimal = -1.2
        BitmapPixelHeight = PicMandel.Height 'oran denklemleri
        BitmapPixelWidth = BitmapPixelHeight * (MandelMaxX - MandelMinX) / (MandelMaxY - MandelMinY)
        'FreezeUI(False)
        ProgressBar1.Visible = True
        Lblcizdirbar.Visible = True
        Lblcizdirbar.Visible = True
        If ZoomFactor < 50000 Then
            Label3.Text = "Single"
            Me.Cursor = Cursors.WaitCursor
            Dim Baslangic = Now
            mandelcizsingle()
            Dim Bitis = Now
            Me.Cursor = Cursors.Default
            Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
        ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
            Label3.Text = "Double"
            Me.Cursor = Cursors.WaitCursor
            Dim Baslangic = Now
            mandelcizdouble()
            Dim Bitis = Now
            Me.Cursor = Cursors.Default
            Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
        ElseIf ZoomFactor > 5000000000000.0 Then
            Label3.Text = "Decimal"
            Me.Cursor = Cursors.WaitCursor
            Dim Baslangic = Now
            mandelcizdecimal()
            Dim Bitis = Now
            Me.Cursor = Cursors.Default
            Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
        End If
        ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        Lblcizdirbar.Visible = False
        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
        'FreezeUI(True)
    End Sub

    Private Sub Btnreset_Click(sender As Object, e As EventArgs) Handles Btnreset.Click
        MandelMaxX = 1
        MandelMinX = -2.39
        MandelMaxY = 1.25
        MandelMinY = -1.2

        ZoomFactor = 1
        ProgressBar1.Visible = True
        Lblcizdirbar.Visible = True
        Dim Baslangic = Now
        Me.Cursor = Cursors.WaitCursor
        mandelcizsingle()
        Me.Cursor = Cursors.Default
        Dim Bitis = Now
        Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
        Label3.Text = "Single"
        ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        Lblcizdirbar.Visible = False
        PicHarita.Refresh()

        'Me.nudMaxIteractions.Value = 100
        'Me.nudEscapeValue.Value = 3
    End Sub

    Private Sub PicMandel_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles PicMandel.Paint
        If FractalBtm IsNot Nothing Then e.Graphics.DrawImage(FractalBtm, 0, 0)
    End Sub


#End Region

#Region "Zoom - Drag&Drop"

    Public Sub picmandel_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
        Dim PixselToMandelXCoef As Decimal = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1)
        Dim PixselToMandelYCoef As Decimal = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)
        Dim XCoor As Decimal = (e.X - 257) * PixselToMandelXCoef + MandelMinX
        Dim YCoor As Decimal = e.Y * PixselToMandelYCoef + MandelMinY
        Dim ZoomIn As Decimal
        Dim ZoomOut As Decimal

        If My.Computer.Keyboard.CtrlKeyDown = True And e.Delta > 0 Then
            ZoomIn = Math.Sqrt((100 - ZoomKatsayisi) / 100)
            a = 10
            Timer1.Enabled = True
            Dim ilkuz As Decimal
            ilkuz = MandelMaxX - MandelMinX
            MandelMaxX = XCoor + ((MandelMaxX - XCoor) * ZoomIn)
            MandelMinX = XCoor - ((XCoor - MandelMinX) * ZoomIn)
            MandelMaxY = YCoor - ((YCoor - MandelMaxY) * ZoomIn)
            MandelMinY = YCoor + ((MandelMinY - YCoor) * ZoomIn)
            ZoomFactor = ZoomFactor * (ilkuz / (MandelMaxX - MandelMinX))
            'ZoomFactor = ZoomFactor + (ZoomFactor * (ZoomKatsayisi / 100))
            FreezeUI(False)
            If ZoomFactor < 50000 Then
                Label3.Text = "Single"
                mandelcizsingle()
            ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
                Label3.Text = "Double"
                mandelcizdouble()
            ElseIf ZoomFactor > 5000000000000.0 Then
                Label3.Text = "Decimal"
                mandelcizdecimal()
            End If
            harita()
            Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
        End If

        If My.Computer.Keyboard.CtrlKeyDown = True And e.Delta < 0 Then
            ZoomOut = ((100 + ZoomKatsayisi) / 100)
            a = 10
            Timer1.Enabled = True
            Dim ilkuz As Decimal
            ilkuz = MandelMaxX - MandelMinX
            'MandelMaxX = XCoor + ((MandelMaxX - XCoor) * ZoomOut)
            'MandelMinX = XCoor - ((XCoor - MandelMinX) * ZoomOut)
            'MandelMaxY = YCoor - ((YCoor - MandelMaxY) * ZoomOut)
            'MandelMinY = YCoor + ((MandelMinY - YCoor) * ZoomOut)
            MandelMaxX = XCoor + ((MandelMaxX - XCoor) * ZoomOut)
            MandelMinX = XCoor - ((XCoor - MandelMinX) * ZoomOut)
            MandelMaxY = YCoor - ((YCoor - MandelMaxY) * ZoomOut)
            MandelMinY = YCoor + ((MandelMinY - YCoor) * ZoomOut)
            ZoomFactor = ZoomFactor * (ilkuz / (MandelMaxX - MandelMinX))
            'ZoomFactor = ZoomFactor - (ZoomFactor * (ZoomKatsayisi / 100))
            FreezeUI(False)
            If ZoomFactor < 50000 Then
                Label3.Text = "Single"
                mandelcizsingle()
            ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
                Label3.Text = "Double"
                mandelcizdouble()
            ElseIf ZoomFactor > 5000000000000.0 Then
                Label3.Text = "Decimal"
                mandelcizdecimal()
            End If
            harita()
        End If

    End Sub

    Public Sub RollZoomInKatsayısı_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RollZoomInKatsayısı.SelectedIndexChanged
        Select Case RollZoomInKatsayısı.SelectedIndex
            Case 0
                ZoomKatsayisi = 5
            Case 1
                ZoomKatsayisi = 10
            Case 2
                ZoomKatsayisi = 20
            Case 3
                ZoomKatsayisi = 50
        End Select
    End Sub

    Private Sub Mandelbrot_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AcceptButton = BtnCiz

        Control.CheckForIllegalCrossThreadCalls = False
        RollZoomInKatsayısı.SelectedIndex = 0
        Rainbow = New Bitmap(PicRenk.Width, PicRenk.Height)
        Dim Clr As RGBxHSL.HSL = RGBxHSL.RGB_to_HSL(Color.Orange)
        Clr.H = 0
        Dim G As Graphics = Graphics.FromImage(Rainbow)

        For i = 0 To PicRenk.Width
            Clr.H = i / PicRenk.Width
            G.DrawLine(New Pen(RGBxHSL.HSL_to_RGB(Clr)), New Point(i, 0), New Point(i, PicRenk.Height))
        Next

        Dim R As Bitmap = Rainbow.Clone
        G = Graphics.FromImage(R)
        G.DrawLine(New Pen(Brushes.White, 2), New Point(HueFrom * PicRenk.Width, 0), New Point(HueFrom * PicRenk.Width, PicRenk.Height))
        G.DrawLine(New Pen(Brushes.Black, 2), New Point(HueTo * PicRenk.Width, 0), New Point(HueTo * PicRenk.Width, PicRenk.Height))

        Me.PicRenk.BackgroundImage = R
    End Sub

    Private Sub PicMandel_MouseDown(sender As Object, e As MouseEventArgs) Handles PicMandel.MouseDown
        If My.Computer.Keyboard.ShiftKeyDown = True Then
            dragx = e.X
            dragy = e.Y
            BackgroundWorker1.RunWorkerAsync()
        End If
        If My.Computer.Keyboard.CtrlKeyDown = True Then
            If e.Button = Windows.Forms.MouseButtons.Left Then SelectFrom = e.Location 'mouse un tıkladığı yeri sabitliyor
        End If

    End Sub

    Private Sub PicMandel_MouseMove(sender As Object, e As MouseEventArgs) Handles PicMandel.MouseMove
        Dim XCoor As Decimal = e.X * (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1) + MandelMinX
        Dim YCoor As Decimal = e.Y * (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1) + MandelMinY
        CoorX.Text = "X: " & Format(XCoor, "##0.### ### ### ### ###")
        CoorY.Text = "Y: " & Format(-YCoor, "##0.### ### ### ### ###")
        If My.Computer.Keyboard.ShiftKeyDown = True Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                MandelMaxX = MandelMaxX - ((e.X - dragx) * (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1))
                MandelMinX = MandelMinX - ((e.X - dragx) * (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1))
                MandelMaxY = MandelMaxY - ((e.Y - dragy) * (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1))
                MandelMinY = MandelMinY - ((e.Y - dragy) * (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1))
                dragx = e.X
                dragy = e.Y
                harita()
            End If
        End If

        If My.Computer.Keyboard.CtrlKeyDown = True And e.Button = Windows.Forms.MouseButtons.Left Then
            If FractalBtm IsNot Nothing And e.Button = Windows.Forms.MouseButtons.Left Then
                ' Selecting = True, çizdirme durumu

                Dim B = FractalBtm.Clone
                Dim G = Graphics.FromImage(B)
                Dim Br = New SolidBrush(Color.FromArgb(80, Color.LightGray))


                If SelectFrom.X > e.X And SelectFrom.Y > e.Y Then
                    SelectingRec = New Rectangle(e.Location, SelectFrom - e.Location)
                ElseIf SelectFrom.X < e.X And SelectFrom.Y > e.Y Then
                    SelectingRec = New Rectangle(SelectFrom.X, e.Y, e.X - SelectFrom.X, SelectFrom.Y - e.Y)
                ElseIf SelectFrom.X > e.X And SelectFrom.Y < e.Y Then
                    SelectingRec = New Rectangle(e.X, SelectFrom.Y, SelectFrom.X - e.X, e.Y - SelectFrom.Y)
                Else
                    SelectingRec = New Rectangle(SelectFrom, e.Location - SelectFrom)
                End If

                G.DrawRectangle(Pens.LightGray, SelectingRec)
                Me.PicMandel.CreateGraphics.DrawImage(B, 0, 0)
            Else
                ' Selecting = False, çizdirmeme durumu
                SelectingRec = Nothing
            End If

        End If
    End Sub

    Private Sub PicMandel_MouseUp(sender As Object, e As MouseEventArgs) Handles PicMandel.MouseUp
        BackgroundWorker1.CancelAsync()
        If My.Computer.Keyboard.ShiftKeyDown = True Then
            ProgressBar1.Visible = True
            Lblcizdirbar.Visible = True
            FreezeUI(False)
            If ZoomFactor < 50000 Then
                Label3.Text = "Single"
                Me.Cursor = Cursors.WaitCursor
                Dim Baslangic = Now
                mandelcizsingle()
                Dim Bitis = Now
                Me.Cursor = Cursors.Default
                Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
            ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
                Label3.Text = "Double"
                Me.Cursor = Cursors.WaitCursor
                Dim Baslangic = Now
                mandelcizdouble()
                Dim Bitis = Now
                Me.Cursor = Cursors.Default
                Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
            ElseIf ZoomFactor > 5000000000000.0 Then
                Label3.Text = "Decimal"
                Me.Cursor = Cursors.WaitCursor
                Dim Baslangic = Now
                mandelcizdecimal()
                Dim Bitis = Now
                Me.Cursor = Cursors.Default
                Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
            End If
            FreezeUI(True)
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0
            Lblcizdirbar.Visible = False
        End If

        If My.Computer.Keyboard.CtrlKeyDown = True Then
            ProgressBar1.Visible = True
            Lblcizdirbar.Visible = True
            If SelectingRec <> Nothing AndAlso SelectingRec.Width > 4 AndAlso SelectingRec.Height > 4 Then

                Dim PixselToMandelXCoef As Decimal = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1)
                Dim PixselToMandelYCoef As Decimal = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)

                Dim TmpX = MandelMinX
                Dim TmpY = MandelMinY

                MandelMinX = SelectingRec.Left * PixselToMandelXCoef + MandelMinX
                MandelMinY = SelectingRec.Top * PixselToMandelYCoef + MandelMinY

                Dim XZoom = BitmapPixelWidth / SelectingRec.Width 'zoom degiskenleri, dikdörtgeni oranlamak icin
                Dim YZoom = BitmapPixelHeight / SelectingRec.Height

                If XZoom < YZoom Then
                    MandelMaxX = SelectingRec.Width * PixselToMandelXCoef + MandelMinX
                    MandelMaxY = SelectingRec.Height * YZoom / XZoom * PixselToMandelYCoef + MandelMinY
                    ZoomFactor = ZoomFactor * XZoom
                Else
                    MandelMaxX = SelectingRec.Width * XZoom / YZoom * PixselToMandelXCoef + MandelMinX
                    MandelMaxY = SelectingRec.Height * PixselToMandelYCoef + MandelMinY
                    ZoomFactor = ZoomFactor * YZoom

                End If
                FreezeUI(False)
                If ZoomFactor < 50000 Then
                    Label3.Text = "Single"
                    Me.Cursor = Cursors.WaitCursor
                    Dim Baslangic = Now
                    mandelcizsingle()
                    Dim Bitis = Now
                    Me.Cursor = Cursors.Default
                    Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."

                ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
                    Label3.Text = "Double"
                    Me.Cursor = Cursors.WaitCursor
                    Dim Baslangic = Now
                    mandelcizdouble()
                    Dim Bitis = Now
                    Me.Cursor = Cursors.Default
                    Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."

                ElseIf ZoomFactor > 5000000000000.0 Then
                    Label3.Text = "Decimal"
                    Me.Cursor = Cursors.WaitCursor
                    Dim Baslangic = Now
                    mandelcizdecimal()
                    Dim Bitis = Now
                    Me.Cursor = Cursors.Default
                    Hesap.Text = Format((Bitis - Baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
                End If
            End If

            Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
            SelectingRec = Nothing
            FreezeUI(True)
            harita()
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0
            Lblcizdirbar.Visible = False
        End If
    End Sub

    Public Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If My.Computer.Keyboard.CtrlKeyDown = False And load = False Then

            a = 1
            Me.Cursor = Cursors.WaitCursor
            Dim baslangic = Now
            ProgressBar1.Visible = True
            Lblcizdirbar.Visible = True
            If ZoomFactor < 50000 Then
                Label3.Text = "Single"
                mandelcizsingle()

                Timer1.Stop()
            ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
                Label3.Text = "Double"
                mandelcizdouble()
                Timer1.Stop()
            ElseIf ZoomFactor > 5000000000000.0 Then
                Label3.Text = "Decimal"
                mandelcizdecimal()
                Timer1.Stop()
            End If
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0
            Lblcizdirbar.Visible = False
            FreezeUI(True)
            Dim bitis = Now
            Me.Cursor = Cursors.Default
            Hesap.Text = Format((bitis - baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
            harita()

        End If
        If load = True Then
            ProgressBar1.Visible = True
            Lblcizdirbar.Visible = True
            Dim baslangic = Now
            FreezeUI(False)
            BitmapPixelHeight = PicMandel.Height 'oran denklemleri
            BitmapPixelWidth = BitmapPixelHeight * (MandelMaxX - MandelMinX) / (MandelMaxY - MandelMinY)
            mandelcizsingle()
            load = False
            Label3.Text = "Single"
            Timer1.Enabled = False
            Dim bitis = Now
            Hesap.Text = Format((bitis - baslangic).TotalMilliseconds / 1000, "0.000") & " sn."
            FreezeUI(True)
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0
            Lblcizdirbar.Visible = False
        End If
    End Sub

#End Region

#Region "Video Properties" 'http://forum.codecall.net/ sitesinden yararlanılmıştır.

    Public Class Avi

        Public Const StreamtypeVIDEO As Integer = 1935960438
        Public Const OF_SHARE_DENY_WRITE As Integer = 32
        Public Const BMP_MAGIC_COOKIE As Integer = 19778



        <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure RECTstruc
            Public left As UInt32
            Public top As UInt32
            Public right As UInt32
            Public bottom As UInt32
        End Structure

        <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure BITMAPINFOHEADERstruc
            Public biSize As UInt32
            Public biWidth As Int32
            Public biHeight As Int32
            Public biPlanes As Int16
            Public biBitCount As Int16
            Public biCompression As UInt32
            Public biSizeImage As UInt32
            Public biXPelsPerMeter As Int32
            Public biYPelsPerMeter As Int32
            Public biClrUsed As UInt32
            Public biClrImportant As UInt32
        End Structure

        <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure AVISTREAMINFOstruc
            Public fccType As UInt32
            Public fccHandler As UInt32
            Public dwFlags As UInt32
            Public dwCaps As UInt32
            Public wPriority As UInt16
            Public wLanguage As UInt16
            Public dwScale As UInt32
            Public dwRate As UInt32
            Public dwStart As UInt32
            Public dwLength As UInt32
            Public dwInitialFrames As UInt32
            Public dwSuggestedBufferSize As UInt32
            Public dwQuality As UInt32
            Public dwSampleSize As UInt32
            Public rcFrame As RECTstruc
            Public dwEditCount As UInt32
            Public dwFormatChangeCount As UInt32
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=64)> _
            Public szName As UInt16()
        End Structure



        'Initialize the AVI library
        <DllImport("avifil32.dll")> _
        Public Shared Sub AVIFileInit()
        End Sub

        'Open an AVI file
        <DllImport("avifil32.dll", PreserveSig:=True)> _
        Public Shared Function AVIFileOpen(ByRef ppfile As Integer, ByVal szFile As [String], ByVal uMode As Integer, ByVal pclsidHandler As Integer) As Integer
        End Function

        'Create a new stream in an open AVI file
        <DllImport("avifil32.dll")> _
        Public Shared Function AVIFileCreateStream(ByVal pfile As Integer, ByRef ppavi As IntPtr, ByRef ptr_streaminfo As AVISTREAMINFOstruc) As Integer
        End Function

        'Set the format for a new stream
        <DllImport("avifil32.dll")> _
        Public Shared Function AVIStreamSetFormat(ByVal aviStream As IntPtr, ByVal lPos As Int32, ByRef lpFormat As BITMAPINFOHEADERstruc, ByVal cbFormat As Int32) As Integer
        End Function

        'Write a sample to a stream
        <DllImport("avifil32.dll")> _
        Public Shared Function AVIStreamWrite(ByVal aviStream As IntPtr, ByVal lStart As Int32, ByVal lSamples As Int32, ByVal lpBuffer As IntPtr, ByVal cbBuffer As Int32, ByVal dwFlags As Int32, _
         ByVal dummy1 As Int32, ByVal dummy2 As Int32) As Integer
        End Function

        'Release an open AVI stream
        <DllImport("avifil32.dll")> _
        Public Shared Function AVIStreamRelease(ByVal aviStream As IntPtr) As Integer
        End Function

        'Release an open AVI file
        <DllImport("avifil32.dll")> _
        Public Shared Function AVIFileRelease(ByVal pfile As Integer) As Integer
        End Function

        'Close the AVI library
        <DllImport("avifil32.dll")> _
        Public Shared Sub AVIFileExit()
        End Sub
    End Class

    Public Class AviWriter
        Private aviFile As Integer = 0
        Private aviStream As IntPtr = IntPtr.Zero
        Private frameRate As UInt32 = 0
        Private countFrames As Integer = 0
        Private width As Integer = 0
        Private height As Integer = 0
        Private stride As UInt32 = 0
        Private fccType As UInt32 = Avi.StreamtypeVIDEO
        Private fccHandler As UInt32 = 1668707181
        Private strideInt As Integer
        Private strideU As UInteger
        Private heightU As UInteger
        Private widthU As UInteger

        Public Sub OpenAVI(ByVal fileName As String, ByVal frameRate As UInt32)
            Me.frameRate = frameRate

            Avi.AVIFileInit()


            Dim OpeningError As Integer = Avi.AVIFileOpen(aviFile, fileName, 4097, 0)
            If OpeningError <> 0 Then
                Throw New Exception("Error in AVIFileOpen: " + OpeningError.ToString())
            End If
        End Sub

        Public Sub AddFrame(ByVal bmp As Bitmap)

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)

            Dim bmpData As BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.[ReadOnly], PixelFormat.Format24bppRgb)

            If countFrames = 0 Then
                Dim bmpDatStride As UInteger = bmpData.Stride
                Me.stride = DirectCast(bmpDatStride, UInt32)
                Me.width = bmp.Width
                Me.height = bmp.Height
                CreateStream()
            End If

            strideInt = stride
            Dim writeResult As Integer = Avi.AVIStreamWrite(aviStream, countFrames, 1, bmpData.Scan0, DirectCast((strideInt * height), Int32), 0, _
             0, 0)

            If writeResult <> 0 Then
                Throw New Exception("Error in AVIStreamWrite: " + writeResult.ToString())
            End If

            bmp.UnlockBits(bmpData)
            System.Math.Max(System.Threading.Interlocked.Increment(countFrames), countFrames - 1)
        End Sub

        Private Sub CreateStream()
            Dim strhdr As New Avi.AVISTREAMINFOstruc()
            strhdr.fccType = fccType
            strhdr.fccHandler = fccHandler
            strhdr.dwScale = 1
            strhdr.dwRate = frameRate
            strideU = stride
            heightU = height
            strhdr.dwSuggestedBufferSize = DirectCast((stride * strideU), UInt32)
            strhdr.dwQuality = 10000

            heightU = height
            widthU = width
            strhdr.rcFrame.bottom = DirectCast(heightU, UInt32)
            strhdr.rcFrame.right = DirectCast(widthU, UInt32)
            strhdr.szName = New UInt16(64) {}

            Dim createResult As Integer = Avi.AVIFileCreateStream(aviFile, aviStream, strhdr)
            If createResult <> 0 Then
                Throw New Exception("Error in AVIFileCreateStream: " + createResult.ToString())
            End If

            Dim bi As New Avi.BITMAPINFOHEADERstruc()
            Dim bisize As UInteger = Marshal.SizeOf(bi)
            bi.biSize = DirectCast(bisize, UInt32)
            bi.biWidth = DirectCast(width, Int32)
            bi.biHeight = DirectCast(height, Int32)
            bi.biPlanes = 1
            bi.biBitCount = 24

            strideU = stride
            heightU = height
            bi.biSizeImage = DirectCast((strideU * heightU), UInt32)

            Dim formatResult As Integer = Avi.AVIStreamSetFormat(aviStream, 0, bi, Marshal.SizeOf(bi))
            If formatResult <> 0 Then
                Throw New Exception("Error in AVIStreamSetFormat: " + formatResult.ToString())
            End If
        End Sub

        Public Sub Close()
            If aviStream <> IntPtr.Zero Then
                Avi.AVIStreamRelease(aviStream)
                aviStream = IntPtr.Zero
            End If
            If aviFile <> 0 Then
                Avi.AVIFileRelease(aviFile)
                aviFile = 0
            End If
            Avi.AVIFileExit()
        End Sub
    End Class

#End Region

#Region "Capture&Video"

    Public Sub ResimSay()
        Dim zoomsbt As Decimal = ZoomFactor
        rsmsayi = 0
        While ZoomFactor >= 1 'burada while'a girecek

            ZoomFactor = ZoomFactor * 98 / 100
            rsmsayi += 1

        End While
        ZoomFactor = zoomsbt
    End Sub

    Public Sub Video()
        ResimSay()
        Dim MaxX, MinX, MaxY, MinY As Decimal
        PrgrsVideo.Visible = True
        PrgrsVideo.Maximum = rsmsayi
        lblvideobar.Text = "Resimleriniz çekiliyor"

        Dim screenBitmap(rsmsayi - 1) As Bitmap 'video çekerken kullanılacak resimlerin depolanacağı dizi(array)

        MaxX = MandelMaxX
        MinX = MandelMinX
        MaxY = MandelMaxY
        MinY = MandelMinY

        Me.Cursor = Cursors.WaitCursor
        Dim zoomsbt As Decimal = ZoomFactor
        Dim n As Integer = 0
        While ZoomFactor >= 1 'burada while'a girecek
            Dim ilkuzX As Decimal = MandelMaxX - MandelMinX 'zoom degiskenleri, dikdörtgeni oranlamak icin
            Dim ilkuzY As Decimal = MandelMaxY - MandelMinY
            Dim sonuzX As Decimal = ZoomFactor * ilkuzX / (ZoomFactor * 98 / 100)
            Dim sonuzY As Decimal = ZoomFactor * ilkuzY / (ZoomFactor * 98 / 100)
            Dim xcoord As Decimal = MandelMinX + ((ilkuzX) / 2)
            Dim ycoord As Decimal = MandelMinY + ((ilkuzY) / 2)
            MandelMaxX = xcoord + (sonuzX / 2)
            MandelMinX = xcoord - (sonuzX / 2)
            MandelMaxY = ycoord + (sonuzY / 2)
            MandelMinY = ycoord - (sonuzY / 2)
            If ZoomFactor < 50000 Then
                mandelcizsingle()
            ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
                mandelcizdouble()
            ElseIf ZoomFactor > 5000000000000.0 Then
                mandelcizdecimal()
            End If
            ZoomFactor = ZoomFactor * 98 / 100
            screenBitmap(rsmsayi - 1 - n) = FractalBtm
            n += 1
            PrgrsVideo.Value = n
            Select Case n Mod 3
                Case 0
                    lblvideobar.Text = "Resimleriniz çekiliyor."
                Case 1
                    lblvideobar.Text = "Resimleriniz çekiliyor.."
                Case 2
                    lblvideobar.Text = "Resimleriniz çekiliyor..."
            End Select

        End While   'burada while bitecek

        PrgrsVideo.Visible = False
        lblvideobar.Visible = False
        Me.Cursor = Cursors.Default

        ZoomFactor = zoomsbt
        MandelMaxX = MaxX
        MandelMinX = MinX
        MandelMaxY = MaxY
        MandelMinY = MinY
        If ZoomFactor < 50000 Then
            mandelcizsingle()
        ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
            mandelcizdouble()
        ElseIf ZoomFactor > 5000000000000.0 Then
            mandelcizdecimal()
        End If

        Dim Writer As New AviWriter
        Dim c As Integer = 1
        Me.Cursor = Cursors.WaitCursor
        My.Computer.FileSystem.CreateDirectory("C:/Mandelbrot Fractal Drawer/Videos")
        Writer.OpenAVI("C:/Mandelbrot Fractal Drawer/Videos" & c & ".Avi", 10)
        c += 1

        For Frame As Integer = 0 To rsmsayi - 1
            Writer.AddFrame(screenBitmap(Frame))
        Next

        Writer.Close()
        Me.Cursor = Cursors.Default

    End Sub

    Public Sub BtnVideo_Click(sender As Object, e As EventArgs) Handles BtnVideo.Click
        PicMandel.Visible = False
        lblvideobar.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Video()
        Me.Cursor = Cursors.Default
        PicMandel.Visible = True
        lblvideobar.Visible = False
    End Sub

    Public Sub Resim()

        ' Displays a SaveFileDialog so the user can save the Image
        ' assigned to Button2.
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif"
        saveFileDialog1.Title = "Save an Image File"
        saveFileDialog1.ShowDialog()

        ' If the file name is not an empty string open it for saving.
        If saveFileDialog1.FileName <> "" Then
            ' Saves the Image via a FileStream created by the OpenFile method.
            Dim fs As System.IO.FileStream = CType _
               (saveFileDialog1.OpenFile(), System.IO.FileStream)
            ' Saves the Image in the appropriate ImageFormat based upon the
            ' file type selected in the dialog box.
            ' NOTE that the FilterIndex property is one-based.
            Select Case saveFileDialog1.FilterIndex
                Case 1
                    FractalBtm.Save(fs, _
                       System.Drawing.Imaging.ImageFormat.Jpeg)

                Case 2
                    FractalBtm.Save(fs, _
                       System.Drawing.Imaging.ImageFormat.Bmp)

                Case 3
                    FractalBtm.Save(fs, _
                       System.Drawing.Imaging.ImageFormat.Gif)
            End Select

            fs.Close()
        End If
    End Sub

    Private Sub Btncapture_Click(sender As Object, e As EventArgs) Handles Btncapture.Click
        Resim()
    End Sub

#End Region

#Region "Renk paleti"

    Private Sub PicRenk_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PicRenk.MouseMove
        Dim X As Integer = e.X
        If e.X < 0 Then X = 0
        If e.X > PicRenk.Width Then X = PicRenk.Width

        If e.Button = Windows.Forms.MouseButtons.Left Then
            HueFrom = X / PicRenk.Width
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            HueTo = X / PicRenk.Width
        End If

        If e.Button = Windows.Forms.MouseButtons.Left Or e.Button = Windows.Forms.MouseButtons.Right Then
            Dim R = Rainbow.Clone
            Dim G = Graphics.FromImage(R)

            G.DrawLine(New Pen(Brushes.White, 2), New Point(HueFrom * PicRenk.Width, 0), New Point(HueFrom * PicRenk.Width, PicRenk.Height))
            G.DrawLine(New Pen(Brushes.Black, 2), New Point(HueTo * PicRenk.Width, 0), New Point(HueTo * PicRenk.Width, PicRenk.Height))

            Me.PicRenk.BackgroundImage = R

        End If
    End Sub
#End Region

#Region "Araç çubuğu"

    Public Sub harita() 'Sony Xperia U'dan yaralanılmıştır.
        Dim x1, x2, y1, y2 As Integer
        Dim h As System.Drawing.Graphics = PicHarita.CreateGraphics
        Dim firca As New SolidBrush(Color.FromArgb(80, Color.LightGray))
        Dim kalem As New Pen(Color.LightGray)

        x1 = PicHarita.Width * (MandelMaxX - (-2.39)) / 3.39
        x2 = PicHarita.Width * (MandelMinX - (-2.39)) / 3.39
        y1 = PicHarita.Height * (MandelMinY - (-1.2)) / 2.45
        y2 = PicHarita.Height * (MandelMaxY - (-1.2)) / 2.45
        h = PicHarita.CreateGraphics
        PicHarita.Refresh()
        h.FillRectangle(firca, x2, y1, (Math.Abs(x1 - x2)), Math.Abs(y1 - y2))
        h.DrawLine(kalem, x1, PicHarita.Height, x1, 0)
        h.DrawLine(kalem, x2, PicHarita.Height, x2, 0)
        h.DrawLine(kalem, 0, y1, PicHarita.Width, y1)
        h.DrawLine(kalem, 0, y2, PicHarita.Width, y2)
    End Sub

    Public Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ProgressBar1.Visible = True
        Lblcizdirbar.Visible = True
        While My.Computer.Keyboard.ShiftKeyDown = True
            If ZoomFactor < 50000 Then
                smandelsingle()
            ElseIf ZoomFactor > 50000 And ZoomFactor < 5000000000000.0 Then
                smandeldouble()
            ElseIf ZoomFactor > 5000000000000.0 Then
                smandeldecimal()
            End If
        End While
        ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        Lblcizdirbar.Visible = False
    End Sub

    Private Sub ToolstrpCikis_Click(sender As Object, e As EventArgs) Handles ToolstrpCikis.Click
        Me.Close()
    End Sub

    Private Sub HakkındaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HakkındaToolStripMenuItem.Click
        Hakkinda.Show()
    End Sub

    Private Sub YardımıGörüntüleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YardımıGörüntüleToolStripMenuItem.Click
        Yardım.Show()
    End Sub

    Public Sub FreezeUI(Enabled As Boolean)
        GrpBtns.Enabled = Enabled
    End Sub

    Public Sub smandelsingle()
        Application.DoEvents()

        FractalBtm = New Bitmap(BitmapPixelWidth, BitmapPixelHeight, PixelFormat.Format32bppRgb)
        Dim Mat As BitmapData = FractalBtm.LockBits(New Rectangle(0, 0, BitmapPixelWidth, BitmapPixelHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, FractalBtm.PixelFormat)
        'system memory e sabitleme

        Dim EscapeValue2 As Single = 3 'sapma payları 'Me.nudEscapeValue.Value ^ 2
        Dim InvLogEscapeValue2 As Single = 1 / Math.Log(EscapeValue2)
        Dim MaxIterasyon As Integer = 50 'Me.nudMaxIteractions.Value
        Dim CurIterasyon As Integer


        'Dim MandelCurX As Single = 0
        'Dim MandelCurY As Single = 0

        Dim CurColor As Color '- smoothcoloring ve smart brightness

        Dim xTmp As Single
        Dim InvLog2 As Single = 1 / Math.Log(2) 'sapma payı

        Dim Dens As Single

        Dim x0 As Single
        Dim y0 As Single
        Dim y02 As Single

        Dim MandelCurX As Single = 0
        Dim MandelCurY As Single = 0

        'Dim Dens As Single - smoothcoloring ve smart brightness

        Dim Pixsel2MandelX As Single = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1) 'pikselden birime çevirme
        Dim Pixsel2MandelY As Single = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)

        Dim BtmPixselPos As Integer
        Dim q As Single

        Dim MandelCurX2 As Single
        Dim MandelCurY2 As Single


        Dim FirstPixPointer As IntPtr = Mat.Scan0
        For x = 0 To BitmapPixelWidth - 1 Step 10
            ProgressBar1.Value = x
            For y = 0 To BitmapPixelHeight - 1 Step 10

                CurIterasyon = 0
                MandelCurX = 0
                MandelCurX2 = 0
                MandelCurY = 0
                MandelCurY2 = 0


                x0 = x * Pixsel2MandelX + MandelMinX
                y0 = y * Pixsel2MandelY + MandelMinY

                y02 = y0 * y0
                q = (x0 - 1 / 4) ^ 2 + y02

                BtmPixselPos = Mat.Stride * y + 4 * x

                If 4 * q * (q + x0 - 1 / 4) < y02 Or (x0 + 1) ^ 2 + y02 < 1 / 16 Then 'kaçmayan noktaları boyama
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos, Color.Black.B)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, Color.Black.G)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, Color.Black.R)
                Else
                    Do While CurIterasyon < MaxIterasyon And MandelCurX2 + MandelCurY2 < EscapeValue2
                        xTmp = MandelCurX2 - MandelCurY2 + x0
                        MandelCurY = 2 * MandelCurX * MandelCurY + y0

                        MandelCurX = xTmp

                        MandelCurX2 = MandelCurX * MandelCurX
                        MandelCurY2 = MandelCurY * MandelCurY

                        CurIterasyon += 1

                    Loop
                    If CurIterasyon < MaxIterasyon Then

                        Dens = (CurIterasyon - 1 - Math.Log(Math.Log(MandelCurX2 + MandelCurY2) * InvLogEscapeValue2) * InvLog2) / MaxIterasyon
                        Dens = 10.897 * Dens ^ 4 - 28.78 * Dens ^ 3 + 25.884 * Dens ^ 2 - 8.1052 * Dens + 1.0717
                        CurColor = RGBxHSL.SetHue(Color.Orange, HueTo - (HueTo - HueFrom) * Dens)


                        CurColor = RGBxHSL.SetBrightness(CurColor, 1 - Dens)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos, CurColor.B)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, CurColor.G)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, CurColor.R)

                    End If

                End If
            Next
        Next

        FractalBtm.UnlockBits(Mat)

        Me.PicMandel.CreateGraphics.DrawImage(FractalBtm, 0, 0) 'bitmapi picturebox ın uzerine alıyor.

        GC.Collect() 'garbage collector

        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
    End Sub

    Public Sub smandeldouble()
        Application.DoEvents()

        FractalBtm = New Bitmap(BitmapPixelWidth, BitmapPixelHeight, PixelFormat.Format32bppRgb)
        Dim Mat As BitmapData = FractalBtm.LockBits(New Rectangle(0, 0, BitmapPixelWidth, BitmapPixelHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, FractalBtm.PixelFormat)
        'system memory e sabitleme

        Dim EscapeValue2 As Single = 3 'sapma payları 'Me.nudEscapeValue.Value ^ 2
        Dim InvLogEscapeValue2 As Double = 1 / Math.Log(EscapeValue2)
        Dim MaxIterasyon As Integer = 50 'Me.nudMaxIteractions.Value
        Dim CurIterasyon As Integer

        'Dim MandelCurX As Single = 0
        'Dim MandelCurY As Single = 0

        Dim CurColor As Color '- smoothcoloring ve smart brightness

        Dim xTmp As Double
        Dim InvLog2 As Double = 1 / Math.Log(2) 'sapma payı

        Dim Dens As Double

        Dim x0 As Double
        Dim y0 As Double
        Dim y02 As Double

        Dim MandelCurX As Double = 0
        Dim MandelCurY As Double = 0

        'Dim Dens As Single - smoothcoloring ve smart brightness

        Dim Pixsel2MandelX As Double = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1) 'pikselden birime çevirme
        Dim Pixsel2MandelY As Double = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)

        Dim BtmPixselPos As Integer
        Dim q As Double

        Dim MandelCurX2 As Double
        Dim MandelCurY2 As Double


        Dim FirstPixPointer As IntPtr = Mat.Scan0
        For x = 0 To BitmapPixelWidth - 1 Step 3
            ProgressBar1.Value = x
            For y = 0 To BitmapPixelHeight - 1 Step 3

                CurIterasyon = 0
                MandelCurX = 0
                MandelCurX2 = 0
                MandelCurY = 0
                MandelCurY2 = 0


                x0 = x * Pixsel2MandelX + MandelMinX
                y0 = y * Pixsel2MandelY + MandelMinY

                y02 = y0 * y0
                q = (x0 - 1 / 4) ^ 2 + y02

                BtmPixselPos = Mat.Stride * y + 4 * x

                If 4 * q * (q + x0 - 1 / 4) < y02 Or (x0 + 1) ^ 2 + y02 < 1 / 16 Then 'kaçmayan noktaları boyama
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos, Color.Black.B)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, Color.Black.G)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, Color.Black.R)
                Else
                    Do While CurIterasyon < MaxIterasyon And MandelCurX2 + MandelCurY2 < EscapeValue2
                        xTmp = MandelCurX2 - MandelCurY2 + x0
                        MandelCurY = 2 * MandelCurX * MandelCurY + y0

                        MandelCurX = xTmp

                        MandelCurX2 = MandelCurX * MandelCurX
                        MandelCurY2 = MandelCurY * MandelCurY

                        CurIterasyon += 1

                    Loop
                    If CurIterasyon < MaxIterasyon Then

                        Dens = (CurIterasyon - 1 - Math.Log(Math.Log(MandelCurX2 + MandelCurY2) * InvLogEscapeValue2) * InvLog2) / MaxIterasyon
                        Dens = 10.897 * Dens ^ 4 - 28.78 * Dens ^ 3 + 25.884 * Dens ^ 2 - 8.1052 * Dens + 1.0717
                        CurColor = RGBxHSL.SetHue(Color.Orange, HueTo - (HueTo - HueFrom) * Dens)


                        CurColor = RGBxHSL.SetBrightness(CurColor, 1 - Dens)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos, CurColor.B)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, CurColor.G)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, CurColor.R)

                    End If

                End If
            Next
        Next

        FractalBtm.UnlockBits(Mat)

        Me.PicMandel.CreateGraphics.DrawImage(FractalBtm, 0, 0) 'bitmapi picturebox ın uzerine alıyor.

        GC.Collect() 'garbage collector

        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
    End Sub

    Public Sub smandeldecimal()
        Application.DoEvents()

        FractalBtm = New Bitmap(BitmapPixelWidth, BitmapPixelHeight, PixelFormat.Format32bppRgb)
        Dim Mat As BitmapData = FractalBtm.LockBits(New Rectangle(0, 0, BitmapPixelWidth, BitmapPixelHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, FractalBtm.PixelFormat)
        'system memory e sabitleme

        Dim EscapeValue2 As Decimal = 3 'sapma payları 'Me.nudEscapeValue.Value ^ 2
        Dim InvLogEscapeValue2 As Decimal = 1 / Math.Log(EscapeValue2)
        Dim MaxIterasyon As Integer = 50 'Me.nudMaxIteractions.Value
        Dim CurIterasyon As Integer

        'Dim MandelCurX As Single = 0
        'Dim MandelCurY As Single = 0

        Dim CurColor As Color '- smoothcoloring ve smart brightness

        Dim xTmp As Decimal
        Dim InvLog2 As Decimal = 1 / Math.Log(2) 'sapma payı

        Dim Dens As Decimal

        Dim x0 As Decimal
        Dim y0 As Decimal
        Dim y02 As Decimal

        Dim MandelCurX As Decimal = 0
        Dim MandelCurY As Decimal = 0

        'Dim Dens As Single - smoothcoloring ve smart brightness

        Dim Pixsel2MandelX As Decimal = (MandelMaxX - MandelMinX) / (BitmapPixelWidth - 1) 'pikselden birime çevirme
        Dim Pixsel2MandelY As Decimal = (MandelMaxY - MandelMinY) / (BitmapPixelHeight - 1)

        Dim BtmPixselPos As Integer
        Dim q As Decimal

        Dim MandelCurX2 As Decimal
        Dim MandelCurY2 As Decimal


        Dim FirstPixPointer As IntPtr = Mat.Scan0
        For x = 0 To BitmapPixelWidth - 1 Step 10
            ProgressBar1.Value = x
            For y = 0 To BitmapPixelHeight - 1 Step 10

                CurIterasyon = 0
                MandelCurX = 0
                MandelCurX2 = 0
                MandelCurY = 0
                MandelCurY2 = 0


                x0 = x * Pixsel2MandelX + MandelMinX
                y0 = y * Pixsel2MandelY + MandelMinY

                y02 = y0 * y0
                q = (x0 - 1 / 4) ^ 2 + y02

                BtmPixselPos = Mat.Stride * y + 4 * x

                If 4 * q * (q + x0 - 1 / 4) < y02 Or (x0 + 1) ^ 2 + y02 < 1 / 16 Then 'kaçmayan noktaları boyama
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos, Color.Black.B)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, Color.Black.G)
                    Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, Color.Black.R)
                Else
                    Do While CurIterasyon < MaxIterasyon And MandelCurX2 + MandelCurY2 < EscapeValue2
                        xTmp = MandelCurX2 - MandelCurY2 + x0
                        MandelCurY = 2 * MandelCurX * MandelCurY + y0

                        MandelCurX = xTmp

                        MandelCurX2 = MandelCurX * MandelCurX
                        MandelCurY2 = MandelCurY * MandelCurY

                        CurIterasyon += 1

                    Loop
                    If CurIterasyon < MaxIterasyon Then

                        Dens = (CurIterasyon - 1 - Math.Log(Math.Log(MandelCurX2 + MandelCurY2) * InvLogEscapeValue2) * InvLog2) / MaxIterasyon
                        Dens = 10.897 * Dens ^ 4 - 28.78 * Dens ^ 3 + 25.884 * Dens ^ 2 - 8.1052 * Dens + 1.0717
                        CurColor = RGBxHSL.SetHue(Color.Orange, HueTo - (HueTo - HueFrom) * Dens)


                        CurColor = RGBxHSL.SetBrightness(CurColor, 1 - Dens)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos, CurColor.B)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 1, CurColor.G)
                        Marshal.WriteByte(FirstPixPointer, BtmPixselPos + 2, CurColor.R)

                    End If

                End If
            Next
        Next

        FractalBtm.UnlockBits(Mat)

        Me.PicMandel.CreateGraphics.DrawImage(FractalBtm, 0, 0) 'bitmapi picturebox ın uzerine alıyor.

        GC.Collect() 'garbage collector

        Label2.Text = Format(ZoomFactor, "#,##0.00") & " x"
    End Sub

#End Region


End Class


