namespace ProyectoFinal
{
    class Program { 
	    /* 
	    The structure that will be applied through out all the program, in which we will be able to create the outputed table in which the input of the user will be showed. In this structure, some inputs will also be used for some Math equations, mostly for the outputed price. 
	    The intended use for each class inside the structure is as follows:
	    - 'id' will be used for the given item number, we find this in string format for the unusable numbers that other classifications change or ignore. The maximmum lenght of characters (char) will be 5.
	    - 'precio' shows the price of the clothing, its uses double classification for the decimal use and for the discount.
	    - 'nombre' will show the name of the clothing, the name uses string for the usege of characters. Its important here that the name of the clothes needs to be inserted completely and correctly.
	    - 'unidadesTalla' shows the Size of the clothing, in this class we will insert either of the following: 'chico'/'mediano'/'grande'. Once the user inputs one of the options showed before there will be a +1 made on the according size.
	    - 'fechaEntrada' will be used to input the date in which the desired clothing is registered in the database, the format of the date will be dd / mm / yyyy.
	    - 'fechaOferta' will be used for the registration of the time that the clothing has stayed in the database, the format will be the same as the one before.
	    */
		public struct Prenda {
			public string id;
			public double precio; // max 5 char
  			public string nombre;
			public int[] unidadesTalla; // chico/mediano/grande
			public DateTime fechaEntrada; // dd/mm/yyyy
			public DateTime fechaOferta; // dd/mm/yyyy
		}
	    
	    	// The start of the Main program
		static void Main(string[] args) {
			

			// imprime a consola, devuelve string
			string obtenerDato(string textoAMostrar) {
				Console.Write(textoAMostrar + " ");
				return Console.ReadLine();
			}

			// valida que un int se encuentre dentro de límites definidos
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

			// valida que un double se encuentre dentro de límites definidos
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

			// valida que un string tenga un tamaño entre límites definidos
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

			// Genera menu y devuelve una opcion del menú
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
					string[] nombreTallas = new string[3] {"CHICA", "MEDIANA", "GRANDE"};

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
		
			string crearFilaTitulos(){
				string[] nombreTallas = new string[3] {"CHICA", "MEDIANA", "GRANDE"};
				string fila;
				fila = formatearPropiedad("ID", -5);
				fila += formatearPropiedad("PRECIO", -7);
				fila += formatearPropiedad("NOMBRE", -20);
				fila += formatearTituloTallas(nombreTallas, -7);
				fila += formatearPropiedad("FECHA DE ENTRADA", -16);
				fila += formatearPropiedad("FECHA DE OFERTA", -16);
				return fila;
			}
			
			string crearFilaPrenda(Prenda prenda){
				string fila;
				fila = formatearPropiedad(prenda.id, -5);
				fila += formatearPropiedad($"$ {prenda.precio.ToString()}", -7);
				fila += formatearPropiedad($"{prenda.nombre}", -20);
				fila += formatearTallas(prenda.unidadesTalla, -7);
				fila += formatearPropiedad($"{prenda.fechaEntrada.ToString("d")}", -16);
				fila += formatearPropiedad($"{prenda.fechaOferta.ToString("d")}", -16);
				return fila;
			}
			
			
			string crearListaPrendas(Prenda[] prendas){
				string titulos = crearFilaTitulos(); 
				string topList = String.Concat(Enumerable.Repeat("-", titulos.Length));
				string listado = "\n" + topList + "\n" + titulos + "\n" + topList + "\n";
				for (int i = 0; i < prendas.Length; i++){
					listado += $"{crearFilaPrenda(prendas[i])}\n";
				}
				return listado + topList + "\n";
			}
			
			void entregaResultados(Prenda[] prendas){
				int opc;
				do
				{
					opc = obtenerOpcMenu(new string[] {"Mostrar listado de prendas", "Buscar por ID", "Búsqueda por nombre", "Mostrar cinco prendas con mayor costo", "Mostrar cinco prendas con menor costo"}, "SUBMENÚ");
					switch (opc)
					{
						case 1:
							Console.WriteLine(crearListaPrendas(prendas));
							break;
						case 2:
							string idBuscar = obtenerDato("Ingrese el ID de prenda a encontrar:");
							Console.WriteLine(busquedaID(prendas, idBuscar)); 
							break;
						case 3:
							string nombreBuscar = obtenerDato("Ingrese el nombre de prenda a encontrar:");
							Console.WriteLine(busquedaNombre(prendas, nombreBuscar));
							break;
						case 4:
							// mayoresCinco(); TODO añadir métodos para mayores números
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
						return crearListaPrendas(new Prenda[] {prendas[i]}); // TODO arreglar impresion
					}
				}
				return $"No se encontró ninguna prenda con el ID: {idEncontrar}";
			}
			
			string busquedaNombre(Prenda[] prendas, string nombreEncontrar){
			for (int i = 0; i < prendas.Length; i++){
					if (prendas[i].nombre == nombreEncontrar){
						return crearListaPrendas(new Prenda[] {prendas[i]}); // TODO arreglar impresion
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
				
				Console.WriteLine(crearListaPrendas(new Prenda[1] {testprenda}));
			}			

			// INICIO PROPIO DEL PROGRAMA ------------------------------------------------------------------
			
			// TEST IMPRESION
			test();

			// numero de prendas que se ingresarán al sistema
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
							Console.WriteLine("No se han registrado prendas aún. Registra prendas antes de ver resultados.");
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
