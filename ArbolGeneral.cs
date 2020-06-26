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

		public int getFuncRaiz()
        {
            return this.getRaiz().getFunc();
        }

        public void setFuncRaiz(int func)
        {
            this.getRaiz().setFunc(func);
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

		public string recorridoDeJugada(List<int> jugada, ArbolGeneral<int> estado)
        {
            if (jugada.Count == 0)
            {
                if (estado.getFuncRaiz() == 1)
                {
                    return "Gana pc";
                }
                else
                {
                    return "Gana humano";
                }

            }
            else
            {
                List<ArbolGeneral<int>> hijos = estado.getHijos();
                foreach (var hijo in hijos)
                {
                    if (hijo.getDatoRaiz() == jugada[0])
                    {
                        List<int> jugadaaux = new List<int>();
                        jugadaaux.AddRange(jugada);
                        jugadaaux.RemoveAt(0);
                        recorridoDeJugada(jugadaaux, hijo);

                    }
                }
                return "Esa jugada no existe";
            }
        }

		public void porNivel(int nivel)
        {
            if (nivel > Game.getEstado().altura())
            {
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
                        Console.Write(arbolAux.getDatoRaiz() + " ");                        
                    }                    
                    if (nivelactual > nivel)
                    {
                        Console.WriteLine("");
                        return;
                    }                    
                }
            }
        }
	}
}
