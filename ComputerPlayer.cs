
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
            //Creo una lista de cards auxiliar para no modificar las originales
            List<int> cards = new List<int>();

            if (turno == true) //Human turn
            {                
                cards.AddRange(cartasOponente);                
            }
            else //AI Turn
            {                
                cards.AddRange(cartasPropias);                      
            }
            
            //Para cada card
            foreach (var card in cards)
            {
                //Creo un arbol auxiliar con la card como dato
                ArbolGeneral<int> hijo = new ArbolGeneral<int>(card);

                //Agrego el arbol auxiliar como hijo de la raíz
                raiz.agregarHijo(hijo);

                //Disminuyo el limite
                int limiteaux = limite - card;

                //Creo una lista de cards auxiliar para eliminar la card sin perder información de la lista "cards"
                List<int> cartasRestantes = new List<int>();
                cartasRestantes.AddRange(cards);                
                cartasRestantes.Remove(card);                
                
                //Si el límite es mayor o igual a 0 sigo agregando hijos a los subárboles
                if (limiteaux >= 0)
                {
                    //Si es el turno del humano, llamo recursivamente a la funcion con la lista de cards auxiliares como nueva lista de cards del humano.
                    if (turno == true)
                    {
                        _inicializar(cartasPropias, cartasRestantes, limiteaux, !turno, hijo);

                        //Asignamos función heurística a cada nodo cuando volvemos de la recursión
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

                    //Si es el turno de la IA, llamamos a la función recursivamente con la lista de cartas auxiliares como nueva lista de cartas de la IA.
                    else
                    {
                        _inicializar(cartasRestantes, cartasOponente, limiteaux, !turno, hijo);

                        //Asignamos función heurística a cada nodo cuando volvemos de la recursión.
                        List<ArbolGeneral<int>> hijos = raiz.getHijos();
                        raiz.setFuncionHeuristica(1);
                        foreach (var h in hijos)
                        {
                            if (h.getFuncionHeuristicaRaiz() == -1)
                            {
                                raiz.setFuncionHeuristica(-1);
                                break;
                            }
                        }
                    }
                }

                //Si se pasó el límite
                else
                {
                    //Asignamos función heurística a cada hoja
                    
                    //(1 IA Gana ; -1 Human Gana)

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
			//Me fijo cuál es la función heurística de cada child del estado
            //Si hay alguna que sea 1, retorno esa card
            
            foreach (var nodo in this.estado.getHijos())
            {
                int func = nodo.getFuncionHeuristicaRaiz();
                if (func == 0)
                {
                    int cartax = nodo.getDatoRaiz();
                    cartaDelOponente(cartax);
                    Console.WriteLine("\nLa IA juega = {0}" ,cartax);
                    return cartax;
                }
            }
            
            foreach (var nodo in this.estado.getHijos())
            {
                int func = nodo.getFuncionHeuristicaRaiz();
                if(func ==1)
                {
                    int cartax = nodo.getDatoRaiz();
                    cartaDelOponente(cartax);
                    Console.WriteLine("\nLa IA juega = {0}" ,cartax);
                    return cartax;
                }
            }
            //Sinó retorno la primera de la lista
            int card = this.estado.getHijos()[0].getDatoRaiz();
            cartaDelOponente(card);
            Console.WriteLine("La IA juega = " + card);
            return card;
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
