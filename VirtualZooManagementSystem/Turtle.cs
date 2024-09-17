using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualZooManagementSystem;


namespace VirtualZooManagementSystem
{
	public class Turtle : Animal, IFeedable, IMoveble, ISpeakable
	{
		public Turtle(string name, int age) : base(name, age) { }

		public override void Feed()
		{
			Console.WriteLine($"Feeding Turtle {Name} with vegetables");
		}

		public override void Feed(string foodType)
		{
			Console.WriteLine($"Feeding Turtle {Name} with {foodType}");
		}

		public override void Speak()
		{
			Console.WriteLine($"{Name} grunting.");
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} is Crawling.");
		}
	}
}
