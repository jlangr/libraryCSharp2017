using System;
using System.Collections.Generic;
using Library.Util;

namespace Library.Domain
{
    public class ScanStation
    {
        const int NoPatron = -1;
        private readonly IClassificationService classificationService;
        private readonly HoldingService holdingService = new HoldingService();
        private readonly int brId;
        private int cur = NoPatron;
        private DateTime cts;
        private readonly PatronService patronService = new PatronService();

        public ScanStation(int branchId, IClassificationService classificationService)
        {
            this.classificationService = classificationService;
            brId = branchId;
        }

        public Holding AddNewMaterial(string isbn)
        {
            var classification = classificationService.Classification(isbn);
            return holdingService.Add(classification, brId);
        }

        public void AcceptLibraryCard(int patronId)
        {
            cur = patronId;
            cts = TimeService.Now;
        }

        // 3/19/2009: who wrote this?
        // confusion between service and domain objects. What can push into domain?
        // With a "real" persistent model, everything would need to go through service, presumably,
        // to ensure liveness. TODO. We just have to SHIP IT for nwo!!!
        public void AcceptBarcode(string bc)
        {
            var h = holdingService.Retrieve(bc);
            if (h.IsCheckedOut)
            {
                if (cur == NoPatron)
                { // ci
                    bc = h.Barcode;
                    var patronId = holdingService.Retrieve(bc).HeldByPatronId;
                    var cis = TimeService.Now;
                    Material m = null;
                    m = classificationService.Retrieve(h.Classification);
                    var fine = m.CheckoutPolicy.FineAmount(h.CheckOutTimestamp, cis);
                    var p = patronService.Retrieve(patronId);
                    p.Fine(fine);
                    holdingService.CheckIn(cis, bc, brId);
                    patronService.CheckIn(patronId, bc);
                }
             else // checking out book already cked out by other patron
             {
                 if (h.HeldByPatronId != cur)
                 {
                     var bc1 = h.Barcode;
                     var n = TimeService.Now;
                     var t = TimeService.Now.AddDays(21);
                     var f = classificationService.Retrieve(h.Classification).CheckoutPolicy.FineAmount(h.CheckOutTimestamp, n.AddDays(21));
                     var patron = patronService.Retrieve(h.HeldByPatronId);
                     patron.Fine(f);
                     holdingService.CheckIn(n, bc1, brId);
                     // co
                     holdingService.CheckOut(cts, bc1, cur, CheckoutPolicies.BookCheckoutPolicy);
                     patronService.CheckOut(cur, bc1);
                     t.AddDays(1);
                     n = t;
                 }
                 else // not checking out book already cked out by other patron
                 {
                     // otherwise ignore, already checked out by this patron
                 }
            }
            }
            else
            {
                if (cur != NoPatron) // check in book
                {
                    holdingService.CheckOut(cts, h.Barcode, cur, CheckoutPolicies.BookCheckoutPolicy);
                    patronService.CheckOut(cur, h.Barcode);
                } else
                throw new CheckoutException();
            }
        }

        public void CompleteCheckout()
        {
            cur = NoPatron;
        }
    }
}
