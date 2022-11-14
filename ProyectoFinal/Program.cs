
namespace ProyectoFinal
{
    class Program { 
		public struct Prenda {
			public string id;
			public double precio;
			public string nombre;
			public int[] unidades;
			public string[] tallas; 
			public DateTime fechaEntrada;
			public DateTime fechaOferta;
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

					if (dato < limInf || limSup < dato){
						Console.WriteLine($"'{dato}' no es una opción válida");
					}

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
			
			void entradaDatos(int numPrendas) {
				for (int i = 0; i < numPrendas; i++) {

					Console.WriteLine($"\nA continuación ingresará la información de la receta {i+1}");
					
					Prenda currPrenda = new Prenda();
					currPrenda.id = obtenerDato("Ingrese el ID de la prenda:");
					currPrenda.nombre = obtenerDato("Ingrese el nombre de la prenda:");

					int nTallas = validacionInt(0,10, "Ingrese el número de tallas disponibles para esta prenda:");
					currPrenda.tallas = new string[nTallas];
					currPrenda.unidades= new int[nTallas];

					for (int k = 0; k < nTallas; k++){
						currPrenda.tallas[k] = obtenerDato($"Ingrese el nombre de la talla número {k+1}:");
						currPrenda.unidades[k] = int.Parse(obtenerDato($"Ingrese el número de unidades de la talla {currPrenda.tallas[k]}:"));
					}

					currPrenda.fechaEntrada = DateTime.Parse(obtenerDato("Dame la fecha de la prenda dd/mm/yyyy:"));
					
					}
				}
			}
	
			// INICIO PROPIO DEL PROGRAMA -----------------kkk-------------------------------------------------
			
			// numero de recetas que se ingresarán al sistema
			int nPrendas = validacionInt(5, 100, "¿Cuántas prendas vas a registar?");
			Prenda[] recetas = new Prenda[nPrendas];
			
			int opc;
			do 
			{
				opc = obtenerOpcMenu(new string[] {"Entrada de datos", "Uso de recetas"}, "MENU PRINCIPAL");
				switch (opc)
				{
					case 1:
						entradaDatos(nPrendas);
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
