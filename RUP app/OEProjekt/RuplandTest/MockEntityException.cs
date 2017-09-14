using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuplandTest
{
    class MockEntityException : Exception
    {
        // az Entity Framework által dobott Exception helyettesítésére

        public MockEntityException()
        {
        }

        public MockEntityException(string message)
        : base(message)
    {
        }

        public MockEntityException(string message, Exception inner)
        : base(message, inner)
    {
        }
    }
}
