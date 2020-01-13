using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C0.Challenges.Codechef
{
    class Program
    { 
    
        static void Main(string[] args)
        {
            Extra.Table table = new Extra.Table();
            Extra.Philosopher p1 = new Extra.PhilosopherWrong(table.Plates[0], "P1");
            Extra.Philosopher p2 = new Extra.PhilosopherWrong(table.Plates[1], "P2");
            Extra.Philosopher p3 = new Extra.PhilosopherWrong(table.Plates[2], "P3");
            Extra.Philosopher p4 = new Extra.PhilosopherWrong(table.Plates[3], "P4");

            Extra.Philosopher o1 = new Extra.PhilosopherRight(table.Plates[0], "UNTEN");
            Extra.Philosopher o2 = new Extra.PhilosopherRight(table.Plates[1], "RECHTS");
            Extra.Philosopher o3 = new Extra.PhilosopherRight(table.Plates[2], "OBEN");
            Extra.Philosopher o4 = new Extra.PhilosopherRight(table.Plates[3], "LINKS");

            o1.Start();
            o2.Start();
            o3.Start();
            o4.Start();
            while (true)
                Console.ReadLine();


        //RunManually();
        //RunAutomatically();
    }

        /// <summary>
        /// 1. Runs all <see cref="IAutomaticChallenge"/> in the current Assembly.
        /// 2. Runs all <see cref="IAutomaticChallenge"/> in all assemblies (.dll) in the current directory.
        /// </summary>
        private static void RunAutomatically()
        {
            Console.WriteLine("Dynamically Run-ning all IAutomaticChallenge-s from this assembly:");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => (type.GetInterfaces().Contains(typeof(IAutomaticChallenge)))).OrderBy(type => type.Name);
            foreach(Type type in types)
            {
                RunChallengeAutomatically(type);
            }

            Console.WriteLine("Dynamically Run-ning all IAutomaticChallenge-s from foreign assemblies:");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            var foreignAssemblies = new List<Assembly>();
            List<string> dLLsInCurrentDir = System.IO.Directory.GetFiles(Environment.CurrentDirectory).Where(fileName => fileName.EndsWith(".dll")).ToList();
            foreach (string dll in dLLsInCurrentDir)
            {
                foreignAssemblies.Add(Assembly.LoadFile(dll));
            }
            foreach (Assembly foreignAssembly in foreignAssemblies)
            {
                IEnumerable<Type> foreignTypes = foreignAssembly.GetTypes().Where(type => (type.GetInterfaces().Contains(typeof(IAutomaticChallenge)))).OrderBy(type => type.Name);
                foreach (Type type in foreignTypes)
                {
                    RunChallengeAutomatically(type);
                }
            }

            Console.WriteLine("Ending the program...");
            Console.ReadLine();
        }

        private static void RunManually()
        {
            var first = new TestEchoChallenge();
            var firstIO = new ChallengeInputOutputCollection<string, string>();
            firstIO.Add("Hello World", "Hello World");
            //RunChallengeManually(first, firstIO, true);

            var second = new Beginner.HS08TEST();
            var secondIO = new ChallengeInputOutputCollection<double[], double>();
            secondIO.Add(new double[] { 50D, 150D }, 99.5D);
            //RunChallengeManually(second, secondIO, false);

            var third = new Beginner.ACBALL();
            var thirdIO = new ChallengeInputOutputCollection<string[], string>();
            thirdIO.Add(new string[] { "WWW", "BBB" }, "BBB");
            thirdIO.Add(new string[] { "WBBBWBWBWBWBBBWBBW", "WBBBWBWBWBWBBBWBBW" }, "BWWWBWBWBWBWWWBWWB");
            thirdIO.Add(new string[] { "W", "W" }, "B");
            thirdIO.Add(new string[] { "BBBBW", "WBBBB" }, "BWWWB");
            //RunChallengeManually(third, thirdIO, false);
        }

        /// <summary>
        /// Standard way to <see cref="Challenge.Run(ChallengeInputOutputCollection{Tin, Tout})"/> a <see cref="Challenge"/> using the Console.
        /// </summary>
        static void RunChallengeManually<Tin, Tout>(Challenge<Tin, Tout> challenge, ChallengeInputOutputCollection<Tin, Tout> inputOutputs, bool verbose = false)
        {
            ConsoleColor temp = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================================================");
            Console.WriteLine("Challenge start: " + DateTime.Now.ToShortTimeString());
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------------");
            Console.WriteLine("Name: " + challenge.Name);
            Console.WriteLine("Code: " + challenge.Code);
            if (verbose)
            {
                Console.WriteLine("Type: " + challenge.GetType().FullName);
                Console.WriteLine("Result-Receiver: " + challenge.ResultReceiver.Method.Name);
                Console.WriteLine("URL: " + challenge.Url);
                Console.WriteLine("Description: " + challenge.Description);
                Console.WriteLine("Notes: " + challenge.Notes);
                Console.WriteLine("Rating: " + challenge.Rating);
            }
            Console.WriteLine("Submitted: " + challenge.Submitted);
            Console.WriteLine("----------------");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Class-Input: ");
            IEnumerable<ChallengeInputOutput<Tin, Tout>> challengeInputOutputs = challenge.Input();
            if (challengeInputOutputs != null)
            {
                foreach (ChallengeInputOutput<Tin, Tout> inputOutput in challenge.Input())
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("  Input: " + JoinToString(inputOutput.Input));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  Expected: " + JoinToString(inputOutput.Output));
                }

            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Call-Input: ");
            if (inputOutputs != null)
            {
                foreach (ChallengeInputOutput<Tin, Tout> inputOutput in inputOutputs.AsEnumerable())
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("  Input: " + JoinToString(inputOutput.Input));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  Expected: " + JoinToString(inputOutput.Output));
                }
            }

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Run:");
                challenge.Run(inputOutputs);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Challenge end: " + DateTime.Now.ToShortTimeString());
            Console.WriteLine("====================================================");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to continue with the next challenge...");
            Console.ReadLine();
            Console.ForegroundColor = temp;
        }

        /// <summary>
        /// Standard way to automatically (reflection on assembly) <see cref="Challenge.Run(ChallengeInputOutputCollection{Tin, Tout})"/> a <see cref="Challenge"/> using the Console.
        /// Dirty - it uses way to much dynamic-typing because the Tin and Tout is not known for the class.
        /// </summary>
        [Obsolete("Warning: Uses way too much dynamic-typing. Must be refactored.")]
        static void RunChallengeAutomatically(Type challengeType, bool verbose = false)
        {
            // Getting object + method from the type (must be a Challenge<Tin, Tout>)
            object challenge = Activator.CreateInstance(challengeType);
            MethodInfo runMethod = challengeType.GetMethod("Run");
            dynamic challengeInfoStub = challenge as dynamic;

            ConsoleColor temp = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================================================");
            Console.WriteLine("Challenge start: " + DateTime.Now.ToShortTimeString());
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------------");
            Console.WriteLine("Name: " + challengeInfoStub.Name);
            Console.WriteLine("Code: " + challengeInfoStub.Code);
            if (verbose)
            {
                Console.WriteLine("Type: " + challenge.GetType().FullName);
                Console.WriteLine("Result-Receiver: " + challengeInfoStub.ResultReceiver.Method.Name);
                Console.WriteLine("URL: " + challengeInfoStub.Url);
                Console.WriteLine("Description: " + challengeInfoStub.Description);
                Console.WriteLine("Notes: " + challengeInfoStub.Notes);
                Console.WriteLine("Rating: " + challengeInfoStub.Rating);
            }
            Console.WriteLine("Submitted: " + challengeInfoStub.Submitted);
            Console.WriteLine("----------------");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Class-Input: ");
            dynamic challengeInputOutputs = challengeInfoStub.Input();
            if (challengeInputOutputs != null)
            {
                foreach (dynamic  inputOutput in challengeInfoStub.Input())
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("  Input: " + JoinToString(inputOutput.Input));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  Expected: " + JoinToString(inputOutput.Output));
                }

            }
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Run:");
                runMethod.Invoke(challenge, new object[] { null });
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Challenge end: " + DateTime.Now.ToShortTimeString());
            Console.WriteLine("====================================================");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to continue with the next challenge...");
            Console.ReadLine();
            Console.ForegroundColor = temp;
        }

        /// <summary>
        /// This helper method looks if element is IEnumerable -> joins all the parts together in one "[x, y, z]"
        /// Reason: object[]{0,1}.ToString() is usually something like "Array[]"
        /// Also looks for Tuple / ValueTuple and turns them into (x, y)
        /// </summary>
        static string JoinToString(object element)
        {
            string output = "";
            if (element is IEnumerable && !(element is string))
            {
                output = "[";
                foreach (object part in (IEnumerable)element)
                    output += (part != null ? part.ToString() : "NULL") + ", ";
                
                output += "]";
            }
            else if (IsAnyTuple(element))
            {
                output += "(";
                FieldInfo[] fields = element.GetType().GetFields().Where(field => field.Name.StartsWith("Item")).ToArray();
                foreach (FieldInfo field in fields)
                {
                    output += JoinToString(field.GetValue(element));
                }
                output += ")";
            }
            else
            {
                output = (element != null ? element.ToString() : "NULL") + ", ";
            }

            output = output.Replace(", ]", "]");
            output = output.Replace(", )", ")");
            return output;
        }

        static bool IsAnyTuple(object obj)
        {
            string[] types = new string[] {
                "Tuple`1", "Tuple`2", "Tuple`3", "Tuple`4", "Tuple`5", "Tuple`6", "Tuple`7", "Tuple`8", "Tuple`9", "Tuple`10",
                "ValueTuple`1", "ValueTuple`2", "ValueTuple`3", "ValueTuple`4", "ValueTuple`5",
                "ValueTuple`6", "ValueTuple`7", "ValueTuple`8", "ValueTuple`9", "ValueTuple`10"
            };
            Type type = obj.GetType();
            return types.Contains(type.Name);
        }
       
      
    }
}
