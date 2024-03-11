// See https://aka.ms/new-console-template for more information
GICCalc();

//function that calculate your best friend's GIC
void GICCalc(){

    //starting varibles for stuff
    double initialInvAmount;
    double intRate;
    double yearInv;
    double FV;
    
    //inputs of values
    Console.WriteLine($"");
    Console.WriteLine($" ---- Future Value Calculator ----");
    Console.WriteLine($"");
    Console.Write($"Enter investment amount: ");
    initialInvAmount = double.Parse(Console.ReadLine());
    Console.Write($"Enter annual rate: ");
    intRate = double.Parse(Console.ReadLine());
    Console.Write($"Enter number of years: ");
    yearInv = int.Parse(Console.ReadLine());
    //calculate 
    FV = initialInvAmount * Math.Pow((1 + ((intRate/100)/12)),(yearInv*12));
    //output answer 
    Console.WriteLine();
    Console.WriteLine($"The future value amount of {initialInvAmount:c} in {yearInv} years is {FV:c}.\nThank you, goodbye");
    Console.WriteLine("");

}
