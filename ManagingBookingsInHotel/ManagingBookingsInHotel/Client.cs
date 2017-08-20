using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSR
{
	class Client
	{
		public int ClientId { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }

		public Client() { }

		public Client(int clientID)
		{
			this.ClientId = clientID;
			Console.Write("Type last name of client: ");
			this.LastName = Console.ReadLine();
			Console.Write("Type first name of client: ");
			this.FirstName = Console.ReadLine();
		}

		public Client(int clientId, string lastName, string firstName)
		{
			this.ClientId = clientId;
			this.LastName = lastName;
			this.FirstName = firstName;
		}

		public void Write(string path)
		{
			using(FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
			{
				using(BinaryWriter bw = new BinaryWriter(fs))
				{
					bw.Write(this.ClientId);
					bw.Write(this.LastName);
					bw.Write(this.FirstName);
				}
			}
		}

		public void Change()
		{
			Console.WriteLine("Leave empty field if you do not want to change information.");
			Console.WriteLine("Last name");
			Console.Write("Old value: {0}	New value: ", this.LastName);
			string inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				this.LastName = inputOfUser;
			}
			Console.WriteLine("First name");
			Console.Write("Old value: {0}	New value: ", this.FirstName);
			inputOfUser = Console.ReadLine();
			if(inputOfUser != "")
			{
				this.FirstName = inputOfUser;
			}
		}

		public override string ToString()
		{
			return string.Format("|{0,18}|{1,19}|{2,19}|", ClientId, LastName, FirstName);
		}
	}
}
