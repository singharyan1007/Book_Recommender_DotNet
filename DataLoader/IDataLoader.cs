using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public  interface IDataLoader
    {
        public BookDetails Load();
        
    }
}
