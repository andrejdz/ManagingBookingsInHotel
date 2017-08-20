using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSR
{
	class ListRooms
	{
		public void RewriteFile(string path, SortedDictionary<int, Room> listOfRooms)
		{
			using(FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using(BinaryWriter bw = new BinaryWriter(fs))
				{
					foreach(Room room in listOfRooms.Values)
					{
						bw.Write(room.NumberOfRoom);
						bw.Write(room.TypeOfRoom);
						bw.Write(room.NumberOfPersons);
						bw.Write(room.NumberOfBeds);
						bw.Write(room.PriceRoom);
						bw.Write(room.TypeOfDiscount);
						bw.Write(room.AvailabilityMark);
					}
				}
			}
		}

		public void ReadFile(string path, ref SortedDictionary<int, Room> listOfRooms)
		{
			using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using(BinaryReader br = new BinaryReader(fs))
				{
					while(br.PeekChar() != -1)
					{
						Room room = new Room(br.ReadInt32(), br.ReadString(), br.ReadInt32(), br.ReadInt32(), br.ReadDouble(), br.ReadString());
						room.AvailabilityMark = br.ReadString();
						listOfRooms.Add(room.NumberOfRoom, room);
					}
				}
			}
		}

		public void ReadFileTXT(string path, ref SortedDictionary<int, Room> listOfRooms)
		{
			using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using(StreamReader sr = new StreamReader(fs))
				{
					string bufReadLine = null;
					while((bufReadLine = sr.ReadLine()) != null)
					{
						string[] bufArrayStrings = bufReadLine.Split('|');
						Room room = new Room(int.Parse(bufArrayStrings[0]), bufArrayStrings[1], int.Parse(bufArrayStrings[2]),
												int.Parse(bufArrayStrings[3]), double.Parse(bufArrayStrings[4]), bufArrayStrings[5]);
						listOfRooms.Add(room.NumberOfRoom, room);
					}
				}
			}
		}

		public void ViewListOfRooms(SortedDictionary<int, Room> listOfRooms)
		{
			int length = 0;
			foreach(Room room in listOfRooms.Values)
			{
				length = room.ToString().Length;
				break;
			}
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			Console.WriteLine("| Number of room |    Type of room    | Number of persons  | Number of beds | Price of room | " +
								"With discount | State of room |");
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			foreach(Room room in listOfRooms.Values)
			{
				Console.WriteLine(room.ToString());
				for(int i = 0; i < length; ++i)
				{
					Console.Write("-");
				}
				Console.WriteLine();
			}
		}

		public void ViewAvailableRooms(SortedDictionary<int, Room> listOfRooms)
		{
			int length = 0;
			foreach(Room room in listOfRooms.Values)
			{
				length = room.ToString().Length;
				break;
			}
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			Console.WriteLine("| Number of room |    Type of room    | Number of persons  | Number of beds | Price of room | " +
								"With discount | State of room |");
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			foreach(Room room in listOfRooms.Values)
			{
				if(room.AvailabilityMark == "available")
				{
					Console.WriteLine(room.ToString());
					for(int i = 0; i < length; ++i)
					{
						Console.Write("-");
					}
					Console.WriteLine();
				}
			}
		}
	}
}
