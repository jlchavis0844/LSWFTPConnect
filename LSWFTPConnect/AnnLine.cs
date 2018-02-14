using System;

namespace LSWFTPConnect {
    internal class AnnLine {
        public string Policy { get; set; }
        public string Fullname { get; set; }
        public string Plan { get; set; }
        public DateTime IssueDate { get; set; }
        public double Premium { get; set; }
        public double RatePer { get; set; }
        public double Rate { get; set; }
        public double Commission { get; set; }
        public double Renewal { get; set; }

        public AnnLine() { }

        public AnnLine(string polNum, string owner, string Plan, DateTime date, double prem, double commRate, double split, double comm, double ren) {
            Policy = polNum;
            Fullname = owner;
            this.Plan = Plan;
            IssueDate = date;
            Premium = prem;
            RatePer = commRate;
            Rate = split;
            Commission = comm;
            Renewal = ren;
        }

        public Object[] GetData() {
            Object[] tempArr = {
                Policy,
                Fullname,
                Plan,
                IssueDate.Date.ToShortDateString(),
                Premium,
                RatePer,
                Rate,
                Commission,
                Renewal
            };
            return tempArr;
        }
    }
}