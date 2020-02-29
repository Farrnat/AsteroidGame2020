using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    static partial class Program
    {
        static void Main(string[] args)   
        {
            //Logger logger = new ListLogger();
            // Logger logger = new FileLogger("program.log");
            // Logger logger = new VisualStudioOutputLogger();
            Logger logger = new TraceLogger();

            Trace.Listeners.Add(new TextWriterTraceListener("trace.log"));
            

              logger.LogInformation("Start program");

            for (var i = 0; i < 10; i++)
                logger.LogInformation($"Do some work {i + 1}");

            logger.LogWarning("Завершение работы приложения");

            Trace.Flush();

            //var log_messages = ((ListLogger)logger).Messages;

            Console.ReadLine();

          
        }

       

        }


    }

    

