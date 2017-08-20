using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSR
{
	class ListOrders
	{
		public void RewriteFile(string path, SortedDictionary<int, Order> listOfOrders)
		{
			using(FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using(BinaryWriter bw = new BinaryWriter(fs))
				{
					foreach(Order order in listOfOrders.Values)
					{
						bw.Write(order.client.ClientId);
						bw.Write(order.room.NumberOfRoom);
						bw.Write(order.NumbrerOfOrder);
						bw.Write(order.beginDate.ToShortDateString());
						bw.Write(order.endDate.ToShortDateString());
						bw.Write(order.PriceRoom);
						bw.Write(order.PriceWithDiscount);
					}
				}
			}
		}

		public void ReadFile(string path, ref SortedDictionary<int, Order> listOfOrders, SortedDictionary<int, Client> listOfClients, SortedDictionary<int, Room> listOfRooms)
		{
			using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using(BinaryReader br = new BinaryReader(fs))
				{
					while(br.PeekChar() != -1)
					{
						listOfClients.TryGetValue(br.ReadInt32(), out Client client); //Get value by specific key (true if contains)
						listOfRooms.TryGetValue(br.ReadInt32(), out Room room); //Get value by specific key (true if contains)
						Order order = new Order(br.ReadInt32(), client, room, br.ReadString(), br.ReadString());
						order.PriceRoom = br.ReadDouble();
						order.PriceWithDiscount = br.ReadDouble();
						listOfOrders.Add(order.NumbrerOfOrder, order);
					}
				}
			}
		}

		public void ViewListOfOrders(SortedDictionary<int, Order> listOfOrders)
		{
			int length = 0;
			foreach(Order order in listOfOrders.Values)
			{
				length = order.ToString().Length;
				break;
			}
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			Console.WriteLine("| Number of order |   Last name   |  First name   | Number of room | Type of room  |  Price room   | With discount |" +
				"  Check-in  | Check-out  | Amount of days |");
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			foreach(Order order in listOfOrders.Values)
			{
				Console.WriteLine(order.ToString());
				for(int i = 0; i < length; ++i)
				{
					Console.Write("-");
				}
				Console.WriteLine();
			}
		}

		public void ViewOrdersOfClient(int clientID, SortedDictionary<int, Order> listOfOrders)
		{
			int length = 0;
			foreach(Order order in listOfOrders.Values)
			{
				length = order.ToString().Length;
				break;
			}
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			Console.WriteLine("| Number of order |   Last name   |  First name   | Number of room | Type of room  |  Price room   | With discount |" +
				"  Check-in  | Check-out  | Amount of days |");
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			double sumPrices = 0;
			int numberOrders = 0;
			foreach(Order order in listOfOrders.Values)
			{
				if(order.client.ClientId == clientID)
				{
					sumPrices += order.room.PriceWithDiscount;
					++numberOrders;
					Console.WriteLine(order.ToString());
					for(int i = 0; i < length; ++i)
					{
						Console.Write("-");
					}
					Console.WriteLine();
				}
			}
			Console.WriteLine("\nAverage price with discount: {0}", sumPrices / numberOrders);
		}
	}
}
