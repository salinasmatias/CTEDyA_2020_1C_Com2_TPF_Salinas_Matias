
using System;

namespace juegoIA
{
	class Juego
	{
		public static void Main(string[] args)
		{
			Menu menu = new Menu();
            menu.menuPrincipal();
			menu.menuConsultas();
			
			Game game = new Game();
			game.play();
			Console.ReadKey();
		}
	}
}