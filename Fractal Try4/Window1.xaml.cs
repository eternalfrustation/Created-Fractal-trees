/*
 * Created by SharpDevelop.
 * User: alphasamirpur
 * Date: 04/20/2020
 * Time: 13:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Shapes;
namespace Fractal_Try4
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}
		double oglength;
		Random rnd = new Random();
		double length;
		double x2;
		double y2;
		int res;
		int id = 0;
		void doit(object sender, RoutedEventArgs e)
		{
			res = Convert.ToInt32(reso.Text);
			grid.Width = res;
			grid.Height = res * 9 / 16;
			grid2.Width = res;
			grid2.Height = res * 9  / 16;
			grid.Children.Clear();
			double width = res/100;
			stem(grid.ActualWidth/2, grid.ActualHeight, grid.ActualWidth/2, grid.ActualHeight*2/3, width);
			double angle = slider.Value;
			double a = (x2 + y2)/2;
			double ea = (grid.ActualWidth + grid.ActualHeight)/2;
			angle = angle*mod1(a,0,ea,1,-1);
			length = Math.Sqrt(Math.Pow((grid.ActualHeight*3/2 - grid.ActualHeight),2));
			oglength = length + 1 - 1;
			createbranch(length*2/3, grid.ActualWidth/2, grid.ActualHeight*2/3, -angle-180, width*0.8);
			createbranch(length*2/3, grid.ActualWidth/2, grid.ActualHeight*2/3, -angle-90, width*0.8);
			
		}
		public void stem(double x1, double y1, double x2, double y2, double width) {
			Line stem = new Line();
			stem.StrokeThickness = width;
			stem.X1 = x1;
			stem.Y1 = y1;
			stem.X2 = x2;
			stem.Y2 = y2;
			stem.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255,255,255));
			grid.Children.Add(stem);
		}
		public void createbranch(double length, double x1, double y1, double angle, double width) {
			id++;
			byte randr = Convert.ToByte(mod1(x1, 0, grid.ActualWidth, 1, 254));
			byte randg = Convert.ToByte(mod1(y1, 0, grid.ActualWidth, 1, 254));
			byte randb = Convert.ToByte(mod1(length, 0, oglength, 1, 254));
			Line branch = new Line();
			length = length*2/3;
			branch.X1 = x1;
			branch.Y1 = y1;
			x2 = x1 + (length * Math.Cos(angle * Math.PI/180));
			y2 = y1 + (length * Math.Sin(angle * Math.PI/180));
			branch.X2 = x2;
			branch.Y2 = y2;
			branch.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(randr, randg, randb));
			branch.StrokeThickness = width;
			double a = (x2 + y2);
			double ea = (grid.ActualWidth + grid.ActualHeight)/2;
			if (staticangle.Text == "") {
				angle = -angle*mod1(a,0,ea,0,-1);
			}
			else {
				angle = angle - Convert.ToDouble(staticangle.Text);
			}
			grid.Children.Add(branch);
			if (length >= Convert.ToInt16(box.Text)) {
				createbranch(length, branch.X2, branch.Y2, angle+45, width * 0.8);
				createbranch(length, branch.X2, branch.Y2, angle-45, width * 0.8);
			}
		}
		public double mod1(double value, double istart, double istop, double ostart, double ostop) {
			return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
		}
		private void CommandBinding_Executed(object sender, RoutedEventArgs e)
		{
			System.Windows.Size a = new System.Windows.Size(grid.ActualWidth, grid.ActualHeight);
   			Rect rect = new Rect(a);
   			RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
     			(int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
   			rtb.Render(grid);
   			//endcode as PNG
   			BitmapEncoder pngEncoder = new PngBitmapEncoder();
   			pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

   			//save to memory stream
   			System.IO.MemoryStream ms = new System.IO.MemoryStream();

   			pngEncoder.Save(ms);
   			ms.Close();
   			System.IO.File.WriteAllBytes("logo.png", ms.ToArray());
		}
	}
}