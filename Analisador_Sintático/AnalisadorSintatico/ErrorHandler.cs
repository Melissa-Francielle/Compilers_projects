using System;

namespace Analisador{
    class ErrorHandler{
        public void Error(string message){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: " + message);
            // Restaura a cor do texto para a cor padrão
            Console.ResetColor();
        }
        
    }
}