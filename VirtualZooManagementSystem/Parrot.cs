using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualZooManagementSystem;


namespace VirtualZooManagementSystem
{
	public class Parrot : Animal, IFeedable, IMoveble, ISpeakable
	{
		public Parrot(string name, int age) : base(name, age) { }

		public override void Feed()
		{
			Console.WriteLine($"Feeding parrot {Name} with seeds");
		}

		public override void Feed(string foodType)
		{
			Console.WriteLine($"Feeding parrot {Name} with {foodType}");
		}

		public override void Speak()
		{
			Console.WriteLine($"{Name} squawks !");
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} flies around.");
		}
	}
}
