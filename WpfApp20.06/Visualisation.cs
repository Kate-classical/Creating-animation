using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace WpfApp20._06
{
	class Visualisation
	{
		private bool rendring = false;
		public Visualisation(Shape shape,bool yes)
		{
			
				Repository repository = new Repository();
				var listScripts = repository.GetScripts();
			var list = new ObservableCollection<InformationScript>();
				double x = Canvas.GetLeft(shape);
				double y = Canvas.GetTop(shape);
			var width = shape.ActualWidth;
			var height = shape.ActualHeight;
				int i = 1;
				int increase = 0;
				int unincrease = 0;
			 
				CompositionTarget.Rendering += RenderFrame;


			void StopRendering()
			{
				CompositionTarget.Rendering -= RenderFrame;
				
			}

			void RenderFrame(object sender, EventArgs e)
			{
				if (yes) StopRendering();
				if (i != listScripts.Count)
				{
					switch (listScripts[i].textScrip)
					{
						case "Начать": i++;
							break;
						case "Конец":
							i++;
							break;
						case "Повторять":
							{
								var n = listScripts[i].n;
								var id = listScripts[i].id_;
								try
								{
									listScripts.Remove(listScripts[i]);
									if(listScripts[i].textScrip == "Начать")
									{
										listScripts.Remove(listScripts[i]);
										while(listScripts[i].textScrip != "Конец")
										{

											list.Add(listScripts[i]);
											listScripts.Remove(listScripts[i]);
										}
										listScripts.Remove(listScripts[i]);
										while (n != 0)
										{
											for (int j = 0; j < list.Count; j++)
											{
												listScripts.Insert(id, list[j]);
												id++;
											}											
											n--;
										}
									}

									else
									{
										MessageBox.Show("Неверный цикл");
										StopRendering();
									}
								}
								catch
								{
									MessageBox.Show("Неверный цикл");
									StopRendering();
								}
							}
							break;
						case "На право":
							{
								var xx = Canvas.GetLeft(shape);
								if (xx > 355)
								{
									i++;
									break;
								}
								else
								{									
									Canvas.SetLeft(shape, xx += 1);

								}
								if (xx == x + listScripts[i].n * 10)
								{
									i++;
									x = xx;
								}
							}
							break;
						case "На лево":
							{
								var xx = Canvas.GetLeft(shape);
								if (xx < 10)
								{
									i++;
									break;
								}
								else
								{									
									Canvas.SetLeft(shape, xx -= 1);
								}
								if (xx == x-listScripts[i].n * 10)
								{
									i++;
									x = xx;
								}
							}
							break;
						case "Вперед на":
							{
								var yy = Canvas.GetTop(shape);
								if (yy < 10)
								{
									i++;
									break;
								}
								else
								{		
									
									Canvas.SetTop(shape, yy -= 1);
								}
								if (yy == y-listScripts[i].n*10 )
								{
									i++;
									y = yy;
								}
							}
							break;
						case "Назад на":
							{
								var yy = Canvas.GetTop(shape);
								if (yy > 500)
								{
									i++;
									break;
								}
								else
								{									
									Canvas.SetTop(shape, yy += 1);
								}
								if (yy == y + listScripts[i].n * 10)
								{
									i++;
									y = yy;
								}
							}
							break;
						case "Уменьшить":
							{
								if (increase != listScripts[i].n * 10)
								{
									Storyboard strBoard = new Storyboard();
									TimeSpan span = TimeSpan.FromSeconds(200);
									//var widthW = shape.ActualWidth;
									width *= 0.99;
									//var heightH = shape.ActualHeight;
									height *= 0.99;
									var animation = new DoubleAnimation(width, span);
									Storyboard.SetTarget(animation, shape);
									Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));
									strBoard.Children.Add(animation);
									animation = new DoubleAnimation(height, span);
									Storyboard.SetTarget(animation, shape);
									Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
									strBoard.Children.Add(animation);
									strBoard.AutoReverse = true;
									strBoard.Duration = span;
									strBoard.Begin();
									strBoard.Seek(span);
									strBoard.Pause();
									increase++;
								}
								else
								{
									i++;
									increase = 0;
								}
								
							}
							break;
						case "Увеличить":
							{
								if (unincrease != listScripts[i].n * 10)
								{
									Storyboard strBoard = new Storyboard();
									TimeSpan span = TimeSpan.FromSeconds(200);
								//	var width = shape.ActualWidth;
									width *= 1.01;
								//	var height = shape.ActualHeight;
									height *= 1.01;
									var animation = new DoubleAnimation(width, span);
									Storyboard.SetTarget(animation, shape);
									Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));
									strBoard.Children.Add(animation);
									animation = new DoubleAnimation(height, span);
									Storyboard.SetTarget(animation, shape);
									Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
									strBoard.Children.Add(animation);
									strBoard.AutoReverse = true;
									strBoard.Duration = span;
									strBoard.Begin();
									strBoard.Seek(span);
									strBoard.Pause();
									unincrease++;
								}
								else
								{
									i++;
									unincrease = 0;
								}

							}
							break;
					}
															
					
				}
				else
				{
					StopRendering();
				}
			}			
		}
		int Roate { get; set; }

	}
}
