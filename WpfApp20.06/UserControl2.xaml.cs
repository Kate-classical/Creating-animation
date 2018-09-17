using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfApp20._06
{
	/// <summary>
	/// Логика взаимодействия для UserControl2.xaml
	/// </summary>
	public partial class UserControl2 : UserControl
	{
		public UserControl2()
		{
			InitializeComponent();
			SetBinding(RequestMoveCommandProperty, new Binding("RequestMove"));
		}
		Vector relativeMousePos; // смещение мыши от левого верхнего угла квадрата
		Canvas container;        // канвас-контейнер
		InformationScript informationScript = new InformationScript();
		Repository repository = new Repository();
		

		/*public UserControl2(UserControl2 usercontrol)
		{
			InitializeComponent();
			this.usercontrol.Height = usercontrol.usercontrol.Height;
			this.usercontrol.Width = usercontrol.usercontrol.Width;
			this.usercontrol.DataContext = usercontrol.usercontrol.DataContext;


		}*/


		public ICommand RequestMoveCommand
		{
			get { return (ICommand)GetValue(RequestMoveCommandProperty); }
			set { SetValue(RequestMoveCommandProperty, value); }
		}

		public Shape DraggedImageContainer
		{
			get { return (Shape)GetValue(DraggedImageContainerProperty); }
			set { SetValue(DraggedImageContainerProperty, value); }
		}

		public string TextScriptCommand
		{
			get { return (string)GetValue(Text); }
			set { SetValue(Text, value); }
		}



		public static readonly DependencyProperty DraggedImageContainerProperty =
			DependencyProperty.Register(
				"DraggedImageContainer", typeof(Shape), typeof(UserControl1));

		public static readonly DependencyProperty RequestMoveCommandProperty =
			DependencyProperty.Register("RequestMoveCommand", typeof(ICommand),
										typeof(UserControl1));

		public static readonly DependencyProperty Text =
			DependencyProperty.Register("TextScriptCommand", typeof(string), typeof(UserControl1));


		private static void OnIsFocusedPropertyChanged(
			DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			var uie = (UIElement)d;
			if ((bool)e.NewValue)
			{
				uie.Focus();
			}
		}

		// по нажатию на левую клавишу начинаем следить за мышью
		void OnMouseDown(object sender, MouseButtonEventArgs e)
		{

			StartUserControl = this;
			StartCanvas = FindParent<Canvas>(this);
			SquareVM square = this.DataContext as SquareVM;
			StartElement = square;
			relativeMousePos = e.GetPosition(this) - new Point();
			MouseMove += OnDragMove;
			LostMouseCapture += OnLostCapture;
			Mouse.Capture(this);


			/*var dragImageContainer = DraggedImageContainer;
			var parent = FindParent<Canvas>(dragImageContainer);
			var l = parent.Children;
			var position = StartPosition;
			Canvas.SetLeft(dragImageContainer, position.X);
			Canvas.SetTop(dragImageContainer, position.Y);*/


		}

		// клавиша отпущена - завершаем процесс
		void OnMouseUp(object sender, MouseButtonEventArgs e)
		{

			FinishDrag(sender, e);

			var dragImageContainer = DraggedImageContainer;
			var l = StartCanvas.Children;
			var position = StartElement.Position;
			Canvas.SetLeft(StartUserControl, position.X);
			Canvas.SetTop(StartUserControl, position.Y);



			Mouse.Capture(null);
		}

		// потеряли фокус (например, юзер переключился в другое окно) - завершаем тоже
		void OnLostCapture(object sender, MouseEventArgs e)
		{
			FinishDrag(sender, e);

			var dragImageContainer = DraggedImageContainer;
			var parent = FindParent<Canvas>(dragImageContainer);
			var l = parent.Children;
			var position = StartElement.Position;
			Canvas.SetLeft(dragImageContainer, position.X);
			Canvas.SetTop(dragImageContainer, position.Y);
		}

		void OnDragMove(object sender, MouseEventArgs e)
		{
			UpdateDraggedSquarePosition(e);
		}

		void FinishDrag(object sender, MouseEventArgs e)
		{

			MouseMove -= OnDragMove;
			LostMouseCapture -= OnLostCapture;
			UpdatePosition(e);
			var dragImageContainer = DraggedImageContainer;
			var parent = FindParent<Canvas>(dragImageContainer);

			var position = e.GetPosition(parent) - relativeMousePos; //позиция после перемещения

			if (position.X > 300 && position.X < 433)
			{

				var qwerty = FindInfoControl<Canvas>(this); //data of usercontrol


				informationScript.AddId(qwerty.Id);
				informationScript.AddTextScript(qwerty.Text);

				if (repository.Create())
				{
					informationScript.AddHeight(22);
					Attraction(repository.GetPosition());
				}
				else
				{
					informationScript.AddHeight(position.Y + 10);
					informationScript.AddX(position.X);
					informationScript.AddIdUnic(-1);
				}
				repository.Add(informationScript);
				//repository.AddSqaresVM(this);

				UpdateDraggedSquarePosition(null);
			}
			else
			{
				SquareVM square = this.DataContext as SquareVM;
				square.Position = StartElement.Position;
				UpdateDraggedSquarePosition(null);
			}
			SquareVM square2 = this.DataContext as SquareVM;
			square2.Position = StartElement.Position;


		}
		// 
		void Attraction(Tuple<double, double> tuple)
		{
			var point = new Point(tuple.Item1, tuple.Item2);
			// не забываем проверку на null
			RequestMoveCommand?.Execute(point);
		}

		// требуем у VM обновить позицию через команду
		void UpdatePosition(MouseEventArgs e)
		{
			var point = e.GetPosition(container);

			// не забываем проверку на null
			RequestMoveCommand?.Execute(point - relativeMousePos);
		}

		// это вспомогательная функция, ей место в общей библиотеке
		static private T FindParent<T>(FrameworkElement from) where T : FrameworkElement
		{
			FrameworkElement current = from;

			T t;
			do
			{
				t = current as T;
				current = (FrameworkElement)VisualTreeHelper.GetParent(current);
			}
			while (t == null && current != null);

			return t;
		}

		static private SquareVM FindInfoControl<T>(FrameworkElement from) where T : FrameworkElement
		{
			FrameworkElement current = from;
			var qwer = current.DataContext as SquareVM;

			return qwer;
		}


		void UpdateDraggedSquarePosition(MouseEventArgs e)
		{
			var dragImageContainer = DraggedImageContainer;
			if (dragImageContainer == null)
				return;
			var needVisible = e != null;
			var wasVisible = dragImageContainer.Visibility == Visibility.Visible;
			// включаем/выключаем видимость перемещаемой картинки
			dragImageContainer.Visibility = needVisible ? Visibility.Visible : Visibility.Collapsed;
			if (!needVisible) // если мы выключились, нам больше нечего делать
				return;
			if (!wasVisible) // а если мы были выключены и включились,
			{                // нам надо привязать изображение себя
				dragImageContainer.Fill = new VisualBrush(this); //перерисоввывывает
				dragImageContainer.SetBinding( // а также ширину/высоту
					Shape.WidthProperty,
					new Binding(nameof(ActualWidth)) { Source = this });
				dragImageContainer.SetBinding(
					Shape.HeightProperty,
					new Binding(nameof(ActualHeight)) { Source = this });
				// Binding нужен потому, что наш размер может по идее измениться
			}
			// перемещаем картинку на нужную позицию
			var parent = FindParent<Canvas>(dragImageContainer);
			var l = parent.Children;
			var position = e.GetPosition(parent) - relativeMousePos;
			Canvas.SetLeft(dragImageContainer, position.X);
			Canvas.SetTop(dragImageContainer, position.Y);
		}

		private void textCount_LostFocus(object sender, RoutedEventArgs e)
		{
			var element = this;
			SquareVM square = element.DataContext as SquareVM;
			int n = Convert.ToInt32(textCount.Text);
			repository.ReplacementCount(n, square.Id);


		}

		private void textCount_GotFocus(object sender, RoutedEventArgs e)
		{


		}

		SquareVM StartElement { get; set; }
		Canvas StartCanvas { get; set; }
		UserControl StartUserControl { get; set; }
	}
}
