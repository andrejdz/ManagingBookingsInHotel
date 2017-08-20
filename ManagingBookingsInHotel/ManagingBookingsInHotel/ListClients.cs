using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSR
{
	class ListClients
	{
		public void RewriteFile(string path, SortedDictionary<int, Client> listOfClients)
		{
			using(FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using(BinaryWriter bw = new BinaryWriter(fs))
				{
					foreach(Client client in listOfClients.Values)
					{
						bw.Write(client.ClientId);
						bw.Write(client.LastName);
						bw.Write(client.FirstName);
					}
				}
			}
		}

		public void ReadFile(string path, ref SortedDictionary<int, Client> listOfClients)
		{
			using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using(BinaryReader br = new BinaryReader(fs))
				{
					while(br.PeekChar() != -1)
					{
						Client client = new Client(br.ReadInt32(), br.ReadString(), br.ReadString());
						listOfClients.Add(client.ClientId, client);
					}
				}
			}
		}

		public void ViewListOfClients(SortedDictionary<int, Client> listOfClients)
		{
			int length = 0;
			foreach(Client client in listOfClients.Values)
			{
				length = client.ToString().Length;
				break;
			}
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			Console.WriteLine("| Number of client |     Last name     |     First name    |");
			for(int i = 0; i < length; ++i)
			{
				Console.Write("-");
			}
			Console.WriteLine();
			foreach(Client client in listOfClients.Values)
			{
				Console.WriteLine(client.ToString());
				for(int i = 0; i < length; ++i)
				{
					Console.Write("-");
				}
				Console.WriteLine();
			}
		}
	}
}
