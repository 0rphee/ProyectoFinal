namespace ProyectoFinal
{
    class Program { 
	    /* 
	    The structure that will be applied through out all the program, in which we will be able to create the outputed table in which the input of the user will be showed. In this structure, some inputs will also be used for some Math equations, mostly for the outputed price. 
	    The intended use for each class inside the structure is as follows:
	    - 'id' will be used for the given item number, we find this in 'string' format for the unusable numbers that other classifications change or ignore. The maximmum lenght of characters (char) will be 5.
	    - 'precio' shows the price of the clothing, its uses 'double' data type for the decimal use and for the discount.
	    - 'nombre' will show the name of the clothing, the name uses 'string' for the usage of characters. Its important here that the name of the clothes needs to be inserted completely and correctly.
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
			

			// Input that the user will type inside the console, the computer will place it as a string data type.
			string obtenerDato(string textoAMostrar) {
				Console.Write(textoAMostrar + " ");
				return Console.ReadLine();
			}

			// This function is the validation that checks if all the variables with the data type 'int' are within the stablished maximum and mimimum limits (interval).
			// The explanation used for this function works for all the other validations as well, int, string, and double validations.
			int validacionInt(int limInf, int limSup, string textoAMostrar) {
				int dato;
				do {
					dato = int.Parse(obtenerDato(textoAMostrar)); // the variable 'dato' will be the input of the user. It is converted from 'string' to 'int' data type.

					if (dato < limInf || limSup < dato){
						Console.WriteLine($"'{dato}' no es una opción válida, debe estar entre {limInf} y {limSup}"); // If 'dato' is within the interval (limInf,LimSup) the input is registered in the database, otherwise it will output the text and ask again for an input.
					}

				} while (dato < limInf || limSup < dato); // Makes sure to ask again for an input when the interval is not satisfied.
				return dato; // Saves the input in the database
			}

			// This function is the validation that checks if all the variables with the data type 'double' are within the stablished maximum and mimimum limits (interval).
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

			// This function is the validation that checks if all the variables with the data type 'string' are within the stablished maximum and mimimum limits (interval).
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

			// Generates the corresponding Menu and returns an option of the menu.
			int obtenerOpcMenu(string[] titulos, string mainTitle) {
				int numTitulos = titulos.Length;

				Console.WriteLine("\n" + mainTitle);

				for (int i = 0; i < numTitulos; i++) {
					Console.WriteLine($"{i + 1}-{titulos[i]}");
				}
                Console.WriteLine($"{numTitulos+1}-SALIR\n");

				return validacionInt(1, numTitulos+1, "Deme la opción deseada:");
			}
			
			// The array used for the second option in the menu ('case 2')
			void entradaDatos(Prenda[] prendas) {
				for (int i = 0; i < prendas.Length; i++) { // Loops all the next commands according to the stablished size of the array that was asked at the beginning of the program to the user.

					Console.WriteLine($"\nA continuación ingresará la información de la prenda {i+1}"); // Reminds the user in which clothing he is.
					
					Prenda currPrenda = new Prenda();
					currPrenda.id = validacionLength(5, 5, "Ingrese el ID de la prenda:"); // The input that will represent the ID. the function 'validacionLength' will place the validation in the input of the user, as we see: (5,5, ....) places the corresponding values to (limInf, limSup, .....), as such the ID needs to be equal to 5 characters.
					currPrenda.nombre = validacionLength(0, 30, "Ingrese el nombre de la prenda:"); // The input for the name with it's corresponding validation, it will be limited from 0 to 30 characters.
					currPrenda.precio = validacionDouble(0, 10000, "Ingrese el precio de la prenda: $"); // The input of the user with the validation that limits the price from 0 up to 10,000. 

					currPrenda.unidadesTalla = new int[3]; // We stablish the lenght of the array, in which we will determine the number of sizes available.
					string[] nombreTallas = new string[3] {"CHICA", "MEDIANA", "GRANDE"};
					
					// This loop will cycle to each size, so that the user can input the corresponding response.
					for (int k = 0; k < currPrenda.unidadesTalla.Length; k++){
						currPrenda.unidadesTalla[k] = int.Parse(obtenerDato($"Ingrese el número de unidades de la talla {nombreTallas[k]}:")); // The input of the user that will determine the quantity of clothes in each size.
					}

					currPrenda.fechaEntrada = DateTime.Parse(obtenerDato("Dame la fecha de la prenda dd/mm/yyyy:"));
					currPrenda.fechaOferta = currPrenda.fechaEntrada.Add(TimeSpan.FromDays(30)) ; 

					prendas[i] = currPrenda;
				}
				ordenación (Prenda[] prendas);
			}
			
			Prenda[] newArray(Prenda[] prendas, int LimInf, int LimSup) // los limInf yLimSup son los indices del array
			{
				Prenda[] NewArr = new Prenda[LimSup-LimInf];
				for (int k = 0; k < NewArr.Length; k++)
				{
					NewArr[k] = prendas [LimInf+k]
				}
				return NewArr
			}
			
			// Bubble Sort
			static void ordenación(Prenda[] prendas) 
			{
				Prenda aux; // We create an extra variable to aid us in the change of position
				for(int i = 0; i < prendas.Length-1; i++) // Loop for all the available clothes
				{
					for (int j = 0; j < prendas.Length-1-i; j++) // Another loop for the other n-1 clothes that need to be compared to the already selected one.
					{
						if (prendas[j] < prendas[j+1]) // will compare if the clothing is lower than the next one. If it is, it will do the following:
						{
							aux = prendas[r]; // saves the clothinng in the auxiliar, so its leaves 'prendas[r]' empty.
							prendas[r] = prendas [r + 1]; // exchanges the values from 'prendas[r + 1]' to 'prendas[r]', now 'prendas[r + 1]' is empty.
							prendas[r + 1] = aux; // Places the value from the variable 'aux' to 'prendas[r + 1]'.
						}
					}
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
			
			// Outputs the SubMenu that was choosen in the first Menu, the second option (case 2).
			void entregaResultados(Prenda[] prendas){
				int opc;
				do
				{
					opc = obtenerOpcMenu(new string[] {"Mostrar listado de prendas", "Buscar por ID", "Búsqueda por nombre", "Mostrar cinco prendas con mayor costo", "Mostrar cinco prendas con menor costo"}, "SUBMENÚ");
					switch (opc)
					{
						case 1:
							// Outputs the complete database in a table format
							Console.WriteLine(crearListaPrendas(prendas));
							break;
						case 2: 
							// Search of a specific piece of clothing by the ID.
							string idBuscar = obtenerDato("Ingrese el ID de prenda a encontrar:");
							Console.WriteLine(busquedaID(prendas, idBuscar)); 
							break;
						case 3:
							// Search for a specific piece of clothing by the name.
							string nombreBuscar = obtenerDato("Ingrese el nombre de prenda a encontrar:");
							Console.WriteLine(busquedaNombre(prendas, nombreBuscar));
							break;
						case 4:
							// Outputs, in table format, the 5 most expensive items.
							newArray(prendas, 0, 4);
							Console.WriteLine(crearListaPrendas(NewArr));
							break;
						case 5:
							// Outputs, in table format, the 5 most affordable clothes.
							newArray(prendas, prendas.Length-6, prendas.Length-1);
							Console.WriteLine(crearListaPrendas(NewArr));
							break;
						case 6:
							// Backs out to the first Menu
							break;
					}
				} while (opc != 6 ); // If the choosen option is bigger than 6 it will repeat the menu. 
			}
			
			// He next line of code searches the input of the user in the ID section.
			string busquedaID(Prenda[] prendas, string idEncontrar){
				for (int i = 0; i < prendas.Length; i++){ // It will loop the search for each clothing item inside the database.
					if (prendas[i].id == idEncontrar){ 
						return crearListaPrendas(new Prenda[] {prendas[i]}); // TODO arreglar impresion
					} // if the 'if' statement is satisfied it will stop the loop and output all the information of the clothing item, otherwise, the it will continue on with the next item and so on.
				}
				return $"No se encontró ninguna prenda con el ID: {idEncontrar}"; // In the event that the if statement does not find anything and the loop has closed it will print the line of text.
			}
			
			// He next line of code searches the input of the user in the Name section.
			string busquedaNombre(Prenda[] prendas, string nombreEncontrar){
			for (int i = 0; i < prendas.Length; i++){ // It will loop the search for each clothing item inside the database.
					if (prendas[i].nombre == nombreEncontrar){
						return crearListaPrendas(new Prenda[] {prendas[i]}); // TODO arreglar impresion
					} // if the 'if' statement is satisfied it will stop the loop and output all the information of the clothing item, otherwise, the it will continue on with the next item and so on.
				}
				return $"No se encontró ninguna prenda con el nombre: {nombreEncontrar}"; // In the event that the if statement does not find anything and the loop has closed it will print the line of text.
			}
			
			// This is te test unit that is used to quickly test the code, without the neccesity of placing the inputs ourselves each time we need to test something.
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

			// PROPER BEGINNING OF THE PROGRAM ------------------------------------------------------------------
			
			// TEST PRINT, we use this command so that the computer outputs the test unit of an example that simulates the input the user could place.
			test();

			// Input of the number of clothes that the user will 
			int nPrendas = validacionInt(1, 100, "¿Cuántas prendas vas a registar?"); // TODO definir numero minimo de prendas
			Prenda[] prendas = new Prenda[nPrendas];
			
			
			
			int opc; // create the variable 'opc' for the menu, its usage is when choosing an option isnide of the menu.
			bool datosRegistrados = false; // This statement is for the case that the user inputs the second option but hasn't registered the data.
			
			do 
			{
				opc = obtenerOpcMenu(new string[] {"Entrada de datos", "Muestra de resultados"}, "MENU PRINCIPAL");
				switch (opc)
				{
					case 1:
						entradaDatos(prendas); // we go directly to the array the user in which the user will input all the data needed for the program.
						datosRegistrados = true; // By converting the boolean from 'false' to 'true' we say that data has been registered inside the database.
						break;
					case 2:
						/*
						With this 'if' statement we check if the user already choose 'case 1' and registered the data.
						The 'if' statement evalutes if the statement is true or false, by default it's in that order. As such, in this program, it will check if the 'datosRegistrados' variable is either 'true' or 'false'.
						*/
						if (datosRegistrados){
							entregaResultados(prendas); // If the variable is 'true' it will send the user into the array which displays the SubMenu, that is for the results..
						} else{
							Console.WriteLine("No se han registrado prendas aún. Registra prendas antes de ver resultados."); // If the variable is 'false' it will output the message and back out to the menu once again.
						}
						break;
					case 3:
						// This option closes the menu, terminate the execution of the program.
						break;
				} // In each case we use the break command so that the program stops and backsteps into the corresponding output.
			} while (opc != 3);
		}
	} 
}
