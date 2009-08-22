using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronLess.Wrapper
{
    using System.Diagnostics;
    using Microsoft.Scripting.Hosting;

    public class IronLessExecuter
    {
        public void CompileLess(string fileName)
        {
            ScriptEngine engine = IronRuby.Ruby.CreateEngine();
            engine.SetSearchPaths(new List<string>()
                                      {
                                          @".\IronRuby",
                                          @".\ruby\site_ruby\1.8",
                                          @".\ruby\1.8",
                                          //@".\lib\\IronRuby\gems\1.8\gems\less-1.1.13\lib\vendor\treetop\lib",
                                          @".\IronRuby\gems\1.8\gems\less-1.1.13\lib\"
                                      });
            engine.Execute(
                @"require 'less'
options = {
  :watch => false,
  :compress => false,
  :debug => true,
  :growl => false
}
options[:source] = 'variables.less'
Less::Command.new( options ).run!");
        }
    }
}
