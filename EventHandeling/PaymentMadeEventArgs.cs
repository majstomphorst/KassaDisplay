using System;
using System.Collections.Generic;

namespace EventHandeling
{
    public class PaymentMadeEventArgs : EventArgs 
    {
        public List<IProduct> Cart {get;}
        public PaymentMadeEventArgs(List<IProduct> list) 
        {
            Cart = list;
        }

    }
}