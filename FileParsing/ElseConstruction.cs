using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    class ElseConstruction: CompositeConstruction
    {
        private IfConstruction FatherIfConstruction;

        public override string Evaluate(Context context)
        {
            StringBuilder result = new StringBuilder();            
            foreach (var unit in Units)
            {
                result.Append(unit.Evaluate(context));
            }          
            return result.ToString();
        }

        public ElseConstruction()
        {
            FatherIfConstruction = new IfConstruction();
        }
        public virtual bool GetResultOfCondition(Context cont)
        {
            return true;
        }
       
        public void SetFatherIfConstruction(IfConstruction ifCon)
        {
            FatherIfConstruction = ifCon;
        }

        public void SetFatherStop()
        {
            FatherIfConstruction.Stop = false;
        }
        public override void Accept(Visitor v)
        {
            v.Visit(this,FatherIfConstruction);
        }
    }
}
