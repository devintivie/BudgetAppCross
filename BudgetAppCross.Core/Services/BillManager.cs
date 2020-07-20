using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.Services
{
    //public class BillManager
    //{
    //    #region Singleton
    //    private static BillManager instance;
    //    public static BillManager Instance
    //    {
    //        get { return instance ?? (instance = new BillManager()); }
    //    }

    //    private BillManager()
    //    {

    //    }
    //    #endregion Singleton

    //    #region Fields

    //    #endregion

    //    #region Properties
    //    public Dictionary<string, Bill> BillsByCompany { get; set; } = new Dictionary<string, Bill>(StringComparer.OrdinalIgnoreCase);
    //    public List<Bill> AllBills { get; set; } = new List<Bill>();
    //    #endregion

    //    #region Methods

    //    public void Clear()
    //    {
    //        BillsByCompany.Clear();
    //        AllBills.Clear();
    //    }
    //    public void DeleteTracker(BillTracker bt)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void AddBill(string company, Bill iBill)
    //    {


    //        //if (BillsByCompany.ContainsKey(company))
    //        //{
    //        //    BillsByCompany[company].Bills.Add(iBill);
    //        //    BillsByCompany[company].Bills.Sort();
    //        //}
    //        //else
    //        //{
    //        //    var bt = new BillTracker(company, iBill);

    //        //    AddTracker(bt);
    //        //}
    //    }

    //    public void DeleteBill(string company, Bill iBill)
    //    {
    //        //if (TrackersByCompany.ContainsKey(company))
    //        //{
    //        //    TrackersByCompany[company].Bills.Remove(iBill);
    //        //}
    //    }

    //    public void PrintBillManager()
    //    {
    //        //foreach (var bt in AllTrackers)
    //        //{
    //        //    Console.WriteLine($"!!!!!{bt.CompanyName}!!!!");
    //        //    foreach (var bill in bt.Bills)
    //        //    {
    //        //        Console.WriteLine(bill);
    //        //    }
    //        //    Console.WriteLine();
    //        //}
    //    }

    //    public void Update(IEnumerable<Grouping<DateTime, AgendaBill>> agendaBills)
    //    {
    //        //Clear();
    //        //var data = (from agenda in agendaBills
    //        //            from b in agenda.Grouped
    //        //            select b).ToList();
    //        ////group b by b.Company into bGroup
    //        ////select new Grouping<string, AgendaBill>(bGroup.Key, bGroup)).ToList();

    //        //foreach (var item in data)
    //        //{
    //        //    AddBill(item.Company, item);
    //        //}

    //        //Console.WriteLine(BillManager.Instance.AllTrackers.Count);



    //    }

    //    #endregion

    //}
}
