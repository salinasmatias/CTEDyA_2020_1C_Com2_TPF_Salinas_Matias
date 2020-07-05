
using System;
using System.Collections.Generic;
using System.Linq;
using tp1;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
		private ArbolGeneral<int> raiz;        
        private ArbolGeneral<int> estado;
		
		public ComputerPlayer()
		{
		}
		
		
        //Método inicializar crea árbol general con todos los estados posibles
        public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.raiz = new ArbolGeneral<int>(0);
            this.estado = raiz;            
            bool turnoH = true;
            _inicializar(cartasPropias, cartasOponente, limite, turnoH, this.raiz);
		}

		private void _inicializar(List<int> cartasPropias, List<int> cartasOponente, int limite, bool turno, ArbolGeneral<int> raiz)
        {
            //Creo una lista de cartas auxiliar para no modificar las originales
            List<int> cartas = new List<int>();

            if (turno == true) //Turno del humano
            {                
                cartas.AddRange(cartasOponente);                
            }
            else //Turno de la pc
            {                
                cartas.AddRange(cartasPropias);                      
            }
            
            //Para cada carta
            foreach (var carta in cartas)
            {
                //Creo un arbol auxiliar con la carta como dato
                ArbolGeneral<int> hijo = new ArbolGeneral<int>(carta);

                //Agrego el arbol auxiliar como hijo de la raíz
                raiz.agregarHijo(hijo);

                //Disminuyo el limite
                int limiteaux = limite - carta;

                //Creo una lista de cartas auxiliar para eliminar la carta sin perder información de la lista "cartas"
                List<int> cartasrestantes = new List<int>();
                cartasrestantes.AddRange(cartas);                
                cartasrestantes.Remove(carta);                
                
                //Si el límite es mayor o igual a 0 sigo agregando hijos a los subárboles
                if (limiteaux >= 0)
                {
                    //Si es el turno del humano, llamo recursivamente a la funcion con la lista de cartas auxiliares como nueva lista de cartas del humano.
                    if (turno == true)
                    {
                        _inicializar(cartasPropias, cartasrestantes, limiteaux, !turno, hijo);

                        //Asigno función heurística a cada nodo cuando vuelvo de la recursión
                        List<ArbolGeneral<int>> hijos = raiz.getHijos();
                        raiz.setFuncionHeuristica(-1);
                        foreach (var h in hijos)
                        {
                            if (h.getFuncionHeuristicaRaiz() == 1)
                            {
                                raiz.setFuncionHeuristica(1);
                                break;
                            }
                        }
                    }

                    //Si es el turno de la pc, llamo recursivamente a la funcion con la lista de cartas auxiliares como nueva lista de cartas de la pc.
                    else
                    {
                        _inicializar(cartasrestantes, cartasOponente, limiteaux, !turno, hijo);

                        //Asigno función heurística a cada nodo cuando vuelvo de la recursión
                        List<ArbolGeneral<int>> hijos = raiz.getHijos();
                        raiz.setFuncionHeuristica(-1);
                        foreach (var h in hijos)
                        {
                            if (h.getFuncionHeuristicaRaiz() == 1)
                            {
                                raiz.setFuncionHeuristica(1);
                                break;
                            }
                        }
                    }
                }

                //Si se pasó el límite
                else
                {
                    //Asigno función heurística a cada hoja
                    
                    //(1 gana pc ; -1 gana humano)

                    if (turno == true)
                    {
                        hijo.setFuncionHeuristica(1);
                    }
                    else
                    {
                        hijo.setFuncionHeuristica(-1);
                    }
                }
            }
            Game.setEstadoInicial(raiz);
        }
		
		
		public override int descartarUnaCarta()
		{
			//Me fijo cuál es la función heurística de cada hijo del estado
            //Si hay alguna que sea 1, retorno esa carta

            foreach (var nodo in this.estado.getHijos())
            {
                int func = nodo.getFuncionHeuristicaRaiz();
                if (func == 1)
                {
                    int cartaaux = nodo.getDatoRaiz();
                    cartaDelOponente(cartaaux);
                    Console.WriteLine("La IA juega = " + cartaaux);
                    return cartaaux;
                }
            }
            //Sinó retorno la primera de la lista
            int carta = this.estado.getHijos()[0].getDatoRaiz();
            cartaDelOponente(carta);
            Console.WriteLine("La IA juega = " + carta);
            return carta;
		}
		
		public override void cartaDelOponente(int carta)
		{
			foreach (var nodo in this.estado.getHijos())
            {
                if (nodo.getDatoRaiz() == carta)
                {
                    this.estado = nodo;
                }
            }
		}
		
	}
}
