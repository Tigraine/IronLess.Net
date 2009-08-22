namespace IronLess.Wrapper
{
    using System.Collections.Generic;
    using Microsoft.Scripting.Hosting;

    internal class RubyEngine
    {
        private static object lockObject = new object();
        private static RubyEngine instance;
        private ScriptEngine engine;
        private ScriptScope scope;

        private RubyEngine()
        {
            engine = IronRuby.Ruby.CreateEngine();
            engine.SetSearchPaths(new List<string>()
                                      {
                                          @".\IronRuby",
                                          @".\ruby\site_ruby\1.8",
                                          @".\ruby\1.8",
                                          //@".\lib\\IronRuby\gems\1.8\gems\less-1.1.13\lib\vendor\treetop\lib",
                                          @".\IronRuby\gems\1.8\gems\less-1.1.13\lib\"
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
    }
}