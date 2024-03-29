
// TODO: declare a constant to represent the max size of the values
// and dates arrays. The arrays must be large enough to store
// values for an entire month.
int physicalSize = 31;
int logicalSize = 0;

// TODO: create a double array named 'values', use the max size constant you declared
// above to specify the physical size of the array.
double[] values = new double[physicalSize];

// TODO: create a string array named 'dates', use the max size constant you declared
// above to specify the physical size of the array.
string[] dates = new string[physicalSize];

bool goAgain = true;
  while (goAgain)
  {
    try
    {
      DisplayMainMenu();
      string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
      if (mainMenuChoice == "L")
        logicalSize = LoadFileValuesToMemory(dates, values);
      if (mainMenuChoice == "S")
        SaveMemoryValuesToFile(dates, values, logicalSize);
      if (mainMenuChoice == "D")
        DisplayMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "A")
        logicalSize = AddMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "E")
        EditMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "Q")
      {
        goAgain = false;
        throw new Exception("Bye, hope to see you again.");
      }
      if (mainMenuChoice == "R")
      {
        while (true)
        {
          if (logicalSize == 0)
					  throw new Exception("No entries loaded. Please load a file into memory");
          DisplayAnalysisMenu();
          string analysisMenuChoice = Prompt("\nEnter an Analysis Menu Choice: ").ToUpper();
          if (analysisMenuChoice == "A")
            FindAverageOfValuesInMemory(values, logicalSize);
          if (analysisMenuChoice == "H")
            FindHighestValueInMemory(values, dates, logicalSize);
          if (analysisMenuChoice == "L")
            FindLowestValueInMemory(values, dates, logicalSize);
          if (analysisMenuChoice == "G")
            GraphValuesInMemory(dates, values, logicalSize);
          if (analysisMenuChoice == "R")
            throw new Exception("Returning to Main Menu");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }

void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("L) Load Values from File to Memory");
	Console.WriteLine("S) Save Values from Memory to File");
	Console.WriteLine("D) Display Values in Memory");
	Console.WriteLine("A) Add Value in Memory");
	Console.WriteLine("E) Edit Value in Memory");
	Console.WriteLine("R) Analysis Menu");
	Console.WriteLine("Q) Quit");
}

void DisplayAnalysisMenu()
{
	Console.WriteLine("\nAnalysis Menu");
	Console.WriteLine("A) Find Average of Values in Memory");
	Console.WriteLine("H) Find Highest Value in Memory");
	Console.WriteLine("L) Find Lowest Value in Memory");
	Console.WriteLine("G) Graph Values in Memory");
	Console.WriteLine("R) Return to Main Menu");
}

string Prompt(string prompt)
{
  string response = "";
  Console.Write(prompt);
  response = Console.ReadLine();
  return response;
}

string GetFileName()
{
	string fileName = "";
	do
	{
		fileName = Prompt("Enter file name including .csv or .txt: ");
	} while (string.IsNullOrWhiteSpace(fileName));
	return fileName;
}

int LoadFileValuesToMemory(string[] dates, double[] values)
{
	string fileName = GetFileName();
	int logicalSize = 0;
	string filePath = $"./data/{fileName}";
	if (!File.Exists(filePath))
		throw new Exception($"The file {fileName} does not exist.");
		string[] csvFileInput = File.ReadAllLines(filePath);
	for(int i = 0; i < csvFileInput.Length; i++)
	{
		Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
		string[] items = csvFileInput[i].Split(',');
		for(int j = 0; j < items.Length; j++)
		{
			Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
		}
		if(i != 0)
		{
			dates[logicalSize] = items[0];
			values[logicalSize] = double.Parse(items[1]);
			logicalSize++;
		}
	}
  Console.WriteLine($"Load complete. {fileName} has {logicalSize} data entries");
	return logicalSize;
}

void DisplayMemoryValues(string[] dates, double[] values, int logicalSize)
{
	if(logicalSize == 0)
		throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
	Console.WriteLine($"\nCurrent Loaded Entries: {logicalSize}");
	Console.WriteLine($"   Date     Value");
	for (int i = 0; i < logicalSize; i++)
		Console.WriteLine($"{dates[i]}   {values[i]}");
}

double PromptDoubleBetweenMinMax(string prompt, double min, double max){
	bool inValidInput = true;
	double num = 0;

	while (inValidInput){
		
		try{

			System.Console.Write($"{prompt} between {min:n2} and {max:n2}: ");
			num = double.Parse(Console.ReadLine());
			if(num < min || num > max)
				throw new Exception($"Invalid. Must be between {min} and {max}.");
			inValidInput = false;

		}
		catch(Exception ex){
			Console.WriteLine($"{ex.Message}");
		}
	}
	return num;
}

string PromptDate(string prompt){

	bool inValidInput = true;
	DateTime date = DateTime.Today;
	Console.WriteLine(date);
	while (inValidInput){
		
		try{
			Console.Write(prompt);
			date = DateTime.Parse(Console.ReadLine());
			Console.WriteLine(date);
			inValidInput = false;
		}
		catch (Exception ex){
			Console.WriteLine($"{ex.Message}");
		}
	}
	return date.ToString("MM-dd-yyyy");
}

