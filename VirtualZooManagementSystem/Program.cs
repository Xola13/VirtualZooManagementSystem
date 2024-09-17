
namespace VirtualZooManagementSystem
{

	public interface IFeedable
	{
		void Feed();
		void Feed(string foodType);
	}

	public interface IMoveble
	{
		void Move();
	}

	public interface ISpeakable
	{
		void Speak();
	}

	public class Program
	{
		private static Zoo zoo = new Zoo();

		public static void Main()
		{

			zoo.LoadAnimals();


			while (true)
			{
				Console.Clear();
				Console.WriteLine("Virtual Zoo Management System");
				Console.WriteLine("1. Add Animals");
				Console.WriteLine("2. Show all Animals");
				Console.WriteLine("3. Feed All Animals");
				Console.WriteLine("4. Feed All Animals with Specific Food");
				Console.WriteLine("5. Move All Animals");
				Console.WriteLine("6. Make All Animals Speak");
				Console.WriteLine("7. Exit.");
				Console.Write("Choose an option: ");

				string choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						zoo.AddAnimal();
						break;
					case "2":
						zoo.ShowAllAnimals();
						break;
					case "3":
						zoo.FeedAllAnimals();
						break;
					case "4":
						FeedAllAnimalsWithFood();
						break;
					case "5":
						zoo.MoveAllAnimals();
						break;
					case "6":
						zoo.SpeakAnimals();
						break;
					case "7":
						if (ConfirmExit())
							return;
						break;
					default:
						Console.WriteLine("Invalid Choice");
						break;
				}
			}
		}
		private static void FeedAllAnimalsWithFood()
		{
			Console.WriteLine("Enter the type of food: ");
			string foodType = Console.ReadLine();
			zoo.FeedAllAnimalsWithFood(foodType);
		}

		private static bool ConfirmExit()
		{
			Console.WriteLine("Are you sure you want to exit? (y/n): ");
			string confirmation = Console.ReadLine().ToLower();
			return confirmation == "y" || confirmation == "yes";
		}
	}
}