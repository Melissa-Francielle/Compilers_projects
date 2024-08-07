using System;
using System.Collections.Generic;
using System.IO;

namespace Analisador{
    public class LL1Parser{
        private string input = "";
        private int indexOfInput = -1;
        private ErrorHandler error;
        private Stack<string> pilha = new Stack<string>();
        private string[][] tabela;
        private string[] variaveis = { "G", "E", "K", "T", "H", "F" };
        private string[] terminais = { "a", "+", "*", "(", ")", "$" };

        public LL1Parser(string input, string tabelaArq){
            this.input = input;
            this.tabela = leTabela(tabelaArq);
            error = new ErrorHandler();
        }

        private string leArquivo(string inputArq){
            using (StreamReader sr = new StreamReader(inputArq)){
                return sr.ReadToEnd();
            }
        }

        private string[][] leTabela(string tabelaArq){
            var linhas = new List<string[]>();
            using (StreamReader sr = new StreamReader(tabelaArq)){
                string linha;
                while ((linha = sr.ReadLine()) != null){
                    var colunas = linha.Split(',');
                    if (colunas.Length != terminais.Length){
                        throw new Exception("Formato da tabela inválido na linha: " + (linhas.Count + 1));
                    }
                    linhas.Add(colunas);
                }
            }

            if (linhas.Count != variaveis.Length){
                throw new Exception("Formato da tabela inválido, número de linhas: " + linhas.Count);
            }

            return linhas.ToArray();
        }

        private void pushRule(string rule){
            for (int i = rule.Length - 1; i >= 0; i--){
                char ch = rule[i];
                string str = ch.ToString();
                if (!string.IsNullOrEmpty(str) && str != "ε") {  // Evita empilhar a regra vazia (ε)
                    Push(str);
                    Console.WriteLine("Push: " + str);
                }
            }
        }

        public void algoritmo(){
            Console.WriteLine("Inicializando análise...");
            Push("$");
            Push("G");

            string token = leitura();
            string top = null;

            do{
                if (pilha.Count == 0){
                    error.Error("Pilha vazia inesperadamente.");
                    return;
                }

                top = Pop();
                if (EhVariavel(top)){
                    string rule = getRule(top, token);
                    if (!string.IsNullOrEmpty(rule)){
                        pushRule(rule);
                    }
                }
                else if (EhTerminal(top)){
                    if (!top.Equals(token)){
                        error.Error("Token incorreto: esperado '" + top + "', encontrado '" + token + "'");
                        return;
                    }else{
                        Console.WriteLine("Correspondência: Terminal (" + token + ")");
                        token = leitura();
                    }
                }else{
                    error.Error("Erro inesperado com o topo: " + top);
                    return;
                }

                if (token.Equals("$") && pilha.Count == 0){
                    break;
                }
            } while (true);

            if (token.Equals("$") && pilha.Count == 0){
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Entrada aceita");
                Console.ResetColor();
            } else{
                 Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrada não foi aceita");
                Console.ResetColor();
            }
             Console.ForegroundColor = ConsoleColor.Green;
             Console.WriteLine("Análise concluída.");
             Console.ResetColor();
        }

        private bool EhTerminal(string term){
            foreach (string terminal in this.terminais){
                if (term.Equals(terminal)){
                    return true;
                }
            }
            return false;
        }

        private bool EhVariavel(string term){
            foreach (string noTerm in this.variaveis){
                if (term.Equals(noTerm)){
                    return true;
                }
            }
            return false;
        }

        private string leitura(){
            indexOfInput++;
            if (indexOfInput >= input.Length){
                return "$";  // Retorna o símbolo de fim de entrada para evitar erros posteriores
            }
            char ch = this.input[indexOfInput];
            return ch.ToString();
        }

        public void Push(string item){
            this.pilha.Push(item);
            Console.WriteLine("Push na pilha: " + item);
        }

        public string Pop(){
            if (this.pilha.Count == 0){
                error.Error("Pilha vazia ao tentar desempilhar");
                return null;  // Evita erro de retorno nulo
            }
            string item = this.pilha.Pop();
            Console.WriteLine("Pop da pilha: " + item);
            return item;
        }

        public string getRule(string semTerm, string term){
            int linhas = getSemTermIndex(semTerm);
            int colunas = getTermIndex(term);
            string rule = this.tabela[linhas][colunas];
            if (rule == null || rule.Equals("null"))
            {
                error.Error("Não há regra para: Não-terminal (" + semTerm + "), Terminal (" + term + ")");
                return "null"; // ou use um valor que indique ausência de regra
            }
            return rule;
        }


        private int getSemTermIndex(string semTerm){
            for (int i = 0; i < this.variaveis.Length; i++){
                if (semTerm.Equals(this.variaveis[i])){
                    return i;
                }
            }
            error.Error(semTerm + " não se classifica como não terminal");
            return -1;
        }

        private int getTermIndex(string term){
            for (int i = 0; i < this.terminais.Length; i++){
                if (term.Equals(this.terminais[i])){
                    return i;
                }
            }
            error.Error(term + " não se classifica como terminal");
            return -1;
        }
    }
}