double FindHighestValueInMemory(double[] values,string[] dates, int logicalSize){

	double HighestValue = 0;
	int dateIndex = 0;

	for (int i = 0; i < logicalSize;i++){
		if (values[i] > HighestValue){
			HighestValue = values[i];
			dateIndex = i;
		}
	}
	Console.WriteLine($"");
	Console.WriteLine($"The highest value in memory is {HighestValue} on the day of {dates[dateIndex]}.");
	return HighestValue;
	//TODO: Replace this code with yours to implement this function.
	
}

double FindLowestValueInMemory(double[] values,string[] dates, int logicalSize)
{
	double MinValue = values.Max();
	int dateIndex = 0;

	for (int i = 0; i < logicalSize;i++){
		if (values[i] < MinValue){
			MinValue = values[i];
			dateIndex = i;
		}
	}
	Console.WriteLine($"");
	Console.WriteLine($"The lowest value in memory is {MinValue} on the day of {dates[dateIndex]}.");
	return logicalSize;
	//TODO: Replace this code with yours to implement this function.
}

void FindAverageOfValuesInMemory(double[] values, int logicalSize)
{
	double Average = 0.00;
	double TotalValues = 0.00;
	for (int i = 0; i < logicalSize;i++){
		TotalValues += values[i];
	}
	Average = TotalValues/logicalSize ;
	Console.WriteLine($"");
	Console.WriteLine($"The average of values in memory is {Average:n2}.");
	//TODO: Replace this code with yours to implement this function.
}

void SaveMemoryValuesToFile(string[] dates, double[] values, int logicalSize)
{
	Console.WriteLine("Not Implemented Yet");
	//TODO: Replace this code with yours to implement this function.
}

int AddMemoryValues(string[] dates, double[] values, int logicalSize)
{
	Console.WriteLine("Not Implemented Yet");
	return logicalSize;
	//TODO: Replace this code with yours to implement this function.
}

void EditMemoryValues(string[] dates, double[] values, int logicalSize)
{
	Console.WriteLine("Not Implemented Yet");
	//TODO: Replace this code with yours to implement this function.
}

//this will used to round the highest value in the memory and be used for better incredments of the graph
double RoundToNextMagnitude(double number)
{
	// If the number is less than 10, round to the next integer
	if (number < 10)
		return Math.Ceiling(number);

	// Calculate the next magnitude
	int magnitude = (int)Math.Pow(10, (int)Math.Log10(number) + 1);

	return magnitude;
}

// take the dd out of the whole date
string GetDateBetween(string date){
        
	// Find the index of the first "-" character
	int firstDashIndex = date.IndexOf('-');
	
	// Find the index of the second "-" character starting from the position after the first one
	int secondDashIndex = date.IndexOf('-', firstDashIndex + 1);
	
	// Extract the substring between the two "-" characters
	string result = date.Substring(firstDashIndex + 1, secondDashIndex - firstDashIndex - 1);

	return result;
}

void GraphValuesInMemory(string[] dates, double[] values, int logicalSize)
{
	int NumbForIncrement = 20;
	double Increments = RoundToNextMagnitude(values.Max()) / NumbForIncrement;
	string[,] Graph = new string[NumbForIncrement + 2,logicalSize + 2];

	int BiggerNumber = 0; 
	if (logicalSize >= NumbForIncrement){
		BiggerNumber = logicalSize;
	}else{
		BiggerNumber = NumbForIncrement;
	}

	double GraphNumber = RoundToNextMagnitude(values.Max());
	//y axis labels
	for (int i = 0; i < NumbForIncrement; i++){
		Graph[i,0] = Convert.ToString(GraphNumber - (Increments * i));
	}
	//x axis labels
	for(int i = 0; i < logicalSize ; i++){
		Graph[NumbForIncrement +1, i + 1] = GetDateBetween(dates[i]);
	}
	//Putting values in correct column
	for (int i = 0; i < logicalSize; i++){
	for (int o = 0; o < NumbForIncrement; o++){
		//System.Console.WriteLine(Graph[o,0]);
		if(values[i] >= Convert.ToDouble(Graph[o,0]) && values[i] < Convert.ToDouble(Graph[o-1,0])){
		Graph[o,i+1] = Convert.ToString(values[i]);
		}
	}
	}
	
	Graph[NumbForIncrement,0] = "0";
	Graph[NumbForIncrement+1,0] = "Date";
	System.Console.WriteLine();
	Console.WriteLine($"{"Values",6}");
	for (int i = 0; i < Graph.GetLength(0); i++)
	{
		for (int j = 0; j < Graph.GetLength(1); j++)
		{
			Console.Write($"{Graph[i,j],6}" + "\t");
		}
		Console.WriteLine();
	}
	
	//TODO: Replace this code with yours to implement this function.
}