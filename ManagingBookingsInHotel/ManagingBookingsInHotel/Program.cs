using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSR
{
    class Program
    {
		static void Main()
		{
			try
			{
				string pathInitialRooms = @"..\..\Repo\Rooms.txt";
				string pathRooms = @"..\..\Repo\RoomsOfHotel.dat";
				string pathClients = @"..\..\Repo\Clients.dat";
				string pathOrders = @"..\..\Repo\OrdersOfClients.dat";
				ListRooms listOfRooms = new ListRooms();
				ListClients listOfClients = new ListClients();
				ListOrders listOfOrders = new ListOrders();
				SortedDictionary<int, Room> roomsList = new SortedDictionary<int, Room>();
				SortedDictionary<int, Client> clientsList = new SortedDictionary<int, Client>();
				SortedDictionary<int, Order> ordersList = new SortedDictionary<int, Order>();
				#region Read information from file, create instances of class Room and write to binary file (reduce time to type data about rooms)
				FileInfo fi = new FileInfo(pathRooms);
				if(!fi.Exists) //If file doesn not exist read data of rooms from text file
				{
					listOfRooms.ReadFileTXT(pathInitialRooms, ref roomsList);
					listOfRooms.RewriteFile(pathRooms, roomsList);
				}
				else //If file exists read data of rooms from binary file
				{
					listOfRooms.ReadFile(pathRooms, ref roomsList);
				}
				#endregion
				#region Read information from file, create instance of class Client and place to list of clients
				fi = new FileInfo(pathClients);
				if(fi.Exists)
				{
					listOfClients.ReadFile(pathClients, ref clientsList);
				}
				#endregion
				#region Read information from file, create instance of class Order and place to list of orders
				fi = new FileInfo(pathOrders);
				if(fi.Exists)
				{
					listOfOrders.ReadFile(pathOrders, ref ordersList, clientsList, roomsList);
				}
				#endregion
				while(true)
				{
					Console.Clear();
					Console.WriteLine("1. Working with list of rooms, type 1.");
					Console.WriteLine("2. View all rooms of hotel, type 2.");
					Console.WriteLine("3. Working with list of clients, type 3.");
					Console.WriteLine("4. View all clients of hotel, type 4.");
					Console.WriteLine("5. Working with bookings, type 5");
					Console.WriteLine("6. View all orders, type 6.");
					Console.WriteLine("7. View orders of specific client, type 7.");
					Console.WriteLine("To exit, leave empty field.");
					string inputOfUser = Console.ReadLine();
					if(inputOfUser == "")
					{
						break;
					}
					int caseSwitch = int.Parse(inputOfUser);
					switch(caseSwitch)
					{
						case 1:
							while(true)
							{
								Console.Clear();
								Console.WriteLine("1. Enter information about rooms, type 1.");
								Console.WriteLine("2. Change information about specific room, type 2.");
								Console.WriteLine("3. Delete information about specific room, type 3.");
								Console.WriteLine("To return previous menu, leave empty field.");
								inputOfUser = Console.ReadLine();
								if(inputOfUser == "")
								{
									break;
								}
								caseSwitch = int.Parse(inputOfUser);
								switch(caseSwitch)
								{
									case 1:
										string exFlag = "";
										while(exFlag.ToLower() != "no")
										{
											int bufNumberOfRoom = 0;
											for(int i = 1; ; ++i) //Looking for available number
											{
												if(!roomsList.ContainsKey(i))
												{
													bufNumberOfRoom = i;
													break;
												}
											}
											Room bufRoom = new Room(bufNumberOfRoom);
											bufRoom.Write(pathRooms);
											roomsList.Add(bufRoom.NumberOfRoom, bufRoom); //Add instance of class Room to list
											Console.Write("Do you want to continue? Yes or No: ");
											exFlag = Console.ReadLine();
										}
										break;
									case 2:
										while(true)
										{
											Console.Write("Type number of room information about you want to change or leave empty to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufNumberOfRoom = int.Parse(inputOfUser);
											if(roomsList.TryGetValue(bufNumberOfRoom, out Room outRoom)) //Get value by specific key (true if contains)
											{
												roomsList.Remove(outRoom.NumberOfRoom);
												outRoom.Change();
											}
											else
											{
												Console.WriteLine("Room with number {0} does not exist.", bufNumberOfRoom);
												break;
											}
											roomsList.Add(outRoom.NumberOfRoom, outRoom);
										}
										listOfRooms.RewriteFile(pathRooms, roomsList); //Rewrite data about rooms to file
										break;
									case 3:
										while(true)
										{
											Console.Write("Type number of room that you want to delete or leave empty field to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufNumberOfRoom = int.Parse(inputOfUser);
											if(roomsList.ContainsKey(bufNumberOfRoom)) //Check if specific key exists
											{
												roomsList.Remove(bufNumberOfRoom);
												Console.WriteLine("Deleted successfully!");
											}
											else
											{
												Console.WriteLine("Room with number {0} does not exist.", bufNumberOfRoom);
												break;
											}
										}
										listOfRooms.RewriteFile(pathRooms, roomsList); //Rewrite data about rooms to file
										break;
									default:
										Console.WriteLine("Type number of list of menu.");
										Console.Write("\nPress any button to continue...");
										Console.ReadKey();
										break;
								}
							}
							break;
						case 2:
							listOfRooms.ViewListOfRooms(roomsList);
							Console.Write("\nPress any button to continue...");
							Console.ReadKey();
							break;
						case 3:
							while(true)
							{
								Console.Clear();
								Console.WriteLine("1. Enter information about clients, type 1.");
								Console.WriteLine("2. Change information about specific client, type 2.");
								Console.WriteLine("3. Delete information about specific client, type 3.");
								Console.WriteLine("To return previous menu, leave empty field.");
								inputOfUser = Console.ReadLine();
								if(inputOfUser == "")
								{
									break;
								}
								caseSwitch = int.Parse(inputOfUser);
								switch(caseSwitch)
								{
									case 1:
										string exFlag = "";
										while(exFlag.ToLower() != "no")
										{
											int bufClientID = 0;
											for(int i = 1; ; ++i) //Looking for available number
											{
												if(!clientsList.ContainsKey(i))
												{
													bufClientID = i;
													break;
												}
											}
											Client bufClient = new Client(bufClientID);
											bufClient.Write(pathClients);
											clientsList.Add(bufClient.ClientId, bufClient); //Add instance of class Client to list
											Console.Write("Do you want to continue? Yes or No: ");
											exFlag = Console.ReadLine();
										}
										break;
									case 2:
										while(true)
										{
											Console.Write("Type number of client information about you want to change or leave empty field to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufClientID = int.Parse(inputOfUser);
											if(clientsList.TryGetValue(bufClientID, out Client outClient)) //Get value by specific key (true if contains)
											{
												clientsList.Remove(outClient.ClientId);
												outClient.Change();
											}
											else
											{
												Console.WriteLine("Client with number {0} does not exist.", bufClientID);
												break;
											}
											clientsList.Add(outClient.ClientId, outClient);
										}
										listOfClients.RewriteFile(pathClients, clientsList); //Rewrite data about clients to file
										break;
									case 3:
										while(true)
										{
											Console.Write("Type number of client that you want to delete or leave empty field to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufClientID = int.Parse(inputOfUser);
											if(clientsList.ContainsKey(bufClientID)) //Check if specific key exists
											{
												clientsList.Remove(bufClientID);
												Console.WriteLine("Deleted successfully!");
											}
											else
											{
												Console.WriteLine("Client with number {0} does not exist.", bufClientID);
												break;
											}
										}
										listOfClients.RewriteFile(pathClients, clientsList); //Rewrite data about clients to file
										break;
									default:
										Console.WriteLine("Type number of list of menu.");
										Console.Write("\nPress any button to continue...");
										Console.ReadKey();
										break;
								}
							}
							break;
						case 4:
							listOfClients.ViewListOfClients(clientsList);
							Console.Write("\nPress any button to continue...");
							Console.ReadKey();
							break;
						case 5:
							while(true)
							{
								Console.Clear();
								Console.WriteLine("1. Book room, type 1.");
								Console.WriteLine("2. Change check-in and check-out dates, type 2.");
								Console.WriteLine("3. Delete booking, type 3.");
								Console.WriteLine("4. Increase or decrease price of room, type 4.");
								Console.WriteLine("To return previous menu, leave empty field.");
								inputOfUser = Console.ReadLine();
								if(inputOfUser == "")
								{
									break;
								}
								caseSwitch = int.Parse(inputOfUser);
								switch(caseSwitch)
								{
									case 1:
										while(true)
										{
											listOfClients.ViewListOfClients(clientsList);
											Console.Write("Type number of client who want to book room or leave empty field to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufClientID = int.Parse(inputOfUser);
											if(clientsList.TryGetValue(bufClientID, out Client outClient)) //Get value by specific key (true if contains)
											{
												;
											}
											else
											{
												Console.WriteLine("Client with number {0} does not exist.", bufClientID);
												Console.Write("\nPress any button to continue...");
												Console.ReadKey();
												break;
											}
											Console.Clear();
											listOfRooms.ViewAvailableRooms(roomsList);
											Console.Write("Type number of room for booking or leave empty to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufNumberOfRoom = int.Parse(inputOfUser);
											if(roomsList.TryGetValue(bufNumberOfRoom, out Room outRoom)) //Get value by specific key (true if contains)
											{
												roomsList.Remove(outRoom.NumberOfRoom);
												outRoom.AvailabilityMark = "nonavailable";
												roomsList.Add(outRoom.NumberOfRoom, outRoom);
											}
											else
											{
												Console.WriteLine("Room with number {0} does not exist.", bufNumberOfRoom);
												Console.Write("\nPress any button to continue...");
												Console.ReadKey();
												break;
											}
											Console.Write("Type check-in date: ");
											string beginDate = Console.ReadLine();
											Console.Write("Type check-out date: ");
											string endDate = Console.ReadLine();
											int bufNumberOfOrder = 0;
											for(int i = 1; ; ++i) //Looking for available number
											{
												if(!ordersList.ContainsKey(i))
												{
													bufNumberOfOrder = i;
													break;
												}
											}
											Order bufOrder = new Order(bufNumberOfOrder, outClient, outRoom, beginDate, endDate);
											ordersList.Add(bufOrder.NumbrerOfOrder, bufOrder);
											Console.Clear();
										}
										listOfRooms.RewriteFile(pathRooms, roomsList);
										listOfOrders.RewriteFile(pathOrders, ordersList);
										break;
									case 2:
										while(true)
										{
											Console.Write("Type number of order that dates you want to change or leave empty field to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufNumberOfOrder = int.Parse(inputOfUser);
											if(ordersList.TryGetValue(bufNumberOfOrder, out Order outOrder))
											{
												ordersList.Remove(outOrder.NumbrerOfOrder);
												outOrder.ChangeDates();
												ordersList.Add(outOrder.NumbrerOfOrder, outOrder);
											}
										}
										listOfOrders.RewriteFile(pathOrders, ordersList);
										break;
									case 3:
										while(true)
										{
											Console.Write("Type number of order that you want to delete or leave empty field to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufNumberOfOrder = int.Parse(inputOfUser);
											if(ordersList.TryGetValue(bufNumberOfOrder, out Order outOrder)) //Get value by specific key (true if contains)
											{
												ordersList.Remove(outOrder.NumbrerOfOrder);
												Console.WriteLine("Deleted successfully!");
											}
											else
											{
												Console.WriteLine("Order with number {0} does not exist.", bufNumberOfOrder);
												break;
											}
											if(roomsList.TryGetValue(outOrder.room.NumberOfRoom, out Room outRoom)) //Get value by specific key (true if contains)
											{
												roomsList.Remove(outRoom.NumberOfRoom);
												outRoom.AvailabilityMark = "available";
												roomsList.Add(outRoom.NumberOfRoom, outRoom);
											}
											else
											{
												Console.WriteLine("Room with number {0} does not exist.", outOrder.room.NumberOfRoom);
												break;
											}
										}
										listOfRooms.RewriteFile(pathRooms, roomsList);
										listOfOrders.RewriteFile(pathOrders, ordersList);
										break;
									case 4:
										while(true)
										{
											Console.Write("Type number of order that price you want to change or leave empty field to exit: ");
											inputOfUser = Console.ReadLine();
											if(inputOfUser == "")
											{
												break;
											}
											int bufNumberOfOrder = int.Parse(inputOfUser);
											if(ordersList.TryGetValue(bufNumberOfOrder, out Order outOrder))
											{
												Console.Write("Type amount on how you want to change the price: ");
												inputOfUser = Console.ReadLine();
												if(inputOfUser != "")
												{
													double amount = double.Parse(inputOfUser);
													ordersList.Remove(outOrder.NumbrerOfOrder);
													if(amount >= 0)
													{
														outOrder += amount; //Use overrided operator '+'
													}
													else
													{
														outOrder -= Math.Abs(amount); //Use overrided operator '-'
													}
													ordersList.Add(outOrder.NumbrerOfOrder, outOrder);
													Console.WriteLine("Price of room was succesfully changed!");
												}
											}
										}
										listOfOrders.RewriteFile(pathOrders, ordersList);
										break;
									default:
										Console.WriteLine("Type number of list of menu!");
										Console.Write("\nPress any button to continue...");
										Console.ReadKey();
										break;
								}
							}
							break;
						case 6:
							listOfOrders.ViewListOfOrders(ordersList);
							Console.Write("\nPress any button to continue...");
							Console.ReadKey();
							break;
						case 7:
							while(true)
							{
								Console.Write("Type number of client whose orders you want to view or leave empty field to exit: ");
								inputOfUser = Console.ReadLine();
								if(inputOfUser == "")
								{
									break;
								}
								int bufClientID = int.Parse(inputOfUser);
								if(clientsList.ContainsKey(bufClientID)) //Get value by specific key (true if contains)
								{
									listOfOrders.ViewOrdersOfClient(bufClientID, ordersList);
								}
								else
								{
									Console.WriteLine("Client with number {0} does not exist.", bufClientID);
								}
								Console.Write("\nPress any button to continue...");
								Console.ReadKey();
								Console.Clear();
							}
							break;
						default:
							Console.WriteLine("Type number of list of menu!");
							Console.Write("\nPress any button to continue...");
							Console.ReadKey();
							break;
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
				Console.Write("\nPress any button to continue...");
				Console.ReadKey();
				Main();
			}
		}
    }
}
