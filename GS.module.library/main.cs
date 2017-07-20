namespace GS.module.library
{
    using entities.Base;
    using System;
    class main
    {
        public static void Main()
        {
            string key = "@@module@@";                         
            Console.WriteLine("*Ingrese una opción:\n* (1-Encriptar Default) - (2- Encriptar Personalizar)");
            Console.WriteLine("* (3-Desencriptar Default) - (4- Desencriptar Personalizar)");
            var opcion = Console.ReadLine();
            string variable;
            Console.WriteLine("*************Ini*************");
            switch (opcion)
            {
                case "1":
                    Console.WriteLine("* Ingrese el texto a encriptar");
                    variable = Console.ReadLine();
                    Console.WriteLine(Helper.Encriptar(key, variable));
                    break;
                case "2":
                    Console.WriteLine("* Ingrese la key");
                    key = Console.ReadLine();
                    Console.WriteLine("* Ingrese el texto a encriptar");
                    variable = Console.ReadLine();
                    Console.WriteLine(Helper.Encriptar(key, variable));
                    break;
                case "3":
                    Console.WriteLine("* Ingrese el texto a desencriptar");
                    variable = Console.ReadLine();
                    Console.WriteLine(Helper.Desencriptar(key, variable));
                    break;
                case "4":
                    Console.WriteLine("* Ingrese la key");
                    key = Console.ReadLine();
                    Console.WriteLine("* Ingrese el texto a desencriptar");
                    variable = Console.ReadLine();
                    Console.WriteLine(Helper.Desencriptar(key, variable));
                    break;
                default:
                    Console.WriteLine("* Opción no válida");
                    break;
            }
            Console.WriteLine("*************Fin*************");
            Console.Read();
        }
    }

}
