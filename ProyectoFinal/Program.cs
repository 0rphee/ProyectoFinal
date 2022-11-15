namespace ProyectoFinal
{
    class Program { 
		public struct Prenda {
			public string id;
			public double precio;
  			public string nombre;
			public int[] unidadesTalla; // chico/mediano/grande TODO cambiar donde pide nombres de talla, e impresion de lista
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
						Console.WriteLine($"'{dato}' no es una opción válida, debe estar entre {limInf} y {limSup}");
					}

				} while (dato < limInf || limSup < dato);
				return dato;
			}

			double validacionDouble(double limInf, double limSup, string textoAMostrar) {
				double dato;
				do {
					dato = double.Parse(obtenerDato(textoAMostrar));

					if (dato < limInf || limSup < dato){
						Console.WriteLine($"'{dato}' no es una opción válida, debe estar entre {limInf} y {limSup}");
					}

				} while (dato < limInf || limSup < dato);
				return dato;
			}

			string validacionLength(int limInf, int limSup, string textoAMostrar) {
				string dato;
				bool condicion;
				do {
					dato = obtenerDato(textoAMostrar);
					
					condicion = dato.Length < limInf || limSup < dato.Length;
					if (condicion){
						Console.WriteLine($"'{dato}' no es una opción válida, debe estar tener longitud de entre {limInf} y {limSup}");
					}

				} while (condicion);
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
			
			void entradaDatos(Prenda[] prendas) {
				for (int i = 0; i < prendas.Length; i++) {

					Console.WriteLine($"\nA continuación ingresará la información de la prenda {i+1}");
					
					Prenda currPrenda = new Prenda();
					currPrenda.id = validacionLength(5, 5, "Ingrese el ID de la prenda:");
					currPrenda.nombre = validacionLength(0, 30, "Ingrese el nombre de la prenda:"); // TODO añadir validación por longitud de nombre
					currPrenda.precio = validacionDouble(0, 10000, "Ingrese el precio de la prenda: $");

					currPrenda.unidadesTalla = new int[3];
					string[] nombreTallas = new string[3] {"chica", "mediana", "grande"};

					for (int k = 0; k < currPrenda.unidadesTalla.Length; k++){
						currPrenda.unidadesTalla[k] = int.Parse(obtenerDato($"Ingrese el número de unidades de la talla {nombreTallas[k]}:"));
					}

					currPrenda.fechaEntrada = DateTime.Parse(obtenerDato("Dame la fecha de la prenda dd/mm/yyyy:"));
					currPrenda.fechaOferta = currPrenda.fechaEntrada.Add(TimeSpan.FromDays(30)) ; 
					prendas[i] = currPrenda;
				}

			}
			
			string formatearPropiedad(string prop, int espacios){
				return String.Format($"| {{0, {espacios}}} |", prop);
			}
			
			string formatearTallas(int[] arrUnidadesTallas, int espacios){
				string section = "";
				
				for (int i = 0; i <  arrUnidadesTallas.Length; i++){
					section += String.Format($"| {{0, {espacios}}} |", arrUnidadesTallas[i]); 
				}
				return section;
			}

			string formatearTituloTallas(string[] arrNombresTallas, int espacios){
				string section = "";
				
				for (int i = 0; i <  arrNombresTallas.Length; i++){
					section += String.Format($"| {{0, {espacios}}} |", arrNombresTallas[i]); 
				}
				return section;
			}
		
			string mostrarLineaTitulos(){
				string[] nombreTallas = new string[3] {"chica", "mediana", "grande"};
				string fila;
				fila = formatearPropiedad("ID", -5);
				fila += formatearPropiedad("PRECIO", -7);
				fila += formatearPropiedad("NOMBRE", -20);
				fila += formatearTituloTallas(nombreTallas, -7);
				fila += formatearPropiedad("FECHA DE ENTRADA", -16);
				fila += formatearPropiedad("FECHA DE OFERTA", -16);
				return fila;
			}
			
			string mostrarLineaPrenda(Prenda prenda){
				string fila;
				fila = formatearPropiedad(prenda.id, -5);
				fila += formatearPropiedad($"$ {prenda.precio.ToString()}", -7);
				fila += formatearPropiedad($"{prenda.nombre}", -20);
				fila += formatearTallas(prenda.unidadesTalla, -7);
				fila += formatearPropiedad($"{prenda.fechaEntrada.ToString("d")}", -16);
				fila += formatearPropiedad($"{prenda.fechaOferta.ToString("d")}", -16);
				return fila;
			}
			
			
			string mostrarListado(Prenda[] prendas){
				string titulos =  mostrarLineaTitulos(); 
				string topList = String.Concat(Enumerable.Repeat("-", titulos.Length));
				string listado = topList + "\n" + titulos + "\n" + topList + "\n";
				for (int i = 0; i < prendas.Length; i++){
					listado += $"{mostrarLineaPrenda(prendas[i])}\n";
				}
				return listado + topList;
			}
			
			void entregaResultados(Prenda[] prendas){
				int opc;
				do
				{
					opc = obtenerOpcMenu(new string[] {"Mostrar listado de prendas", "Buscar por ID", "Búsqueda por nombre", "Mostrar cinco prendas con mayor costo", "Mostrar cinco prendas con menor costo"}, "SUBMENÚ");
					switch (opc)
					{
						case 1:
							Console.WriteLine(mostrarListado(prendas));
							break;
						case 2:
							string idBuscar = obtenerDato("Ingrese el ID de prenda a encontrar:");
							Console.WriteLine(busquedaID(prendas, idBuscar)); 
							break;
						case 3:
							string nombreBuscar= obtenerDato("Ingrese el nombre de prenda a encontrar:");
							Console.WriteLine(busquedaNombre(prendas, nombreBuscar));
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
						return mostrarListado(new Prenda[] {prendas[i]}); // TODO arreglar impresion
					}
				}
				return $"No se encontró ninguna prenda con el ID: {idEncontrar}";
			}
			
			string busquedaNombre(Prenda[] prendas, string nombreEncontrar){
			for (int i = 0; i < prendas.Length; i++){
					if (prendas[i].nombre == nombreEncontrar){
						return mostrarListado(new Prenda[] {prendas[i]}); // TODO arreglar impresion
					}
				}
				return $"No se encontró ninguna prenda con el nombre: {nombreEncontrar}";
			}
			
						
			void test(){
				Prenda testprenda = new Prenda();
				testprenda.id = "12345";
				testprenda.nombre = "adsljkfsd";
				testprenda.precio = 34242;
				testprenda.unidadesTalla = new int[3] {1,0,0};
				testprenda.fechaEntrada = DateTime.Today;
				testprenda.fechaOferta = DateTime.Today;
				
				Console.WriteLine(mostrarListado(new Prenda[1] {testprenda}));
	
			}			
						// INICIO PROPIO DEL PROGRAMA ------------------------------------------------------------------
			
			// TEST IMPRESION
			test();

			// numero de recetas que se ingresarán al sistema
			int nPrendas = validacionInt(1, 100, "¿Cuántas prendas vas a registar?"); // TODO definir numero minimo de prendas
			Prenda[] prendas = new Prenda[nPrendas];
			
			int opc;
			bool datosRegistrados = false;
			do 
			{
				opc = obtenerOpcMenu(new string[] {"Entrada de datos", "Muestra de resultados"}, "MENU PRINCIPAL");
				switch (opc)
				{
					case 1:
						entradaDatos(prendas);
						datosRegistrados = true;
						break;
					case 2:
						if (datosRegistrados){
						entregaResultados(prendas);
						} else{
							Console.WriteLine("No se han registrado prendas aún.");
						}
						break;
					case 3:
						// salida de menú, termina ejecución del programa
						break;
				}
			} while (opc != 3);
		}
	} 
}
