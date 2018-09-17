using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Media.Animation;

namespace WpfApp20._06
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{

			InitializeComponent();



			/*var AnimationX = new DoubleAnimation(0, 100, TimeSpan.FromSeconds(1));
			var AnimationY = new DoubleAnimation(0, 200, TimeSpan.FromSeconds(1));

			var Transform = new TranslateTransform();
			RFID_Token.RenderTransform = Transform;

			Transform.BeginAnimation(TranslateTransform.XProperty, AnimationX);
			Transform.BeginAnimation(TranslateTransform.YProperty, AnimationY);*/




		}


		private void Grid_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void button3_Click(object sender, RoutedEventArgs e)
		{
			Visualisation visualisation = new Visualisation(RFID_Token,false); //ок


		}

		private void TextBlockB()
		{

		}

		static private T FindParent<T>(FrameworkElement from) where T : FrameworkElement
		{

			var current = from.DataContext;
			
			T t;
			do
			{
				t = current as T;
		
			}
			while (t == null && current != null);

			return t;
		}



		private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			
			FocusControl.Focus();
			
		}

		private void DraggableItemsHost_Loaded(object sender, RoutedEventArgs e)
		{
			MainVM mainVM = new MainVM();
			DraggableItemsHost.DataContext = mainVM;

		}



		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			MainVM mainVM = new MainVM();
			DraggableItemsHost.DataContext = mainVM;
			Repository repository = new Repository();
			repository.Delete();
		}
		 
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MainVM mainVM = new MainVM();
			DraggableItemsHost.DataContext = mainVM;
		}

		private void compilar_Drop(object sender, DragEventArgs e)
		{
			if (e.Handled == false)
			{
				Panel _panel = (Panel)sender;
				UIElement _element = (UIElement)e.Data.GetData("Object");

				if (_panel != null && _element != null)
				{
					// Get the panel that the element currently belongs to,
					// then remove it from that panel and add it the Children of
					// the panel that its been dropped on.
					Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

					if (_parent != null)
					{
						if (e.KeyStates == DragDropKeyStates.ControlKey &&
							e.AllowedEffects.HasFlag(DragDropEffects.Copy))
						{
							UserControl1 _circle = new UserControl1((UserControl1)_element);
							_panel.Children.Add(_circle);
							// set the value to return to the DoDragDrop call
							e.Effects = DragDropEffects.Copy;
						}
						else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
						{
							_parent.Children.Remove(_element);
							_panel.Children.Add(_element);
							// set the value to return to the DoDragDrop call
							e.Effects = DragDropEffects.Move;
						}
					}
				}
			}
		}

		private void compilar_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("Object"))
			{
				// These Effects values are used in the drag source's
				// GiveFeedback event handler to determine which cursor to display.
				if (e.KeyStates == DragDropKeyStates.ControlKey)
				{
					e.Effects = DragDropEffects.Copy;
				}
				else
				{
					e.Effects = DragDropEffects.Move;
				}
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{			
			Visualisation visualisation = new Visualisation(RFID_Token,false);
		}

		private void Button_ClickSpravka(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("Title of this help project.chm");
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			Visualisation visualisation = new Visualisation(RFID_Token, true);
		}
	}
	class VM : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
            {
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        class SquareVM : VM
        {
		public SquareVM()
		{
			RequestMove = new SimpleCommand<Point>(MoveTo);
			
		}

		

		double pointWhile;
		public double PointWhike
		{
			get { return pointWhile; }
			set { if (pointWhile != value) { pointWhile = value; NotifyPropertyChanged(); } }
		}
		
		// стандартное свойство
		Point position;
            public Point Position
            {
                get { return position; }
                set { if (position != value) { position = value; NotifyPropertyChanged(); } }
            }

		bool focusTextBoxScript;
		public bool FocusTextBoxScript
		{
			get { return focusTextBoxScript; }
			set { if (focusTextBoxScript != value) { focusTextBoxScript = value; NotifyPropertyChanged(); } }
		}

			// выставляем команду, которая занимается перемещением
		public ICommand RequestMove { get; }

            void MoveTo(Point newPosition)
            {
                // в реальности тут могут быть всякие проверки, конечно
                Position = newPosition;
            }

            Brush color;
            public Brush Color
            {
                get { return color; }
                set { if (color != value) { color = value; NotifyPropertyChanged(); } }
            }

        int width;

        public int Width
        {
            get { return width;}
            set { if (width != value) { width = value;NotifyPropertyChanged(); } }
        }
       

            String text;
            public string Text
            {
                get { return text; }
                set { if (text != value) { text = value; NotifyPropertyChanged(); } }
            }
            int id;
            public int Id
            {
                get { return id; }
                set { if (id !=value) { id = value; NotifyPropertyChanged(); } }
            }

    }
        class SimpleCommand<T> : ICommand
        {
            readonly Action<T> onExecute;
            public SimpleCommand(Action<T> onExecute) { this.onExecute = onExecute; }

            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => onExecute((T)parameter);
	
        }

	class MainVM : VM
	{

		public ObservableCollection<SquareVM> Squares { get; } =
			new ObservableCollection<SquareVM>()
			{

					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false,PointWhike = 21 },
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false ,PointWhike = 21},
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false ,PointWhike = 21},
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false ,PointWhike = 21},
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false,PointWhike = 21 },
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false,PointWhike = 21 },
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false ,PointWhike = 21},
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false,PointWhike = 21 },
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false,PointWhike = 21 },
					new SquareVM() { Position = new Point( 30,  30),
					 Color = Brushes.Blue , Id=1,Text="Вперед на",Width=SizeText("Вперед на"),FocusTextBoxScript = false,PointWhike = 21 },


			new SquareVM() { Position = new Point( 30,  60), PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,  60),PointWhike = 21,
				Color = Brushes.Blue, Id=2, Text="Назад на",Width=SizeText("Назад на"),FocusTextBoxScript = false},


			new SquareVM() { Position = new Point(30,  90), Id=3, PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point(30,  90), Id=3,PointWhike = 21,
			Color = Brushes.Blue, Text="На лево",Width=SizeText("На лево"),FocusTextBoxScript = false },


			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},
			new SquareVM() { Position = new Point( 30,   120), Id=4,PointWhike = 21,
			Color = Brushes.Blue,Text="На право",Width=SizeText("На право"),FocusTextBoxScript = false},


			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 150), Id=5,
			Color = Brushes.DeepPink,Text = "Уменьшить",Width=SizeText("Уменьшить "),FocusTextBoxScript = false,PointWhike = 21 },


			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },
			new SquareVM() { Position = new Point( 30, 180), Id=6,
			Color = Brushes.DeepPink,Text = "Увеличить",Width=SizeText("Увеличить "),FocusTextBoxScript = false,PointWhike = 21 },


			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},
			new SquareVM() { Position = new Point( 30,  210),
					 Color = Brushes.Orange , Id=7,Text="Повторять",Width=SizeText("Повторять "),FocusTextBoxScript = false,
					PointWhike = 21},

			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 240), Id=8,
			Color = Brushes.Brown,Text = "Создать",Width=SizeText("Создать"),FocusTextBoxScript = false },

			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 270), Id=9,
			Color = Brushes.Brown,Text = "Начать",Width=SizeText("Начать"),FocusTextBoxScript = false },

			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			new SquareVM() { Position = new Point( 30, 300), Id=10,
			Color = Brushes.Brown,Text = "Конец",Width=SizeText("Конец"),FocusTextBoxScript = false },
			};


		static int SizeText(string scriptText)
		{
			return scriptText.Length * 10 + 20;
		}
	}
	class MainVM2 : VM
	{

		public ObservableCollection<SquareVM> Squares { get; } =
			new ObservableCollection<SquareVM>()
			{

					new SquareVM() { Position = new Point( 100,  100),
					 Color = Brushes.Orange , Id=1,Text="Вперед",Width=SizeText("Вперед на"),FocusTextBoxScript = false,
					PointWhike = 21, },

			new SquareVM() { Position = new Point( 30,  60),
				Color = Brushes.Blue, Id=2, Text="Назад",Width=SizeText("Назад на"),FocusTextBoxScript = false},

			

			new SquareVM() { Position = new Point(30, 20), Id=6,
			Color = Brushes.Blue, Text="Выбратьвчаспмриотлььд",Width=SizeText("Выбратьвчаспмриотлььд"),FocusTextBoxScript = false }
			};
			
		static int SizeText(string scriptText)
		{
			return scriptText.Length * 10 + 20;
		}
	}
}
