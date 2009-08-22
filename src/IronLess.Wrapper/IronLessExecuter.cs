namespace IronLess.Wrapper
{
    public class IronLessExecuter
    {
        public void CompileLess(string fileName, string outputFile)
        {
            RubyEngine engine = RubyEngine.Instance;
            engine.Execute(
                @"options = {
  :watch => false,
  :compress => false,
  :debug => true,
  :growl => false
}");
            engine.Execute(string.Format(@"options[:source] = '{0}'", fileName));
            engine.Execute(string.Format(@"options[:destination] = '{0}'", outputFile));
            engine.Execute(@"Less::Command.new( options ).run!");
        }
    }
}