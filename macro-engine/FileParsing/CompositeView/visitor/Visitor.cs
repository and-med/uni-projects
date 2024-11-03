namespace FileParsing.CompositeView.visitor
{
    abstract class Visitor
    {
        public abstract void Visit(CompositeConstruction comCon);
        public abstract void Visit(IfConstruction ifCon);
        public abstract void Visit(ElseConstruction elseCon, IfConstruction ifCon);
    }
}
