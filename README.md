# Mandelbrot_drawer
Draws a Mandelbrot fractal, colorizes and explores it deeply

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Mandelbrot cluster is one of the impressive clusters that still remains a mystery since Benoit Mandelbrot's discovery in the 1970s. 
Mathematicians and computer scientists have been working for years to reveal the beauties and secrets of this cluster. 
A wide variety of iteration and coloring algorithms have been developed to take a closer look at the points of the cluster and to color the resulting images. 
In this study, the existing algorithms were examined and an approach program was prepared using the algorithm called "cardioid test". 
Thus, it was possible to observe the secrets of this cluster by getting close enough to any point. In addition, an approach journey to any point of this cluster is saved as a video file.  
<p align="center"><img src="https://user-images.githubusercontent.com/5355062/199925990-80ae54eb-e244-4de6-960d-460783ca37bf.jpg" width="513" height="371"></p>

<h2>Coloring the Fractal</h2>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; While coloring in the program, the HSL (Hue - Saturation - Luminance) system is used, not the RGB (Red - Green - Blue) coloring system that the computer detects by default. In the RGB system, the transition between the iterations is shown with sharp color differences, while in the HSL system this transition is shown fluently with different shades of several colors. Thus, the user is given an eye-pleasing image. At any time, the user can choose two different colors from the color palette and color the fractal according to the tone between these two colors.
<br/><br/>
<p align="center"><img src="https://user-images.githubusercontent.com/5355062/199927773-89d529dc-a0c5-4397-be67-23bce7432d37.jpg" width="513" height  ="371"></p>

<h2>Saving the Output as Video or Picture File</h2>
<h3>Save As Picture</h3>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; In this procedure, the image saving feature (“Bitmap.Save”) of the surface on which the fractal is drawn is used. When the user presses the image save button, the program saves the current image to the computer.  
<h3>Save As Picture</h3>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Since there is no command in any programming language to directly videotape the actions performed in the program, to do this it is necessary to call certain configuration settings from the system files of the operating system. In the video shooting procedure, a video file was created in (.avi) format, and the configuration setting named “avifil32.dll” was used in this process. After this configuration setting is called, the properties of the (.avi) format are coded into the program, and the properties of the video file that the program will create (number of pictures to be played per second, height, width, total number of pictures to be used, etc.) are introduced to the program.
  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; As it is known, videos are actually pictures that are played back and forth. Therefore, in order to create a video file, you must first have the images on which you will create the video. When a video is wanted to be taken in the program, the program zooms out from the current location at a certain rate, redraws the fractal at each zoom out, takes its picture, and creates a series of these pictures. For example, if you take 1800 pictures and the number of pictures to be played per second is 10, the program creates a 3-minute video. The amount of removal is determined by dividing the difference between the border points of the current region and the first border points by the number of pictures to be taken, so the image drawn from the current location with the first border values ​​is reached. The program creates a video file in such a way that it plays the obtained picture sequence from the end to the beginning, and thus the resulting video gives the feeling of falling from the first state of the fractal to the current location.

<div align="center">
  <video src="https://user-images.githubusercontent.com/5355062/199934960-5368969f-ca98-47a4-8d2a-de5060598c26.mp4"/>
<div/>
<br/><br/>



