using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualZooManagementSystem;

namespace VirtualZooManagementSystem
{
	public abstract class Animal : IFeedable, IMoveble, ISpeakable
	{
		public string Name { get; set; }
		public int Age { get; set; }

		protected Animal(string name, int age)
		{
			Name = name;
			Age = age;
		}

		public abstract void Feed();
		public abstract void Feed(string foodType);
		public abstract void Move();
		public abstract void Speak();

		public override string ToString()
		{
			return $"{GetType().Name} - Name: {Name} , Age: {Age}";
		}
	}
}
