/*
Se piden recetas de cocina, se ingresan características.
Para los ingredientes de la receta podríamos crear otra estructura, struct Ingrediente, para poder 
guardar el nombre y la cantidad del ingrediente en un solo array? Como ustedes lo vean mejor. También
a saber si Vicky lo aceptaría. Por lo mientra solo deje un array para los nombres.

Para resultados se buscará por nombre, id.
Se mostrará una tabla con cada receta y sus características


*/

namespace ProyectoFinal
{
    class Program { 
		public struct Receta {
			public int id;
			public string nombre;
			public string[] ingredientes; // TODO tal vez cambiarlo, ver inicio del archivo
			public int porciones;
		}
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
                Console.WriteLine($"{numTitulos+1}-SALIR\n");

				return validacionInt(1, numTitulos+1, "Deme la opción deseada:");
			}
			
			void entradaDatos(int numRecetas) {
				for (int i = 0; i < numRecetas; i++) {

					Console.WriteLine($"\nA continuación ingresará la información de la receta {i+1}");
					
					Receta currRecipe = new Receta();
					currRecipe.id = i;
					currRecipe.nombre = obtenerDato("Ingrese el nombre de la receta:");
					currRecipe.porciones = validacionInt(0, 100, "Ingrese el número de porciones para las que la receta rinde:");
					
					int numIngredientes = validacionInt(0, 100, "Ingrese el número de ingredientes necesarios para realizar la receta:");
					currRecipe.ingredientes = new string[numIngredientes];

					for (int j = 0; j < numIngredientes; j++) {
						currRecipe.ingredientes[j] = obtenerDato($"Ingrese el nombre del ingrediente {j+1}:");
					}
	
				}
				
			}
		
			// INICIO PROPIO DEL PROGRAMA ------------------------------------------------------------------
			
			
			// numero de recetas que se ingresarán al sistema
			int nRecetas = validacionInt(0, 10, "¿Cuántas recetas vas a registar?");
			Receta[] recetas = new Receta[nRecetas];
			
			int opc;
			do 
			{
				opc = obtenerOpcMenu(new string[] {"Entrada de datos", "Uso de recetas"}, "MENU PRINCIPAL");
				switch (opc)
				{
					case 1:
						entradaDatos(nRecetas);
						break;
					case 2:
						//usoDeRecetas();
						break;
					case 3:
						// salida de menú, termina ejecución del programa
						break;
				}
			} while (opc != 3);
			
			
			
			
		}
	} 
}
