using System;

namespace EventHandeling
{
    public class RaiseDisplayEventArgs : EventArgs 
    {
        public decimal TotalPrice { get; set; }
        public string ProductInformationString {get; set; }

        public void RaiseClientDisplay(object source, RaiseDisplayEventArgs e)
        {
           TotalPrice = e.TotalPrice;
           ProductInformationString = e.ProductInformationString;
        }
    }
}