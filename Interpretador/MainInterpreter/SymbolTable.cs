using System;
using System.Collections.Generic;

namespace Calc {
    public class SymbolTable {
        private Dictionary<string, double> symbols;

        public SymbolTable() {
            symbols = new Dictionary<string, double>();
        }

        public double AddSymbol(string name, double value) {
            symbols[name] = value;
            return value;
        }

        public double GetSymbol(string name) {
            if (symbols.ContainsKey(name)) {
                return symbols[name];
            }
            throw new ArgumentException("Undefined variable: " +name);
        }

        public double AddSymbol(string name) {
            if (!symbols.ContainsKey(name)) {
                symbols[name] = 0;
            }
            return symbols[name];
        }
    }
}
