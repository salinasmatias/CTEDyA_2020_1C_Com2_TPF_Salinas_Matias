using System;
using System.Collections.Generic;

namespace tp1
{
	/// <summary>
	/// Description of NodoGeneral.
	/// </summary>
	public class NodoGeneral<T>
	{
		private T dato;
		private List<NodoGeneral<T>> hijos;
		
		//(1 gana pc, -1 gana humano)
        private int funcionHeuristica;
		
		public NodoGeneral(T dato){		
			this.dato = dato;
			this.hijos = new List<NodoGeneral<T>>();
		}

		public T getDato(){		
			return this.dato; 
		}
		
		public List<NodoGeneral<T>> getHijos(){		
			return this.hijos;
		}

		public void setDato(T dato){		
			this.dato = dato;
		}
		
		public void setHijos(List<NodoGeneral<T>> hijos){		
			this.hijos = hijos;
		}

		public int getFuncionHeuristica()
        {
            return this.funcionHeuristica;
        }

        public void setFuncionHeuristica(int funcionHeuristica)
        {
            this.funcionHeuristica = funcionHeuristica;
        }
	
	}
}
