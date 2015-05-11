using System;
using System.Collections.Generic;
using System.Text;
using FileParsing.Base;
using FileParsing.CompositeView.visitor;
using FileParsing.Exceptions;

namespace FileParsing.CompositeView
{
    abstract class CompositeConstruction : TextUnit
    {
        public int StartPositionInFile { get; set; }
        public int EndPositionInFile { get; set; }
        protected List<TextUnit> Units;
        protected CompositeConstruction(TextUnit father, string data): base(father, data)
        {
            Units = new List<TextUnit>();
        }
        public void AddUnit(TextUnit tu)
        {
            Units.Add(tu);
        }
        public virtual void Accept(Visitor v)
        {
            v.Visit(this);
        }
        public override string Evaluate(Context context)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                foreach (var unit in Units)
                {
                    result.Append(unit.Evaluate(context));
                }
            }
            catch (BreakException breakException)
            {
                breakException.AddToResult(result.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while executing!");
                Console.WriteLine("Error message: {0}", e.Message);
            }
            return result.ToString();
        }
    }
}
