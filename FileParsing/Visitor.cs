namespace FileParsing
{
    abstract class Visitor
    {
        //public abstract void Visit(StaticText st);
        public abstract void Visit(CompositeConstruction comCon);
        //public abstract void Visit(UserDefinedMacrosConstruction usDef);
    }
}
