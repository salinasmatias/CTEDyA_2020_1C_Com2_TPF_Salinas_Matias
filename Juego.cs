
using System;

namespace juegoIA
{
	class Juego
	{
		public static void Main(string[] args)
		{
			string respuesta = "si";
			while (respuesta == "si")
			{
				Menu menu = new Menu();
            	menu.menuPrincipal();
				menu.menuConsultas();
				Game game = new Game();
				game.play();
				Console.WriteLine("El juego ha concluido. ¿Desea Jugar otra vez? Si/No");
				respuesta = Console.ReadLine();
				Console.Clear();
			}
			Console.WriteLine("Game Over.");
		}
	}
}