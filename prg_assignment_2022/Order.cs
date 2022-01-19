using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    class Order
    {
        public int orderNo { get; set; } = 0;  //starts from 1
        public DateTime orderDateTime { get; set; }
        public double amount { get; set; }
        public string status { get; set; }  //unpaid(when new order is created), Paid(after payment is made), Cancelled
        public List<Ticket> ticketList { get; set; } = new List<Ticket>();

        public Order() { }

        public Order(int OrderNo, DateTime OrderDateTime)
        {
            orderNo = OrderNo;
            orderDateTime = OrderDateTime;
        }

        public void AddTicket(Ticket ticketToBeAdded)
        {
            ticketList.Add(ticketToBeAdded);
        }

        public int increaseCountByOne()
        {
            orderNo++;
            return orderNo;
        }

        public override string ToString()
        {
            return string.Format("{0,-20} {1,-30} {2,-20} {3,-20}", orderNo, orderDateTime, amount, status);
        }
    }
}
