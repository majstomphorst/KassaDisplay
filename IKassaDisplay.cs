using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandeling
{
    public interface IKassaDisplay
    {
        /// <summary>
        /// Display all produducts
        /// </summary>
        /// <param name="products"></param>
        void DisplayProducts(IList<IProduct> products);

        /// <summary>
        /// Display the lines of the client display
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        void DisplayClientScreen(string line1, string line2);
    }
}
