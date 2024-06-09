using System;

namespace Calc{
    class Program{
        public static void Main(String[] args){   
            
            Console.WriteLine("Calc Interpreter");
            string input = " ";
            Interpreter interpreter = new Interpreter();

            do{
                Console.Write(">> ");
                input = Console.ReadLine();
                if(!string.IsNullOrEmpty(input)){
                    string output = interpreter.Exec(input);
                    Console.WriteLine(output);
                }
            }while(!string.IsNullOrEmpty(input));
        }
        
    }
}