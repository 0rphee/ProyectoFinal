using System;

namespace ProyectoFinal { 
	class Program { 
		static void Main(string[] args) {
			
			// funciones genéricas que suelo utilizar

			string obtenerDato(string textoAMostrar) {
				Console.Write(textoAMostrar + " ");
				return Console.ReadLine();
			}

			// Valida un valor, se dan los límites dentro de los cuales debe encontrarse
			int validacionInt(int limInf, int limSup, string textoAMostrar) {
				int dato;
				do {
					dato = int.Parse(obtenerDato(textoAMostrar));
				} while (dato < limInf || limSup < dato);
				return dato;
			}

			// Genera menu y pide una opcion
			int obtenerOpcMenu(string[] titulos, string mainTitle) {
				int numTitulos = titulos.Length;

				Console.WriteLine("\n" + mainTitle);

				for (int i = 0; i < numTitulos; i++) {
					Console.WriteLine($"{i + 1}-{titulos[i]}");
				}
                Console.WriteLine($"{numTitulos+1}-SALIR");

				return validacionInt(1, numTitulos+1, "Deme la opción deseada: ");
			}
			
			Console.WriteLine("Hello World");
			Console.ReadKey();
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
		}
	} 
}
