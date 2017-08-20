using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSR
{
    class Room
    {
		private double priceWithDiscount;
		private double priceRoom;
		private string typeOfDiscount;
		public const double percentDiscount = 0.2;
		public const double fixDiscount = 15.0;
		private string availabilityMark = "available"; //Default value

		public int NumberOfRoom { get; set; }

		public string TypeOfRoom { get; set; }

		public int NumberOfBeds { get; set; }

		public int NumberOfPersons { get; set; }

		public string TypeOfDiscount
		{
			get
			{
				return typeOfDiscount;
			}
			set
			{
				if(value.ToLower() == "fix" || value.ToLower() == "percent")
				{
					typeOfDiscount = value;
				}
				else
				{
					throw new Exception("Type text fix or percent!");
				}
			}
		}

		public string AvailabilityMark
		{
			get
			{
				return availabilityMark;
			}
			set
			{
				if(value.ToLower() == "available" || value.ToLower() == "nonavailable")
				{
					availabilityMark = value;
				}
				else
				{
					throw new Exception("Type text available or nonavailable!");
				}
			}
		}

		public double PriceRoom
		{
			get
			{
				return priceRoom;
			}
			set
			{
				if(value <= 0)
				{
					throw new Exception("The price of room can not be zero or negative!");
				}
				else
				{
					priceRoom = value;
				}
			}
		}

		public double PriceWithDiscount
		{
			get
			{
				return priceWithDiscount;
			}
			set
			{
				if(value >= 100)
				{
					throw new Exception("The price of room including discount more than 100 dollars!");
				}
				else if(value <= 0)
				{
					throw new Exception("The price of room can not be zero or negative!");
				}
				else
				{
					priceWithDiscount = value;
				}
			}
		}

		public Room() { }

		public Room(int numderOfRoom)
		{
			this.NumberOfRoom = numderOfRoom;
			Console.Write("Type of room: ");
			this.TypeOfRoom = Console.ReadLine();
			Console.Write("Number of persons: ");
			this.NumberOfPersons = int.Parse(Console.ReadLine());
			Console.Write("Number of beds: ");
			this.NumberOfBeds = int.Parse(Console.ReadLine());
			Console.Write("Price of room: ");
			this.PriceRoom = double.Parse(Console.ReadLine());
			Console.Write("Type of discount (fix or percent): ");
			this.TypeOfDiscount = Console.ReadLine();
			switch(this.TypeOfDiscount)
			{
				case "percent":
					this.PriceWithDiscount = this.PriceRoom - this.PriceRoom * percentDiscount;
					break;
				case "fix":
					this.PriceWithDiscount = this.PriceRoom - fixDiscount;
					break;
			}
		}

		public Room(int numberOfRoom, string typeOfRoom, int numberOfPersons, int numberOfBeds, double price, string typeOfDiscount)
		{
			this.NumberOfRoom = numberOfRoom;
			this.TypeOfRoom = typeOfRoom;
			this.NumberOfPersons = numberOfPersons;
			this.NumberOfBeds = numberOfBeds;
			this.PriceRoom = price;
			this.TypeOfDiscount = typeOfDiscount;
			switch(this.TypeOfDiscount)
			{
				case "percent":
					this.PriceWithDiscount = this.PriceRoom - this.PriceRoom * percentDiscount;
					break;
				case "fix":
					this.PriceWithDiscount = this.PriceRoom - fixDiscount;
					break;
			}
		}

		public void Write(string path)
		{
			using(FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
			{
				using(BinaryWriter bw = new BinaryWriter(fs))
				{
					bw.Write(this.NumberOfRoom);
					bw.Write(this.TypeOfRoom);
					bw.Write(this.NumberOfPersons);
					bw.Write(this.NumberOfBeds);
					bw.Write(this.PriceRoom);
					bw.Write(this.TypeOfDiscount);
					bw.Write(this.AvailabilityMark);
				}
			}
		}

		public void Change()
		{
			Console.WriteLine("Leave empty field if you do not want to change information.");
			Console.WriteLine("Type of Room");
			Console.Write("Old value: {0}	New value: ", this.TypeOfRoom);
			string inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				this.TypeOfRoom = inputOfUser;
			}
			Console.WriteLine("Number of persons");
			Console.Write("Old value: {0}	New value: ", this.NumberOfPersons);
			inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				int bufNumberOfPersons = int.Parse(inputOfUser);
				this.NumberOfPersons = bufNumberOfPersons;
			}
			Console.WriteLine("Number of beds");
			Console.Write("Old value: {0}	New value: ", this.NumberOfBeds);
			inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				int bufNumberOfBeds = int.Parse(inputOfUser);
				this.NumberOfBeds = bufNumberOfBeds;
			}
			Console.WriteLine("Price of room");
			Console.Write("Old value: {0}	New value: ", this.PriceRoom);
			inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				double bufPriceOfRoom = double.Parse(inputOfUser);
				this.PriceRoom = bufPriceOfRoom;
			}
			Console.WriteLine("Type of discount");
			Console.Write("Old value: {0}	New value: ", this.TypeOfDiscount);
			inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				this.TypeOfDiscount = inputOfUser;
			}
			switch(this.TypeOfDiscount)
			{
				case "percent":
					this.PriceWithDiscount = this.PriceRoom - this.PriceRoom * percentDiscount;
					break;
				case "fix":
					this.PriceWithDiscount = this.PriceRoom - fixDiscount;
					break;
			}
		}

		public override string ToString()
		{
			return string.Format("|{0,16}|{1,20}|{2,20}|{3,16}|{4,15}|{5,15}|{6,15}|", NumberOfRoom, TypeOfRoom, NumberOfPersons,
								NumberOfBeds, PriceRoom, PriceWithDiscount, AvailabilityMark);
		}
	}
}