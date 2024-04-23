using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using ClientXie;

Client myClient = new Client();
List<Client> listOfClient = new List<Client>();

LoadFileValuesToMemory(listOfClient);

bool loopAgain = true;
while (loopAgain){

	try
	{
		DisplayMainMenu();
		string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
		if (mainMenuChoice == "N")
			myClient = NewClient();
		if (mainMenuChoice == "S")
			ShowClientInfo(myClient);
		if (mainMenuChoice == "F")
			myClient = FindClientInList(listOfClient);
		if (mainMenuChoice == "R")
			RemoveClientFromList(myClient, listOfClient);
		if (mainMenuChoice == "L")
			DisplayAllClientInList(listOfClient);
		if (mainMenuChoice == "Q")
		{
			SaveMemoryValuesToFile(listOfClient);
			loopAgain = false;
			throw new Exception("Bye, hope to see you again.");
		}
		if (mainMenuChoice == "E")
		{
			while (true)
			{
				DisplayEditMenu();
				string editMenuChoice = Prompt("\nEnter a Edit Menu Choice: ").ToUpper();
				if (editMenuChoice == "F")
					getFirstName(myClient);
				if (editMenuChoice == "L")
					getLastName(myClient);
				if (editMenuChoice == "H")
					getHeight(myClient);
				if (editMenuChoice == "W")
					getWeight(myClient);
				if (editMenuChoice == "R")
					throw new Exception("Returning to Main Menu");
			}
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine($"{ex.Message}");
	}
}

void DisplayMainMenu(){

    Console.WriteLine("/---------------------------------/");
	Console.WriteLine("         Client Stuff App          ");
    Console.WriteLine("/---------------------------------/");
	Console.WriteLine("\nMenu Options");
	Console.WriteLine("============");
	Console.WriteLine("\n[L]ist all Clients");
	Console.WriteLine("[F]ind Client");
	Console.WriteLine("[N]ew Client");
	Console.WriteLine("[E]dit Client");
	Console.WriteLine("[R]emove Client");
	Console.WriteLine("[S]how Client BMI Info");
    Console.WriteLine("[Q]uit");
}

void DisplayEditMenu(){

	Console.WriteLine("Edit Menu");
    Console.WriteLine("=========");
	Console.WriteLine("\n[F]irst name");
	Console.WriteLine("[L]ast name");
	Console.WriteLine("[H]eight");
	Console.WriteLine("[W]eight");
	Console.WriteLine("[R]eturn to Main Menu");
}

void LoadFileValuesToMemory(List<Client> listOfClient){

	while(true){
		try
		{

			string fileName = "DataIn.csv";
			string filePath = $"./data/{fileName}";
			if (!File.Exists(filePath))
				throw new Exception($"The file {fileName} does not exist.");
			string[] csvFileInput = File.ReadAllLines(filePath);
			for(int i = 0; i < csvFileInput.Length; i++)
			{

				string[] items = csvFileInput[i].Split(',');

				Client myClient = new Client(items[0], items[1], double.Parse(items[2]), double.Parse(items[3]));
				listOfClient.Add(myClient);

			}
			Console.WriteLine($"Load complete. {fileName} has {listOfClient.Count} data entries");
			break;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Message}");
		}
	}
}

void SaveMemoryValuesToFile(List<Client> listOfClient){

	string fileName = "Data.csv";
	string filePath = $"./data/{fileName}";
	string[] csvLines = new string[listOfClient.Count];
	for (int i = 0; i < listOfClient.Count; i++)
	{
		csvLines[i] = listOfClient[i].ToString();
	}
	File.WriteAllLines(filePath, csvLines);
	Console.WriteLine($"Save complete. {fileName} has {listOfClient.Count} entries.");
}

string Prompt(string prompt){
	string myString = "";
	while (true)
	{
		try
		{
		Console.Write(prompt);
		myString = Console.ReadLine().Trim();
		if(string.IsNullOrEmpty(myString))
			throw new Exception($"Empty Input: Please enter something.");
		break;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	return myString;
}

Client NewClient(){

    Client myClient = new Client();
    getFirstName(myClient);
    getLastName(myClient);
    getWeight(myClient);
    getHeight(myClient);
    AddClientToList(myClient, listOfClient);
    return myClient;
}

void getFirstName(Client client){
    
    string myString = Prompt($"Enter First Name: ");
	client.FirstName = myString;

}

void getLastName(Client client){
    
    string myString = Prompt($"Enter Last Name: ");
	client.LastName = myString;

}

void getWeight(Client client){

    double mydouble = double.Parse(Prompt($"Enter Weight: "));
	client.Weight = mydouble;

}

void getHeight(Client client){
    
    double mydouble = double.Parse(Prompt($"Enter Height: "));
	client.Height = mydouble;

}

void ShowClientInfo(Client client){
	if(client == null)
		throw new Exception($"No Client In Memory");
	Console.WriteLine($"\n{client.fullName}");
	Console.WriteLine($"BMI Score: {client.bmiScore,8:n2}");
	Console.WriteLine($"BMI Status: {client.bmiStatus,7:n2}");
}

void AddClientToList(Client myClient, List<Client> listOfClient){

	if(myClient == null)
		throw new Exception($"No Client provided to add to list");
	listOfClient.Add(myClient);
	Console.WriteLine($"Client Added");
}

Client FindClientInList(List<Client> listOfClient){

	string myString = Prompt($"Enter Partial Client's Last Name: ");
	foreach(Client client in listOfClient)
		if(client.LastName.ToLower().Contains(myString.ToLower())){
            System.Console.WriteLine("Client found");
            return client;
        } 
	Console.WriteLine($"No Client in Match");
	return null;
}

void RemoveClientFromList(Client myClient, List<Client> listOfClient){

	if(myClient == null)
		throw new Exception($"No Client provided to remove from list");
	listOfClient.Remove(myClient);
	Console.WriteLine($"Client Removed");
}

void DisplayAllClientInList(List<Client> listOfClient){

	foreach(Client client in listOfClient)
		ShowClientInfo(client);
}