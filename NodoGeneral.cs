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
        private int func;
        private string v;
		
		public NodoGeneral(T dato){		
			this.dato = dato;
			this.hijos = new List<NodoGeneral<T>>();
		}

		public NodoGeneral(string v)
        {
            this.v = v;
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

		public int getFunc()
        {
            return this.func;
        }

        public void setFunc(int func)
        {
            this.func = func;
        }
	
	}
}
