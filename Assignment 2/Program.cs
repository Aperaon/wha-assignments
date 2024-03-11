Crypto();

void Crypto(){
    Console.Clear();

    Random rnd = new Random();
    double ETHSpotRnd = rnd.NextDouble() * (2999.99 - 2600.00) + 2600.00;
    double ETHSpot = Math.Round(ETHSpotRnd, 2 );
    
    Console.WriteLine($"");
    Console.WriteLine($"---- DMITCryptoEx ETH Trader ----");
    Console.WriteLine($"");
    Console.WriteLine($"Current ETH spot price is: {ETHSpot:c}");
    Console.WriteLine($"");
    
    bool InputError = true;
    double PurchaseAmount = 0.00;
    double CommissionRate = 0.00;

    while(InputError){

        try{
            
            Console.Write($"Enter amount of ETH to purchase: ");
            PurchaseAmount = double.Parse(Console.ReadLine());
            
            if(PurchaseAmount < 0){
                throw new Exception($"ETH amount must be positive");
            }
            if(PurchaseAmount > 0){
                
                if(PurchaseAmount < 10){
                    CommissionRate = 1.5;
                }
                if (PurchaseAmount < 5){
                    CommissionRate = 1.75;
                }
                if (PurchaseAmount < 1){
                    CommissionRate = 1.9;
                }
                if(PurchaseAmount >= 10){
                    CommissionRate = 1.25;
                }
                
                InputError = false;
            }
        }catch(FormatException){

            Console.WriteLine($"ETH amount must be a number");
            
        }catch(Exception ex){

            Console.WriteLine(ex.Message);

        }

    }
    
    Console.WriteLine($"Current stake rate is 3.100%");
    bool InputError2 = true;
    string Staked = ""; 
    double StakedEarning = 0.00;

    while(InputError2){

        try{
            
            Console.Write($"Stake your ETH (Y/N): ");
            Staked = Console.ReadLine().ToUpper();

           if(Staked != "Y" && Staked != "N"){
            throw new Exception($"Response must be Y or N");
           }else{
            
            if(Staked.ToUpper() == "Y"){
                StakedEarning = ETHSpot*0.031/12;
                System.Console.WriteLine($"");
                Console.WriteLine($"You will earn {StakedEarning} per month for your staked ETH");
            }
            InputError2 = false;
           }

        }catch(Exception ex){
            Console.WriteLine(ex.Message);
        }

    }
    
    double TotalCommission = CommissionRate/100*ETHSpot;
    Console.WriteLine($"");
    Console.WriteLine($"Please review you order ...");

    string OrderView = 
        //$"\n{"",15}"+z
        $"\n{"Total ETH purchased: ",-23}{PurchaseAmount,12:n6}"+
        $"\n{"ETH spot price: ",-23}{ETHSpot,12:c}"+
        $"\n{"Commission Rate: ",-23}{$"{CommissionRate:n3}%",12}"+
        $"\n{"Total commission: ",-23}{TotalCommission,12:c}";

    System.Console.WriteLine(OrderView);

    if(Staked == "Y"){
        System.Console.WriteLine($"{"Stacked? ",-23}{"Yes",12}");
        System.Console.WriteLine($"Staked monthly reward: {ETHSpot*0.031/12,12:c}");
    }else{
        System.Console.WriteLine($"{"Stacked? ",-23}{"No",12}");
    }
    System.Console.WriteLine($"-----------------------------------");
    System.Console.WriteLine($"{"Total purchase:  ",-23}{$"{ETHSpot+TotalCommission:c}",12}");

    bool InputError3 = true;
    string Cancel = "";

    while(InputError3){

        try{
            
            Console.Write($"\nWould you like to continue with your order (Y/N): ");
            Cancel = Console.ReadLine().ToUpper();

           if(Cancel != "Y" && Cancel != "N"){
            throw new Exception($"Response must be Y or N");
           }else{
            
            if(Staked.ToUpper() == "Y"){
                
                Console.WriteLine($"Your order has been sent, Thank you.");
            }else{
                Console.WriteLine($"Your order has been cancelled.");
            }
            InputError3 = false;
           }

        }catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }

    System.Console.WriteLine($"\nThank you for using DMITCryptoEx");
    System.Console.WriteLine($"");
    
}
