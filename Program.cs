using System;
using System.Reflection;

namespace ReflectionSample
{
    class Program
    {
        public static string _typeFormConfiguration = "ReflectionSample.Person";
        static void Main(string[] args)
        {


            var personType = typeof(Person);
            var personConstructors = personType.GetConstructors(BindingFlags.Instance | BindingFlags.Public| BindingFlags.NonPublic);
            foreach (var personConstructor in personConstructors)
            {
                Console.WriteLine(personConstructor);

            }

            var privatePesonCostructor = personType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(string), typeof(int) }, null);
            Console.WriteLine(privatePesonCostructor);


            var peson1 = personConstructors[0].Invoke(null);
            var peson2 = personConstructors[1].Invoke(new object[] { "Jassar" });
            var peson3 = personConstructors[2].Invoke(new object[] { "Jassar",27 });

            var peson4 = Activator.CreateInstance("ReflectionSample", "ReflectionSample.Person");
            var peson5 = Activator.CreateInstance("ReflectionSample", "ReflectionSample.Person",true,BindingFlags.Instance | BindingFlags.Public,null,new object[] { "Jassar" },null,null);

            var pesonTypeFrmString = Type.GetType("ReflectionSample.Person");
            var peson6 = Activator.CreateInstance(pesonTypeFrmString, new object[] { "Jassar" });

            var peson7= Activator.CreateInstance("ReflectionSample", "ReflectionSample.Person", true, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { "Jassar",27 }, null, null);


            var assembly = Assembly.GetExecutingAssembly();
            var peson8 = assembly.CreateInstance("ReflectionSample.Person");

            //Create a new instance of a configured type
            var actualTypeFromConfiguration = Type.GetType(_typeFormConfiguration);
            var iTalkInstance = Activator.CreateInstance(actualTypeFromConfiguration) as ITalk;
            iTalkInstance.Talk("Hello Word");
            Console.ReadLine();
        }

        public  void InspectingMetadata()
        {
            string name = "jassar";
            //var stringType = name.GetType();
            var stringType = typeof(string);
            Console.WriteLine(stringType);

            var currentAssembly = Assembly.GetExecutingAssembly();
            var typesFromCurrentAssembly = currentAssembly.GetTypes();
            foreach (var type in typesFromCurrentAssembly)
            {
                Console.WriteLine(type.Name);
            }

            var oneTypeFromCurrentAssembly = currentAssembly.GetType("ReflectionSample.Person");
            Console.WriteLine(oneTypeFromCurrentAssembly.Name);

            var externalAssembly = Assembly.Load("System.Text.Json");
            var typesFromExternalAssembly = externalAssembly.GetTypes();
            var oneTypeFromExternalAssembly = externalAssembly.GetType("System.Text.Json.JsonProperty");

            var modulesFromExternalAssembly = externalAssembly.GetModules();
            var oneModuleFromExternalAssembly = externalAssembly.GetModule("System.Text.Json.dll");

            var typesFromModuleFromExternalAssembly = oneModuleFromExternalAssembly.GetTypes();
            var oneTypeFromModuleFromExternalAssembly =
                oneModuleFromExternalAssembly.GetType("System.Text.Json.JsonProperty");

            foreach (var constructor in oneTypeFromCurrentAssembly.GetConstructors())
            {
                Console.WriteLine(constructor);
            }

            //foreach (var method in oneTypeFromCurrentAssembly.GetMethods())
            //{
            //    Console.WriteLine(method);
            //}

            foreach (var method in oneTypeFromCurrentAssembly.GetMethods(
                 BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{method}, public: {method.IsPublic}");
            }

            foreach (var field in oneTypeFromCurrentAssembly.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine(field);
            }
        }
    }
}
