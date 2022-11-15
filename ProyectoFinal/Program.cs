
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
					currPrenda.fechaOferta = currPrenda.fechaEntrada.Add(TimeSpan.FromDays(30)) ; 
				}
			}
			
			string formatearPropiedad(string prop){
				return String.Format("|{0, -45}|", prop);
			}
			
			string mostrarLineaPrenda(Prenda prenda){
				string fila;
				fila = formatearPropiedad(formatearPropiedad(prenda.id));
				fila += formatearPropiedad(formatearPropiedad($"{prenda.precio.ToString()}"));
				// TODO terminar fila += formatearPropiedad(formatearPropiedad(prenda.precio));
				return fila;
			}
			
			void entregaResultados(Prenda[] prendas){
				int opc;
				do
				{
					opc = obtenerOpcMenu(new string[] {"Mostrar listado de prendas", "Buscar por ID", "Búsqueda por nombre", "Mostrar cinco prendas con mayor costo", "Mostrar cinco prendas con menor costo"}, "SUBMENÚ");
					switch (opc)
					{
						case 1:
							// mostrarListado();
							break;
						case 2:
							// TODO BUSQUEDA ID Console.WriteLine(busquedaID())
							break;
						case 3:
							// busquedaNombre();
							break;
						case 4:
							// mayoresCinco();
							break;
						case 5:
							// menoresCinco();
							break;
						case 6:
							break;
					}
				} while (opc != 6 );
			}
			
			string busquedaID(Prenda[] prendas, string idEncontrar){
				for (int i = 0; i < prendas.Length; i++){
					if (prendas[i].id == idEncontrar){
						return ""; // TODO metodo para imprimir bonito prenda
					}
				}
				return $"No se encontró ninguna prenda con el ID: {idEncontrar}";
			}
			
			string busquedaNombre(Prenda[] prendas, string nombreEncontrar){
				for (int i = 0; i < prendas.Length; i++){
					if (prendas[i].nombre == nombreEncontrar){
						return ""; // TODO metodo para imprimir bonito prenda
					}
				}
				return $"No se encontró ninguna prenda con el nombre: {nombreEncontrar}";
			}
	
			// INICIO PROPIO DEL PROGRAMA ------------------------------------------------------------------
			
			// numero de recetas que se ingresarán al sistema
			int nPrendas = validacionInt(5, 100, "¿Cuántas prendas vas a registar?");
			Prenda[] prendas = new Prenda[nPrendas];
			
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
						entregaResultados(prendas);
						break;
					case 3:
						// salida de menú, termina ejecución del programa
						break;
				}
			} while (opc != 3);
		}
	} 
}
