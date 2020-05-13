/*
 * Created by SharpDevelop.
 * User: alphasamirpur
 * Date: 04/19/2020
 * Time: 17:28
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
using System.Windows.Shapes;
namespace Fractal_try2
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
		double angle = 0;
		double deg = 0;
		void click(object sender, RoutedEventArgs e)
		{
			if (deg < 181) {
				deg += 0.4;
			}
			int iterations = 8;
			canvas.LayoutTransform = new ScaleTransform(1,-1);
			double centerx = canvas.ActualWidth / 2;
			double centery = canvas.ActualHeight / 2;
			double length = Math.Sqrt(Math.Pow(centery,2));			
			double x1 = centerx;
			double x2 = centerx;
			double y1 = 0;
			double y2 = centery;
			branchoff(x1,y1,x2,y2);
			angle = deg/90*Math.PI;
			for (int i = 0; i < iterations; i++) {
				length = length * 2 / 3;
				x2 = x2 + (length * Math.Sin(angle));
				y2 = y2 + (length * Math.Cos(angle));
				branchoff(x1,y1,x2,y2);
				x1 = x2 + 1 - 1;
				y1 = y2 + 1 - 1;
			}
		}
		public void branchoff(double x1, double y1, double x2, double y2) {
			Line branch = new Line();
			branch.Stroke = new SolidColorBrush(Colors.White);
			branch.X1 = x1;
			branch.X2 = x2;
			branch.Y1 = y1;
			branch.Y2 = y2;
			canvas.Children.Add(branch);
		
		}
	}
}