using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CakeCuttingProblem.Library
{
	public class DemoSituation
	{
		public static void Demo(
			Action<string> ColorsGreen,
			Action<string> ColorsBlue,
			Action<string> ColorsRed,
			Action<string> ColorsYellow
			)
		{
			var h = new Host("Mike", ColorsGreen)
			{
				Menu = new[] { "Bork", "Cereal", "Soup", "Salad" }
			};


			new Client("Jay", h, ColorsBlue)
			{
				PreferenceList = new[] { 55, 38, 14, 7 }
			}.PrintPreferenceList();

			new Client("Andy", h, ColorsRed)
			{
				PreferenceList = new[] { 50, 40, 16, 3 }
			}.PrintPreferenceList();

			new Client("Sally", h, ColorsYellow)
			{
				PreferenceList = new[] { 51, 41, 12, 5 }
			}.PrintPreferenceList();

			h.Decide();
		}
	}

	public class Host
	{
		public string[] Menu;
		public LinkedList<Client> Clients = new LinkedList<Client>();

		public Host(string Name, Action<string> Say)
		{
			this.Say = Text =>
			{
				Say(Name + ": " + Text);
			};

			this.Say("Welcome to my bub, my name is " + Name);
		}

		public readonly Action<string> Say;

		public void AddClient(Client klient)
		{
			this.Clients.Add(klient);
			this.Say("Hi " + klient.Name + ", what can I get you?");
		}

		public class Tuple1
		{
			public int Target;
			public int TotalCost;
			public int Difference;
			public int Compensation;

			public readonly LinkedList<int> MenuIndecies = new LinkedList<int>();
		}

		public class Tuple2
		{
			public int ClientIndex;
			public int Cost;
		}

		public void Decide()
		{
			this.Say("I am sorry. The available meals were fairly divided between you.");

			var Compensation = this.Menu.Length.ToArray(() => new Tuple2());
			var Results = this.Clients.Length.ToArray(() => new Tuple1());
			var Max = new int[this.Menu.Length];

			for (int i = 0; i < Max.Length; i++)
			{
				Max[i] = 0;
			}

			// share menu items between clients
			this.Clients.ForEach(
				(Client, ClientIndex) =>
				{
					Client.PreferenceList.ForEach(
						(v, k) =>
						{


							if (v > Max[k])
							{
								Max[k] = v;
								Compensation[k].ClientIndex = ClientIndex;
								Compensation[k].Cost = v;


							}

							Results[ClientIndex].Target += v;
						}
					);


					Results[ClientIndex].Target /= this.Clients.Length;
				}
			);


			// lets remember what meal clients get at what cost
			Compensation.ForEach(
				(value, key) =>
				{

					Results[value.ClientIndex].TotalCost += value.Cost;
					Results[value.ClientIndex].MenuIndecies.Add(key);
				}
			);

			var fond = 0;

			// lets remember if compensation is needed
			Results.ForEach(
				value =>
				{


					value.Difference = value.Target - value.TotalCost;


					fond -= value.Difference;
				}
			);

			// lets remember the compensation
			Results.ForEach(
				value =>
				{
					value.Compensation = (int)Math.Round((double)fond / this.Clients.Length + value.Difference);
				}
			);


			Results.ForEach(
				(value, key) =>
				{
					if (value.Compensation < 0)
					{
						this.Say(this.Clients[key].Name + " pays to the fond for others $" + -value.Compensation + "");
					}
					else
					{
						this.Say(this.Clients[key].Name + " recieves from the fond $" + value.Compensation + "");

					}

					value.MenuIndecies.ForEach(
						eine =>
						{
							this.Say(this.Clients[key].Name + " can eat " + this.Menu[eine]);
						}
					);


				}
			);


		}


	}

	public class Client
	{
		public int[] PreferenceList;

		public readonly string Name;

		public readonly Host Host;

		public Client(string Name, Host Host, Action<string> Say)
		{
			this.Name = Name;
			this.Host = Host;

			this.Say = Text =>
			{
				Say(Name + ": " + Text);
			};

			this.Say("Hey, my name is " + this.Name);

			Host.AddClient(this);
		}

		public readonly Action<string> Say;


		public void PrintPreferenceList()
		{
			this.PreferenceList.ForEach(
				(e, i) =>
				{
					this.Say("For " + this.Host.Menu[i] + " I would pay $" + e + "");
				}
			);

		}



	}

	public class LinkedList<T>
	{
		public T Value { get; set; }

		public LinkedList<T> Next { get; set; }

		public int Length
		{
			get
			{
				var p = this;
				var c = -1;

				while (p != null)
				{
					c++;
					p = p.Next;
				}
				return c;
			}
		}

		public T this[int i]
		{
			get
			{
				var p = this;
				var c = -1;

				while (p != null)
				{
					c++;
					p = p.Next;
					if (c == i)
						break;
				}
				if (p == null)
					return default(T);

				return p.Value;
			}
		}
	}


	public static class LinkedListExtensions
	{
		public static T[] ToArray<T>(this int Length, Func<T> Constructor)
		{
			var a = new T[Length];

			for (int i = 0; i < Length; i++)
			{

				a[i] = Constructor();
			}

			return a;
		}

		public static void Add<T>(this LinkedList<T> e, T value)
		{
			e.Next = new LinkedList<T>
			{
				Value = value,
				Next = e.Next
			};
		}

		public static void ForEach<T>(this LinkedList<T> e, Action<T> handler)
		{
			var p = e;

			while (p.Next != null)
			{
				p = p.Next;
				handler(p.Value);
			}
		}

		public static void ForEach<T>(this LinkedList<T> e, Action<T, int> handler)
		{
			var p = e;
			var c = -1;

			while (p.Next != null)
			{
				c++;
				p = p.Next;
				handler(p.Value, c);
			}
		}

		public static void ForEach<T>(this T[] e, Action<T> handler)
		{
			for (int i = 0; i < e.Length; i++)
			{
				handler(e[i]);
			}
		}

		public static void ForEach<T>(this T[] e, Action<T, int> handler)
		{
			for (int i = 0; i < e.Length; i++)
			{
				handler(e[i], i);
			}
		}
	}

}
