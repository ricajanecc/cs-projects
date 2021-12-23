using System;
using System.Collections.Generic;
using System.Threading;

namespace calculator
{
    class Calculator //här har jag skapat en klass som representerar staten av calculator
    {
        public double ansValue; //här lagrar förra resultat från uträkningen för att återanvända när användaren väljer ut ANS
        public List<HistoryElement> history; //alla uträkningar som användaren har skapat är sparas i denna lista

        public Calculator() //konstruktion, sätter default value till calculator
        {
            ansValue = 0; //vi ansValue tilldelar vi med noll, för att i första hand om användaren väljer ut ans så att det inte kör fel i programmet
            history = new List<HistoryElement>(); // vi tilldelar history till en tom lista
        }


        public void calcOperate(HistoryElement historyElem) //funktionen konverterar siffrorna till double och sedan utför uträkningen. Det lagrar resultatet till historyElem.result
        {
            double firstNumber = 0; //här har jag deklarerar de två double som ska senare hjälpa att beräkna resultatet
            double secondNumber = 0;
            bool error = false; //boolean ska tilldelas true om en av siffrorna kan inte blir konverterar till double  
            if (historyElem.firstNumber.ToUpper().Trim() == "ANS") //här programmet kontrollerar om användaren har valt ANS
            {
                firstNumber = ansValue; //här har jag tilldelat ansValue till firstNumber
            }
            else
            {
                try
                {
                    firstNumber = Double.Parse(historyElem.firstNumber); //här försöker konvertera firstNumber till double
                }
                catch (Exception)
                {
                    error = true; //om omvändling är fel, tilldelar jag true till error
                }
            }
            if (historyElem.secondNumber.ToUpper().Trim() == "ANS") //här har jag gjort samma processen till den andra siffra
            {
                secondNumber = ansValue;
            }
            else
            {
                try
                {
                    secondNumber = Double.Parse(historyElem.secondNumber);
                }
                catch (Exception)
                {
                    error = true;
                }
            }

            if (error) //om error är true, resultatet blir syntaxfel
            {
                historyElem.result = "Syntaxfel!"; 
                return; //här slutar funktionen så att det inte försöker att räkna ut 
            }

            switch (historyElem.operatorSymbol.ToUpper().Trim()) //här detekterar vilken operatör användaren väljer 
            {
                case "+": 
                    double result = firstNumber + secondNumber; //här uträknas summan av två siffror
                    historyElem.result = result.ToString(); //här lagras resultatet till historyElem
                    ansValue = result; //här lagras resultatet till nästa uträkningen
                    break;

                case "-":
                    double subResult = firstNumber - secondNumber; //här uträknas differens av två siffror 
                    historyElem.result = subResult.ToString();
                    ansValue = subResult;
                    break;

                case "*":
                    double multResult = firstNumber * secondNumber; //här uträknas produkten av två siffror
                    historyElem.result = multResult.ToString();
                    ansValue = multResult;
                    break;

                case "/": 
                    if (secondNumber == 0) //om användaren skriver noll till den andra siffran resultatet blir "ogiltig nämnare"
                    {
                        historyElem.result = "Ogiltig nämnare!"; 
                    }
                    else 
                    {
                        double divResult = firstNumber / secondNumber; //annars uträknas kvoten av två siffror
                        historyElem.result = divResult.ToString();
                        ansValue = divResult;
                    }
                    break;



                case "MARCUS": //om användaren skriver Marcus, så säger konsolen hej och stängs av
                    Console.Clear();
                    Console.Write("\n\tHej!");
                    Thread.Sleep(1000); //konsolen stängs av efter 1 sekund
                    Environment.Exit(0); // programmet stängs av
                    break;


                default:  //om användaren skriver annan än de 4 operatorer så skriver ut meddelande
                    historyElem.result = "Operatoren är otillgänglig.";
                    break;

            }
        }
    }
}
