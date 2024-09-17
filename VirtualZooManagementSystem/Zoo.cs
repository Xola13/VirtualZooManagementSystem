using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using VirtualZooManagementSystem;

namespace VirtualZooManagementSystem
{
	public class Zoo
	{
		private List<Animal> animals = new List<Animal>();
		private List<IMoveble> movebleAnimals = new List<IMoveble>();
		private List<ISpeakable> speakableAnimals = new List<ISpeakable>();
		private List<IFeedable> feedableAnimals = new List<IFeedable>();
		private const string FilePath = "animals.json";


		// Function of adding animals to the System

		public void AddAnimal()
		{
			Console.WriteLine("Select the type of animal to add: ");
			Console.WriteLine("1. Lion");
			Console.WriteLine("2. Parrot");
			Console.WriteLine("3. Turtle");
			Console.WriteLine("Enter the number corresponding to the animal type: ");

			if (!int.TryParse(Console.ReadLine(), out int choice))
			{
				Console.WriteLine("Invalid input. Please enter a correct number");
				return;
			}

			Animal animal = null;

			switch (choice)
			{
				case 1:
					animal = AddAnimalFromUserInput<Lion>("Lion");
					break;
				case 2:
					animal = AddAnimalFromUserInput<Parrot>("Parrot");
					break;
				case 3:
					animal = AddAnimalFromUserInput<Turtle>("Turtle");
					break;
				default:
					Console.WriteLine("Invalid choice. Please select correct choice..");
					break;
			}

			if (animal != null)
			{
				animals.Add(animal);
				Console.WriteLine($"Added {animal.GetType().Name} named {animal.Name} to the zoo.");
				

				// Add to specific lists based on interfaces
				if (animal is IMoveble moveble)
				{
					movebleAnimals.Add(moveble);
				}

				if (animal is ISpeakable speakable)
				{
					speakableAnimals.Add(speakable);
				}

				if (animal is IFeedable feedable)
				{
					feedableAnimals.Add(feedable);
				}
			}

		}

		// Addition of animals from the user input function

		private T AddAnimalFromUserInput<T>(string species) where T : Animal
		{
			Console.Write($"Enter the name of the {species}: ");
			string name = Console.ReadLine();
			Console.Write($"Enter the age of the {species}: ");

			if (!int.TryParse(Console.ReadLine(), out int age))
			{
				Console.WriteLine("Invalid age please enter a valid number");
				return null;
			}

			T animal = (T)Activator.CreateInstance(typeof(T), name, age);
			return animal;
			
		}

		// Function of adding Lion to the system

		public void AddLion()
		{
			AddAnimalFromUserInput<Lion>("Lion");
		}

		// Function of adding Parrot to the System

		public void AddParrot()
		{
			AddAnimalFromUserInput<Parrot>("Parrot");

		}

		// Function of adding Turtle to the System

		public void AddTurtle()
		{

			AddAnimalFromUserInput<Turtle>("Turtle");

		}

		// Function to get all the existing animals in the system

		public void ShowAllAnimals()
		{
			Console.WriteLine("List of all animals:");
			if (animals.Count == 0)
			{
				Console.WriteLine("No animals found.");
			}
			else
			{

				foreach (var animal in animals)
				{
					Console.WriteLine(AnimalFormatter.Format(animal));
				}
			}
			Console.WriteLine("Press any key to return to the menu..");
			Console.ReadKey();
		}

		// Function to Feed all the animals in the system

		public void FeedAllAnimals()
		{
			foreach (var animal in feedableAnimals)
			{
				animal.Feed();
			}
			Console.WriteLine("Press any key to return to the menu..");
			Console.ReadKey();
		}

		// Function to Feed all the animals with the specific type of food.

		public void FeedAllAnimalsWithFood(string foodType)
		{
			foreach (var animal in feedableAnimals)
			{
				try
				{
					animal.Feed(foodType);
				}
				catch 
				{
					Console.WriteLine($"Error feeding {animal.GetType().Name}: {ex.Message}");
				}

			}
			Console.WriteLine("Feeding attempt complete. Check for any errors above.");
			Console.WriteLine("Press any key to return to the menu..");
			Console.ReadKey();
		}

		// Function to move all the animals

		public void MoveAllAnimals()
		{
			foreach (var animal in movebleAnimals)
			{
				animal.Move();
			}
			Console.WriteLine("Press any key to return to the menu");
			Console.ReadKey();
		}

		// Function too make all animals speak

		public void SpeakAnimals()
		{
			foreach (var animal in speakableAnimals)
			{
				animal.Speak();
			}
			Console.WriteLine("Press any key to return to the menu");
			Console.ReadKey();
		}

		// Function to save the animals

		private void SaveAnimals()
		{
			try
			{
				var options = new JsonSerializerOptions
				{
					WriteIndented = true,
					Converters = { new AnimalJsonConverter() }
				};
				string jsonString = JsonSerializer.Serialize(animals, options);
				File.WriteAllText(FilePath, jsonString);
				Console.WriteLine("Saved json: " + jsonString);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving animals: {ex.Message}");
			}
		}


		// Function of loading animals

		public void LoadAnimals()
		{

			if (File.Exists(FilePath))
			{
				try
				{
					string jsonString = File.ReadAllText(FilePath);
					Console.WriteLine("Loaded Json: " + jsonString);


					var options = new JsonSerializerOptions
					{
						Converters = { new AnimalJsonConverter() }
					};

					animals = JsonSerializer.Deserialize<List<Animal>>(jsonString, options) ?? new List<Animal>();

					Console.WriteLine("Loaded animals");
					foreach (var animal in animals)
					{
						Console.WriteLine(animal.ToString());
					}

					movebleAnimals = animals.OfType<IMoveble>().ToList();
					speakableAnimals = animals.OfType<ISpeakable>().ToList();
					feedableAnimals = animals.OfType<IFeedable>().ToList();

				}
				catch (Exception ex)
				{
					Console.WriteLine("Json exception: " + ex.Message);
				}

			}
			else
			{
				Console.WriteLine("No saved animal data found.");
			}


		}
	}


	public static class AnimalFormatter
	{
		public static string Format(Animal animal)
		{
			return $"{animal.GetType().Name} - Name: {animal.Name}, Age: {animal.Age}";
		}
	}



	public class AnimalJsonConverter : JsonConverter<Animal>
	{
		public override Animal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
			{
				JsonElement root = doc.RootElement;

				Console.WriteLine("Deserializing: " + root.ToString());

				if (!root.TryGetProperty("Species", out JsonElement speciesProperty))
				{
					throw new JsonException("Missing required property 'Species'.");
				}

				string species = speciesProperty.GetString();

				if (!root.TryGetProperty("Name", out JsonElement nameProperty))
				{
					throw new JsonException("Missing required property 'Name'.");
				}

				string name = nameProperty.GetString();

				if (!root.TryGetProperty("Age", out JsonElement ageProperty))
				{
					throw new JsonException("Missing required property 'Age'.");
				}

				int age = ageProperty.GetInt32();

				return species switch
				{
					"Lion" => new Lion(name, age),
					"Parrot" => new Parrot(name, age),
					"Turtle" => new Turtle(name, age),
					_ => throw new NotSupportedException($"Type {species} is not supported."),
				};
			}
		}

		public override void Write(Utf8JsonWriter writer, Animal value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WriteString("Species", value.GetType().Name);
			writer.WriteString("Name", value.Name);
			writer.WriteNumber("Age", value.Age);
			writer.WriteEndObject();
		}
	} 

}

