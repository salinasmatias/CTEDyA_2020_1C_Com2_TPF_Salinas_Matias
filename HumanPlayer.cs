
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
				if(Char.IsLetter(entrada, 0))
				{
					consultas(entrada); //Si el valor de entrada es una opción válida de consultas, se ejecuta la consulta que corresponde.
				}
                
                Int32.TryParse(entrada, out carta); //Sino, se intenta parsear el valor de entrada.
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
				Console.Clear();
                Juego.Main(ke);
                break;
				
				case "a":
                Game.getEstado().recorridoDeCaminos();
				break;

                case "b": 
				int condicion = 1;
            	int nuevacarta;
                List<int> jugada = new List<int>();
                while (condicion <= this.naipes.Count)
                {
                    Menu menu = new Menu();
                    menu.submenuB();
                    nuevacarta = int.Parse(Console.ReadLine());
					if(nuevacarta == 13)
					{
						break;
					}
					jugada.Add(nuevacarta);
                    condicion++;
                }
				if(!Game.getEstado().recorridoDeJugada(jugada, Game.getEstado()).esHoja())
				{
					Game.getEstado().recorridoDeJugada(jugada, Game.getEstado()).recorridoDeCaminos();
				}
                break;

                case "c":
				Console.Write("\n" + "Inserte la profundidad (Menor o igual a " + Game.getEstado().altura() + ") =");                    
                int profundidad = Int32.Parse(Console.ReadLine());
                Game.getEstado().porNivel(profundidad);
				if(profundidad%2==0)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\nTurno de la IA\n");
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine("\nTurno del Humano\n");
					Console.ResetColor();
				}
				break;
            }
        }
    }
}

