using ApplicationCore.Brokages;
using ApplicationCore.Helpers;
using ApplicationCore.OrderMaker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker
{
    public abstract class BaseOrderMaker : BaseBrokage, IOrderMaker
    {
        public BaseOrderMaker(BrokageName name, BrokageSettings settings):base(name, settings)
        {
            InitYearMonth();
        }

        public event EventHandler AccountPositionUpdated;
        protected void OnAccountPositionUpdated(AccountEventArgs e)
        {
            AccountPositionUpdated?.Invoke(this, e);
        }

        public List<AccountViewModel> AccountList => _accounts;


        List <AccountViewModel> _accounts = new List<AccountViewModel>();
        protected void AddAccount(AccountViewModel account)
        {
            if (FindAccountByNumber(account.Number) == null) _accounts.Add(account);
        }

        protected AccountViewModel FindAccountByNumber(string number)
        {
            if (_accounts.IsNullOrEmpty()) return null;

            return _accounts.FirstOrDefault(x => x.Number == number);
        }

        public virtual int GetAccountPositions(string accountId, string symbol)
        {
            var account = FindAccountByNumber(accountId);
            if (account.Positions.IsNullOrEmpty()) return 0;

            string productId = GetProductId(symbol);
            var items = account.Positions.Where(x => x.ProductId == productId);

            if (items.IsNullOrEmpty()) return 0;

            int buyLots = items.Where(x => x.BS.EqualTo("B")).Sum(x => x.Qty);
            int sellLots = items.Where(x => x.BS.EqualTo("S")).Sum(x => x.Qty);

            return buyLots - sellLots;

        }

        string _monthCode = "";
        string _yearCode = "";
        protected void InitYearMonth()
        {
            var monthDictionary = new Dictionary<int, string>
            {
                { 1, "A" },{ 2, "B" },{ 3, "C" },
                { 4, "D" },{ 5, "E" },{ 6, "F" },
                { 7, "G" },{ 8, "H" },{ 9, "I" },
                { 10, "J" },{ 11, "K" },{ 12, "L" }
            };

            var date = DateTime.Today;

            int year = date.Year;
            int month = date.Month;

            var cachDay = date.FindThirdWedOfMonth(); //本月結算日
            if (date > cachDay)
            {
                month += 1;
                if (month > 12)
                {
                    year += 1;
                    month = 1;
                }
            }

            _monthCode = monthDictionary[month];
            _yearCode = year.ToString().Substring(year.ToString().Length - 1, 1);

        }

        string[] _allowSymbols = new string[] { "TXF", "MXF" };
        protected string GetSymbolCode(string code)
        {
            if (_allowSymbols.Contains(code)) return code;

            var symbolCode = code.ToUpper();
            if (_allowSymbols.Contains(symbolCode)) return symbolCode;

            return symbolCode == "TX" ? "TXF" : "MXF";
        }

        Dictionary<string, string> productIdDictionary = new Dictionary<string, string>();
        protected string GetProductId(string code)
        {
            if (productIdDictionary.ContainsKey(code)) return productIdDictionary[code];

            string id = $"{GetSymbolCode(code)}{_monthCode}{_yearCode}";
            productIdDictionary.Add(code, id);

            return id;
        }

        public abstract void FetchDeals(string account);

        public abstract List<DealViewModel> GetDeals();
    }
}
