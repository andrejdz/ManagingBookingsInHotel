using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR
{
	class Order
	{
		public Client client;
		public Room room;
		public DateTime beginDate;
		public DateTime endDate;
		private TimeSpan daysBooking;
		private double priceRoom;
		private double priceWithDiscount;
		private string typeOfDiscount;

		public int DaysBooking
		{
			get
			{
				return daysBooking.Days;
			}
		}

		public int NumbrerOfOrder { get; set; }

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

		public Order() { }

		public Order(int number, Client client, Room room, string beginDate, string endDate)
		{
			this.NumbrerOfOrder = number;
			this.client = client;
			this.room = room;
			if(!DateTime.TryParse(beginDate, out this.beginDate))
			{
				throw new Exception("Error! Type date in appropriate format: DD.MM.YYYY, YYYY.MM.DD, DD/MM/YYYY or YYYY/MM/DD!");
			}
			if(!DateTime.TryParse(endDate, out this.endDate))
			{
				throw new Exception("Error! Type date in appropriate format: DD.MM.YYYY, YYYY.MM.DD, DD/MM/YYYY or YYYY/MM/DD!");
			}
			daysBooking = this.endDate - this.beginDate;
			this.TypeOfDiscount = this.room.TypeOfDiscount;
			this.PriceRoom = this.room.PriceRoom;
			switch(this.TypeOfDiscount)
			{
				case "percent":
					this.PriceWithDiscount = this.PriceRoom - this.PriceRoom * Room.percentDiscount;
					break;
				case "fix":
					this.PriceWithDiscount = this.PriceRoom - Room.fixDiscount;
					break;
			}
		}

		public void ChangeDates()
		{
			Console.WriteLine("Leave empty field if you do not want to change information.");
			Console.WriteLine("Type check-in date");
			Console.Write("Old value: {0}	New value: ", beginDate.ToShortDateString());
			string inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				if(!DateTime.TryParse(inputOfUser, out beginDate))
				{
					throw new Exception("Error! Type date in appropriate format: DD.MM.YYYY, YYYY.MM.DD, DD/MM/YYYY or YYYY/MM/DD!");
				}
			}
			Console.WriteLine("Type check-out date");
			Console.Write("Old value: {0}	New value: ", endDate.ToShortDateString());
			inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				if(!DateTime.TryParse(inputOfUser, out endDate))
				{
					throw new Exception("Error! Type date in appropriate format: DD.MM.YYYY, YYYY.MM.DD, DD/MM/YYYY or YYYY/MM/DD!");
				}
			}
		}

		public static Order operator +(Order order, double amount)
		{
			order.PriceRoom = order.PriceRoom + amount;
			switch(order.TypeOfDiscount)
			{
				case "percent":
					order.PriceWithDiscount = order.PriceRoom - order.PriceRoom * Room.percentDiscount;
					break;
				case "fix":
					order.PriceWithDiscount = order.PriceRoom - Room.fixDiscount;
					break;
			}
			return order;
		}

		public static Order operator -(Order order, double amount)
		{
			order.PriceRoom = order.PriceRoom - amount;
			switch(order.TypeOfDiscount)
			{
				case "percent":
					order.PriceWithDiscount = order.PriceRoom - order.PriceRoom * Room.percentDiscount;
					break;
				case "fix":
					order.PriceWithDiscount = order.PriceRoom - Room.fixDiscount;
					break;
			}
			return order;
		}

		public override string ToString()
		{
			return string.Format("|{0,17}|{1,15}|{2,15}|{3,16}|{4,15}|{5,15}|{6,15}|{7,12}|{8,12}|{9,16}|", NumbrerOfOrder, client.LastName, client.FirstName, room.NumberOfRoom, room.TypeOfRoom,
									this.PriceRoom, this.PriceWithDiscount, beginDate.ToShortDateString(), endDate.ToShortDateString(), DaysBooking);
		}
	}
}
