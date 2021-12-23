

namespace calculator
{
    class HistoryElement //här har jag en class som lagrar alla operatorer
    {
        public string firstNumber; //här lagrar den första siffran av uträkningen
        public string secondNumber; // och den andra siffran
        public string operatorSymbol; // här lagrar jag operatorer som +, - , * eller /
        public string result; //här lagrar jag resultatet av uträkningen eller fel meddelandet 

        public HistoryElement(string firstNumberParam, string secondNumberParam, string operationParam) //här är konstruktionen, resultatet är inte med eftersom det är uträknad senare. 
        {
            firstNumber = firstNumberParam;
            secondNumber = secondNumberParam; 
            operatorSymbol = operationParam;
        }

        public string displayOperation() //funktionen skriver ut uträkningen 
        {
            return firstNumber + " " + operatorSymbol + " " + secondNumber + " = " + result;
        }

        public HistoryElement clone() //funktionen skapar en klon av HistoryElement för att repetera uträkningar som användaren har skapat före.
        {
            
            return new HistoryElement(firstNumber, secondNumber, operatorSymbol); //returnerar en klon
        }

    }
}
