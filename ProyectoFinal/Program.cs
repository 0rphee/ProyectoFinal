namespace ProyectoFinal
{
    class Program {
        /*
        The structure that will be applied throughout all the program, in which we will be able to create the outputed table in which the
        input of the user will be shown. In this structure, some inputs will also be used for some Math equations, mostly for the outputted
        price.The intended use for each property of the structure is detailed as follows:
 
        - 'id' will be used for the given item id number. The id is stored in 'string format for the unusable numbers that would be valid
          for an id, but ignored by an 'int' type. The maximum length of the string is 5.
        - 'precio' stores the price of the clothing item as a 'double' data type. Due to it being a price, it needs to use decimal values.
        - 'nombre' stores the name of the clothing item. Its important here that the name of the clothes needs to be inserted completely and correctly.
        - 'unidadesTalla' stores the units per size of the clothing item. It will store the items available for the next size classification 'chico'/'mediano'/'grande'.
        - 'fechaEntrada' stores the date in which the desired clothing is registered into the inventory, the format of the date will be dd / mm / yyyy.
        - 'fechaOferta' stores the date at which the item will be put on discount. It is calculated by adding 30 days to 'fechaEntrada'
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

            // Method that asks for user input to be typed in the console, returning it as a string value.
            string obtenerDato(string textoAMostrar) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(textoAMostrar + " ");
                return Console.ReadLine();
            }

            // This function validates that an input of type 'int' is within an established maximum and minimum limits (interval).
            int validacionInt(int limInf, int limSup, string textoAMostrar) {
                int dato;
                do {
                    dato = int.Parse(obtenerDato(textoAMostrar)); // the variable 'dato' will be the input of the user. It is converted from 'string' to 'int' data type.

                    if (dato < limInf || limSup < dato) {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"'{dato}' no es una opción válida, debe estar entre {limInf} y {limSup}"); // If 'dato' is within the interval (limInf,LimSup) the input is registered in the database, otherwise it will output the text and ask again for an input.
                        Console.ResetColor();
                    }

                } while (dato < limInf || limSup < dato); // Makes sure to ask again for an input when the interval is not satisfied.
                return dato; // Saves the input in the database
            }

            // This function validates that an input of type 'double' is within an established maximum and minimum limits (interval).
            double validacionDouble(double limInf, double limSup, string textoAMostrar) {
                double dato;
                do {
                    dato = double.Parse(obtenerDato(textoAMostrar));

                    if (dato < limInf || limSup < dato) {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"'{dato}' no es una opción válida, debe estar entre {limInf} y {limSup}");
                        Console.ResetColor();
                    }

                } while (dato < limInf || limSup < dato);
                return dato;
            }

            // This function validates that the length of an input of type 'string' is within an established maximum and minimum limits (interval).
            string validacionLength(int limInf, int limSup, string textoAMostrar) {
                string dato;
                bool condicion;
                do {
                    dato = obtenerDato(textoAMostrar);

                    condicion = dato.Length < limInf || limSup < dato.Length;
                    if (condicion) {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"'{dato}' no es una opción válida, debe estar tener longitud de entre {limInf} y {limSup}");
                        Console.ResetColor();
                    }

                } while (condicion);
                return dato;
            }

            // Generates a menu from an array of titles and returns the user-selected option for the menu.
            int obtenerOpcMenu(string[] titulos, string mainTitle) {
                int numTitulos = titulos.Length;

                Console.WriteLine("\n" + mainTitle);

                for (int i = 0; i < numTitulos; i++) {
                    Console.WriteLine($"{i + 1}-{titulos[i]}");
                }
                Console.WriteLine($"{numTitulos + 1}-SALIR\n");

                return validacionInt(1, numTitulos + 1, "Deme la opción deseada:");
            }

            // Method that asks for the items to be stored. It's the second option in the main menu ('case 2')
            void entradaDatos(Prenda[] prendas) {
                // Loops through all the next commands necessary to create a new clothing item according to the number of items to be registered by the user (established at the beginning of the program).  
                for (int i = 0; i < prendas.Length; i++) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nA continuación ingresará la información de la prenda {i + 1}"); // Reminds the user which clothing item they are registering.
                    Console.ResetColor();

                    Prenda currPrenda = new Prenda();

                    // The input that will represent the ID. the function 'validacionLength' will validate the input of the user to be of Length == 5.
                    // As we see: (5,5,...) assigns the corresponding values to (limInf, limSup, .....), thus needing the input to be equal to 5 characters long.
                    currPrenda.id = validacionLength(5, 5, "Ingrese el ID de la prenda:");

                    // The input for the name with it's corresponding validation, it will be limited from 0 to 30 characters.
                    currPrenda.nombre = validacionLength(0, 20, "Ingrese el nombre de la prenda:");

                    // The input of the user with the validation that limits the price to be from 0 up to 10,000.
                    currPrenda.precio = validacionDouble(0, 10000, "Ingrese el precio de la prenda: $");

                    string[] nombreTallas = new string[3] { "CHICA", "MEDIANA", "GRANDE" };
                    currPrenda.unidadesTalla = new int[3]; // We establish the length of the array, in which we will determine the number of sizes available.

                    // This loop will cycle through each size, so that the user can input the corresponding response.
                    for (int k = 0; k < currPrenda.unidadesTalla.Length; k++) {
                        // The input of the user that will determine the quantity of clothes available for each size.
                        currPrenda.unidadesTalla[k] = validacionInt(0, 100, $"Ingrese el número de unidades de la talla {nombreTallas[k]}:");
                    }

                    currPrenda.fechaEntrada = DateTime.Parse(obtenerDato("Deme la fecha de entrada de la prenda dd/mm/yyyy:"));
                    currPrenda.fechaOferta = currPrenda.fechaEntrada.Add(TimeSpan.FromDays(30));

                    prendas[i] = currPrenda;
                }
                ordenación(prendas);
            }

            // Method for obtaining an array from a subsection of another. indiceInf and indiceSup are the indices indicating where the values of the new array will come from            
            Prenda[] newArray(Prenda[] prendas, int indiceInf, int indiceSup) {
                Prenda[] NewArr = new Prenda[indiceSup - indiceInf];
                for (int k = 0; k < NewArr.Length; k++) {
                    NewArr[k] = prendas[indiceInf + k];
                }
                return NewArr;
            }

            // Bubble Sorting, by the price a 'Prenda 'array
            static void ordenación(Prenda[] prendas) {
                Prenda aux; // We create an extra variable to aid us in the change of position
                for (int i = 0; i < prendas.Length - 1; i++){ // Loop for all the available clothes 
                    for (int j = 0; j < prendas.Length - 1 - i; j++){ // Another loop for the other n-1 clothes that need to be compared to the already selected one. 
                        if (prendas[j].precio < prendas[j + 1].precio){ // will compare if the clothing is lower than the next one. If it is, it will do the following: 
                            aux = prendas[j]; // saves the clothing in the auxiliar var, so its leaves 'prendas[j]' empty.
                            prendas[j] = prendas[j + 1]; // exchanges the values from 'prendas[j+ 1]' to 'prendas[j]', now 'prendas[j + 1]' is empty.
                            prendas[j + 1] = aux; // Places the value from the variable 'aux' to 'prendas[j + 1]'.
                        }
                    }
                }
            }

            // This function formats a string to a specific length of spaces
            string formatearPropiedad(string prop, int espacios) {
                return String.Format($"| {{0, {espacios}}} |", prop);
            }

            // This function formats the number of units for every size in the array to the amount of spaces that you want
            string formatearTallas(int[] arrUnidadesTallas, int espacios) {
                string section = "";

                for (int i = 0; i < arrUnidadesTallas.Length; i++) {
                    section += String.Format($"| {{0, {espacios}}} |", arrUnidadesTallas[i]);
                }
                return section;
            }

            // This function formats every title in the array to the amount of spaces that you want
            string formatearTituloTallas(string[] arrNombresTallas, int espacios) {
                string section = "";

                for (int i = 0; i < arrNombresTallas.Length; i++) {
                    section += String.Format($"| {{0, {espacios}}} |", arrNombresTallas[i]);
                }
                return section;
            }

            // This function outputs row with the headers that will be found at the beginning of the table.
            string crearFilaTitulos() {
                string[] nombreTallas = new string[3] { "CHICA", "MEDIANA", "GRANDE" };
                string fila;
                fila = formatearPropiedad("ID", -5);
                fila += formatearPropiedad("PRECIO", -7);
                fila += formatearPropiedad("NOMBRE", -20);
                fila += formatearTituloTallas(nombreTallas, -7);
                fila += formatearPropiedad("FECHA DE ENTRADA", -16);
                fila += formatearPropiedad("FECHA DE OFERTA", -16);
                return fila;
            }

            // This function outputs the corresponding row for each clothing item.
            string crearFilaPrenda(Prenda prenda) {
                string fila;
                fila = formatearPropiedad(prenda.id, -5);
                fila += formatearPropiedad($"$ {prenda.precio.ToString()}", -7);
                fila += formatearPropiedad($"{prenda.nombre}", -20);
                fila += formatearTallas(prenda.unidadesTalla, -7);
                fila += formatearPropiedad($"{prenda.fechaEntrada.ToString("d")}", -16);
                fila += formatearPropiedad($"{prenda.fechaOferta.ToString("d")}", -16);
                return fila;
            }

            // The function that does the connection with the other functions that make up a part of the complete table.
            string crearListaPrendas(Prenda[] prendas) {
                string titulos = crearFilaTitulos();
                string topList = String.Concat(Enumerable.Repeat("-", titulos.Length));
                string listado = "\n" + topList + "\n" + titulos + "\n" + topList + "\n";
                for (int i = 0; i < prendas.Length; i++) {
                    listado += $"{crearFilaPrenda(prendas[i])}\n";
                }
                return listado + topList + "\n";
            }

            // Outputs the SubMenu after selecting the second option in the main Menu (case 2).
            void entregaResultados(Prenda[] prendas) {
                int opc;
                do
                {
                    opc = obtenerOpcMenu(new string[] { "Mostrar listado de prendas", "Buscar por ID", "Búsqueda por nombre", "Mostrar cinco prendas con mayor costo", "Mostrar cinco prendas con menor costo" }, "SUBMENÚ");
                    switch (opc)
                    {
                        case 1:
                            // Outputs the complete database in a table format.
                            Console.WriteLine(crearListaPrendas(prendas));
                            break;
                        case 2:
                            // Searches for a specific piece of clothing by ID.
                            string idBuscar = obtenerDato("Ingrese el ID de prenda a encontrar:");
                            Console.WriteLine(busquedaID(prendas, idBuscar));
                            break;
                        case 3:
                            // Searches for a specific piece of clothing by name.
                            string nombreBuscar = obtenerDato("Ingrese el nombre de prenda a encontrar:");
                            Console.WriteLine(busquedaNombre(prendas, nombreBuscar));
                            break;
                        case 4:
                            // Outputs in table format, the 5 most expensive items.
                            Console.WriteLine(crearListaPrendas(newArray(prendas, 0, 4)));
                            break;
                        case 5:
                            // Outputs in table format, the 5 most affordable clothes.
                            Console.WriteLine(crearListaPrendas(newArray(prendas, prendas.Length - 6, prendas.Length - 1)));
                            break;
                        case 6:
                            // Backs out to the Main Menu
                            break;
                    }
                } while (opc != 6); // If the chosen option is different to 6 it will ask to select again an option from the submenu.
            }

            // The next line of code searches for the input of the user in the ID section.
            string busquedaID(Prenda[] prendas, string idEncontrar) {
                for (int i = 0; i < prendas.Length; i++)
                { // This will loop through each clothing item inside the database until it finds a match.
                    if (prendas[i].id == idEncontrar)
                    { // when the 'if' statement is satisfied, it will stop the loop and output all the information of the clothing item, otherwise, it will continue on with the next item and so on.
                        return crearListaPrendas(new Prenda[] { prendas[i] });
                    }
                }
                // In the event that the if statement does not find anything and the loop has closed it will print to inform the user of the failure to find an item with the given id.
                return $"No se encontró ninguna prenda con el ID: {idEncontrar}";
            }

            // The next line of code searches for input of the user in the Name section.
            string busquedaNombre(Prenda[] prendas, string nombreEncontrar) {
                for (int i = 0; i < prendas.Length; i++)
                { // This will loop through each clothing item inside the database until it finds a match.
                    if (prendas[i].nombre == nombreEncontrar)
                    { // when the 'if' statement is satisfied, it will stop the loop and output all the information of the clothing item, otherwise, it will continue on with the next item and so on.
                        return crearListaPrendas(new Prenda[] { prendas[i] });
                    }
                }
                // In the event that the if statement does not find anything and the loop has closed it will print inform the user of the failure to find an item with the given name.
                return $"No se encontró ninguna prenda con el nombre: {nombreEncontrar}";
            }

            // This is the test unit that is used to quickly test the printing method, without the necessity of entering the inputs ourselves each time.
            void test() {
                Prenda testprenda = new Prenda();
                testprenda.id = "12345";
                testprenda.nombre = "adsljkfsd";
                testprenda.precio = 34242;
                testprenda.unidadesTalla = new int[3] { 1, 0, 0 };
                testprenda.fechaEntrada = DateTime.Today;
                testprenda.fechaOferta = DateTime.Today;

                Console.WriteLine(crearListaPrendas(new Prenda[1] { testprenda }));
            }

            // METHOD DEFINITIONS END ---------------------------------------------------------------------------
            // PROPER BEGINNING OF THE PROGRAM ------------------------------------------------------------------

            // TEST PRINT, we use this command so that the computer outputs the test unit of an example that simulates the input the user could place.
            test();

            // Input of the number of clothes that the user will register
            int nPrendas = validacionInt(5, 100, "¿Cuántas prendas vas a registar?");
            Prenda[] prendas = new Prenda[nPrendas];

            int opc; // create the variable 'opc' for later choosing an option from the menu.
            bool datosRegistrados = false; // This statement is for the case that the user inputs the second option but hasn't registered the data.

            do
            {
                opc = obtenerOpcMenu(new string[] { "Entrada de datos", "Muestra de resultados" }, "MENU PRINCIPAL");
                switch (opc) {
                    case 1:
                        entradaDatos(prendas); // we input the array in which the user will enter all the data needed for the program.
                        datosRegistrados = true; // By converting the boolean from 'false' to 'true' we say that data has been registered inside the database.
                        break;
                    case 2:
                        // With this 'if' statement we check if the user already chose 'case 1' and registered the data.
                        // The 'if' statement evaluates if the statement is true or false, by default it's in that order. As such, in this program, it will check
                        // if the 'datosRegistrados' variable is either 'true' or 'false'.
                        if (datosRegistrados) {
                            entregaResultados(prendas); // If the variable is 'true' it will send the user into the SubMenu to show the results.
                        }
                        else
                        {
                            // If the variable is 'false' it will output this message and back out to the main menu.
                            Console.WriteLine("No se han registrado prendas aún. Registra prendas antes de ver resultados.");
                        }
                        break;
                    case 3:
                        // This option closes the menu, terminating the execution of the program.
                        break;
                } // In each case we use the break command so that the program stops and backsteps into the corresponding output.
            } while (opc != 3);
        }
    }
}
