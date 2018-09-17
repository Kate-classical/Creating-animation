using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp20._06
{
	
    public class InformationScript
	{
		public int id_ { get; set; }
		public string textScrip { get; set; }
		public int n { get; set; }		
		public int id { get; set; }
		public double height { get; set; }
		public void AddId(int id)
		{
			this.id = id;
		}
		public void AddTextScript(string textScript)
		{
			this.textScrip = textScript;
		}
		public void AddHeight(double height)
		{
			this.height = height;
			ListScpipts.YYY += height;
		}
		public void AddX(double x)
		{
			ListScpipts.XXX = x;
		}
		public void AddIdUnic(int id)
		{
			ListScpipts.id = id;
		}
	}
	static class ListScpipts
	{
		public static ObservableCollection<InformationScript> Scripts = new ObservableCollection<InformationScript>();
		public static double YYY;
		public static double XXX;
		public static int id;

		public static ObservableCollection<UserControl1> squares = new ObservableCollection<UserControl1>();
	}

	

	
    class Repository
    {	
		
		public ObservableCollection<InformationScript> GetScripts()
		{			
			return ListScpipts.Scripts;
		}
		public void Delete()
		{
			ListScpipts.Scripts.Remove(ListScpipts.Scripts.First());
			for (int i = 0; i < ListScpipts.Scripts.Count ; i++)
			{
				ListScpipts.Scripts.RemoveAt(0);
			}
			ListScpipts.id = 0;
			ListScpipts.XXX = 0;
			ListScpipts.YYY = 0;
			for (int k = 0; k < ListScpipts.squares.Count ; k++)
			{
				ListScpipts.squares.RemoveAt(0);
			}
		}
		public void Add( InformationScript Script)
		{
			Script.id_ = ++ListScpipts.id;
			ListScpipts.Scripts.Add(Script);
		}
		public void AddSqaresVM(UserControl1 userControl)
		{
			ListScpipts.squares.Add(userControl);
		}
		public void ReplacementCount(int nn,int id)
		{
			InformationScript informationScript = new InformationScript();
			foreach (var script in ListScpipts.Scripts)
			{
				if (script.id == id)
				{
					ListScpipts.Scripts[script.id_].n = nn;					
					break;
				}
			}
		}
		public Tuple<double,double> GetPosition()
		{
			return Tuple.Create(ListScpipts.XXX,ListScpipts.YYY);
		}
		public bool Create ()
		{
			if (ListScpipts.Scripts.Count > 0)
				return true;
			else
				return false;
		}
		
    }
}
