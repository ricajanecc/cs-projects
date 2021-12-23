using System;
using System.Linq;
using System.Threading;

namespace calculator
{
    class Program
    {

        static void Main(string[] args)
        {
            bool isMenuOpen = true; //här använder jag bool, så länge det är TRUE så kör programmet. För att avsluta då tilldelar vi bara bool till falskt. 
            Calculator calculator = new Calculator(); // här deklarar jag 

            Console.Write("\n\t Hej! Var god och skriv in ditt namn: "); // här frågar programmet om användarens namn. 
            string userName = Console.ReadLine(); // här får användaren skriva in sitt namn. 

            Console.WriteLine("\n\t Välkommen till miniräknaren, " + userName + "!"); // här hälsar programmet välkommen till användaren med sitt namn. 
            Thread.Sleep(2000);
            

            while (isMenuOpen) // Menyn kommer förevisa sig så länge isMenuOpen är TRUE.
            {
                Console.Clear(); // raderar bort innehåll i konsol som att börja på nytt. 
                if (calculator.history.Count > 0) // här är en if-sats och om history är inte tömt så skriver ut den senaste uträkningen. 
                {
                    Console.Write("\n\t Senaste uträkning: " + calculator.history.Last().displayOperation());
                }
                Console.Write(
                    "\n\t [1] Ny beräkning" +
                    "\n\t [2] Skriv ut historik" +
                    "\n\t [3] Repetera senaste uträkning" +
                    "\n\t [4] Avsluta"
                ); //meny, om history är tömt så skriver ut meny istället. 

                Console.Write("\n\t Välj: "); // här för användaren ange sitt val
                Int32.TryParse(Console.ReadLine(), out int menuChoice); // för att förhindra körtidsfel så använder jag tryParse
                Console.Clear();

                switch (menuChoice)
                {
                    case 1: //om användaren väljer alternativ 1
                        string firstNumber = askForNumber("första"); // här har jag tilldelat firstNumber med metoden askForNumber. 
                        string operation = askForOperation(); // här gör jag samma men med en annan metod askForOperation
                        string secondNumber = askForNumber("andra"); // här har jag också tilldelat secondNumber med askForNumber. Men har bytt parameter till andra
                        HistoryElement historyElement = new HistoryElement(firstNumber, secondNumber, operation); //med informationer från användaren, så har jag skapat en ny historyElement 

                        calculator.calcOperate(historyElement); //här är en metod calcOperate, som beräknar resultatet och sparas den till historyElement. 
                        calculator.history.Add(historyElement); //här har jag tillsatt historyElement till calculators history.
                        break;

                    case 2: //om användaren väljer alternativ 2
                        Console.Write("\n\t Välj en beräkning för att repetera: "); //här frågar programmet användaren att välja en beräkning från calculator.history  , för att kunna repetera och beräkna igen. 
                        for (int i = 0; i < calculator.history.Count; i++ ) 
                        {
                            Console.Write("\n\t [" + (i + 1) + "] " + calculator.history[i].displayOperation()); //här skriver programmet ut, alla alternativ som användaren kommer ha genom att loopar igenom calculators history.
                        }
                        Console.Write("\n\t [" + (calculator.history.Count + 1) + "] avbryt"); //skriver ut alternativ till att avbryta
                        Console.Write("\n\t Välj: ");
                        Int32.TryParse(Console.ReadLine(), out int repeatHistory); //här lagras användarens svar
                        repeatHistory = repeatHistory - 1; //eftersom array börjar med 0, men meny alternativ börja med 1, så har jag substrahera med 1

                        if (repeatHistory >= 0 && repeatHistory < calculator.history.Count) //här detekteras om användaren har valt en beräkning för att repetera 
                        {
                            HistoryElement historyElement2 = calculator.history[repeatHistory].clone(); //här klonar jag uträkningen som användaren har valt 
                            calculator.calcOperate(historyElement2); //här uträknas resultatet av kloning 
                            calculator.history.Add(historyElement2); //kloning har sparas till calculator.history 
                        }
                        break;

                    case 3:
                        if(calculator.history.Count > 0) //programmet fortsätter om calculator.history är inte tömt
                        {
                            HistoryElement historyElement3 = calculator.history.Last().clone();//här är det nästan samma som case 2, istället för att klona den som användaren har valt, klonas den sista uträkningen
                            calculator.calcOperate(historyElement3);
                            calculator.history.Add(historyElement3);
                        }
                        break;

                    case 4:
                        Console.Write("\n\t Tack för att du har använt miniräknaren!"); //om användaren väljer detta alternativ så skriver ut meddelande och stängs av
                        Thread.Sleep(1000);
                        isMenuOpen = false; //här är boolean tilldelas till false, för att går ut från while-loopen av menyn
                        break;

                    default:
                        Console.Write("\n\t Operatoren är otillgängligt."); //om användaren skriver en annan siffra än 1-4, så skriver ut meddelande
                        Thread.Sleep(1000);
                        break;
                }



            }
            static string askForNumber(string position)
            {
                Console.Write("\n\t Ditt " + position + " tal eller ans: "); //här har vi en metod som skriver ut meddelande med en position. Det är också för att återanvända till den första och andra tal. 
                return Console.ReadLine(); // här sparas användarens tal 
            }

            static string askForOperation() //här är en metod som skriver ut olika operatörer som användaren kan välja ut och sparar användarens val 
            {
                Console.Write("\n\t Välj en operator mellan: " +
                    "\n\t [+] Addition " +
                    "\n\t [-] Substraktion" +
                    "\n\t [*] Multiplikation" +
                    "\n\t [/] Division" 
                    );
                Console.Write("\n\t Välj: ");
                return Console.ReadLine();
            }

        }
    }
}
