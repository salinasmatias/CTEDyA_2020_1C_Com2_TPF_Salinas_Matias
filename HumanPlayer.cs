
using System;
using System.Collections.Generic;
using System.Linq;


namespace juegoIA
{	public class HumanPlayer : Jugador
	{
		private List<int> naipes = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool random_card = false;
		
		public HumanPlayer(){}
		
		public HumanPlayer(bool random_card)
		{
			this.random_card = random_card;
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.naipes = cartasPropias;
			this.naipesComputer = cartasOponente;
			this.limite = limite;
		}
		
		public override int descartarUnaCarta()
		{
			int carta = 0;
			Console.WriteLine("Naipes disponibles (Usuario):");
			for (int i = 0; i < naipes.Count; i++) {
				Console.Write(naipes[i].ToString());
				if (i<naipes.Count-1) {
					Console.Write(", ");
				}
			}
			Console.WriteLine("\n\nNaipes disponibles (IA):");
			for (int i = 0; i < naipesComputer.Count; i++) {
				Console.Write(naipesComputer[i].ToString());
				if (i<naipesComputer.Count-1) {
					Console.Write(", ");
				}
			}
		
			Console.WriteLine();
			if (!random_card) {
				Console.Write("Ingrese naipe o consulta:");
				string entrada = Console.ReadLine();

                consultas(entrada);

                Int32.TryParse(entrada, out carta);
				while (!naipes.Contains(carta)) {
					Console.Write("Ingrese naipe o consulta:");
					entrada = Console.ReadLine();

                    consultas(entrada);
                    
                    Int32.TryParse(entrada, out carta);
				}
			} 
			else 
			{
				var random = new Random();
				int index = random.Next(naipes.Count);
				carta = naipes[index];
				Console.Write("Ingrese naipe:" + carta.ToString());
			}
			
			return carta;
		}
		
		public override void cartaDelOponente(int carta){
		}

        public void consultas(string entrada)
        {

            switch (entrada)
            {
                case "r":
				string[] ke;
                ke = new string[1];
                Juego.Main(ke);
                break;
				
				case "a":
                Game.getEstado().porNiveles();
				break;

                case "b": 
				//Pedir 6 nuevas cartas al usuario y crear nuevo arbol
				int condicion = 1;
            	int nuevacarta;
                List<int> jugada = new List<int>();
                while (condicion <= this.naipes.Count)
                {
                    Menu menu = new Menu();
                    menu.submenuB();
                    nuevacarta = int.Parse(Console.ReadLine());
                    jugada.Add(nuevacarta);
                    condicion++;
                }
				
				Console.WriteLine(Game.getEstado().recorridoDeJugada(jugada, Game.getEstado()));
                break;

                case "c":
				Console.Write("\n" + "Inserte la profundidad (Menor o igual a " + Game.getEstado().altura() + ") =");                    
                int profundidad = Int32.Parse(Console.ReadLine());
                Game.getEstado().porNivel(profundidad);
				break;
            }
        }
    }
}

