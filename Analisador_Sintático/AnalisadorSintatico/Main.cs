using System;

namespace Analisador{
    class Program{
        public static void Main(string[] args){
            string input;  // Alterado para tipo 'string'
            string tabelaArq = "tabela.csv";

            try{
                Console.WriteLine("\nEnter with the input: ");
                input = Console.ReadLine();  // Corrigido para atribuição correta

                if (string.IsNullOrEmpty(input)){
                    throw new ArgumentException("A entrada não pode ser vazia.");
                }

                LL1Parser parser = new LL1Parser(input, tabelaArq);
                parser.algoritmo();
                Console.WriteLine("Processamento concluído.");
            }
            catch (Exception ex){
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
