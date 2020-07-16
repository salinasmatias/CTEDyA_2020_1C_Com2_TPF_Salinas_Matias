using System;
using System.Collections.Generic;
using System.Text;

namespace juegoIA
{
    class Menu
    {
        public void menuPrincipal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                              Juego de Cartas                                 \n" +
                   "\n" +
                   "\n");
            Console.ResetColor();
                   
        }

        public void menuConsultas()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Consultas in-game\n"+
            "a) Imprimir todos los posibles resultados.\n" +
            "b) Ingresar jugadas para obtener posibles resultados.\n" +
            "c) Ingrese una profundidad para obtener las posibles jugadas.\n" +
            "r) Reiniciar el programa.\n" +
            "");
            Console.ResetColor();

        }

        public void submenuB()
        {
            Console.WriteLine("\n" +
                        "Ingrese una carta:\n" +
                        "Ingrese 13 para finalizar.\n" +
                        ""
                        );
        }
    }
}
