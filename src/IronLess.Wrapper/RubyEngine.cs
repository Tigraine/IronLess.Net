namespace IronLess.Wrapper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using Microsoft.Scripting.Hosting;

    public class RubyEngine
    {
        private static object lockObject = new object();
        private static string physicalPath = ".";
        private static RubyEngine instance;
        private ScriptEngine engine;
        private ScriptScope scope;

        private RubyEngine()
        {
            string directory = Directory.GetCurrentDirectory();
            Console.WriteLine(directory);
            engine = IronRuby.Ruby.CreateEngine();
            engine.SetSearchPaths(new List<string>()
                                      {
                                          String.Format(@"{0}\IronRuby", physicalPath),
                                          String.Format(@"{0}\ruby\site_ruby\1.8", physicalPath),
                                          String.Format(@"{0}\ruby\1.8", physicalPath),
                                          //@".\lib\\IronRuby\gems\1.8\gems\less-1.1.13\lib\vendor\treetop\lib",
                                          String.Format(@"{0}\IronRuby\gems\1.8\gems\less-1.1.13\lib\", physicalPath)
                                      });

            scope = engine.CreateScope();
            engine.Execute(@"require 'less'", scope);
        }

        public void Execute(string command)
        {
            engine.Execute(command, scope);
        }

        public static RubyEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                            instance = new RubyEngine();
                    }
                }
                return instance;
            }
        }

        public static void Initialize(HttpContext context)
        {
            if (context.Request.PhysicalApplicationPath != null)
            {
                physicalPath = Path.Combine(context.Request.PhysicalApplicationPath, "bin");
            }
            var x = Instance;
        }
    }
}