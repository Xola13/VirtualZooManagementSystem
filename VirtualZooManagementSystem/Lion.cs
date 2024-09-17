using System;
using System.Xml.Linq;
using VirtualZooManagementSystem;

namespace VirtualZooManagementSystem
{
	public class Lion : Animal, IFeedable, IMoveble, ISpeakable
	{
		public Lion(string name, int age) : base(name, age) { }

		public override void Feed()
		{
			Console.WriteLine($"Feeding Lion {Name} with meat");
		}

		public override void Feed(string foodType)
		{
			Console.WriteLine($"Feeding Lion {Name} with {foodType}");
		}


		public override void Speak()
		{
			Console.WriteLine($"{Name} roars");
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} prowls stealthliy");
		}
	}
}
