using System;
using System.Collections.Generic;
using juegoIA;

namespace tp1
{
	public class ArbolGeneral<T>
	{
		
		private NodoGeneral<T> raiz;

		public ArbolGeneral(T dato) {
			this.raiz = new NodoGeneral<T>(dato);
		}
	
		private ArbolGeneral(NodoGeneral<T> nodo) {
			this.raiz = nodo;
		}
	
		private NodoGeneral<T> getRaiz() {
			return raiz;
		}
	
		public T getDatoRaiz() {
			return this.getRaiz().getDato();
		}
	
		public List<ArbolGeneral<T>> getHijos() {
			List<ArbolGeneral<T>> temp= new List<ArbolGeneral<T>>();
			foreach (var element in this.raiz.getHijos()) {
				temp.Add(new ArbolGeneral<T>(element));
			}
			return temp;
		}
	
		public void agregarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Add(hijo.getRaiz());
		}
	
		public void eliminarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Remove(hijo.getRaiz());
		}
	
		public bool esVacio() {
			return this.raiz == null;
		}
	
		public bool esHoja() {
			return this.raiz != null && this.getHijos().Count == 0;
		}

		public int getFuncionHeuristicaRaiz()
        {
            return this.getRaiz().getFuncionHeuristica();
        }

        public void setFuncionHeuristica(int funcion)
        {
            this.getRaiz().setFuncionHeuristica(funcion);
        }
	
		public int altura() {
			if(this.esVacio())
			{
				return -1;
			}
			if(this.esHoja())
			{
				return 0;
			}else
			{
				int alturaMaxima = 0;
				foreach(var hijo in this.getHijos())
				{
					int alturaHijo = hijo.altura();
					if(alturaMaxima < alturaHijo)
					{
						alturaMaxima = alturaHijo;
					}
				}
				return alturaMaxima +1;
			}
		}
	
		public int nivel(T dato) 
		{
			if(!this.esVacio())
			{
				if(this.getDatoRaiz().Equals(dato))
				{
					return 0;
				}else
				{
					foreach (var hijo in this.getHijos())
					{
						int level = hijo.nivel(dato);
						if(hijo.include(dato))
						{
							return level +1;
						}
					}
				}
			}
			return -1;
		}

		public bool include(T dato)
		{
			//Primero proceso la raíz 
			if(!this.esVacio())
			{
				if(this.getDatoRaiz().Equals(dato))
				{
					return true;
				}
			}
			//después proceso los hijos
			if(!this.esHoja())
			{
				foreach(var hijo in this.getHijos())
				{
					if(hijo.include(dato))
					{
						return hijo.include(dato);
					}
				}
			}
			return false;
		}

		public int ancho() 
		{
			Cola<ArbolGeneral<T>> c = new Cola<ArbolGeneral<T>>();
			ArbolGeneral<T> arbolAux;

			// Inicializamos las variables auxiliares
			int cantArbol = 0, anchoMax = 1;

			// Encolamos el árbol desde donde se invoca la función y el separador de niveles 'null'
			c.encolar(this);
			c.encolar(null);

			// Si el árbol es vacío, se retorna 0
			if (this.esVacio())
				return 0;

			// Si el árbol es hoja, se retorna 1
			if (this.esHoja())
				return 1;

			while (!c.esVacia()) {
				arbolAux = c.desencolar();

				if (arbolAux == null) { //Si el árbol desencolado es nulo...
					if (!c.esVacia()) // Se encola mientras haya más elementos en la lista
						c.encolar(null);

					if (cantArbol > anchoMax)
						anchoMax = cantArbol;

					cantArbol = 0;
				} else {
					cantArbol++;

					if (!arbolAux.esHoja()) { // Si no es hoja, se encolan todos los hijos del árbol
						foreach (var hijo in arbolAux.getHijos())
							c.encolar(hijo);
					}
				}
			}
			return anchoMax;
		}

		

		public void preOrden()
		{
			//Primero proceso la raíz 
			if(!this.esVacio())
			{
				Console.Write(this.getDatoRaiz() + " ");
			}
			//después proceso los hijos
			if(!this.esHoja())
			{
				foreach(var hijo in this.getHijos())
				{
					hijo.preOrden();
				}
			}
		}

		public void postOrden()
		{
			//Primero proceso los hijos
			if(!this.esHoja())
			{
				foreach(var hijo in this.getHijos())
				{
					hijo.postOrden();
				}
			}
			//Después proceso la raíz.
			Console.Write(this.getDatoRaiz() + " ");
		}

		public void porNiveles() 
		{
			// Creamos la cola
			Cola<ArbolGeneral<T>> c = new Cola<ArbolGeneral<T>>();
			
			// Creamos una variable que servirá como auxiliar
			ArbolGeneral<T> arbolAux;
			
			// Incorporamos al árbol actual a la cola
			c.encolar(this);

			while (!c.esVacia()) {
				// Se imprime el valor de salida de la cola
				arbolAux = c.desencolar();
				Console.Write(arbolAux.getDatoRaiz() + " ");

				// Si no es hoja, se agregan todos los hijos del árbol actual a la cola
				if (!arbolAux.esHoja()) {
					foreach (var hijo in arbolAux.getHijos()) {
						c.encolar(hijo);
					}
				}
			}
		}

		public float getCaudalMinimo(float caudalActual, float min) 
		{
			if (caudalActual < min)
			{
				min = caudalActual;
			}
			
			if (this.esHoja())
			{
				return caudalActual;
			}else 
			{
				List<ArbolGeneral<T>> hijos = this.getHijos();
				int cantHijos = hijos.Count;

				float caudalMin = min;
				foreach (var hijo in hijos) 
				{
					caudalMin = hijo.getCaudalMinimo(caudalActual/cantHijos, min);
				}

				if (caudalMin < min)
				{
					min = caudalMin;
				}
				
				return min;
			}
		}

		public void recorridoDeCaminos()
		{
			List<NodoGeneral<T>> lista = new List<NodoGeneral<T>>();
			caminos(lista);
		}

		private void caminos(List<NodoGeneral<T>>list)
		{
			List<NodoGeneral<T>> auxList = new List<NodoGeneral<T>>();
			auxList.AddRange(list);
			auxList.Add(this.getRaiz());
			
			if(this.esHoja())
			{
				foreach (var node in auxList)
				{
					Console.Write("({0}, {1})", node.getDato(), node.getFuncionHeuristica());
				}
				if(auxList[auxList.Count-1].getFuncionHeuristica()==1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(" Gana la IA");
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine(" Gana el Humano");
					Console.ResetColor();
				}
				Console.Write("\n");
			}
			
			foreach (var node in this.getHijos())
			{
				node.caminos(auxList);	
			}
		}	       

		public ArbolGeneral<int> recorridoDeJugada(List<int> jugada, ArbolGeneral<int> estado)
        {
			bool valid_squence=false;
			ArbolGeneral<int> current = estado;
			try
			{
				foreach (int card in jugada)
				{
					valid_squence = false;
					foreach (var subtree in current.getHijos())
					{
						if(subtree.getDatoRaiz() == card)
						{
							current = subtree;
							valid_squence = true;
							break;
						}
					}
					if(!valid_squence)
					{
						throw new Exception();
					}
				}
			}
			catch (System.Exception)
			{
				Console.WriteLine("La secuencia ingresada es invalida");
				current = new ArbolGeneral<int>(0);
			}
			//si la jugada es inválida, devolver un error.
			return current;
        }

		public void porNivel(int nivel)
        {
            if(nivel%2==0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Blue;
			}
			if (nivel > Game.getEstado().altura())
            {
                Console.ResetColor();
				Console.WriteLine("Profundidad inválida.\n" +
                    "");
                return;
            }
            Cola<ArbolGeneral<T>> c = new Cola<ArbolGeneral<T>>();
            ArbolGeneral<T> arbolAux;
            
            int nivelactual = 0;
            c.encolar(this);
            c.encolar(null);

            while (!c.esVacia())
            {
                arbolAux = c.desencolar();
                if (arbolAux == null)
                {
                    nivelactual += 1;
                    c.encolar(null);
                    if (nivelactual > nivel)
                    {
                        Console.WriteLine("");
                        break;
                    }
                }
                else
                {
                    if (!this.esHoja())
                    {
                        foreach (var hijo in arbolAux.getHijos())
                        {
                            c.encolar(hijo);
                        }
                    }
                    if (nivelactual == nivel)
                    {
                        Console.Write("({0},{1})", arbolAux.getDatoRaiz(), arbolAux.getFuncionHeuristicaRaiz());
                    }                    
                    if (nivelactual > nivel)
                    {
                        Console.WriteLine("");
                        return;
                    }                    
                }
            }
			Console.ResetColor();
        }
	}
}
