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
            Console.WriteLine("                              Juego de Cartas                                 \n" +
                   "\n" +
                   "\n");
                   
        }

        public void menuConsultas()
        {
            Console.WriteLine("Consultas in-game\n"+
            "a) Imprimir todos los posibles resultados.\n" +
            "b) Ingresar jugadas para obtener resultado.\n" +
            "c) Ingrese una profundidad para obtener las posibles jugadas.\n" +
            "r) Reiniciar el programa.\n" +
            "");

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
