using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using SKCOMLib;
using System.Collections.Generic;

namespace ApplicationCore.Brokages.Capital
{
    public partial class CapitalBrokage
    {

        private Dictionary<short, string> _symbolIndexCode = new Dictionary<short, string>();
        private Dictionary<short, double> _symbolIndexPoints = new Dictionary<short, double>();
        const string TX_SYMBOL_KEY = "TX00";
        bool IsStock(string code) => code != TX_SYMBOL_KEY;


        void InitSymbolIndexCode()
        {
            _symbolIndexCode = new Dictionary<short, string>();
            _symbolIndexPoints = new Dictionary<short, double>();

            var tx = GetSKSTOCKByCode(TX_SYMBOL_KEY);
            _symbolIndexCode[tx.sStockIdx] = TX_SYMBOL_KEY;

            double txPoints = 1;
            for (int i = 0; i < tx.sDecimal; i++)
            {
                txPoints *= 10;
            }
            _symbolIndexPoints[tx.sStockIdx] = txPoints;

            if (_symbolCodes.IsNullOrEmpty()) return;

            foreach (var code in _symbolCodes)
            {
                var pSKStock = GetSKSTOCKByCode(code);
                _symbolIndexCode[pSKStock.sStockIdx] = code;

                double symbolPoints = 1;
                for (int i = 0; i < pSKStock.sDecimal; i++)
                {
                    symbolPoints *= 10;
                }
                _symbolIndexPoints[pSKStock.sStockIdx] = symbolPoints;
            }
        }
        SKSTOCK GetSKSTOCKByCode(string code)
        {
            SKSTOCK pSKStock = new SKSTOCK();
            int nCode = _SKQuoteLib.SKQuoteLib_GetStockByNo(code, ref pSKStock);
            return pSKStock;
        }

    }
}
