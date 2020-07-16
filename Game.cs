
using System;
using System.Collections.Generic;
using System.Linq;
using tp1;
namespace juegoIA
{

	public class Game
	{
		public static int WIDTH = 12;
		public static int UPPER = 35;
		public static int LOWER = 25;
		
		private Jugador player1 = new ComputerPlayer();
		private Jugador player2 = new HumanPlayer();
		private List<int> naipesHuman = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool juegaHumano = false;
		private static ArbolGeneral<int> estado;
		
		public Game()
		{
			var rnd = new Random();
			limite = rnd.Next(LOWER, UPPER);
			
			naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();
			
			for (int i = 1; i <= WIDTH; i++) {
				if (!naipesHuman.Contains(i)) {
					naipesComputer.Add(i);
				}
			}
			player1.incializar(naipesComputer, naipesHuman, limite);
			player2.incializar(naipesHuman, naipesComputer, limite);
		}
		
		
		private void printScreen()
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Limite:" + limite.ToString());
			Console.ResetColor();
		}
		
		private void turn(Jugador jugador, Jugador oponente, List<int> naipes)
		{
			int carta = jugador.descartarUnaCarta();
			naipes.Remove(carta);
			limite -= carta;
			setEstado(carta);
			oponente.cartaDelOponente(carta);
			juegaHumano = !juegaHumano;
		}
		
		
		
		private void printWinner()
		{
			if (!juegaHumano) {
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("¡Ganaste!");
				Console.ResetColor();
			} else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Ganó la IA");
				Console.ResetColor();
			}
			
		}
		
		private bool fin()
		{
			return limite < 0;
		}
		
		public void play()
		{
			while (!this.fin()) {
				this.printScreen();
				this.turn(player2, player1, naipesHuman); // Juega el usuario
				if (!this.fin()) {
					this.printScreen();
					this.turn(player1, player2, naipesComputer); // Juega la IA
				}
			}
			this.printWinner();
			return;
		}
		
		private void setEstado(int carta)
        {
            foreach (var nodo in estado.getHijos())
            {
                if (nodo.getDatoRaiz() == carta)
                {
                    estado = nodo;
                }
            }
        }

        public static void setEstadoInicial(ArbolGeneral<int> raiz)
        {
            estado = raiz;
        }

        public static ArbolGeneral<int> getEstado()
        {
            return estado;
        }
	}
}
